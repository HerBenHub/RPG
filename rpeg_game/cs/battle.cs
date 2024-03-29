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
            Dictionary<string, Action<Characters,Characters>> Interact = [];
            List<Characters?> CharactersInBattle = new List<Characters?>();
            int turn = 1;

            // karakterek beillesztése
            foreach (Hero hero in Heroes)
            { CharactersInBattle.Add(hero); }
            foreach (Enemy enemy in Enemies)
            { CharactersInBattle.Add(enemy); }

            // karakterek sorba rakása priority szerint
            CharactersInBattle.Sort(delegate(Characters c1, Characters c2) { return c1.priority.CompareTo(c2.priority); });
            // a .Sort felfele növekvő sorrenbe van, viszont a kódban a magasabb szám a jobb, így meg kell fordítani a sorszámot
            CharactersInBattle.Reverse();

            Characters? currentAttacker = CharactersInBattle[0];
            Characters? targetedCharacter = Enemies[0];


            void Action()
            {
                
                
                var actionPrompt = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[cyan3]Mit akarsz csinálni?[/]")   
                        .PageSize(5)
                        .HighlightStyle(new Style(new Color(255,100,100),null,Decoration.Italic))
                        .AddChoices(Interact.Select(kvp => kvp.Key).ToList())
                        .AddChoices("kör lezárása"));

                if (actionPrompt != "kör lezárása")
                {
                    SelectionPrompt<string> selpro = new SelectionPrompt<string>()
                            .Title("[cyan3]Kit célozol meg?[/]")
                            .PageSize(10)
                            .HighlightStyle(new Style(new Color(150,150,100),null,Decoration.Italic))
                            .MoreChoicesText("[lightcyan3](Menj lejjebb!)[/]");
                

                    Dictionary<string,Characters> heroNameSync = new Dictionary<string,Characters>();
                    // Hero tipus szinkronizálása a SelectionPrompt stringjével

                    foreach (Hero hero in CharactersInBattle.OfType<Hero>())
                    {
                        heroNameSync.Add(hero.name,hero);
                        selpro.AddChoice(hero.name); 
                    }
                    foreach (Enemy enemy in CharactersInBattle.OfType<Enemy>())
                    {
                        heroNameSync.Add(enemy.name,enemy);
                        selpro.AddChoice(enemy.name); 
                    }

                    selpro.AddChoices("vissza");

                    if (AnsiConsole.Prompt(selpro) != "vissza")
                    {
                        targetedCharacter = heroNameSync[AnsiConsole.Prompt(selpro)];
                        Interact[actionPrompt].Invoke(currentAttacker, targetedCharacter);
                    }

                }
                else
                {
                    EndTurn();
                }

                


            }

            ////////////////////////////////////////////////////////////////
            // Támadások

            void Attack(Characters Attacker, Characters Target)
            {
                if (Attacker.GetType() == typeof(Hero))
                {
                    if (Attacker.points >= fegyverek[Attacker.weapon].hasznalat)
                    {
                        Target.hp -= DamageCalculator.HeroAttack(Attacker,Target);
                        Attacker.points -= fegyverek[Attacker.weapon].hasznalat;

                        if (Target.hp <= 0) { CharactersInBattle.Remove(Target); }
                    }
                    else
                    {
                        AnsiConsole.Write(new Text("Nincs elég AP-d!", new Style( 	new Color(200,10,10))));
                        System.Console.ReadLine();
                    }
                }
                // if type Enemy ide
            }

            void CastSpell(Characters Attacker, Characters Target)
            {
                if (Attacker.GetType() == typeof(Hero))
                {
                    Dictionary<string,string> spellNameSync = new Dictionary<string,string>();

                    SelectionPrompt<string> selpro_spell = new SelectionPrompt<string>()
                        .Title("Melyik varázst használod fel?")
                        .PageSize(10)
                        .HighlightStyle(new Style(new Color(150,100,150),null,Decoration.Italic))
                        .MoreChoicesText("[lightcyan3](Menj lejjebb!)[/]");

                    foreach (var varazslat in varazslatok)
                    {
                        spellNameSync.Add(varazslat.Value.nev, varazslat.Key);
                        selpro_spell.AddChoice(varazslat.Value.nev);
                    }   

                    VarazslatAdatok varazs = varazslatok[spellNameSync[AnsiConsole.Prompt(selpro_spell)]];

                    if (Attacker.points >= varazs.hasznalat)
                    {
                        Attacker.points -= varazs.hasznalat;

                        foreach (KeyValuePair<string, double> hatas in varazs.hatas)
                        {
                            if (hatas.Key == "defense"){Target.defense = (int)Math.Round(Target.defense + hatas.Value);}
                            if (hatas.Key == "damage"){Target.damage = (int)Math.Round(Target.damage * hatas.Value);}
                            if (hatas.Key == "hp"){Target.hp = (int)Math.Round(Target.hp + (Target.eredeti.hp * hatas.Value));}
                            if (hatas.Key == "canFly"){Target.canFly = true;}
                        }
                    }
                    else
                    {
                        AnsiConsole.Write(new Text("Nincs elég AP-d!", new Style( 	new Color(200,10,10))));
                        System.Console.ReadLine();
                    }
                }
                // if type Enemy ide
            }   

            ////////////////////////////////////////////////////////////////
            // Csata

            void EndTurn(Characters? Attacker = null, Characters? Target = null)
            {
                currentAttacker = CharactersInBattle[turn % CharactersInBattle.Count];
                turn += 1;
            }

            
            void Alapok()
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
                    {AnsiConsole.Write(new Text($"ɸ {enemy.name}\n   HP:[{enemy.hp}/{enemy.eredeti.hp}]  AP:[{enemy.points}]\n\n", new Style(new Color(215, 0, 255))));}
                    else
                    {AnsiConsole.Write(new Text($"ɸ {enemy.name}\n   HP:[{enemy.hp}/{enemy.eredeti.hp}]  AP:[{enemy.points}]\n\n", new Style(new Color(255,135,135))));}

                }
                AnsiConsole.Write(new Rule($"{turn}. kör"));

            }

            
            Interact["sima támadás"] = Attack;
            Interact["mágia használata"] = CastSpell;

            // main csata code
            while (true)
            {
                if (CharactersInBattle.Where(s=>s!=null && s.GetType()==typeof(Enemy)).Count() <= 0)
                {
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
                Action();

            };
            

            // Interact["Attack"].Invoke(currentHero, targetedEnemy);

        }
    }

    
}
