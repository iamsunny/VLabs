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
    using System.Linq;
    using System.Web.UI.WebControls;

    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var products = this.Application["Products"] as List<string>;
            var itemsInSession = this.Session["Cart"] as List<string> ?? new List<string>();
            
            // add all products currently not in session
            var filteredProducts = products.Where(item => !itemsInSession.Contains(item));

            //// Add additional filters here
            // filter product list for home users
            if (User.IsInRole("Home"))
            {
                filteredProducts = filteredProducts.Where(item => item.Contains("Home"));
            }

            foreach (var product in filteredProducts)
            {
                this.products.Items.Add(product);
            }
        }

        protected void AddItem_Click(object sender, EventArgs e)
        {
            ListItem selectedItem = this.products.SelectedItem;
            if (selectedItem != null)
            {
                List<string> cart = this.Session["Cart"] as List<string> ?? new List<string>();
                cart.Add(this.products.SelectedItem.ToString());
                this.products.Items.Remove(selectedItem);
                Session["Cart"] = cart;
            }
        }
    }
}
