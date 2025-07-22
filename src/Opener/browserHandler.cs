
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using read;

namespace ProcessStarter
{

    public class BrowserHandler
    {
        Process process = new Process();
        Opener s = new Opener();

        List<string> domains = s.fileOpen();

        public string chromePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";

        public string browser;
        public List<string> targets;

        public string BrowserLocator()
        {
            try
            {
                Console.Write("hello world");
            }
            catch (Exception e)
            {
                Console.WriteLine("hello world" + e.Message);
            }

            return "hello world";
        }



        public void OperaOpener()
        {
            string url = "https://15min.lt";
            try
            {
                process.StartInfo.FileName = @"C:\Users\Vytautas\AppData\Local\Programs\Opera\opera.exe";
                process.StartInfo.UseShellExecute = true;
                System.Diagnostics.Process.Start(process.StartInfo.FileName, url);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

    }
}