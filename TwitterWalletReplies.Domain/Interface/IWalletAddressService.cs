﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWalletReplies.Domain
{
    public interface IWalletAddressService
    {
        public IEnumerable<string> GetTweetAddresses(List<Tweet> tweets);
    }
}
