using Microsoft.Extensions.Configuration;

namespace TwitterWalletReplies.App
{
    internal static class InputHelper
    {
        internal static long GetTweetId(string[] args)
        {
            long TweetId;

            if (args.Length < 1)
            {
                throw new ArgumentException("TweetId is mandatory");
            }

            if (!long.TryParse(args[0], out TweetId))
            {
                throw new ArgumentException("Invalid format for TweetId");
            }

            return TweetId;
        }

        internal static string GetConfig(IConfigurationRoot config, string key, bool isMandatory)
            => isMandatory
                ? config.GetRequiredSection(key).Value.ToString()
                : config.GetSection(key)?.Value.ToString();
    }
}