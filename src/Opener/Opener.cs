using System.Runtime.Versioning;


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