using DamageManager;
using BattleManager;
using JsonManager;
using static BattleManager.Battles;
using static MainProgram.Program;
using Spectre.Console;

namespace AIController
{
    public class AI
    {
        // fake ai
        public static void StartAI()
        {
            AnsiConsole.Markup("[red3_1]Az ellenség támad![/]\n");

            while (true)
            {
                
                // visuals (kiirás)
                var rand = new Random();
                AnsiConsole.Status()
                    .Start("[red3_1][/]", ctx => 
                    {
                        ctx.Spinner(Spinner.Known.SimpleDots);
                        ctx.SpinnerStyle(Style.Parse("red3_1"));
                        Thread.Sleep(rand.Next(2000,5000));
                    });

                if (currentAttacker.points < 10)
                {
                    break;
                }

                foreach (var character in CharactersInBattle.OfType<Hero>())
                {
                    if (character.hp <= currentAttacker.damage)
                    {
                        Attack(currentAttacker,character);
                        break;
                    }
                    
                }
                List<Hero> OrderByHp = CharactersInBattle.OfType<Hero>().ToList();
                OrderByHp.Sort(delegate(Hero c1, Hero c2) { return c1.hp.CompareTo(c2.hp); });
                Attack(currentAttacker, OrderByHp[0]);
            }
            EndTurn();
        }
    }
}