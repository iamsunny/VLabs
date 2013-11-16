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

namespace CustomerManager.WebApi.CloudServices.Notifications.Sql
{
    using System.ComponentModel.DataAnnotations;

    public class SqlEndpointTableRow : Endpoint
    {
        [Key, Column(Order = 0)]
        public override string ApplicationId { get; set; }

        [Key, Column(Order = 1)]
        public override string TileId { get; set; }

        [Key, Column(Order = 2)]
        public override string ClientId { get; set; }
    }
}
