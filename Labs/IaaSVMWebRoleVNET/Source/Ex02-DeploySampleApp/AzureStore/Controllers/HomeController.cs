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

namespace AzureStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AzureStore.Models;

    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            return Search(null);
        }

        [HttpPost]
        public ActionResult Add(string selectedProduct)
        {
            if (selectedProduct != null)
            {
                List<string> cart = this.Session["Cart"] as List<string> ?? new List<string>();
                cart.Add(selectedProduct);
                Session["Cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string SearchCriteria)
        {
            Services.IProductRepository productRepository = new Services.ProductsRepository();
            var products = string.IsNullOrEmpty(SearchCriteria) ?
                productRepository.GetProducts() : productRepository.Search(SearchCriteria);

            // add all products currently not in session
            var itemsInSession = this.Session["Cart"] as List<string> ?? new List<string>();
            var filteredProducts = products.Where(item => !itemsInSession.Contains(item));

            var model = new IndexViewModel()
            {
                Products = filteredProducts,
                SearchCriteria = SearchCriteria
            };

            return View("Index", model);
        }

        public ActionResult Checkout()
        {
            var itemsInSession = this.Session["Cart"] as List<string> ?? new List<string>();
            var model = new IndexViewModel()
            {
                Products = itemsInSession
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Remove(string Products)
        {
            if (Products != null)
            {
                var itemsInSession = this.Session["Cart"] as List<string>;
                if (itemsInSession != null)
                {
                    itemsInSession.Remove(Products);
                }
            }

            return RedirectToAction("Checkout");
        }
    }
}