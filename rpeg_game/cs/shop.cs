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
using InventoryManager;

namespace ShopManager
{
    class Shop
    {
        public static void Merchant()
        {
            AnsiConsole.WriteLine("Összetalálkoztál egy árússal.");

            var arus = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Beszélsz vele?")
                .PageSize(10)
                .AddChoices(["[green]Igen[/]","[red]Nem[/]"]));
            if (arus == "[green]Igen[/]")
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Spectre.Console.Rule("Mire van szükséged?"));
                Tartalom();
            }
        }
        
        
        public static string Tartalom()
        {
            Dictionary<string, FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();
            
            // tábla
            var table = new Table();

            table.AddColumn("Eszközök");
            table.AddColumn(new TableColumn("Típus"));
            table.AddColumn(new TableColumn("Ritkaság"));
            table.AddColumn(new TableColumn("Árak").Centered());

            Dictionary<string, string> ritkasagok = new Dictionary<string, string>()
            {
                {"common", "100"},
                {"uncommon", "120"},
                {"rare", "130"},
                {"epic", "140"},
                {"legendary", "150"}
            };

            ////////////////////////////////////////////////////////////////////////
            
            string? currentRarity = null;
            Random rand = new Random();

            List<FegyverAdatok> saveFegyver = new List<FegyverAdatok>();
            for (int i = 0; i < 3; i++)
            {
                currentRarity = ritkasagok.Keys.ToList()[rand.Next(0,4)];
                FegyverAdatok fegyver = fegyverek[RitkasagSzamolo.LootGenerate(currentRarity,1)];
                saveFegyver.Add(fegyver);
                table.AddRow(fegyver.nev, fegyver.tipus, fegyver.ritkasag, ritkasagok[currentRarity]);
            }
            
            List<PancelAdatok> savePancel = new List<PancelAdatok>();
            for (int i = 0; i < 3; i++)
            {
                currentRarity = ritkasagok.Keys.ToList()[rand.Next(0,4)];
                PancelAdatok pancel = pancelok[RitkasagSzamolo.LootGenerate(currentRarity,2)];
                savePancel.Add(pancel);
                table.AddRow(pancel.nev, pancel.tipus, pancel.ritkasag, ritkasagok[currentRarity]);
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

            AnsiConsole.Write("\n");
            AnsiConsole.Write(table);
            
            // választás
            
            var fegyvalaszt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Válassz föl vagy le nyillal)[/]")
                    .AddChoices(tempFegyver)
                    .AddChoices(tempPancel)
                    .AddChoices("[red]Kilépés[/]")
                );
            
            AnsiConsole.WriteLine(fegyvalaszt);
            
            if (AnsiConsole.Confirm("Biztos akarod?"))
            {
                if (tempFegyver.Contains(fegyvalaszt))
                {
                    FegyverAdatok fegyver = saveFegyver[tempFegyver.IndexOf(fegyvalaszt)];
                    Items.penz = Items.penz - Int32.Parse(ritkasagok[fegyver.ritkasag]);
                }
                else if(tempPancel.Contains(fegyvalaszt))
                {
                    PancelAdatok pancel = savePancel[tempPancel.IndexOf(fegyvalaszt)];
                    Items.penz = Items.penz - Int32.Parse(ritkasagok[pancel.ritkasag]);
                }
                
                Console.WriteLine("Aktuális összeged: " + Items.penz);
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
                    .AddChoices(Items.inventory));
            
            if (fegyverek.ContainsKey(kivalaszt))
            {
                Items.penz += (int)Math.Round(Int32.Parse(ritkasagok[fegyverek[kivalaszt].ritkasag]) * 0.8);
            }
            else if(pancelok.ContainsKey(kivalaszt))
            {
                Items.penz += (int)Math.Round(Int32.Parse(ritkasagok[pancelok[kivalaszt].ritkasag]) * 0.8);
            }
            
            Items.inventory.Remove(kivalaszt);
            Console.WriteLine("Kiválasztott elem: " + kivalaszt + " eladva!");
        }
        public void  StartShop()
        {
            Merchant();
            Tartalom();

            if (Tartalom() != "Kilépés")
            {
                Items.inventory.Add(Tartalom());
                Console.WriteLine("Megvásárolt eszköz hozzáadva az inventorydhoz!");
            }
        }
    }
}
//Hivatkozás!! 59, 67