using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;

namespace Read
{

    [SupportedOSPlatform("windows")]
    public class Opener
    {
        string domainList = "../../../../../domain.txt";
        List<string> domains = new List<string>();


        public List<string> FileOpen()
        {
            Dictionary<string, string> T_url = new Dictionary<string, string>();

            T_url.Add("Pornogr", null);
            T_url.Add("Erotik", null);
            T_url.Add("Smurt", null);
            T_url.Add("Lošim", null);
            T_url.Add("ginkl", null);
            T_url.Add("alkohol", null);
            T_url.Add("natkotik", null);
            T_url.Add("tabak", null);
            T_url.Add("rasin", null);
            T_url.Add("server", null);


            try
            {
                StreamReader sr = new StreamReader(domainList);
                string line;
                bool processingDomains = false;
                string activeDomainType = null;
                uint counter = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    counter++;
                    Console.WriteLine("the current line is: " + counter);
                    /* we need to set a setter, being bool processingDomains = false;

                         for each domtype in T_url.key
                              if line != domtype[i]{
                              contine
                              }
                              else{
                              sr.readline() until
                              \sr.readline() == !domtype[i]
                              }

                            state management:
                        Before processing domains, you are in "searching for domain type" mode.

                        Once you find a matching domain type, you switch processingDomains = true and set currentType = matchedKey.

                        While processingDomains is true, you append URLs to T_url[currentType].

                        If a new domain type is encountered (a line that matches another key), switch currentType and continue appending to that key.
                     */

                    /*if (processingDomains)
                    {
                        Console.WriteLine(activeDomainType + " processing domains is true");
                        foreach (string domType in T_url.Keys)
                        {
                            if (line.Contains(domType, StringComparison.OrdinalIgnoreCase))
                            {
                                activeDomainType = null;
                                processingDomains = false;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(activeDomainType + " processing domains is false");
                        foreach (string domType in T_url.Keys)
                        {
                            if (line.Contains(domType, StringComparison.OrdinalIgnoreCase))
                            {
                                activeDomainType = domType;
                                processingDomains = true;
                            }
                            else
                            {
                                continue;
                            }


                        }
                    }
                    */

                    foreach (string DomType in T_url.Keys)
                    {
                        if (line.Contains(DomType))
                        {
                            //Console.WriteLine(DomType + " Tipas");
                            //Console.WriteLine (line + " kas linijoje");
                            processingDomains = true;
                            break;
                            
                        }

                        else if (processingDomains && !line.Contains(DomType, StringComparison.OrdinalIgnoreCase))
                        {

                            //Console.WriteLine(DomType + " tipas");
                            //Console.WriteLine(line + " linija.");
                            T_url[DomType] = line;

                        }


                    }
                    

                }
                    foreach (KeyValuePair<string, string> kvp in T_url)
                            {
                                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
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