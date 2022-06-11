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
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "No addresses here")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsEmpty(addresses);
        }

        [Test]
        public void GetTweetAddresses_SingleAddress_ReturnsOneAddress()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 1);
            Assert.AreEqual(addresses.ToList()[0], "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_SingleEthENS_ReturnsOneAddress()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "angleart.eth")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 1);
            Assert.AreEqual(addresses.ToList()[0], "angleart.eth");
        }

        [Test]
        public void GetTweetAddresses_SingleLrcENS_ReturnsOneAddress()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "angleart.loopring.eth")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 1);
            Assert.AreEqual(addresses.ToList()[0], "angleart.loopring.eth");
        }

        [Test]
        public void GetTweetAddresses_AddressAtEnd_ReturnsOneAddress()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "This is and address 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 1);
            Assert.AreEqual(addresses.ToList()[0], "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_EnsAtStart_ReturnsOneAddress()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "angleart.loopring.eth <~ that's my ENS")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 1);
            Assert.AreEqual(addresses.ToList()[0], "angleart.loopring.eth");
        }

        [Test]
        public void GetTweetAddresses_AddressAndEns_ReturnsFirstAddress()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "Address ~> 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9 & angleart.loopring.eth <~ that's my ENS")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 1);
            Assert.AreEqual(addresses.ToList()[0], "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_MultipleAddresses1_ReturnsAddresses()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "angleart.loopring.eth <~ that's my ENS"),
                new Tweet("", "This is and address 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 2);
            Assert.AreEqual(addresses.ToList()[0], "angleart.loopring.eth");
            Assert.AreEqual(addresses.ToList()[1], "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_MultipleAddresses2_ReturnsAddresses()
        {
            List<Tweet> tweets = new List<Tweet>()
            {
                new Tweet("", "angleart.loopring.eth <~ that's my ENS"),
                new Tweet("", "This is and address 0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9")
            };

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 2);
            Assert.AreEqual(addresses.ToList()[0], "angleart.loopring.eth");
            Assert.AreEqual(addresses.ToList()[1], "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
        }

        [Test]
        public void GetTweetAddresses_MultipleAddresses3_ReturnsAddresses()
        {
            var tweets = TestFactory.GetTweets();

            var addresses = _service.GetTweetAddresses(tweets);

            Assert.IsNotEmpty(addresses);
            Assert.IsTrue(addresses.Count() == 6);
            Assert.AreEqual(addresses.ToList()[0], "0x83f94F411F2e03DAb64971CAa82253d5cd86a6E9");
            Assert.AreEqual(addresses.ToList()[2], "angleart.loopring.eth");
            Assert.AreEqual(addresses.ToList()[4], "angleart2.loopring.eth");
            Assert.AreEqual(addresses.ToList()[5], "0x1853B3EEA8d0E8A5c8C7F03109109F87A659d6d9");
        }
    }
}