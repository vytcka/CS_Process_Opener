using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Versioning;
using System.Runtime.InteropServices.Marshalling;
using System.Security;
using Microsoft.Win32;


namespace path
{
    [SupportedOSPlatform("windows")]
    public class Browsers
    {


        //path for brave, chrome: found in: Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\*



        public Dictionary<string, object> getRegistrySubKeys()
        {
            var valsnames = new Dictionary<string, object>();
            const string REGISTRY_ROOT = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";


            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_ROOT))
            {
                if (key != null)
                {
                    try
                    {
                        string[] val_names = key.GetValueNames();
                        foreach (string s in val_names)
                        {
                            Console.WriteLine(s);
                            if (s.Contains("MicrosoftEdgeAutoLaunch%"))
                            {
                                try
                                {
                                    string[] subKey_vals = key.GetSubKeyNames();
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
            }
            return valsnames;
        }





        public void GetEdge()
        {
            RegistryKey rk = Registry.LocalMachine;
            string[] names = rk.GetSubKeyNames();

            foreach (string s in names)
            {
                Console.WriteLine(s);
            }
        }
    }
}