using System.Diagnostics;
using System.Text;

namespace TwitterWalletReplies.Domain
{
    public class IOService
    {
        public static string SaveAddresses(IEnumerable<Tweet> tweets, bool saveUsernames)
        {
            if (tweets.Count() < 1)
            {
                throw new Exception("No addresses to save!");
            }

            var outPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\output";
            var dirInfo = new DirectoryInfo(outPath);

            // Checks/creates the output folder
            CheckDirectoryExists(dirInfo);

            return SaveCsv(dirInfo, tweets, saveUsernames);
        }

        private static string SaveCsv(DirectoryInfo dirInfo, IEnumerable<Tweet> tweets, bool saveUsernames)
        {
            StringBuilder sb = new StringBuilder("");
            BuildCsvContent(tweets, saveUsernames, sb);

            var outPath = $@"{dirInfo.FullName}\{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv";

            using (StreamWriter swriter = new StreamWriter(outPath))
            {
                swriter.Write(sb.ToString());
            }

            return outPath;
        }

        private static void BuildCsvContent(IEnumerable<Tweet> tweets, bool saveUsernames, StringBuilder sb)
        {
            foreach (var item in tweets)
            {
                if (saveUsernames)
                {
                    sb.Append($"{item.Username},");
                }

                sb.AppendLine($"{item.Address}");
            }
        }

        private static void CheckDirectoryExists(DirectoryInfo dirInfo)
        {
            if (!dirInfo.Exists)
            {
                System.IO.Directory.CreateDirectory(dirInfo.FullName);
            }
        }
    }
}
