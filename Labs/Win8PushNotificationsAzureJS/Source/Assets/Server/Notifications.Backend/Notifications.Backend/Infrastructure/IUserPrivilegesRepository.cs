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

namespace Notifications.Backend.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Notifications.Backend.Models;

    public interface IUserPrivilegesRepository
    {
        IEnumerable<UserPrivilege> GetUsersWithPrivilege(string privilege);

        void AddPrivilegeToUser(string userId, string privilege);

        void AddPublicPrivilege(string privilege);

        void RemovePrivilegeFromUser(string userId, string privilege);

        void DeletePublicPrivilege(string privilege);

        void DeletePrivilege(string privilege);

        bool HasUserPrivilege(string userId, string privilege);

        bool PublicPrivilegeExists(string privilege);
    }
}