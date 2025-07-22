
using System;
using System.IO;
using System.Collections.Generic;
using ProcessStarter;
using path;

namespace read
{


    public class Opener
    {
        List<string> domains = new List<string>();


        public List<string> fileOpen()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader("../../../../../domain.txt");
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
            BrowserHandler opera = new BrowserHandler();

            opera.OperaOpener();

            Browsers check = new Browsers();

            check.GetEdge();

            

            

        }

    }
}