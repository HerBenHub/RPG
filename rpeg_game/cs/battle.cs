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

namespace BattleManager
{
    
    public class Battles
    {   
        public static bool StartBattle(Hero[] Heroes, Enemy[] Enemies)
        {
            ////////////////////////////////////////////////////////////////
            // Alap cuccok

            Dictionary<string,VarazslatAdatok>? varazslatok = JsonOlvaso.VarazslatLista();
            Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            Dictionary<string, Action<Hero,Characters>> Interact = [];

            Hero currentHero = Heroes[0];
            Characters targetedCharacter = Enemies[0];

            void Action()
            {
                
                
                var actionPrompt = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[cyan3]Mit akarsz csinálni?[/]")   
                        .PageSize(5)
                        .HighlightStyle(new Style(new Color(255,100,100),null,Decoration.Italic))
                        .AddChoices(Interact.Select(kvp => kvp.Key).ToList()));
                
                
                
                // System.Console.WriteLine("\nChoose character");
                // Int32 chosenCharacter = Int32.Parse(System.Console.ReadLine());
                // chosenCharacter = Math.Clamp(chosenCharacter,0,Heroes.Length);
                // currentHero = Heroes[chosenCharacter-1];
                // System.Console.WriteLine("Selected character: " + currentHero.name);

                Dictionary<string,Characters> heroNameSync = new Dictionary<string,Characters>();
                // Hero tipus szinkronizálása a SelectionPrompt stringjével
                
                SelectionPrompt<string> selpro = new SelectionPrompt<string>()
                        .Title("Kit támadsz meg?")
                        .PageSize(3)
                        .MoreChoicesText("[grey](Menj lejjebb!)[/]");

                foreach (Hero hero in Heroes)
                {
                    heroNameSync.Add(hero.name,hero);
                    selpro.AddChoice(hero.name); 
                }
                foreach (Enemy enemy in Enemies)
                {
                    heroNameSync.Add(enemy.name,enemy);
                    selpro.AddChoice(enemy.name); 
                }

                var targetPrompt = heroNameSync[AnsiConsole.Prompt(selpro)];
                
                
                

                Interact[actionPrompt].Invoke(currentHero, targetedCharacter);

                //
                ////////////////////////////////////////////////////////////////

                


            }

            ////////////////////////////////////////////////////////////////
            // Felhasználó választásai

            void PlayerAttack(Hero Player, Characters Target)
            {
                System.Console.WriteLine(Target);
                Target.hp -= DamageCalculator.HeroToEnemy(Player,Target);
                Player.points -= fegyverek[Player.weapon].hasznalat;
            }

            void CastSpell(Hero Player, Characters Target)
            {
                int i=0;
                System.Console.WriteLine("\nChoose spell:");
                foreach (KeyValuePair<string, VarazslatAdatok> varazslat in varazslatok)
                {
                    i += 1;
                    Console.WriteLine($"{i} : {varazslat.Value.nev}");
                }
                Int32 chosenSpell = Int32.Parse(System.Console.ReadLine());
                chosenSpell = Math.Clamp(chosenSpell,0,varazslatok.Count);
                VarazslatAdatok varazs = varazslatok.Values.ElementAt(chosenSpell-1);

                foreach (KeyValuePair<string, double> hatas in varazs.hatas)
                {
                    if (hatas.Key == "defense"){Target.defense = Target.defense * (int)Math.Round(hatas.Value);}
                    if (hatas.Key == "damage"){Target.damage = Target.damage * (int)Math.Round(hatas.Value);}
                    if (hatas.Key == "hp"){Target.hp = Target.hp * (int)Math.Round(hatas.Value);}
                    if (hatas.Key == "canFly"){Target.canFly = true;}
                }
            }

            ////////////////////////////////////////////////////////////////
            // Ellenség választásai

            void EnemyAttack(Enemy Attacker, Hero Target )
            {
                Target.hp -= DamageCalculator.EnemyToHero(Attacker,Target);
            }

            ////////////////////////////////////////////////////////////////
            // Csata

            
            
            void Alapok()
            {
                Console.Clear();
                var rule = new Rule("Csata Folymatban!");   
                AnsiConsole.Write(rule);

                // irja ki a baratokat
                AnsiConsole.WriteLine("Társak a csatában:");
                foreach (Hero hero in Heroes)
                {
                    AnsiConsole.Write(new Text($"Ω {hero.name} ({hero.eredeti.name}) > {hero.hp}/{hero.eredeti.hp}\n", new Style( 	new Color(0,215,95))));
                }
                // irja ki az ellensegeket
                AnsiConsole.WriteLine("Ellenségek a csatában:");
                foreach (Enemy enemy in Enemies)
                {
                    AnsiConsole.Write(new Text($"ɸ {enemy.name} > {enemy.hp}/{enemy.eredeti.hp}\n", new Style(new Color(255,135,135))));
                }
                AnsiConsole.Write(new Rule());


            }

            
            Interact["sima támadás"] = PlayerAttack;
            Interact["mágia használata"] = CastSpell;


            int turn = 0;

            while (Enemies.Length > 0)
            {
                turn += 1;

                System.Console.WriteLine($"[{turn}. Kör]");

                Alapok();
                Action();

            };



            
            
            

            // Interact["Attack"].Invoke(currentHero, targetedEnemy);





            return false;
        }
    }

    
}
