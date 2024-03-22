using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using static MainProgram.Program;
using JsonManager;

namespace inventoryManager
{
    public class Items
    {
        Dictionary<string, FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
        Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();
        
        public static int penz = 0;
        public static List<string> inventory = new List<string>();
        public static void EquipSword(Hero? hero, string weapon, Dictionary<string, FegyverAdatok>? fegyverek)
        {
            if (fegyverek[weapon].tipus == hero.eredeti.name)
            {
                inventory.Add(hero.weapon);
                inventory.Remove(weapon);

                hero.weapon = weapon;
            }
                
        }
        public static void EquipArmor(Hero? hero, string armour, Dictionary<string, PancelAdatok>? pancelok)//armour file read!!
        {
    
            if (pancelok[armour].tipus == hero.eredeti.name)
            {

                inventory.Add(hero.armour);
                inventory.Remove(armour);
                    
                // visszaállítjuk az eredetire
                hero.eredeti.hp = (int)Math.Round(hero.eredeti.hp/pancelok[hero.armour].ertekek["hp"]);
                hero.eredeti.defense = (int)Math.Round(hero.eredeti.defense-pancelok[hero.armour].ertekek["defense"]);
                hero.eredeti.damage = (int)Math.Round(hero.eredeti.damage/pancelok[hero.armour].ertekek["damage"]);

                hero.armour = armour;

                // beleszámoljuk az új páncélt
                hero.eredeti.hp = (int)Math.Round(hero.eredeti.hp*pancelok[hero.armour].ertekek["hp"]);
                hero.eredeti.defense = (int)Math.Round(hero.eredeti.defense+pancelok[hero.armour].ertekek["defense"]);
                hero.eredeti.damage = (int)Math.Round(hero.eredeti.damage*pancelok[hero.armour].ertekek["damage"]);

                hero.hp = hero.eredeti.hp;
                hero.defense = hero.eredeti.defense;
                hero.damage = hero.eredeti.damage;
            }
        }
    }
    
    public class viewInventory
    {
        
        public static void Tarhely(List<Hero>? csapat)
        {
            Dictionary<string, FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();
            
            var tarolo = new Table();

            // Add columns to the table
            tarolo.AddColumn("[Red]Név[/]");
            tarolo.AddColumn("[grey50]Típus[/]");
            tarolo.AddColumn("[grey50]Leírás[/]");

            // Itemek
            foreach (var item in Items.inventory)
            {
                if (fegyverek.ContainsKey(item))
                {
                    tarolo.AddRow(item, fegyverek[item].tipus, fegyverek[item].szoveg);
                }
                if (pancelok.ContainsKey(item))
                {
                    tarolo.AddRow(item, pancelok[item].tipus);
                }
            }

            tarolo.Border = TableBorder.Rounded;
            
            var panelContent = new StringBuilder();

            foreach (var elem in csapat)
            {
                // Panel tartalma 
                panelContent.AppendLine($"\nNév: {elem.name}");
                panelContent.AppendLine($"Fegyver: {elem.weapon}");
                panelContent.AppendLine($"Páncél: {elem.armour}");
                
            }
            // A panel
            var jelenleg = new Panel("[Grey]Jelenlegi Itemek:[/]\n" + panelContent.ToString());
            jelenleg.Border = BoxBorder.Rounded;
            // Grid táblának + Panelnek
            var tabpan = new Grid()
                .AddColumn(new GridColumn().PadRight(2))
                .AddColumn(new GridColumn());

            // tábla + Panel a gridbe
            tabpan
                .AddRow(tarolo, jelenleg);
            AnsiConsole.Render(tabpan);
            
        }
        public static void Itemval(List<Hero>? heroes)
        {
            Dictionary<string, FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();
            
            var invval = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Válaszd ki a fegyvert a Tárhelyből")
                .PageSize(10)
                .HighlightStyle(new Style(new Color(0, 128, 0)))
                .AddChoices(Items.inventory)
                .AddChoices("[Red]Kilépés[/]"));
            
            if (invval != "[Red]Kilépés[/]")
            {
                //stat kiírása + leírás
                
                    var csere = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Ki akarod cserélni?")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "[Green]Igen[/]", "[Red]Nem[/]"
                        })) ;
                if (csere == "[Green]Igen[/]")
                {
                    var melyik = AnsiConsole.Prompt(
                        new SelectionPrompt<Hero>()
                            .Title("Melyik eszközödet cserélnéd?")
                            .PageSize(10)
                            .HighlightStyle(new Style(new Color(0, 128, 0)))
                            .AddChoices(heroes));
                    
                    foreach (var item in Items.inventory)
                    {
                        if (fegyverek.ContainsKey(item))
                        {
                            Items.EquipSword(melyik, invval, fegyverek);
                        }
                        if (pancelok.ContainsKey(item))
                        {
                            Items.EquipArmor(melyik, invval, pancelok);

                        }
                    }
                }
                else if (csere == "[Red]Nem[/]")
                {
                    AnsiConsole.Console.Clear();
                    Tarhely(MainProgram.Program.csapat);
                    Itemval(heroes);
                }
            }
        }
    }
}
