using Microsoft.Playwright;
using System;
using Read;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace Emulator
{
    [SupportedOSPlatform("windows")]
    public class PlaywrightEmulator
    {
        private List<string> _domains;
        public PlaywrightEmulator()
        {
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
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true});
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync($"{domain}", new() {Timeout = 9500}); 
        }


    }

}