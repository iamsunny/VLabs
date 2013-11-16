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

namespace TicTacToe.Web.WebApi
{
    using System;
    using System.Json;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.Samples.SocialGames.Web.Services;

    public class EventController : BaseApiController
    {
        private IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage PostEvent([FromUri]string id)
        {
            dynamic formContent = this.Request.Content.ReadAsAsync<JsonValue>().Result;

            try
            {
                this.eventService.PostEvent(id, formContent);
                return SuccessResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}