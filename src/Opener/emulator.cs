using Microsoft.Playwright;
using System;
using Read;
using System.Runtime.Versioning;
using System.Threading.Tasks;



namespace Emulator
{
    [SupportedOSPlatform("windows")]
    public class PlaywrightEmulator
    {
        private List<string> _domains;
        public PlaywrightEmulator()
        {
            Opener domainRetrieval = new Opener();

            _domains = domainRetrieval.FileOpen();
        }

        public string domainList(int index)
        {
            string val = _domains[index];
            return val;
        }


        public async Task EmulatorAsync()
        {
            Int32 length = _domains.Count;
            Console.WriteLine(length);
            
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://google.com");
            Console.WriteLine(await page.TitleAsync());
        }


    }

}