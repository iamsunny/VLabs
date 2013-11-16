// ----------------------------------------------------------------------------------
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

[assembly: WebActivator.PostApplicationStartMethod(typeof(CustomerManager.WebApi.App_Start.NotificationServiceSql), "PostStart")]

namespace CustomerManager.WebApi.App_Start
{
    using CustomerManager.WebApi.CloudServices.Notifications;
    using CustomerManager.WebApi.CloudServices.Notifications.Sql;

    public static class NotificationServiceSql
    {
        public static void PostStart()
        {
            // Configure the SQL database as the storage for the Push Notifications Registration Service.
            NotificationServiceContext.Current.Configure(
                c =>
                {
                    var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    c.StorageProvider = new SqlEndpointRepository(connectionString);
                });
        }
    }
}