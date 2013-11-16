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
    using System.Web.Http;
    using Microsoft.Samples.SocialGames.Web.Services;

    public class GameController : BaseApiController
    {
        private IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpPost]
        public HttpResponseMessage Create()
        {
            var gameQueueId = string.Empty;

            try
            {
                gameQueueId = this.gameService.Create();

                return HttpResponse<string>(gameQueueId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Join(Guid gameQueueId)
        {
            try
            {
                this.gameService.Join(gameQueueId);

                return SuccessResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Start(Guid gameQueueId)
        {
            try
            {
                this.gameService.Start(gameQueueId);

                return SuccessResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Command([FromUri]Guid id)
        {
            try
            {
                dynamic formContent = this.Request.Content.ReadAsAsync<JsonValue>().Result;
                this.gameService.Command(id, formContent);

                return SuccessResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage Invite([FromUri]Guid id)
        {
            try
            {
                dynamic formContent = this.Request.Content.ReadAsAsync<JsonValue>().Result;
                this.gameService.Invite(id, formContent);

                return SuccessResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}