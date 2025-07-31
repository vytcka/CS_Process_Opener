using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace Read
{

    [SupportedOSPlatform("windows")]
    public class Opener
    {
        string domainList = "../../../../../domain.txt";
        List<string> domains = new List<string>();
        

        

        public List<string> FileOpen()
        {
            List<string> domains = new List<string>();


            List<string> domainTypes =
            [
                "Pornogr",
                "Erotik",
                "Smurt",
                "Lošim",
                "ginkl",
                "alkohol",
                "natkotik",
                "tabak",
                "rasin",
                "server",
            ];



            try
            {
                StreamReader sr = new StreamReader(domainList);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
  
                    
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

                            //IMPLEMENT A DYNAMIC ONE FOR THE FUTURE, SINCE ITS DIFFICULT, AND I NEED TO UNDERSTAND HOW...
                        }
                    }
                    */

                    foreach (string DomType in domainTypes)
                    {
                        

                        if (line.Contains(DomType))
                        {
                            //Console.WriteLine(DomType + " Tipas");
                            //Console.WriteLine (line + " kas linijoje");
                            //processingDomains = true;

                            break;

                        }

                        else if (!line.Contains(DomType, StringComparison.OrdinalIgnoreCase))
                        {

                            if (!domains.Contains(line))
                            {
                                domains.Add(line);
                            }
                            //Console.WriteLine(DomType + " tipas");
                                //Console.WriteLine(line + " linija.");

                            }


                    }


                }

                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("The error: " + e.Message);
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