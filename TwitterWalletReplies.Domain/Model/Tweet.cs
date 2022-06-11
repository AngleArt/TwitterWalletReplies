namespace TwitterWalletReplies.Domain
{
    public class Tweet
    {
        public string Username { get; }
        public string Text { get; }

        public Tweet(string username, string text)
        {
            this.Username = username;
            this.Text = text;
        }
    }
}