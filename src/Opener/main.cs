
using System;
using Path;
using Read;
using System.Runtime.Versioning;
using ProcessStarter;

[SupportedOSPlatform("windows")]
class MainLauncher
{
    public static void Main()
    {
        Browsers browserPaths = new Browsers();
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
    

        
        }
}