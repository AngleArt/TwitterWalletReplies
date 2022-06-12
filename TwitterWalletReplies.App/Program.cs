using Microsoft.Extensions.Configuration;
using TwitterWalletReplies.Domain;

namespace TwitterWalletReplies.App
{
    class Program
    {
        private static IConfigurationRoot _config;
        private static ITwitterApiService _apiService;
        private static string _bearerToken;

        static async Task Main(string[] args)
        {
            try
            {
                long tweetId = InputHelper.GetTweetId(args);

                StartConfig();
                StartServices();

                Console.WriteLine($"Processing the replies for '{tweetId}'...");
                var replies = await _apiService.GetReplies(tweetId);

                //Console.WriteLine("Saving addresses...");
                //IOService.SaveAddresses(replies);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Argument error: {ex.Message}.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Configuration error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown issue found, please contact AngleArt.\n\nError: {ex.Message}\n\nSource: {ex.Source}");
            }
            finally
            {
                Console.WriteLine("Press any key to close the application.");
                Console.ReadKey();
            }
        }

        private static void StartConfig()
        {
            // AppSettings / Config
            _config = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", false)
               .Build();

            // Set values
            _bearerToken = InputHelper.GetConfig(_config, "TwitterBearerToken", true);
        }

        private static void StartServices()
            => _apiService = new TwitterApiService(_bearerToken);
    }
}