using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterWalletReplies.Domain;

namespace TwitterWalletReplies.Test
{
    internal class TestFactory
    {
        internal static List<Tweet> GetTweets()
        {
            List<Tweet> tweets = new List<Tweet>();

            tweets.Add(new Tweet("", "This is a test tweet! 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9"));
            tweets.Add(new Tweet("", "Hey! This is another test tweet! 0xab5808215d298Bc4af23cbf22baA0418E0CdDCB9"));
            tweets.Add(new Tweet("", "This is a repeated address: 0xab5808215d298Bc4af23cbf22baA0418E0CdDCB9"));
            tweets.Add(new Tweet("", "angleart.loopring.eth <~ this is a loopring address"));
            tweets.Add(new Tweet("", "This is ~> angleart.eth <~ this is an eth address"));
            tweets.Add(new Tweet("", "Tweet with multiple addresses angleart2.loopring.eth & 0xC27B9EB06d9c4E9154A444ccA392C0EEf796bC5C"));
            tweets.Add(new Tweet("", @"This is
              A multiple line tweet
              0x1853B3EEA8d0E8A5c8C7F03109109F87A659d6d9
              With an address"));

            return tweets;
        }
    }
}