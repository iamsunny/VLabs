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

namespace TicTacToe.Web.WebApi
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web.Http;
    
    public abstract class BaseApiController : ApiController
    {
        protected static HttpResponseMessage SuccessResponse
        {
            get { return HttpResponse(HttpStatusCode.OK); }
        }

        protected static HttpResponseMessage HttpStatusResponse(HttpStatusCode statusCode, string description = "")
        {
            return HttpResponse<string>(description, statusCode);
        }

        protected static HttpResponseMessage BadRequest(string description)
        {
            return HttpStatusResponse(HttpStatusCode.BadRequest, description);
        }

        protected static HttpResponseMessage HttpResponse<T>(T content, HttpStatusCode statusCode = HttpStatusCode.OK, string contentType = "")
        {
            HttpResponseMessage tempResponse = null;
            HttpResponseMessage response = null;

            try
            {
                if (typeof(T) == typeof(string))
                {
                    tempResponse = new HttpResponseMessage();
                    tempResponse.Content = new StringContent(content.ToString(), Encoding.UTF8);
                }
                else
                {
                    tempResponse = new HttpResponseMessage<T>(content);
                }

                tempResponse.StatusCode = statusCode;

                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    tempResponse.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }

                response = tempResponse;
                tempResponse = null;
            }
            finally
            {
                if (tempResponse != null)
                {
                    tempResponse.Dispose();
                }
            }

            return response;
        }
    }
}