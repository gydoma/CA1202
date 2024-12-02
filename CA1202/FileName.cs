namespace CS241111;

internal class Fish
{
    private float weight;
    private bool weightIsSet = false;
    private bool predator;
    private int top;
    private int depth;
    private string species;

    public float Weight
    {
        get => weight; set
        {
            if (value < .5f || value > 40f) throw new Exception("Nem megfelelő hal súlyt adtál meg!");
            if (weightIsSet && (value > weight * 1.1 || value < weight * .9)) throw new Exception("A hal súlya nem változhat ennyit!");

            weight = value;
            weightIsSet = true;
        }
    }
    public bool Predator { get; private set; }
    public int Top
    {
        get => top;
        set
        {
            if (value < 0 || value > 400) throw new Exception("Nem megfelelő értéket adtál meg a topnak!");
            top = value;
        }
    }
    public int Depth
    {
        get => depth;
        set
        {
            if (value < 10 || value > 400) throw new Exception("Nem megfelelő értéket adtál meg a mélységnek!");
            depth = value;
        }
    }
    public string Species
    {
        get => species; set
        {
            if (value is null) throw new Exception("Nem adtál meg fajt!");
            if (value.Length < 3 || value.Length > 30) throw new Exception("Nem megfelelő hosszúságu a faj!");
            species = value;
        }
    }

    public Fish(float weight, bool predator, int top, int depth, string species)
    {
        Weight = weight;
        Predator = predator;
        Top = top;
        Depth = depth;
        Species = species;
    }
}


/*
 Ragadozó és növényevő halak

Egy halat (fish) jellemez, hogy a vízben milyen mélységben szeret úszni. Megadjuk ezen mélység-intervallum (tól-ig) felső határát (top) és mélységét (depth). Pl. ha top=30, depth=80, akkor a hal nem úszik 30 centinél közelebb a vízfelszínhez, és 1.1m (30cm+80cm=110cm) mélység alá sem úszik. Minden halnak van súlya, és tároljuk hogy ragadozó-e vagy növényevő.

    weight: súlya, tört érték, 0.5 kilótól 40.0 kilóig valid, a hal súlya növekedhet és csökkenhet is, de egyszerre maximum az aktuális halsúly 10% -val
    predator: ragadozó-e (vagy növényevő), bool, true/false, ha egyszer beállítottuk, nem módosítható
    top: hány cm mélység fölé nem merészkedik, egész, 0cm-től 400cm-ig valid,
    depth: hány cm a mozgási sávjának mélysége, egész, 10cm-től 400cm-ig valid
    a halfaj (busa, keszeg, stb) string formában kerül tárolásra (most), a hal fajának neve nem lehet null, legalább 3 betű, maximum 30 betű hosszú lehet.

Készítsünk egy listába

    100 db halat,
    90% eséllyel növényevőt,
    különböző random súllyal és úszási mélységekkel, halfajjal,
    írassuk ki a 100 halból hány darab lett végül is a ragadozó, mennyi a növényevő,
    mennyi a legnagyobb súlyú hal súlya,
    illetve számoljuk meg hány hal képes 1.1m mélységben úszni.
    válasszunk random 2 halat, ha az egyik növényevő, a másik ragadozó, és az ragadozó be tud úszni a növényevő sávjába, akkor 30% eséllyel megeszi azt. Ekkor a ragadozó hal súlya nőjön meg, és töröljük a listából a növényevőt (100 ismétlés).
    a végén írassuk ki a 100 halból hány darab és hány kilónyi növényevő halat ettek meg a ragadozók.

HF: Pálinka kezelése

Egy pálinkáz jellemez:

    alkoholfoka: egész szám (30-87)
    gyümölcs: szilva, barack, dió, körte, stb, nem lehet null, minimum 3 betű, maximum 20 betű
    mennyiség: egész szám, 0-50, deciliterben
    készítés éve: egész szám, 2000 .. aktuális év (DateTime.Now.Year adja meg az aktuális évet))
    ára: 50-1000 ft/deciliter

Készítsünk főprogramot:

    pálinkák listáját készíti el, 20 db pálinka, random adatokkal,
    meghatározza az össz pálinka mennyiséget (átszámolja literbe),
    random választ egy pálinkt a listáról, és megisszuk a felét, a bevételt számoljuk, 50 ismétlés,
    kiírjuk mennyi bevétel jött össze.

 */

//namespace CA24112503;

//internal class Auto
//{
//    private List<Ember> tulajdonosok = [];

//    public string Rendszam { get; set; }
//    public string Gyarto { get; set; }
//    public string Modell { get; set; }
//    public int Teljesitmeny { get; set; }
//    public float Fogyasztas { get; set; }

//    public void TulajokRogzitese(params string[] tulajdonosok)
//    {
//        foreach (var tulaj in tulajdonosok)
//        {
//            this.tulajdonosok.Add(new Ember(tulaj));
//        }
//    }

//    public string TulajdonosokListaja =>
//        string.Join('\n', this.tulajdonosok);

//    public override string ToString() =>
//        $"[{Rendszam}] {Gyarto} {Modell} ({Teljesitmeny} hp, {Fogyasztas} l/100km)";

//    //DRY = Don't Repeat Yourself!

//    /// <summary>
//    /// részletesen meg kell adni azt autó adatait
//    /// </summary>
//    /// <param name="rendszam">az autó rendszáma</param>
//    /// <param name="gyarto">az autót gyártó cég</param>
//    /// <param name="modell">az autómodell megnevezése</param>
//    /// <param name="teljesitmeny">a teljesítmény lóerőben</param>
//    /// <param name="fogyasztas">fogyasztás lirben 100km-en</param>
//    public Auto(string rendszam, string gyarto, string modell, int teljesitmeny, float fogyasztas, params string[] tulajok)
//    {
//        Rendszam = rendszam;
//        Gyarto = gyarto;
//        Modell = modell;
//        Teljesitmeny = teljesitmeny;
//        Fogyasztas = fogyasztas;

//        foreach (var tulaj in tulajok)
//        {
//            this.tulajdonosok.Add(new Ember(tulaj));
//        }
//    }


//    /// <summary>
//    /// Ez a konstruktor egy 250hp-s, 6.7-es fogyasztású Ford Focust fog inicializálni. 
//    /// </summary>
//    /// <param name="rendszam">az autó rendszáma</param>
//    public Auto(string rendszam) : this(rendszam, "Ford", "Focus", 250, 6.7F) { }

//    public Auto(int randomSeed)
//    {
//        Random rnd = new(randomSeed);
//        Rendszam = $"{(char)rnd.Next(65, 91)}{(char)rnd.Next(65, 91)}{(char)rnd.Next(65, 91)}-{rnd.Next(100, 1000)}";

//        string[] gyartok = ["Ford", "Peugeto", "Audi"];
//        Gyarto = gyartok[rnd.Next(gyartok.Length)];

//        string[] modellek = ["Bubu", "Ruru", "Bika"];
//        Modell = modellek[rnd.Next(modellek.Length)];

//        Teljesitmeny = rnd.Next(80, 300);

//        Fogyasztas = rnd.Next(50, 100) / 10F;

//    }
//}