namespace TwitterWalletReplies.Domain
{
    public class WalletAddressService : IWalletAddressService
    {
        public IEnumerable<Tweet> GetTweetAddresses(IEnumerable<Tweet> tweets)
        {
            foreach (var item in tweets)
            {
                item.ScrapeWalletAddress();
            }

            return tweets
                .Where(t => !String.IsNullOrEmpty(t.Address))
                .DistinctBy(t => t.Address);
        }
    }
}
