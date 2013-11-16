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

namespace TicTacToe.Web.WebApi
{
    using System;
    using System.Json;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Microsoft.Samples.SocialGames.Entities;
    using Microsoft.Samples.SocialGames.Web.Services;

    public class UserController : BaseApiController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public HttpResponseMessage Verify()
        {
            try
            {
                var userId = this.userService.Verify();

                return HttpResponse<string>(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateProfile()
        {
            dynamic formContent = this.Request.Content.ReadAsAsync<JsonValue>().Result;

            try
            {
                this.userService.UpdateProfile(this.Request);

                return SuccessResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage Leaderboard(int count)
        {
            try
            {
                var board = this.userService.Leaderboard(count);

                var response = HttpResponse<Board>(board, contentType: "application/json");
                response.Headers.CacheControl = new CacheControlHeaderValue();
                response.Headers.CacheControl.Public = false;
                response.Headers.CacheControl.NoStore = true;
                response.Headers.CacheControl.NoCache = true;
                
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage GetFriends()
        {
            try
            {
                var friends = this.userService.GetFriends();
                return HttpResponse<string[]>(friends, contentType: "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage GetFriendsInfo()
        {
            try
            {
                var friends = this.userService.GetFriendsInfo();
                return HttpResponse<UserInfo[]>(friends, contentType: "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddFriend(string friendId)
        {
            try
            {
                this.userService.AddFriend(friendId);
                return SuccessResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}