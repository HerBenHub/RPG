
using System;
using System.Collections.Generic;
using System.IO;
using JsonManager;
using static MainProgram.Program;

namespace DamageManager
{
    class DamageCalculator
    {
        public static int HeroAttack(Hero hero, Characters target)
        {
            Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();

            // módosítók
            // string modosito = hero.weapon.Split(" ")[0];
            // string hasznaltFegyver = hero.weapon.Split(" ")[1];

            int baseDamage = fegyverek[hero.weapon].sebzes;
            int resistance = target.defense;
            double damageMultiplier = hero.damage;

            // dobokocka

            int damageNumber = (int)Math.Round((baseDamage * damageMultiplier)/(1.00+(resistance/100)));

            return damageNumber;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////

        public static int EnemyToHero(Enemy enemy, Hero hero)
        {
            // Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();

            int resistance = hero.defense;
            double damage = enemy.damage;
            

            // dobokocka

            int damageNumber = (int)Math.Round(damage/(1.00+(resistance/100)));

            return damageNumber;
        }
    }
}