
using System;
using System.IO;
using System.Collections.Generic;
using ProcessStarter;
using path;
using System.Runtime.Versioning;

namespace read
{

    [SupportedOSPlatform("windows")]
    public class Opener
    {
        string domainList = "../../../../../domain.txt";
        List<string> domains = new List<string>();


        public List<string> fileOpen()
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



        public static void Main()
        {


            Browsers check = new Browsers();

            Dictionary<String, object> s = check.getRegistrySubKeys();

            foreach (var ele in s)
            {
                Console.WriteLine($"Key: {ele.Key}, Value: {ele.Value}");
            }

            

            

        }

    }
}