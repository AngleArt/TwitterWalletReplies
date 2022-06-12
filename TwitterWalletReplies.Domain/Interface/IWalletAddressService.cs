namespace TwitterWalletReplies.Domain
{
    public interface IWalletAddressService
    {
        public IEnumerable<Tweet> GetTweetAddresses(IEnumerable<Tweet> tweets);
    }
}
