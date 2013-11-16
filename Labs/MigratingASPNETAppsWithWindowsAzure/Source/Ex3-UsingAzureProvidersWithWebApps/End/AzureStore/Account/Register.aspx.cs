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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureStore.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CreateUserWizard1.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];

            if (!IsPostBack)
            {
                var list = (RadioButtonList)this.CreateUserWizard1.WizardSteps[0].FindControl("roles");

                list.DataSource = System.Web.Security.Roles.GetAllRoles().OrderByDescending(a => a);
                list.DataBind();

                if (list.Items.Count > 0)
                {
                    list.Items[0].Selected = true;
                }
            }
        }

        protected void OnCreatedUser(object sender, EventArgs e)
        {
            var list = (RadioButtonList)this.CreateUserWizard1.WizardSteps[0].FindControl("roles");

            System.Web.Security.Roles.AddUserToRole(
                                       this.CreateUserWizard1.UserName,
                                       list.SelectedItem.Text);
        }
    }
}
