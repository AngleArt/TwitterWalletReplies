using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWalletReplies.Domain
{
    public class TwitterApiService : ITwitterApiService
    {
        private string BearerToken { get; set; }

        public TwitterApiService(string bearerToken)
        {
            BearerToken = bearerToken;
        }
    }
}
