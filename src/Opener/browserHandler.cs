using System.Diagnostics;
using Read;
using System.Runtime.Versioning;

namespace ProcessStarter
{
[SupportedOSPlatform("windows")]
    public class BrowserHandler
    {
        Process process = new Process();
        Opener s;
    List<string> domains;

        public BrowserHandler()
        {
        s = new Opener();
        domains = s.FileOpen();
        }


        public string GetDomains()
        {
            return string.Join(" ", domains.Select(d => $"\"{d}\""));
        }

        public void BrowserDomainOpener(string browser)
        {
            try
            {
                process.StartInfo.FileName = @browser;
                process.StartInfo.UseShellExecute = true;
                Process.Start(process.StartInfo.FileName, GetDomains());
            }
            catch (Exception e)
            {
                foreach (string s in domains)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("Error:" + e.Message);
                Console.WriteLine(browser);
                
            }
        }

    }
}