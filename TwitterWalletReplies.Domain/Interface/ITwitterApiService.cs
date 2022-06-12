namespace TwitterWalletReplies.Domain
{
    public interface ITwitterApiService
    {
        public Task<IEnumerable<Tweet>> GetReplies(long tweetId);
    }
}
