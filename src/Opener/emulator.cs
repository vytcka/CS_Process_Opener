using Microsoft.Playwright; 
using Read; 
using System.Runtime.Versioning; 
using System.Reflection; 

namespace Emulator
{


    [SupportedOSPlatform("windows")]
    public class PlaywrightEmulator
    {
        // Saugo domenų sąrašą, gautą iš Opener klasės.
        // Domenai bus panaudoti naršyklės automatizavimui.
        private List<string> _domains;

        // Konstruktorius: Vykdomas automatiškai, kai sukuriamas naujas PlaywrightEmulator objektas.
      
        public PlaywrightEmulator()
        {

            var assemblyLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


            var browserBinariesPath = System.IO.Path.Combine(assemblyLocation, "ms-playwright");


            Console.WriteLine("********* " + browserBinariesPath + " *********");


            Environment.SetEnvironmentVariable("PLAYWRIGHT_BROWSERS_PATH", browserBinariesPath);

  
            Opener domainRetrieval = new Opener();


            _domains = domainRetrieval.RegexMatcher();
        }

        public List<string> domainRetrieval()
        {
            List<string> doms = _domains;
            return doms;
        }

       
        public async Task EmulatorAsync(string domain)
        {
   
            using var playwright = await Playwright.CreateAsync();

    
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });

     
            var context = await browser.NewContextAsync();

          
            var page = await context.NewPageAsync();

            await page.GotoAsync($"{domain}", new() { Timeout = 5000 });
        }
    }
}
