using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWalletReplies.Domain
{
    public class TweetUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        public TweetUser(long id, string name, string username)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
        }
    }
}
