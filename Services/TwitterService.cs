using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using twitterApp.Models.Domain;

namespace twitterApp.Services
{
    public class TwitterService
    {
        private string pbToken;

        public string GetPostBearerToken(string encodedKey)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + encodedKey;
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded;charset=UTF-8";
            string data = "grant_type=client_credentials";
            string response = client.UploadString("https://api.twitter.com/oauth2/token", data);
            pbToken = response;
            return response;
        }

        public string GetUserTimeline(TwitterUser user)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + pbToken;
            String link = "https://api.twitter.com/1.1/statuses/user_timeline.json?count=10&screen_name=" + user.screenName;
            string response = client.DownloadString(link);

            return response;
        }
    }
}
