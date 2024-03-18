using System.Text.Json;
using JsonManager;
using RitkasagManager;
using DamageManager;
using static MainProgram.Program;

using Spectre.Console;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace BattleManager
{
    
    public class Battles
    {   
        public static bool StartBattle(Hero[] Heroes, Enemy[] Enemies)
        {
            ////////////////////////////////////////////////////////////////
            // Alap cuccok

            Dictionary<string,VarazslatAdatok>? varazslatok = JsonOlvaso.VarazslatLista();
            Dictionary<string, Action<Hero,Enemy>> Interact = [];

            Hero currentHero = Heroes[0];
            Enemy targetedEnemy = Enemies[0];

            void Action()
            {

                System.Console.WriteLine("\nChoose action:");
                foreach (KeyValuePair<string, Action<Hero,Enemy>> action in Interact)
                {
                    Console.WriteLine($"> {action.Key}");
                }

                string chosenAction = System.Console.ReadLine().ToLower();
                


                System.Console.WriteLine("\nChoose character");
                Int32 chosenCharacter = Int32.Parse(System.Console.ReadLine());
                chosenCharacter = Math.Clamp(chosenCharacter,0,Heroes.Length);
                currentHero = Heroes[chosenCharacter-1];
                System.Console.WriteLine("Selected character: " + currentHero.name);


                System.Console.WriteLine("\nChoose enemy to attack");
                Int32 chosenEnemy = Int32.Parse(System.Console.ReadLine());
                chosenEnemy = Math.Clamp(chosenEnemy,0,Enemies.Length);
                targetedEnemy = Enemies[chosenEnemy-1];
                System.Console.WriteLine("Selected enemy: " + targetedEnemy.name);

                Interact[chosenAction].Invoke(currentHero, targetedEnemy);

                


            }

            ////////////////////////////////////////////////////////////////
            // Felhasználó választásai

            void PlayerAttack(Hero Player, Enemy Target)
            {
                Target.hp -= DamageCalculator.HeroToEnemy(Player,Target);
            }

            void CastSpell(Hero Player, Enemy Target)
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

            Console.Clear();
            System.Console.WriteLine("Csata elkezdve!");
            // irja ki a baratokat
            System.Console.WriteLine("Társak a csatában:");
            foreach (Hero hero in Heroes)
            {
                System.Console.WriteLine($"> {hero.name} ({hero.eredeti.name})");
            }
            // irja ki az ellensegeket
            System.Console.WriteLine("Ellenségek a csatában:");
            foreach (Enemy enemy in Enemies)
            {
                System.Console.WriteLine($"> {enemy.name}");
            }
            System.Console.WriteLine("--------------------------------------------------");

            
            Interact["attack"] = PlayerAttack;
            Interact["cast"] = CastSpell;


            int turn = 0;

            while (Enemies.Length > 0)
            {
                turn += 1;

                System.Console.WriteLine($"[Turn {turn}]");

                Action();

            };



            
            
            

            // Interact["Attack"].Invoke(currentHero, targetedEnemy);





            return false;
        }
    }

    
}
