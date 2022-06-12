using System.Text.RegularExpressions;

namespace TwitterWalletReplies.Domain
{
    public class Tweet
    {
        public string Username { get; }
        public string Text { get; }
        public string Address { get; set; }

        public Tweet(string username, string text)
        {
            this.Username = username;
            this.Text = text;
        }

        public string ScrapeWalletAddress()
        {
            string rgPattern = @"([a-zA-Z0-9_-]+(\.loopring)?(\.eth+))|(0x[a-fA-F0-9]{40})";

            Regex rg = new Regex(rgPattern);
            MatchCollection matches = rg.Matches(this.Text);

            if (matches.Count() > 0)
            {
                this.Address = matches[0].Value;
                return matches[0].Value;
            }

            return null;
        }
    }
}