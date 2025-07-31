
using System;
using Path;
using Read;
using System.Runtime.Versioning;
using ProcessStarter;
using Emulator;
using System.Text;
using System.Runtime.Intrinsics.Arm;


[SupportedOSPlatform("windows")]
class MainLauncher
{
    static async Task Main(string[] args)

    {
        /*Browsers browserPaths = new Browsers();
        BrowserHandler browserOpener = new BrowserHandler();
        Opener s = new Opener();

        s.FileOpen();

        browserOpener.GetDomains();

        string edgePath = browserPaths.bruteEdgeGetter();
        string mozillaPath = browserPaths.bruteForceMozillaGetter();
        string chromePath = browserPaths.bruteForceChromeGetter();
        string operaPath = browserPaths.bruteForceOperaGetter();

        if (edgePath == null || mozillaPath == null || chromePath == null || operaPath == null)
        {
            Console.WriteLine("blogai");
            return;
        }
        else
        {
            Console.WriteLine(operaPath);
            Console.WriteLine("gerai");
        }

        browserOpener.BrowserDomainOpener(edgePath);
        browserOpener.BrowserDomainOpener(mozillaPath);
        browserOpener.BrowserDomainOpener(chromePath);
        browserOpener.BrowserDomainOpener(operaPath);
    */
        //await vars.EmulatorAsync();
        //organizacija maine, turetu buti isdesti, kiek domenu idetoje kategorija ir hronologiskai isdelioti jas.

        Opener processedDom = new Opener();

        PlaywrightEmulator tests = new PlaywrightEmulator();



        List<string> domains = processedDom.RegexMatcher();
        List<string> results = new List<string>();


        int counter = 0;
        foreach (string domain in domains)
        {
            try
            {
                await tests.EmulatorAsync(domain);
                Console.WriteLine("websaitas {0} praleistas", domain);
                results.Add("Užkrovė");
            }
            catch (Exception)
            {
                Console.WriteLine("websaitas {0} uzblokuotas", domain);
                counter++;
                results.Add("Nepraleido");
            }


        }


        float num = ((float) counter/ domains.Count() ) * 100;


        StringBuilder report = new StringBuilder();
        report.AppendLine("=== Domenu blokavimo rezultatas ===");
        report.AppendLine("Dabartinis Laikas: " + DateTime.Now);
        report.AppendLine();



        for (int i = 0; i < domains.Count; i++)
        {
            report.AppendLine($"- {domains[i]} ......... {results[i]}");
        }
        report.AppendLine();
        report.AppendLine($"iš viso užblokavo {num}%");

        Console.WriteLine(report.ToString());
    }
        
    
}