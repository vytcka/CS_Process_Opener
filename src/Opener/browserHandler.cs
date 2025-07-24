
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using read;
using System.Runtime.Versioning;

namespace ProcessStarter
{
[SupportedOSPlatform("windows")]
    public class BrowserHandler
    {
        Process process = new Process();
        Opener s = new Opener();

        List<string> domains;



        public string GetDomains()
        {
            return string.Join(" ", domains.Select(d => $"\"{d}\""));
        }

        public string chromePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";



        public void OperaOpener()
        {
            domains = s.fileOpen();

            try
            {
                process.StartInfo.FileName = @"C:\Users\Vytautas\AppData\Local\Programs\Opera\opera.exe";
                process.StartInfo.UseShellExecute = true;
                Process.Start(process.StartInfo.FileName, GetDomains());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

    }
}