using Microsoft.Playwright; // Playwright API naršyklių automatizavimui (Chromium, Firefox, WebKit ir kt.)
using Read; // Įtraukiame "Read" vardų sritį, kad galėtume naudoti Opener klasę iš kito failo.
using System.Runtime.Versioning; // Leidžia nurodyti platformos suderinamumo atributus (pvz., tik Windows).
using System.Reflection; // Naudojama gauti informaciją apie vykdomą surinktį (assembly), pvz., jos kelią diske. naudinga naudoti sita nes randa musu txt faila su domenais

namespace Emulator
{
    // Šis atributas nurodo, kad klasė palaikoma tik Windows sistemoje.
    // Tai naudinga daugia-platforminiams projektams, kad būtų išvengta neteisingo kompiliavimo ar įspėjimų.
    [SupportedOSPlatform("windows")]
    public class PlaywrightEmulator
    {
        // Saugo domenų sąrašą, gautą iš Opener klasės.
        // Domenai bus panaudoti naršyklės automatizavimui.
        private List<string> _domains;

        // Konstruktorius: Vykdomas automatiškai, kai sukuriamas naujas PlaywrightEmulator objektas.
        // Čia nustatome Playwright aplinką ir užkrauname domenų sąrašą.
        public PlaywrightEmulator()
        {
            // 1. Gauname katalogą, kuriame yra vykdomasis failas.
            //    Tai leidžia dirbti su reliatyviais keliais, neįrašant jų ranka.
            var assemblyLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // 2. Sukuriame kelią iki "ms-playwright" aplanko, kuriame saugomi naršyklių failai.
            //    Playwright turi žinoti, kur rasti savo naršyklių binarinius failus.
            var browserBinariesPath = System.IO.Path.Combine(assemblyLocation, "ms-playwright");

            // 3. Atspausdiname kelią į konsolę (debug tikslams).
            //    Jei naršyklės failai nerandami, tai padeda diagnozuoti problemą.
            Console.WriteLine("********* " + browserBinariesPath + " *********");

            // 4. Nustatome aplinkos kintamąjį PLAYWRIGHT_BROWSERS_PATH.
            //    Tai būtina, jei Playwright vykdomas aplinkoje, kur naršyklės nėra įdiegtos numatytame kelyje.
            Environment.SetEnvironmentVariable("PLAYWRIGHT_BROWSERS_PATH", browserBinariesPath);

            // 5. Sukuriame Opener klasės egzempliorių.
            //    Ji atsakinga už domenų nuskaitymą ir filtravimą.
            Opener domainRetrieval = new Opener();

            // 6. Iškviečiame RegexMatcher(), kad gautume suformatuotą domenų sąrašą.
            _domains = domainRetrieval.RegexMatcher();
        }

        // Paprastas metodas domenų sąrašui gauti.
        // Naudinga, jei kitos programos dalys turi prie jo prieigą.
        public List<string> domainRetrieval()
        {
            List<string> doms = _domains;
            return doms;
        }

        // Pagrindinis metodas vienam domenui patikrinti su Playwright.
        // Vykdo asinchroniškai: paleidžia Chromium naršyklę, atidaro puslapį ir nueina į nurodytą adresą.
        public async Task EmulatorAsync(string domain)
        {
            // 1. Sukuriame Playwright aplinkos egzempliorių.
            using var playwright = await Playwright.CreateAsync();

            // 2. Paleidžiame Chromium naršyklę „headless“ režimu (be grafikinio lango).
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });

            // 3. Sukuriame naują naršyklės kontekstą (atskira sesija, be bendrinamų slapukų ar talpyklos).
            var context = await browser.NewContextAsync();

            // 4. Atidarome naują skirtuką (puslapį) tame kontekste.
            var page = await context.NewPageAsync();

            // 5. Einame į nurodytą domeną.
            //    Timeout = 5000 reiškia, kad lauksime max 5 sekundes.
            //    Jei puslapis neatsidaro per tą laiką, galima apdoroti klaidą per try/catch.
            await page.GotoAsync($"{domain}", new() { Timeout = 5000 });
        }
    }
}
