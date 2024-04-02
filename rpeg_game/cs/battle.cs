using System.Text.Json;
using JsonManager;
using RitkasagManager;
using DamageManager;
using static MainProgram.Program;
using Spectre.Console;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AIController;

namespace BattleManager
{
    public class Battles
    {   
        ////////////////////////////////////////////////////////////////
        // Alap cuccok
        static readonly Dictionary<string,VarazslatAdatok>? varazslatok = JsonOlvaso.VarazslatLista();
        static readonly Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
        static Dictionary<string, Action<Characters,Characters>> Interact = [];
        public static List<Characters?> CharactersInBattle = new List<Characters?>();
        static int turn = 1;

        public static Characters? currentAttacker = null;
        public static Characters? targetedCharacter = null;



        //    ___   ___     _     _____     _       ___   _   _    ___    ___  __   __   __  _  _  __   __
        //   / __| / __|   /_\   |_   _|   /_\     | __| (_) (_)  / __|  / __| \ \ / /  /_/ | \| | \ \ / /
        //  | (__  \__ \  / _ \    | |    / _ \    | _|  | |_| | | (_ | | (_ |  \ V /  | -< | .` |  \ V / 
        //  \___|  |___/ /_/ \_\   |_|   /_/ \_\   |_|   \___/   \___|  \___|   \_/   |__< |_|\_|   |_|                                                                                                                                                                     
        //                                                      
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool StartBattle(Hero[] Heroes, Enemy[] Enemies)
        {
            // karakterek beillesztése
            foreach (Hero hero in Heroes)
            { CharactersInBattle.Add(hero); }
            foreach (Enemy enemy in Enemies)
            { CharactersInBattle.Add(enemy); }

            // karakterek sorba rakása priority szerint
            CharactersInBattle.Sort(delegate(Characters c1, Characters c2) { return c1.priority.CompareTo(c2.priority); });
            // a .Sort felfele növekvő sorrenbe van, viszont a kódban a magasabb szám az előbbi, így meg kell fordítani a sort
            CharactersInBattle.Reverse();

            currentAttacker = CharactersInBattle[0];
            targetedCharacter = Enemies[0];
            



            void Action()
            {                
                var action = new SelectionPrompt<string>()
                        .Title("[cyan3]Mit akarsz csinálni?[/]")   
                        .PageSize(5)
                        .HighlightStyle(new Style(new Color(255,100,100),null,Decoration.Italic));

                //Interact.Select(kvp => kvp.Key).ToList()

                action.AddChoice($"támadás ({fegyverek[currentAttacker.weapon].hasznalat})");

                if (currentAttacker.eredeti.name == "Sorcerer")
                { action.AddChoice("mágia használata"); }

                action.AddChoice("kör lezárása");

                string actionPrompt = AnsiConsole.Prompt(action);

                string asdconverter(Characters c)
                {
                    return c.name;
                }

                if (actionPrompt == "kör lezárása")
                { EndTurn(); }
                else
                {
                    SelectionPrompt<Characters> celpontPrompt = new SelectionPrompt<Characters>()
                            .Title("[cyan3]Kit célozol meg?[/]")
                            .PageSize(10)
                            .HighlightStyle(new Style(new Color(150,150,100),null,Decoration.Italic))
                            .MoreChoicesText("[lightcyan3](Menj lejjebb!)[/]")
                            .UseConverter(new Func<Characters, string>(asdconverter));


                    foreach (Hero hero in CharactersInBattle.OfType<Hero>())
                    {
                        celpontPrompt.AddChoice(hero); 
                    }
                    foreach (Enemy enemy in CharactersInBattle.OfType<Enemy>())
                    {
                        celpontPrompt.AddChoice(enemy); 
                    }

                    // celpont.AddChoices("[red]<---[/]");

                    Characters celpont = AnsiConsole.Prompt(celpontPrompt);
                    
                    if (actionPrompt == "mágia használata" && currentAttacker.eredeti.name == "Sorcerer")
                    {
                        CastSpell(currentAttacker, celpont);
                    }
                    else
                    {
                        Attack(currentAttacker, celpont);
                    }


                    // if (celpontPrompt != "[red]<---[/]")
                    // {
                    //     targetedCharacter = NameSync[celpontPrompt];
                    //     if (actionPrompt == "mágia használata" && currentAttacker.eredeti.name == "Sorcerer")
                    //     {
                    //         CastSpell(currentAttacker, targetedCharacter);
                    //     }
                    //     else
                    //     {
                    //         Attack(currentAttacker, targetedCharacter);
                    //     }
                    // }
                }
            }

            
            // Interact["sima támadás"] = Attack;
            // Interact["mozgás"] = move;



            // main csata code
            while (true)
            {
                if (CharactersInBattle.Where(s=>s!=null && s.GetType()==typeof(Enemy)).Count() <= 0)
                {
                    foreach (Hero chr in CharactersInBattle.OfType<Hero>())
                    {
                        chr.defense = chr.eredeti.defense;
                        chr.damage = chr.eredeti.damage;
                        chr.hp = chr.eredeti.hp;
                        chr.points = chr.eredeti.points;
                    }
                    AnsiConsole.Clear();
                    AnsiConsole.Write(new Rule("MEGNYERTED A CSATÁT!").RuleStyle("yellow"));
                    return false;
                }
                if (CharactersInBattle.Where(s=>s!=null && s.GetType()==typeof(Hero)).Count() <= 0)
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Write(new Rule("ELVESZTETTED A CSATÁT!").RuleStyle("red"));
                    return true;
                }


                Alapok();
                if (currentAttacker.GetType() == typeof(Hero))
                {
                    Action();
                }
                else
                {
                    AI.StartAI();
                }
                
            };

        }                                                     



        //    ___   _   _   _  _   _  __   ___   ___     __   _  __
        //   | __| | | | | | \| | | |/ /  / __| |_ _|  _/_/  | |/ /
        //   | _|  | |_| | | .` | | ' <  | (__   | |  / __ \ | ' < 
        //   |_|    \___/  |_|\_| |_|\_\  \___| |___| \____/ |_|\_\                                                                                                
        //                                                      
        ////////////////////////////////////////////////////////////////
        // Támadások
        public static void Attack(Characters Attacker, Characters Target)
        {
            if (Attacker.GetType() == typeof(Hero))
            {
                if (Attacker.points >= fegyverek[Attacker.weapon].hasznalat)
                {
                    Target.hp -= DamageCalculator.BasicAttack(Attacker,Target);
                    if (Target.hp <= 0) { CharactersInBattle.Remove(Target); }
                }
                else
                {
                    AnsiConsole.Write(new Text("Nincs elég AP-d!", new Style( 	new Color(200,10,10))));
                    System.Console.ReadLine();
                }
            }
            if (Attacker.GetType() == typeof(Enemy))
            {
                if (Attacker.points >= 10)
                {
                    Target.hp -= DamageCalculator.BasicAttack(Attacker,Target);
                    if (Target.hp <= 0) { CharactersInBattle.Remove(Target); }
                }
            }
        }

        static void HandleSpell(Characters Target, VarazslatAdatok Varazs)
        {
            foreach (KeyValuePair<string, double> hatas in Varazs.hatas)
            {
                if (hatas.Key == "defense"){Target.defense = (int)Math.Round(Target.defense + hatas.Value);}
                if (hatas.Key == "damage"){Target.damage = (int)Math.Round(Target.damage * hatas.Value);}
                if (hatas.Key == "hp"){Target.hp = (int)Math.Round(Target.hp + (Target.eredeti.hp * hatas.Value));}
                if (hatas.Key == "canFly"){Target.canFly = true;}
            }
        }
        public static void CastSpell(Characters Attacker, Characters Target, string? varazslatnev = null)
        {
            if (Attacker.GetType() == typeof(Hero))
            {
                Dictionary<string,string> spellNameSync = new Dictionary<string,string>();
                SelectionPrompt<string> celpont_spell = new SelectionPrompt<string>()
                    .Title("Melyik varázst használod fel?")
                    .PageSize(10)
                    .HighlightStyle(new Style(new Color(150,100,150),null,Decoration.Italic))
                    .MoreChoicesText("[lightcyan3](Menj lejjebb!)[/]");
                foreach (var varazslat in varazslatok)
                {
                    spellNameSync.Add(varazslat.Value.nev, varazslat.Key);
                    celpont_spell.AddChoice($"{varazslat.Value.nev} ({varazslat.Value.hasznalat}) - {varazslat.Value.leiras}");
                }
                VarazslatAdatok varazs = varazslatok[spellNameSync[AnsiConsole.Prompt(celpont_spell).Split(" (")[0]]];
                if (Attacker.points >= varazs.hasznalat)
                {
                    Attacker.points -= varazs.hasznalat;
                    HandleSpell(Target,varazs);
                }
                else
                {
                    AnsiConsole.Write(new Text("Nincs elég AP-d!", new Style( 	new Color(200,10,10))));
                    System.Console.ReadLine();
                }
            }
            // if type Enemy
            if (Attacker.GetType() == typeof(Enemy))
            {
                if (varazslatnev != null)
                {
                    HandleSpell(Target,varazslatok[varazslatnev]);
                }
            }
        }   


         ///////////////////////////////////////////////////////////////
        // Csata funkcik //////////////////////////////////////////////
        public static void EndTurn()
        {
            currentAttacker.points += currentAttacker.eredeti.points / 2;
            currentAttacker = CharactersInBattle[turn % CharactersInBattle.Count];
            turn += 1;
        }

        public static void Alapok()
        {
            AnsiConsole.Clear();
            var rule = new Rule("Csata Folymatban!");   
            AnsiConsole.Write(rule);

            // irja ki a baratokat
            AnsiConsole.WriteLine("Társak a csatában:");
            foreach (Hero hero in CharactersInBattle.OfType<Hero>())
            {
                if (hero == currentAttacker)
                {AnsiConsole.Write(new Text($"Ω {hero.name} ({hero.eredeti.name})\n   HP:[{hero.hp}/{hero.eredeti.hp}]  AP:[{hero.points}]\n", new Style( 	new Color(215, 0, 255))));}
                else
                {AnsiConsole.Write(new Text($"Ω {hero.name} ({hero.eredeti.name})\n   HP:[{hero.hp}/{hero.eredeti.hp}]  AP:[{hero.points}]\n", new Style( 	new Color(0,215,95))));}
            }

            // irja ki az ellensegeket
            AnsiConsole.WriteLine("\nEllenségek a csatában:");
            foreach (Enemy enemy in CharactersInBattle.OfType<Enemy>())
            {
                if (enemy == currentAttacker)
                {AnsiConsole.Write(new Text($"ɸ {enemy.name}\n   HP:[{enemy.hp}/{enemy.eredeti.hp}]  AP:[{enemy.points}]\n", new Style(new Color(215, 0, 255))));}
                else
                {AnsiConsole.Write(new Text($"ɸ {enemy.name}\n   HP:[{enemy.hp}/{enemy.eredeti.hp}]  AP:[{enemy.points}]\n", new Style(new Color(255,135,135))));}
            }
            AnsiConsole.Write(new Rule($"{turn}. kör"));
        }
    }
}
