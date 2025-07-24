using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Versioning;
using System.Runtime.InteropServices.Marshalling;
using System.Security;
using Microsoft.Win32;
using System.Reflection.Metadata.Ecma335;


namespace path
{
    [SupportedOSPlatform("windows")]
    public class Browsers
    {


        //path for brave, chrome: found in: Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\*



        public string getEdgeReg()
        {
            var valsnames = new Dictionary<string, object>();
            const string REGISTRY_ROOT = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";


            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_ROOT))
            {
                if (key != null)
                {
                    try
                    {
                        foreach (string s in key.GetValueNames())
                        {
                            foreach (string valueName in key.GetValueNames())
                            {
                                try
                                {

                                    Object valueData = key.GetValue(valueName);


                                    if (valueName.Contains("MicrosoftEdgeAutoLaunch"))
                                    {
                                        string path = valueData.ToString();
                                        Console.WriteLine(path);
                                        return path;
                                    }
                                }
                                catch (SecurityException e)
                                {
                                    Console.WriteLine("you don't have the premission to read the subkey" + e.Message);
                                }
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error:" + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("The key provided is invalid.");
                }
            }
            return "";
        }


        public string getMozillaReg()
        {
            var valsnames = new Dictionary<string, object>();
            const string REGISTRY_ROOT = @"SOFTWARE\Mozilla\Mozilla Firefox\128.13.0 ESR (x64 en-US)\Main";


            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_ROOT))
            {
                if (key != null)
                {
                    try
                    {
                        foreach (string valueName in key.GetValueNames())
                        {
                            try
                            {

                                Object valueData = key.GetValue(valueName);


                                if (valueName.Contains("PathToExe"))
                                {
                                    string path = valueData.ToString();
                                    Console.WriteLine(path);
                                    return path;
                                }
                            }
                            catch (SecurityException e)
                            {
                                Console.WriteLine("you don't have the premission to read the subkey" + e.Message);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error:" + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("The key provided is invalid.");
                }
            }
            return "";
        }

        public string bruteEdgeGetter()
        {
            string username = Environment.UserName;

        string[] pathArr = {
            @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
            @"C:\Program Files\Microsoft\Edge\Application\msedge.exe",
            $@"C:\Users\{username}\AppData\Local\Microsoft\Edge\Application\msedge.exe"
        };

            foreach (string path in pathArr)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return "Microsoft edge  has not been found, please search for it via pressing the windows button and the type 'Microsoft Edge' and do the filtration check manually";
        }

        public string bruteForceMozillaGetter()
        {
            string username = Environment.UserName;

        string[] pathArr = {
            @"C:\Program Files\Mozilla Firefox\firefox.exe",
            @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe",
            $@"C:\Users\{username}\AppData\Local\Mozilla Firefox\firefox.exe"
        };

            foreach (string path in pathArr)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return "Mozilla firefox has not been found, please search for it via pressing the windows button and the type 'firefox' and do the filtration check manually";
        }



        public string bruteForceChromeGetter()
        {
            string username = Environment.UserName;

        string[] pathArr = {
            @"C:\Program Files\Google\Chrome\Application\chrome.exe",
            @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
            $@"C:\Users\{username}\AppData\Local\Google\Chrome\Application\chrome.exe"
            };

            foreach (string path in pathArr)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return "Google Chrome has not been found, please search for it via pressing the windows button and the type 'Google Chrome' and do the filtration check manually";
        }


        public string bruteForceOperaGetter()
        {

            string username = Environment.UserName;


            string[] pathArr = {
            @"C:\Program Files\Opera\launcher.exe",
            @"C:\Program Files (x86)\Opera\launcher.exe",
            $@"C:\Users\{username}\AppData\Local\Programs\Opera\launcher.exe",
            $@"C:\Users\{username}\AppData\Local\Opera Software\Opera Stable\opera.exe"
        };
        
            foreach (string path in pathArr)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return "Opera browser has not been found, please search for it via pressing the windows button and the type 'Opera' and do the filtration check manually";
        }

    }
}