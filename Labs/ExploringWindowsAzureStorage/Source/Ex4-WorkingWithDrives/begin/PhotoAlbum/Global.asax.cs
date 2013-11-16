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

namespace PhotoAlbum
{
    using System;
    using System.IO;
    using System.Web.Configuration;

    public class Global : System.Web.HttpApplication
    {
        private static string imageStorePath;

        public static string ImageStorePath
        {
            get
            {
                return imageStorePath;
            }

            set
            {
                imageStorePath = value;
            }
        }

        // Application_Start is called after the OnStart method.
        protected void Application_Start(object sender, EventArgs e)
        {
            if (imageStorePath == null)
            {
                ImageStorePath = WebConfigurationManager.AppSettings["ImageStorePath"];
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}