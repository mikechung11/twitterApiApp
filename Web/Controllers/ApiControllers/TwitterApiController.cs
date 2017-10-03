using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using twitterApp.Models.Domain;
using twitterApp.Services;

namespace twitterApp.Web.Controllers.ApiControllers
{
    [RoutePrefix("api/twitter")]
    public class TwitterApiController : ApiController
    {
        private TwitterService _twitterService = new TwitterService();

        [Route(), HttpGet]
        public HttpResponseMessage GetPostBearerToken()
        {
            string apiKey = "sfUO4NiHYY0VljcV2Hh3sh5sa";
            string apiSecret = "b357KciJinhRTPKYr2m3FxTAFqCZdVP5r1cCm7Dffl78pOTpFh";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(apiKey + ":" + apiSecret);
            var encodedKey = System.Convert.ToBase64String(plainTextBytes);

            try
            {
                string response = _twitterService.GetPostBearerToken(encodedKey);
                JObject jResponse = JObject.Parse(response);
                string token = jResponse["access_token"].ToString();
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{screenName}"), HttpGet]
        public HttpResponseMessage GetUserTimeline(string screenName)
        {
            TwitterUser user = new TwitterUser();
            user.screenName = screenName;

            try
            {
                string response = _twitterService.GetUserTimeline(user);
                JArray jResponse = JArray.Parse(response);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
