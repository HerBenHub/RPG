using Spectre.Console;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using MainProgram;
using JsonManager;
using RitkasagManager;
using System.Linq;

namespace ShopManager
{
    class Shop
    {
        public static void Merchant()
        {
            AnsiConsole.WriteLine("Találtál egy Árust.");
            AnsiConsole.WriteLine("");
            if (AnsiConsole.Confirm("Beszélsz vele!"))
            {
                AnsiConsole.MarkupLine("Mire van szükséged?");
                AnsiConsole.Clear();
                Tartalom();
            }
            else
            {
                AnsiConsole.MarkupLine("Tovább haladsz az utadon");
            }
        }
        
        
        public static string Tartalom()
        {
            Dictionary<string, FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();
            
            // tábla
            var table = new Table();

            table.AddColumn("Eszközök");
            table.AddColumn(new TableColumn("Típus").Centered());
            table.AddColumn(new TableColumn("Árak").Centered());

            Dictionary<string, string> ritkasagok = new Dictionary<string, string>()
            {
                {"common", "100"},
                {"uncommon", "120"},
                {"rare", "130"},
                {"epic", "140"},
                {"legendary", "150"}
                
            };
            
            List<FegyverAdatok> saveFegyver = new List<FegyverAdatok>();
            for (int i = 0; i < 5; i++)
            {
                FegyverAdatok fegyver = fegyverek[RitkasagSzamolo.LootGenerate(ritkasagok.Keys.ToList()[i])];
                saveFegyver.Add(fegyver);
                table.AddRow(fegyver.nev, fegyver.tipus, fegyver.ritkasag, ritkasagok[ritkasagok.Keys.ToList()[i]]);
            }
            
            List<PancelAdatok> savePancel = new List<PancelAdatok>();
            for (int i = 0; i < 5; i++)
            {
                PancelAdatok pancel = pancelok[RitkasagSzamolo.LootGenerate(ritkasagok.Keys.ToList()[i])];
                savePancel.Add(pancel);
                table.AddRow(pancel.nev, pancel.tipus, pancel.ritkasag, ritkasagok[ritkasagok.Keys.ToList()[i]]);
            }
            
            List<string> tempFegyver = new List<string>();
            List<string> tempPancel = new List<string>();

            foreach (FegyverAdatok fegyver in saveFegyver)
            {
                tempFegyver.Add(fegyver.nev);
            }
            foreach (PancelAdatok pancel in savePancel)
            {
                tempPancel.Add(pancel.nev);
            }
            
            
            // választás
            
            var fegyvalaszt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Válassz föl vagy le nyillal)[/]")
                    .AddChoices(tempFegyver)
                    .AddChoices(tempPancel)
                );
            
            AnsiConsole.WriteLine(fegyvalaszt);
            
            if (AnsiConsole.Confirm("Biztos akarod?"))
            {
                if (tempFegyver.Contains(fegyvalaszt))
                {
                    FegyverAdatok fegyver = saveFegyver[tempFegyver.IndexOf(fegyvalaszt)];
                    Program.Items.penz = Program.Items.penz - Int32.Parse(ritkasagok[fegyver.ritkasag]);
                }
                else if(tempPancel.Contains(fegyvalaszt))
                {
                    PancelAdatok pancel = savePancel[tempPancel.IndexOf(fegyvalaszt)];
                    Program.Items.penz = Program.Items.penz - Int32.Parse(ritkasagok[pancel.ritkasag]);
                }
                
                Console.WriteLine("Aktuális összeged: " + MainProgram.Program.Items.penz);
            }
            else
            {
                AnsiConsole.Clear();
                Tartalom();
            }
            return fegyvalaszt;
        }
        public void Eladas(Dictionary<string, string> ritkasagok, Dictionary<string, FegyverAdatok>? fegyverek, Dictionary<string, PancelAdatok> pancelok)
        {
            
            Console.WriteLine("Melyik elemet szeretnéd eladni?");
            var kivalaszt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Válassz föl vagy le nyillal)[/]")
                    .AddChoices(MainProgram.Program.Items.inventory));
            
            if (fegyverek.ContainsKey(kivalaszt))
            {
                Program.Items.penz += (int)Math.Round(Int32.Parse(ritkasagok[fegyverek[kivalaszt].ritkasag]) * 0.8);
            }
            else if(pancelok.ContainsKey(kivalaszt))
            {
                Program.Items.penz += (int)Math.Round(Int32.Parse(ritkasagok[pancelok[kivalaszt].ritkasag]) * 0.8);
            }
            
            Program.Items.inventory.Remove(kivalaszt);
            Console.WriteLine("Kiválasztott elem: " + kivalaszt + " eladva!");
        }
        public void  StartShop()
        {
            Merchant();
            Tartalom();

            if (Tartalom() != "Kilépés")
            {
                MainProgram.Program.Items.inventory.Add(Tartalom());
                Console.WriteLine("Megvásárolt eszköz hozzáadva az inventorydhoz!");
            }
        }
    }
}