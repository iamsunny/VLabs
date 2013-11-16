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

namespace AzureStore.Store
{
    using System;
    using System.Collections.Generic;

    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var itemsInSession = this.Session["Cart"] as List<string>;

            if (itemsInSession != null)
            {
                foreach (var item in itemsInSession)
                {
                    this.cart.Items.Add(item);
                }
            }
        }

        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            var selectedItem = this.cart.SelectedItem;
            if (selectedItem != null)
            {
                var itemsInSession = this.Session["Cart"] as List<string>;
                if (itemsInSession != null)
                {
                    itemsInSession.Remove(selectedItem.ToString());
                    this.cart.Items.Remove(selectedItem.ToString());
                }
            }
        }
    }
}
