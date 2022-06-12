using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWalletReplies.Domain
{
    public class TwitterApiService : ITwitterApiService
    {
        private string _bearerToken { get; set; }

        public TwitterApiService(string bearerToken)
        {
            _bearerToken = bearerToken;
        }

        public async Task<IEnumerable<Tweet>> GetReplies(long tweetId)
        {
            var originalTweet = await GetTweetData(tweetId);
            var conversationId = GetConversationId(originalTweet);
            var repliesData = await GetConversationReplies(conversationId);
            var tweetReplies = GetReplyTweets(repliesData.data, repliesData.includes.users);

            return tweetReplies;
        }

        private IEnumerable<Tweet> GetReplyTweets(dynamic repliesData, dynamic dataUsers)
        {
            if (!repliesData.HasValues)
            {
                throw new Exception("Could not find any replies.");
            }

            var users = GetUsers(dataUsers);
            var tweets = new List<Tweet>(); // TODO: No idea why yield return isn't working on this method

            foreach (var item in repliesData)
            {
                string? username = GetUsername(users, item);

                tweets.Add(new Tweet(username, item.text.Value));
            }

            return tweets;
        }

        private string? GetUsername(IEnumerable<TweetUser> users, dynamic item)
        {
            var userId = long.Parse(item.author_id.Value);
            var username = users
                .FirstOrDefault(u => u.Id == userId)
                ?.Username;
            return username;
        }

        private IEnumerable<TweetUser> GetUsers(dynamic dataUsers)
        {
            if (!dataUsers.HasValues)
            {
                throw new Exception("Could not find any users.");
            }

            foreach (var item in dataUsers)
            {
                yield return new TweetUser(long.Parse(item.id.Value), item.name.Value, item.username.Value);
            }
        }

        private async Task<dynamic> GetTweetData(long tweetId)
        {
            var url = $"https://api.twitter.com/2/tweets?ids={tweetId}&tweet.fields=conversation_id,created_at&expansions=referenced_tweets.id&user.fields=name,username";

            return await ExecuteRequest(url, HttpMethod.Get);
        }

        private long GetConversationId(dynamic originalTweet)
        {
            if (!originalTweet.data.HasValues)
            {
                throw new Exception("Could not find tweet data.");
            }

            return originalTweet.data[0].conversation_id;
        }

        private async Task<dynamic> GetConversationReplies(long conversationId)
        {
            var url = $"https://api.twitter.com/2/tweets/search/recent?query=conversation_id:{conversationId}&tweet.fields=in_reply_to_user_id,author_id,created_at,conversation_id&expansions=author_id";
            
            return await ExecuteRequest(url, HttpMethod.Get);
        }

        private async Task<dynamic> ExecuteRequest(string url, HttpMethod httpMethod)
        {
            using (var httpClient = new HttpClient())
            {
                SetupHttpClient(httpClient);

                using (var httpRequest = new HttpRequestMessage(httpMethod, url))
                {
                    var httpResponse = await httpClient.SendAsync(httpRequest);

                    httpResponse.EnsureSuccessStatusCode();

                    var response = await httpResponse.Content.ReadAsStringAsync();
                    var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                    return JsonConvert.DeserializeObject<dynamic>(response);
                }
            }
        }

        private void SetupHttpClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{this._bearerToken}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
    }
}