using System.Runtime.Versioning;
using System.Text.RegularExpressions;


namespace Read
{

    [SupportedOSPlatform("windows")]
    public class Opener
    {
        string domainList = "../../../../../domain.txt";
        List<string> domains = new List<string>();


        public List<string> FileOpen()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader(domainList);
                line = sr.ReadLine();
                domains.Add(line);
                while (line != null)
                {

                    line = sr.ReadLine();
                    domains.Add(line);
                }
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("The error: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Finished");
            }
            return domains;
        }

        public List<string> RegexMatcher()
        {
            string pattern = @"(?m)http(?:s?):\/\/.*?([^\.\/]+?\.[^\.]+?)(?:\/|$)";

            List<string> UnporcessedDoms = FileOpen();
            List<string> ProcessedDoms = new List<string>();


            foreach (string UnprocessedDom in UnporcessedDoms)
            {
                if (UnprocessedDom != null)
                {
                    MatchCollection matches = Regex.Matches(UnprocessedDom, pattern);

                    foreach (Match match in matches)
                    {
                        Console.WriteLine(match.Groups[1].Value);
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