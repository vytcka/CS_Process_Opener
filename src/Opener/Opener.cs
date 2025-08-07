using System.Runtime.Versioning; // Leidžia nurodyti, kuriose OS platformose klasė/metodas veiks
using System.Text.RegularExpressions; // Naudojama darbui su reguliariosiomis išraiškomis (Regex)

namespace Read
{
    [SupportedOSPlatform("windows")] // Ši klasė veiks tik Windows operacinėje sistemoje bei programa veikia tiktais ant anglu kalbos.
    public class Opener
    {
     
        // Kelias iki aktyviai naudojamo domenų failo (pvz., "C:/ProgramFiles/ManoPrograma/domenai.txt")
        private readonly string liveConfigPath;

        // Programos paleidimo direktorija (kurioje yra .exe failas)
        string exeDirectory = AppContext.BaseDirectory;

        // Pagrindinio domenų failo pavadinimas
        string liveConfigFileName = "domenai.txt";

        // Atsarginio (šabloninio) domenų failo pavadinimas
        string defaultConfigFileName = "domains.default.txt";

        // ---------------------
        // KONSTRUKTORIUS
        // ---------------------
        public Opener()
        {
            // Sudarome pilną kelią iki "domenai.txt"
            liveConfigPath = System.IO.Path.Combine(exeDirectory, liveConfigFileName);

            // Sudarome pilną kelią iki "domains.default.txt"
            string defaultConfigPath = System.IO.Path.Combine(exeDirectory, defaultConfigFileName);

            // Paleidžiame inicializacijos metodą, kuris pasirūpina,
            // kad vartotojas turėtų bent tuščią domenų failą
            InitializeUserConfig(liveConfigPath, defaultConfigPath);
        }

        // ---------------------
        // METODAS: Failo inicializacija
        // ---------------------
        private void InitializeUserConfig(string userPath, string defaultPath)
        {
            // Tikriname, ar egzistuoja pagrindinis domenų failas
            if (!File.Exists(userPath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("neradome failo, 'domenai.txt', luktėlkite...");

                // Jei randame atsarginį domenų failą – kopijuojame jį kaip naują domenai.txt
                if (File.Exists(defaultPath))
                {
                    Console.WriteLine("Kuriamas naujas failas, 'domenai.txt'");
                    Console.WriteLine($"You can now edit this file: {userPath}");

                    // Kopijuojame failą iš atsarginės vietos
                    File.Copy(defaultPath, userPath);
                }
                else
                {
                    // Jei atsarginio failo nėra – sukuriame naują failą su komentaru-instrukcija
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Pranešame, jog domenu failo nera direkotryjoje, pridedame txt faila");

                    File.WriteAllText(
                        userPath,
                        "# Idėkite visus domėnus čia, svarbiausia jog domėnai būtų pilni; pvz: https://pavizdys.xyz"
                    );
                }

                // Grąžiname konsolės spalvą į pradinę
                Console.ResetColor();
            }
        }

        // ---------------------
        // METODAS: Nuskaito domenus iš failo
        // ---------------------
        public List<string> FileOpen()
        {
            // Jei failas neegzistuoja – pranešame vartotojui ir grąžiname tuščią sąrašą
            if (!File.Exists(liveConfigPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Konfiguracija nerasta: '{liveConfigPath}'");
                Console.ResetColor();
                return new List<string>();
            }

            var domains = new List<string>();

            // Raktiniai žodžiai, žymintys kategorijų antraštes domenų sąraše
            // Tokias eilutes praleisime, nes jos nėra realūs domenai
            var domainTypes = new List<string>
            {
                "Pornogr", "Erotik", "Smurt", "Lošim", "ginkl",
                "alkohol", "natkotik", "tabak", "rasin", "server"
            };

            try
            {
                // Atidarome failą skaitymui
                using (var sr = new StreamReader(liveConfigPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Jei eilutė tuščia arba sudaryta tik iš tarpų – praleidžiame
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        bool isCategoryHeader = false;

                        // Tikriname, ar ši eilutė yra kategorijos antraštė
                        foreach (string domType in domainTypes)
                        {
                            if (line.Contains(domType, StringComparison.OrdinalIgnoreCase))
                            {
                                isCategoryHeader = true;
                                break; // Nebereikia tikrinti kitų raktažodžių
                            }
                        }

                        // Jei tai ne kategorijos pavadinimas – pridedame į sąrašą
                        if (!isCategoryHeader)
                        {
                            string cleanDomain = line.Trim();

                            // Užtikriname, kad sąraše nebūtų pasikartojančių domenų
                            if (!domains.Contains(cleanDomain))
                            {
                                domains.Add(cleanDomain);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Jei įvyksta klaida skaitant failą – parodome klaidos pranešimą
                Console.WriteLine("Error while reading domains.txt: " + e.Message);
            }

            // Grąžiname galutinį domenų sąrašą
            return domains;
        }

        // ---------------------
        // METODAS: Regex filtravimas
        // ---------------------
        public List<string> RegexMatcher()
        {
            // Regex šablonas, skirtas išgauti domeno pavadinimą be protokolo (http, https) ir www
            string pattern = @"(?:https?:\/\/)?(?:www\.)?([a-zA-Z0-9-]+\.[a-zA-Z]{2,})";

            // Nuskaitome visus domenus iš failo
            List<string> UnprocessedDoms = FileOpen();

            // Sąrašas, kuriame saugosime apdorotus domenus
            List<string> ProcessedDoms = new List<string>();

            // Einame per kiekvieną neapdorotą domeną
            foreach (string UnprocessedDom in UnprocessedDoms)
            {
                if (UnprocessedDom != null)
                {
                    // Atliekame regex paiešką pagal nurodytą šabloną
                    MatchCollection matches = Regex.Matches(UnprocessedDom, pattern);

                    // Kiekvienam regex atitikmeniui – pridedame tik pagrindinę domeno dalį
                    foreach (Match match in matches)
                    {
                        ProcessedDoms.Add(match.Groups[1].Value);
                    }
                }
            }

            // Grąžiname tik išgrynintus domenus
            return ProcessedDoms;
        }

        // ---------------------
        // METODAS: Iš registro reikšmės ištraukia failo kelią
        // ---------------------
        public string regValueToString(string regValue)
        {
            // Randame pirmos kabutės indeksą
            int firstQuote = regValue.IndexOf('"');

            // Randame antros kabutės indeksą (po pirmos)
            int secondQuote = regValue.IndexOf('"', firstQuote + 1);

            // Jei abi kabutės rastos – iškerpame tekstą tarp jų
            if (firstQuote != -1 && secondQuote != -1)
            {
                string exePath = regValue.Substring(firstQuote + 1, secondQuote - firstQuote - 1);
                return exePath;
            }
            else
            {
                // Jei kabučių nerasta – pranešame vartotojui
                Console.WriteLine("No quoted path found.");
                return "";
            }
        }
    }
}
