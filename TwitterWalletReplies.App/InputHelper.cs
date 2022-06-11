using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWalletReplies.App
{
    internal static class InputHelper
    {
        internal static long GetTwitId(string[] args)
        {
            long twitId;

            if (args.Length < 1)
            {
                throw new ArgumentException("TwitId is mandatory");
            }

            if (!long.TryParse(args[0], out twitId))
            {
                throw new ArgumentException("Invalid format for TwitId");
            }

            return twitId;
        }

        internal static string GetConfig(IConfigurationRoot config, string key, bool isMandatory)
            => isMandatory
                ? config.GetRequiredSection(key).ToString()
                : config.GetSection(key).ToString();
    }
}