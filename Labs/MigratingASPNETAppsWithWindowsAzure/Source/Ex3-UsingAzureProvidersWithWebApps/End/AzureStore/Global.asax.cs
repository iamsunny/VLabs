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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AzureStore
{
    public class Global : System.Web.HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            Microsoft.WindowsAzure.CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));
            });

            this.LoadProducts();

            // Initialize the application roles 
            if (!System.Web.Security.Roles.RoleExists("Home"))
            {
                System.Web.Security.Roles.CreateRole("Home");
            }

            if (!System.Web.Security.Roles.RoleExists("Enterprise"))
            {
                System.Web.Security.Roles.CreateRole("Enterprise");
            }
        }

        public void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        public void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        public void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        public void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }

        private void LoadProducts()
        {
            this.Application["Products"] = new List<string>
            {
                "Microsoft Office 2007 Ultimate",
                "Microsoft Office Communications Server Enterprise CAL",
                "Microsoft Core CAL - License & software assurance - 1 user CAL",
                "Windows Server 2008 Enterprise",
                "Windows Vista Home Premium (Upgrade)",
                "Windows XP Home Edition w/SP2 (OEM)",
                "Windows Home Server - 10 Client (OEM License)",
                "Console XBOX 360 Arcade" 
            };
        }
    }
}
