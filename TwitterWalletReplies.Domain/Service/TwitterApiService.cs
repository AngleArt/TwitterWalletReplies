using Newtonsoft.Json;
using System.Net.Http.Headers;

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
            dynamic repliesData;
            string nextToken = "";
            List<Tweet> tweetReplies = new List<Tweet>();

            do
            {
                repliesData = await GetStatusIdReplies(tweetId, nextToken);
                nextToken = repliesData.meta?.next_token?.Value;

                tweetReplies.AddRange(GetReplyTweets(repliesData.data, repliesData.includes.users));
            } while (!String.IsNullOrEmpty(nextToken));

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

        private async Task<dynamic> GetStatusIdReplies(long conversationId, string nextToken = "")
        {
            var url = $"https://api.twitter.com/2/tweets/search/recent?query=in_reply_to_status_id:{conversationId}&tweet.fields=in_reply_to_user_id,author_id,created_at,conversation_id&expansions=author_id&max_results=100";

            if (!String.IsNullOrEmpty(nextToken))
            {
                url += $"&next_token={nextToken}";
            }
            
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