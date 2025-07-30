
using System;
using Path;
using Read;
using System.Runtime.Versioning;
using ProcessStarter;
using Emulator;

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

        Opener processedDom = new Opener();


        //organizacija maine, turetu buti isdesti, kiek domenu idetoje kategorija ir hronologiskai isdelioti jas.

        processedDom.FileOpen();
        }
}