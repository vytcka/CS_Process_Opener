using Read;
using System.Runtime.Versioning;
using Emulator;

[SupportedOSPlatform("windows")]
class MainLauncher
{



    static async Task Main(string[] args)
    {
        Opener processedDom = new();

        PlaywrightEmulator tests = new();




        List<string> domains = processedDom.FileOpen();
        List<string> results = new List<string>();

        if (domains.Count() == 0)
        {
            while (true)
            {
                Console.WriteLine("Domenu sarasas tuscias, patikrinkite domenu sarasa per nauja");
                Console.WriteLine("Paspauskite raide 'b' arba 'B' ir enter, kad iseitumete is programos");
                string atsakymas = Console.ReadLine();
                if (atsakymas == "B" || atsakymas == "b")
                {
                    return;
                }
            }
        }

        int counterPraleido = 0;
        int counterNepraleido = 0;

        foreach (string domain in domains)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                await tests.EmulatorAsync(domain);
                Console.WriteLine("websaitas {0} praleistas", domain);
                results.Add("Užkrovė");
                counterPraleido++;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("Timeout"))
                {
                    Console.WriteLine("Domenas " + domain + " per ilgai užtruko, pažiūrėkite ar jis tikrai veikia.");
                    results.Add("neprieinamas");
                    counterNepraleido++;
                    continue;
                }
                else if (e.ToString().Contains("ERR_NAME_NOT_RESOLVED"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("websaitas {0} uzblokuotas", domain);
                    results.Add("uzblokavo");
                    counterNepraleido++;

                }

                else if (e.ToString().Contains("playwright.ps1 install"))
                {
                    while (true)
                    {
                        Console.WriteLine("Naršyklės nėra įdiegtos. Prašome kreiptis į programos kūrėją dėl papildomos informacijos. Programa neveiks tinkamai.");
                        Console.WriteLine("Paspauskite raide 'b' arba 'B' ir enter, kad iseitumete is programos");
                        string atsakymas = Console.ReadLine();
                        if (atsakymas == "B" || atsakymas == "b")
                        {
                            return;
                        }
                    }
                }
                else{
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Įvyko netikėta klaida su domenu {domain}: {e.Message}");
                        results.Add("Klaida (Netikėta)");
                        counterNepraleido++;
                    }
            }

            }

            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        Console.ForegroundColor = ConsoleColor.White;

        float numPraleido = ((float)counterPraleido / domains.Count()) * 100;
        float numNepraleido = ((float)counterNepraleido / domains.Count()) * 100;

        Console.WriteLine("\n=== Domenų blokavimo rezultatas ===");
        Console.WriteLine("Dabartinis Laikas: " + DateTime.Now + "\n");

        for (int i = 0; i < domains.Count; i++)
        {
            try
            {
                if (results[i] == "Užkrovė")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else if (results[i] == "uzblokavo")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }


                Console.WriteLine($"- {domains[i]} ......... {results[i]}");
                Console.ForegroundColor = ConsoleColor.White;

            }
            catch (ArgumentOutOfRangeException)
            {
                while (true)
                {
                    Console.WriteLine("Perziurekite faila 'domenai.txt' nes truksta domenu.");
                    Console.WriteLine("Paspauskite raide B arba b + enter, kad isteitumete is programos.");
                    string input = Console.ReadLine();
                    if (input == "b" || input == "B")
                    {
                        return;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine($"\niš viso praleido svetainių: {numPraleido}%");
            Console.WriteLine($"\niš viso užblokavo svetainių: {numNepraleido}%");


            while (true)
        {

            Console.WriteLine("Paspauskite raide B kad isteitumete is programos.");
            string input = Console.ReadLine();
            if (input == "b" || input == "B")
            {
                return;
            }
        }
    }
}
