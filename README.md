Autorius: Vytautas Pakalka;
įmonė: RRT;

Programos aprašymas
*****************
Ši programa yra įrankis, skirtas automatiškai patikrinti, ar interneto domenai (svetainių adresai) yra pasiekiami. Ji imituoja vartotojo bandymą pasiekti svetainę ir patikrina serverio atsaką. Programa veikia automatiškai, kai tik yra tinkamai paruošta.
Paleidimas ir naudojimas

Reikalavimai: Kad programa veiktų, aplankas ms-playwright, kuriame yra naršyklių failai, turi būti toje pačioje direktorijoje kaip ir paleidžiamasis failas Opener.exe.
Domenų sąrašas: Pirmą kartą paleidus programą, ji automatiškai sukurs failą pavadinimu domenai.txt (jei jo dar nėra).

Naudojimas: Į šį domenai.txt failą reikia įrašyti norimų patikrinti domenų adresus. Kiekvienas adresas turi būti
naujoje eilutėje ir pateiktas pilnu formatu, pavyzdžiui: "https://pavyzdys.lt.xyz" 
Programos struktūra
*****************
Programą sudaro trys pagrindiniai failai, kurių kiekvienas atlieka savo funkciją:
Opener.cs: Šis failas atsakingas už pradinę programos konfigūraciją. Jis patikrina, ar egzistuoja domenai.txt failas, jį sukuria, jei reikia, ir nuskaito jame esančius domenus.
emulator.cs: Tai yra pagrindinis tikrinimo modulis. Jis naudoja ms-playwright aplanke esančią „Chromium“ naršyklės versiją (be grafinės sąsajos), kad prisijungtų prie kiekvieno domeno. Šiame faile taip pat galima keisti serverio laukimo laiką (angl. timeout), jei interneto ryšys yra lėtas.
main.cs: Šis failas valdo visą programos eigą nuo domenų nuskaitymo iki tikrinimo proceso paleidimo ir rezultatų pateikimo.

Apribojimai ir svarbūs aspektai
*****************

Naršyklės plėtiniai: Programa tikrina tik tiesioginį serverio atsaką. Ji neaptinka naršyklės plėtinių (angl. extensions), kurie gali blokuoti turinį. Pavyzdžiui,
jei mokymo įstaigos kompiuteriuose yra įdiegti turinį filtruojantys plėtiniai, programa jų nematys ir gali rodyti, kad domenas pasiekiamas, nors realiai vartotojui jis bus
blokuojamas per naršyklę. Taip yra todėl, kad įrankis naudoja savo, „švarią“ naršyklės versiją.

Interneto greitis: Jei programa naudojama tinkle su lėtu interneto ryšiu (pvz., viešosiose įstaigose, bibliotekose), BŪTINA PRAILGINTI DOMENO ATSAKYMO LAUKIMO LAIKĄ.
Priešingu atveju programa gali per anksti nustoti laukti serverio atsako ir klaidingai pranešti, kad domenas nepasiekiamas. Šį nustatymą galima keisti emulator.cs faile, linijoje 72.

Tolesnis vystymas
*****************

Norintiems tobulinti ar modifikuoti šią programą, rekomenduojama atsižvelgti į kelis patarimus:

Naršyklių valdymas: Didžiausi iššūkiai vystant panašaus pobūdžio programas dažniausiai kyla dėl naršyklių integravimo ir konfigūravimo. Prieš atliekant pakeitimus,
labai svarbu susipažinti su oficialia Playwright bibliotekos dokumentacija.

Kodo struktūra: Siekiant išlaikyti tvarką ir aiškumą, patartina kodą skirstyti į atskirus, logiškai susijusius failus (kaip padaryta dabar). Tai palengvina kodo supratimą ir priežiūrą.

Naršyklių naudojimas: Vystymui naudokite tik tas naršykles, kurios yra ms-playwright aplanke (pvz., Chromium su grafine sąsaja arba be jos, Firefox).
Nenaudokite sistemoje įdiegtų ar kitų naršyklių, nes programa pritaikyta veikti būtent su Playwright teikiamomis versijomis.

Kompiliavimas ir paruošimas
Ši dalis skirta paaiškinti, kaip iš programos kodo sukurti veikiančią programą (ją kompiliuoti).
1. Pakeitimų išsaugojimas
Prieš kompiliuojant programą, itin svarbu įsitikinti, kad visi atlikti pakeitimai koduose yra išsaugoti. Jei pakeitimai nebus išsaugoti, kompiliatorius naudos senąją failo versiją, ir jūsų atlikti pakeitimai neatsispindės galutinėje programoje.
Pakeitimus galite išsaugoti vienu iš šių būdų:
Paspaudę klavišų kombinaciją Ctrl + S.
Dešiniuoju pelės mygtuku spustelėję ant redaguojamo failo ir pasirinkę parinktį Išsaugoti (Save).
2. Programos kompiliavimas
Programa kompiliuojama naudojant dotnet publish komandą, kuri paruošia programą paleidimui. Rekomenduojame naudoti komandą, kuri sukurs savarankišką (angl. self-contained) programos versiją, veiksiančią 64 bitų „Windows“ operacinėse sistemose.
Rekomenduojama komanda
Atidarykite terminalą (komandinę eilutę) programos pagrindiniame aplanke ir įvykdykite šią komandą:
code
Bash
dotnet publish -r win-x64 --self-contained true
Komandos paaiškinimas
dotnet publish: Pagrindinė komanda, kuri pradeda kompiliavimo ir publikavimo procesą.
-r win-x64: Nurodo tikslinę platformą (-r yra --runtime trumpinys). Šiuo atveju tai yra win-x64, reiškianti 64 bitų „Windows“ versiją, kuri yra populiariausia tarp šiuolaikinių kompiuterių.
--self-contained true: Vienas svarbiausių parametrų. Jis nurodo, kad kartu su programa į galutinį paketą būtų įtraukta ir visa .NET vykdymo aplinka (angl. runtime). Tai reiškia, kad programa veiks net tuose kompiuteriuose, kuriuose nėra specialiai įdiegtas .NET.
3. Rezultatas
Sėkmingai įvykdžius šią komandą, bin/Release/netX.X/win-x64/publish (ar panašiame) aplanke rasite paruoštą programą su visais reikalingais failais, įskaitant .exe failą, kurį galima paleisti.