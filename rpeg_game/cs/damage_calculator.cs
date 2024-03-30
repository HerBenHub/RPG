
using System;
using System.Collections.Generic;
using System.IO;
using JsonManager;
using static MainProgram.Program;

namespace DamageManager
{
    class DamageCalculator
    {
        public static int BasicAttack(Characters attacker, Characters target)
        {

            Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();

            int resistance = target.defense;
            double damageMultiplier = attacker.damage;

            int damageNumber = 0; // várakozik a számolásra

            if (attacker.GetType() == typeof(Hero))
            {
                

                // módosítók
                // string modosito = hero.weapon.Split(" ")[0];
                // string hasznaltFegyver = hero.weapon.Split(" ")[1];


                int baseDamage = fegyverek[attacker.weapon].sebzes;
                

                attacker.points -= fegyverek[attacker.weapon].hasznalat;

                // dobokocka
                //Mindig a baseDamage kiszámolása
                //Egy dobás - max 20
                //Külön cs file az egészre

                damageNumber = (int)Math.Round((baseDamage * damageMultiplier)/(1.00+(resistance/100)));
            }


            if (attacker.GetType() == typeof(Enemy))
            {
                attacker.points -= 10;

                // dobokocka
                //Mindig a baseDamage kiszámolása
                //Egy dobás - max 20
                //Külön cs file az egészre

                damageNumber = (int)Math.Round(attacker.damage/(1.00+(resistance/100)));
            }
                
            return damageNumber;
        }

    }
    
}