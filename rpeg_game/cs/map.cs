using System.Collections;
using System.Security.Cryptography;
using System.Text.Json;
using JsonManager;
using RitkasagManager;
using BattleManager;
using System.Xml.Serialization;
using Spectre.Console;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using MainProgram;

namespace mapGenerate
{
    public class generateMap
    {
        public static void genPlaces(
            Program.Hero? hero, 
            Program.Enemy? enemy, 
            Dictionary<Program.Hero, Dictionary<int, int>> placeHero,  
            Dictionary<Program.Enemy, Dictionary<int, int>> placeEnemy)
        {
            //Optimalizálni kell az egész csapatra!!!
            //El kell dönteni, hogy hero vagy enemy pozíciók kellenek e (Jól csináltam?)
            if (hero != null)
            {
                Dictionary<int, int> heroPos = new Dictionary<int, int>();
                heroPos.Add(5, 5);
                placeHero.Add(hero, heroPos);
            }

            if (enemy != null)
            {
                Dictionary<int, int> enemyPos = new Dictionary<int, int>();
                enemyPos.Add(6, 6);
                placeEnemy.Add(enemy, enemyPos);
            }
            
        }
        //Dict-ek deklarálása
        static Dictionary<Program.Hero, Dictionary<int, int>> placeHero =
            new Dictionary<Program.Hero, Dictionary<int, int>>();

        static Dictionary<Program.Enemy, Dictionary<int, int>> placeEnemy =
            new Dictionary<Program.Enemy, Dictionary<int, int>>();
        
        public static void genMap<TKey, TValue>(Dictionary<TKey, Dictionary<int, int>> dictionary, 
            Program.Hero? hero, 
            Program.Enemy? enemy)
        {
            int heroPrintX = 0;
            int heroPrintY = 0;
            
            int enemyPrintX = 0;
            int enemyPrintY = 0;
            
            //Adatok kikérése
            
            if (typeof(TKey) == typeof(Program.Hero))
            {
                //Itt vizsgálom melyik karakter melyik pozícióban van
                foreach (var heroChar in placeHero)
                {
                    //Itt tudom elérni a hero karaktert
                    //heroChar.Key
                    foreach (var heroData in heroChar.Value)
                    {
                        //Itt érem el a dict-en belüli dict-et
                        heroPrintX = heroData.Key;
                        heroPrintY = heroData.Value;
                    }
                }
            }

            if (typeof(TKey) == typeof(Program.Enemy))
            {
                //Itt vizsgálom melyik karakter melyik pozícióban van
                foreach (var enemyChar in placeEnemy)
                {
                    //Itt tudom elérni a enemy karaktert
                    //enemyChar.Key
                    foreach (var enemyData in enemyChar.Value)
                    {
                        //Itt érem el a dict-en belüli dict-et
                        enemyPrintX = enemyData.Key;
                        enemyPrintY = enemyData.Value;
                    }
                }
            }
            
            int sizeX = 5;
            int sizeY = 5;

            //Szoba, map számának a kiírása
            Console.WriteLine($"{null} számú pálya");
            Console.WriteLine($"{null} számú szoba");
            
            //Oszlop
            for (int i = sizeY; i >= 0; i--)
            {
                //Sor
                for (int j = 0; j < sizeX; j++)
                {

                    //Itt kell megvizsgálnom, hogy hero vagy enemy kirajzolás lesz e, esetleg mindkettő
                    //Meg kell vizsgálnom, hogy melyik pozícióba szeretném őket kirajzolni
                    //Mindenkihez tartozik egy x és y adat (a belső (második) ciklusban az indexelés miatt eltérés lehet a pozíció adatoknál)
                    //Pl.: x = 3, y = 3 --> a valóságban: x = 2, y = 3 (vagy valami ilyesmi, lehet fordítva, most nem tudok tesztelni)

                    if (i == 1 && i == 6)
                    {
                        if (j == 1 && j == 5)
                        {
                            Console.Write('#');
                        }
                    }
                    
                }
                Console.Write("\n");
            }
        }
        public void mapManager()
        {
            
        }
    }
}