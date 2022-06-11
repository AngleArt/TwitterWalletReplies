using Microsoft.Extensions.Configuration;
using TwitterWalletReplies.Domain;

namespace TwitterWalletReplies.App
{
    class Program
    {
        private static IConfigurationRoot _config;
        private static TwitterApiService _apiService;
        private static string _bearerToken;

        static void Main(string[] args)
        {
            try
            {
                long twitId = InputHelper.GetTwitId(args);

                StartConfig();
                StartServices();

                Console.WriteLine($"Press enter to process the replies for '{twitId}'...");
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
                Console.WriteLine($"Unknown issue found, please contact AngleArt.\nError: {ex.Message}\nSource: {ex.Source}\nInner ex: {ex.InnerException}");
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
        {
            _apiService = new TwitterApiService(_bearerToken);
        }
    }
}