using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWalletReplies.Domain
{
    public class WalletAddressService : IWalletAddressService
    {
        public IEnumerable<string> GetTweetAddresses(List<Tweet> tweets)
            => GetNonUniqueAddresses(tweets)
                .Distinct();

        private IEnumerable<string> GetNonUniqueAddresses(List<Tweet> tweets)
        {
            foreach (var item in tweets)
            {
                var address = item.ScrapeWalletAddress();

                if (!String.IsNullOrEmpty(address))
                {
                    yield return address;
                }
            }
        }
    }
}
