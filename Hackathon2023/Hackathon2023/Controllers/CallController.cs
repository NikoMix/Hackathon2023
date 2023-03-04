using Azure.Core;
using Azure;

using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Graph.Communications.Core.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System;
using Microsoft.Graph.Communications.Common.Telemetry;
using Microsoft.Graph.Communications.Client;
using Microsoft.Graph;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Hackathon2023.Bots;
using Hackathon2023.Models;
using Microsoft.Graph.Communications.Common;

namespace Hackathon2023.Controllers
{
    [Route(HttpRouteConstants.CallSignalingRoutePrefix)]
    [ApiController]
    public class CallController : ControllerBase
    {
        private readonly CallingBot bot;
        private readonly IGraphLogger _logger;
        // private ICommunicationsClient Client => Bot.Instance.Client;


        public CallController(CallingBot bot, IGraphLogger logger)
        {
            this.bot = bot;
            _logger = logger;
        }

        [HttpPost, HttpGet]
        [Route("callback")]
        public async Task HandleCallbackRequestAsync()
        {
            //await bot.ProcessNotificationAsync(Request, Response).ConfigureAwait(false);
            await Task.CompletedTask;
        }

        //[HttpPost]
        //[Route(HttpRouteConstants.OnIncomingRequestRoute)]
        // [BotAuthentication]
        //public async Task<HttpResponseMessage> OnIncomingRequestAsync()
        //{
        //_logger.Info($"Received HTTP {this.Request.Method}, {this.Request.RequestUri}");

        //// Pass the incoming message to the sdk. The sdk takes care of what to do with it.
        //var response = await this.Client.ProcessNotificationAsync(this.Request).ConfigureAwait(false);

        //// Enforce the connection close to ensure that requests are evenly load balanced so
        //// calls do no stick to one instance of the worker role.
        //response.Headers.ConnectionClose = true;
        //return response;
        //}


        /// <summary>
        /// The making outbound call async.
        /// </summary>
        /// <param name="makeCallBody">
        /// The making outgoing call request body.
        /// </param>
        /// <returns>
        /// The action result.
        /// </returns>
        [HttpPost]
        [Route(HttpRouteConstants.OnMakeCallRoute)]
        public async Task<IActionResult> MakeOutgoingCallAsync([FromBody] MakeCallRequestData makeCallBody)
        {
            Validator.NotNull(makeCallBody, nameof(makeCallBody));

            try
            {
                await this.bot.MakeCallAsync(makeCallBody, Guid.NewGuid()).ConfigureAwait(false);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }

        /// <summary>
        /// The join call async.
        /// </summary>
        /// <param name="joinCallBody">
        /// The join call body.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpPost]
        [Route(HttpRouteConstants.JoinCall)]
        public async Task<IActionResult> JoinCallAsync([FromBody] JoinCallBody joinCallBody)
        {
           
            try
            {
                var call = await Bot.Instance.JoinCallAsync(joinCallBody).ConfigureAwait(false);
                var callPath = "/" + HttpRouteConstants.CallRoute.Replace("{callLegId}", call.Id);
                var callUri = new Uri(Service.Instance.Configuration.CallControlBaseUrl, callPath).AbsoluteUri;
                var values = new Dictionary<string, string>
                {
                    { "legId", call.Id },
                    { "scenarioId", call.ScenarioId.ToString() },
                    { "call", callUri },
                    { "logs", callUri.Replace("/calls/", "/logs/") },
                    { "changeScreenSharingRole", callUri + "/" + HttpRouteConstants.OnChangeRoleRoute },
                };

                var serializer = new CommsSerializer(pretty: true);
                var json = serializer.SerializeObject(values);

                return new OkObjectResult(json);
            }
            catch (ServiceException e)
            {
                IActionResult response = (int)e.StatusCode >= 300
                    ? new StatusCodeResult((int)e.StatusCode)
                    : new BadRequestResult();

                //if (e.ResponseHeaders != null)
                //{
                //    foreach (var responseHeader in e.ResponseHeaders)
                //    {
                //        response.Headers.TryAddWithoutValidation(responseHeader.Key, responseHeader.Value);
                //    }
                //}

                //response.Content = new StringContent(e.ToString());
                return response;
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        /// <summary>
        /// The join call body.
        /// Provide either:
        ///     1) JoinURL or
        ///     2) VideoTeleconferenceId and TenantId
        /// The second method is reserved for cloud-video-interop partners.
        /// The VideoTeleconferenceId is the short key generated for the room system devices.
        /// </summary>
        public class JoinCallBody
        {
            /// <summary>
            /// Gets or sets the VTC Id.
            /// This id is used to retrieve meeting info through Graph endpoint.
            /// Please see README regarding how to obtain a VTC Id.
            /// </summary>
            public string VideoTeleconferenceId { get; set; }

            /// <summary>
            /// Gets or sets the tenant id.
            /// The tenant id is needed to acquire authentication to get meeting info.
            /// </summary>
            public string TenantId { get; set; }

            /// <summary>
            /// Gets or sets the Teams meeting join URL.
            /// This URL is used to join a Teams meeting.
            /// </summary>
            public string JoinURL { get; set; }

            /// <summary>
            /// Gets or sets the display name.
            /// Teams client does not allow changing of ones own display name.
            /// If display name is specified, we join as anonymous (guest) user
            /// with the specified display name.  This will put bot into lobby
            /// unless lobby bypass is disabled.
            /// </summary>
            public string DisplayName { get; set; }
        }
    }
}
