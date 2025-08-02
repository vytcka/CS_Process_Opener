using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.IO;

namespace Read
{

    [SupportedOSPlatform("windows")]
    public class Opener
    {
        private readonly string liveConfigPath;

        string exeDirectory = AppContext.BaseDirectory;

        string liveConfigFileName = "domains.txt";
        string defaultConfigFileName = "domains.default.txt";

        public Opener()
        {
            liveConfigPath = System.IO.Path.Combine(exeDirectory, liveConfigFileName);
            string defaultConfigPath = System.IO.Path.Combine(exeDirectory, defaultConfigFileName);

            
            InitializeUserConfig(liveConfigPath, defaultConfigPath);
        }

        private void InitializeUserConfig(string userPath, string defaultPath)
        {
            if (!File.Exists(userPath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("neradome failo, 'domenai.txt', luktėlkite...");

                if (File.Exists(defaultPath))
                {
                    Console.WriteLine("Kuremas naujas failas, 'domenai.txt'");
                    Console.WriteLine($"You can now edit this file: {userPath}");

                    File.Copy(defaultPath, userPath);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Pranešame, jog domenu failo nera direkotryjoje, pridedame txt faila");
                    File.WriteAllText(userPath, "# Idėkite visus domėnus čia, svarbiausia jog domėnai būtų pilni; pvz: https://pavizdys.xyz");
                }
                Console.ResetColor();
            }
        }

        public List<string> FileOpen()
        {
            // 1. Start with the robust structure: Check if the file exists.
            if (!File.Exists(liveConfigPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"FATAL ERROR: Configuration file could not be found at '{liveConfigPath}'");
                Console.ResetColor();
                return new List<string>();
            }

            var domains = new List<string>();

            var domainTypes = new List<string>
        {
            "Pornogr", "Erotik", "Smurt", "Lošim", "ginkl",
            "alkohol", "natkotik", "tabak", "rasin", "server"
        };

            try
            {
                // 4. Use a 'using' block to ensure the file is closed automatically.
                using (var sr = new StreamReader(liveConfigPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        bool isCategoryHeader = false;
                        foreach (string domType in domainTypes)
                        {
                            if (line.Contains(domType, StringComparison.OrdinalIgnoreCase))
                            {
                                isCategoryHeader = true;
                                break;
                            }
                        }

                        if (!isCategoryHeader)
                        {
                            string cleanDomain = line.Trim();
                            if (!domains.Contains(cleanDomain))
                            {
                                domains.Add(cleanDomain);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading domains.txt: " + e.Message);
            }
            return domains;
        }
        public List<string> RegexMatcher()
        {
            string pattern = @"(?:https?:\/\/)?(?:www\.)?([a-zA-Z0-9-]+\.[a-zA-Z]{2,})";

            // Ideja, veiks pagal domenu tipa, ir privalumas butu chronologiskumas, nebent nuo iki veiks, tai jeigu
            // this would work when iterating through dict keys.. so file open  has to be changed
            List<string> UnporcessedDoms = FileOpen();
            List<string> ProcessedDoms = new List<string>();



            foreach (string UnprocessedDom in UnporcessedDoms)
            {


                if (UnprocessedDom != null)
                {
                    MatchCollection matches = Regex.Matches(UnprocessedDom, pattern);

                    foreach (Match match in matches)
                    {
                        ProcessedDoms.Add(match.Groups[1].Value);
                    }

                }
            }


            return ProcessedDoms;
        }

        public string regValueToString(string regValue)
        {
            int firstQuote = regValue.IndexOf('"');
            int secondQuote = regValue.IndexOf('"', firstQuote + 1);

            if (firstQuote != -1 && secondQuote != -1)
            {
                string exePath = regValue.Substring(firstQuote + 1, secondQuote - firstQuote - 1);

                return exePath;
            }
            else
            {
                Console.WriteLine("No quoted path found.");
                return "";
            }
        }
    }
}