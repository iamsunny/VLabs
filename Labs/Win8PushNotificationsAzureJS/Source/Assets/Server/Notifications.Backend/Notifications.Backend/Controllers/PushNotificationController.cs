﻿// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace Notifications.Backend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using NotificationsExtensions;
    using NotificationsExtensions.BadgeContent;
    using NotificationsExtensions.ToastContent;
    using Notifications.Backend.Infrastructure;
    using Notifications.Backend.Infrastructure.Helpers;
    using Notifications.Backend.Models;
    using Notifications.Backend.Repositories;
    using Notifications.Backend.ViewModel;

    [CustomAuthorize(Roles = PrivilegeConstants.AdminPrivilege)]
    public class PushNotificationController : Controller
    {
        private readonly IWnsEndpointRepository endpointsRepository;
        private readonly CloudQueueClient cloudQueueClient;
        private readonly CloudBlobClient blobClient;

        private readonly IAccessTokenProvider tokenProvider;

        public PushNotificationController()
            : this(
                    null,
                    null,
                    new WnsEndpointRepository(),
                    new WnsAccessTokenProvider(CloudConfigurationManager.GetSetting("WNSPackageSID"), CloudConfigurationManager.GetSetting("WNSClientSecret")))
        {
        }

        public PushNotificationController(CloudQueueClient cloudQueueClient, CloudBlobClient cloudBlobClient, IWnsEndpointRepository endpointsRepository, IAccessTokenProvider tokenProvider)
        {
            this.endpointsRepository = endpointsRepository;

            CloudStorageAccount account = null;

            if ((account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString")) == null)
            {
                if (cloudQueueClient == null)
                {
                    throw new ArgumentNullException("cloudQueueClient", "Cloud Queue Client cannot be null if no configuration is loaded.");
                }
            }

            this.cloudQueueClient = cloudQueueClient ?? account.CreateCloudQueueClient();
            this.blobClient = cloudBlobClient ?? account.CreateCloudBlobClient();

            this.tokenProvider = tokenProvider;
        }

        public ActionResult Index()
        {
            return View(this.endpointsRepository.GetAllEndpoints());
        }

        [HttpPost]
        public ActionResult SendNotification(
            [ModelBinder(typeof(NotificationTemplateModelBinder))] INotificationContent notification,
            string channelUrl,
            NotificationPriority priority = NotificationPriority.Normal)
        {
            var options = new NotificationSendOptions()
                {
                    Priority = priority
                };

            NotificationSendResult result = notification.Send(new Uri(channelUrl), this.tokenProvider, options);

            object response = new
            {
                DeviceConnectionStatus = result.DeviceConnectionStatus.ToString(),
                NotificationStatus = result.NotificationStatus.ToString(),
                Status = result.LookupHttpStatusCode()
            };

            return this.Json(response);
        }

        public ActionResult GetNotificationTypes(string notificationType)
        {
            dynamic ddlTypes = null;

            switch (notificationType)
            {
                case "Badge":
                    ddlTypes = (new List<string> { "BadgeNumeric", "BadgeGlyph" }).Select(x => new { Id = x, Name = x });
                    break;
                case "Raw":
                    ddlTypes = (new List<string> { "Raw" }).Select(x => new { Id = x, Name = x });
                    break;
                case "Toast":
                    ddlTypes = Enum.GetNames(typeof(ToastType)).Select(x => new { Id = x, Name = x });
                    break;
                case "Tile":
                    ddlTypes = Enum.GetNames(typeof(TileType)).Select(x => new { Id = x, Name = x });
                    break;
            }

            return Json(ddlTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSendTemplate(NotificationTemplateViewModel templateOptions)
        {
            PartialViewResult result = null;

            switch (templateOptions.NotificationType)
            {
                case "Badge":
                    templateOptions.BadgeGlyphValueContent = Enum.GetNames(typeof(GlyphValue));
                    ViewBag.ViewData = templateOptions;
                    result = PartialView("_" + templateOptions.NotificationTemplateType);
                    break;
                case "Raw":
                    ViewBag.ViewData = templateOptions;
                    result = PartialView("_Raw");
                    break;
                case "Toast":
                    templateOptions.TileImages = this.blobClient.GetAllBlobsInContainer(CloudConfigurationManager.GetSetting("TileImagesContainer")).OrderBy(i => i.FileName).ToList();
                    templateOptions.ToastAudioContent = Enum.GetNames(typeof(ToastAudioContent));
                    templateOptions.Priorities = Enum.GetNames(typeof(NotificationPriority));
                    ViewBag.ViewData = templateOptions;
                    result = PartialView("_" + templateOptions.NotificationTemplateType);
                    break;
                case "Tile":
                    templateOptions.TileImages = this.blobClient.GetAllBlobsInContainer(CloudConfigurationManager.GetSetting("TileImagesContainer")).OrderBy(i => i.FileName).ToList();
                    ViewBag.ViewData = templateOptions;
                    result = PartialView("_" + templateOptions.NotificationTemplateType);
                    break;
            }

            return result;
        }

        private void Escape(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(items[i]))
                {
                    items[i] = System.Security.SecurityElement.Escape(items[i]);
                }
            }
        }
    }
}
