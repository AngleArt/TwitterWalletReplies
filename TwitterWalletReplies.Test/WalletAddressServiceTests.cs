using NUnit.Framework;
using System.Collections.Generic;
using TwitterWalletReplies.Domain;
using System.Linq;

namespace TwitterWalletReplies.Test
{
    public class WalletAddressServiceTests
    {
        IWalletAddressService _service;

        [SetUp]
        public void Setup()
        {
            _service = new WalletAddressService();
        }

        [Test]
        public void GetTweetAddresses_NoAddress_ReturnsEmpty()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("", "No tweets here")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsEmpty(tweets);
        }

        [Test]
        public void GetTweetAddresses_SingleAddress_ReturnsOneAddressAndUsername()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("test_username", "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 1);
            Assert.AreEqual(tweets.ToList()[0].Address, "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
            Assert.AreEqual(tweets.ToList()[0].Username, "test_username");
        }

        [Test]
        public void GetTweetAddresses_SingleEthENS_ReturnsOneAddressAndUsername()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("test_username", "angleart.eth")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 1);
            Assert.AreEqual(tweets.ToList()[0].Address, "angleart.eth");
            Assert.AreEqual(tweets.ToList()[0].Username, "test_username");
        }

        [Test]
        public void GetTweetAddresses_SingleLrcENS_ReturnsOneAddress()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("test_username", "angleart.loopring.eth")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 1);
            Assert.AreEqual(tweets.ToList()[0].Address, "angleart.loopring.eth");
            Assert.AreEqual(tweets.ToList()[0].Username, "test_username");
        }

        [Test]
        public void GetTweetAddresses_AddressAtEnd_ReturnsOneAddress()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("", "This is and address 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 1);
            Assert.AreEqual(tweets.ToList()[0].Address, "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_EnsAtStart_ReturnsOneAddress()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("", "angleart.loopring.eth <~ that's my ENS")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 1);
            Assert.AreEqual(tweets.ToList()[0].Address, "angleart.loopring.eth");
        }

        [Test]
        public void GetTweetAddresses_AddressAndEns_ReturnsFirstAddress()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("", "Address ~> 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9 & angleart.loopring.eth <~ that's my ENS")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 1);
            Assert.AreEqual(tweets.ToList()[0].Address, "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_MultipleAddresses1_ReturnsAddressesAndUsernames()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("test_username", "angleart.loopring.eth <~ that's my ENS"),
                new Tweet("test_username2", "This is and address 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 2);
            Assert.AreEqual(tweets.ToList()[0].Address, "angleart.loopring.eth");
            Assert.AreEqual(tweets.ToList()[0].Username, "test_username");
            Assert.AreEqual(tweets.ToList()[1].Address, "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
            Assert.AreEqual(tweets.ToList()[1].Username, "test_username2");
        }

        [Test]
        public void GetTweetAddresses_MultipleAddresses2_ReturnsAddresses()
        {
            List<Tweet> input = new List<Tweet>()
            {
                new Tweet("", "angleart.loopring.eth <~ that's my ENS"),
                new Tweet("", "This is and address 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 2);
            Assert.AreEqual(tweets.ToList()[0].Address, "angleart.loopring.eth");
            Assert.AreEqual(tweets.ToList()[1].Address, "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_MultipleAddresses3_ReturnsAddresses()
        {
            var input = TestFactory.GetTweets();

            var tweets = _service.GetTweetAddresses(input);

            Assert.IsNotEmpty(tweets);
            Assert.IsTrue(tweets.Count() == 6);
            Assert.AreEqual(tweets.ToList()[0].Address, "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
            Assert.AreEqual(tweets.ToList()[2].Address, "angleart.loopring.eth");
            Assert.AreEqual(tweets.ToList()[4].Address, "angleart2.loopring.eth");
            Assert.AreEqual(tweets.ToList()[5].Address, "0x1853B3EEA8d0E8A5c8C7F03109109F87A659d6d9");
        }
    }
}