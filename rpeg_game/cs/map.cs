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
            //Random helyzetek létrehozása, felosztása a mapon
            //10 x 10es map esetén
            Random rnd = new Random();
            
            //Map alsó része
            int randomHeroX = rnd.Next(5, 9);
            int randomHeroY = rnd.Next(5, 9);
            
            //Map felső része
            int randomEnemyX = rnd.Next(0, 4);
            int randomEnemyY = rnd.Next(0, 4);
            
            //Optimalizálni kell az egész csapatra!!!
            //El kell dönteni, hogy hero vagy enemy pozíciók kellenek e (Jól csináltam?)
            if (hero != null)
            {
                Dictionary<int, int> heroPos = new Dictionary<int, int>();
                heroPos.Add(randomHeroX, randomHeroY);
                placeHero.Add(hero, heroPos);
            }

            if (enemy != null)
            {
                Dictionary<int, int> enemyPos = new Dictionary<int, int>();
                enemyPos.Add(randomEnemyX, randomEnemyY);
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
            
            //Távolság leellenőrzése
            //Segéd távolság a pitagorasz tételhez
            double tavolsagX = 0;
            double tavolsagY = 0;
            
            //Fő távolság
            double tavolsag = 0;

            int referenciaErtek = 5;
            bool tamadhatosag = false;
            
            //Egyenes távolság
            if (heroPrintX == enemyPrintX)
            {
                tavolsagY = Math.Abs(heroPrintY) - Math.Abs(enemyPrintY);
                tavolsag = tavolsagY;
            }
            else if(heroPrintY == enemyPrintY)
            {
                tavolsagX = Math.Abs(heroPrintX) - Math.Abs(enemyPrintX);
                tavolsag = tavolsagX;
            }
            //Keresztbe lévő távolság
            else if(heroPrintX != enemyPrintX && heroPrintY != enemyPrintY)
            {
                tavolsag = Math.Sqrt(Math.Pow(tavolsagX, 2) + Math.Pow(tavolsagY, 2));
            }

            //Távolságon bekül van e
            if (referenciaErtek <= Math.Round(tavolsag))
            {
                tamadhatosag = true;
            }
            
            //Map generálás
            int sizeX = 5;
            int sizeY = 5;

            //Szoba, map számának a kiírása
            Console.WriteLine($"{null} számú pálya");
            Console.WriteLine($"{null} számú szoba");
            
            int[,] numbers = new int[sizeY,sizeX];
                
        }
        
        public void mapManager()
        {
            
        }
    }
}
//Random lehelyezés (map felső része enemy, alsó része hero)10 x 10es map esetén (felső: [0-4, 0-4],alsó: [5-9, 5-9]) pipa
//Hero - Enemy távolság kiszámolása (lehet bool érték visszaadása, hogy távolságon belül van-e) pipa
//Kirajzolás - Xavér

//Mozgás a mapon - pozíciók átírása a mátrixban!!
//Map manager - melyik map és szoba