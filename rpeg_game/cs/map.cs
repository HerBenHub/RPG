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
    class generateMap
    {
        public ArrayList genPlaces()
        {
            //Az egész többször is lefuthat, mert mindig új adatok lesznek a listában a clear és az újbeli feltöltés miatt
            ArrayList genRandoms = new ArrayList();
            genRandoms.Clear();
            Random random = new Random();
            
            //7 közül választ egyet
            int randomEnemy = random.Next(1, 7);
            var enemys = Program.createEnemy(randomEnemy);

            //Random hely, hogy hova rakje be
            int randomPlaces = random.Next(1, 4);
            int randomPlacesHero = random.Next(1, 4);
            
            genRandoms.Add(enemys);
            genRandoms.Add(randomPlaces);
            genRandoms.Add(randomPlacesHero);
            genRandoms.Add(Program.createCharacter());
            
            return genRandoms;
        }
        //Indexek
        //0: enemy
        //1: Hova rakja be az enemyt
        public void genMap(ArrayList genRandoms, bool nextMap)
        {
            int counter = 0;
            int firstIndex = (int)genRandoms[1];
            int secondIndex = (int)genRandoms[2];
            
            //Ha a szoba random száma megegyezik az iterécióval akkor ott lesz az enemy
            //Szobák
            int szobaSize = 4;
            int szobaDarab = 4;

            for (int sor = 0; sor < szobaSize; sor++)
            {
                for (int square = 0; square < szobaDarab; square++)
                {
                    if (firstIndex== szobaDarab)
                    {
                        var enemy = genRandoms[0];
                    }

                    if (secondIndex == szobaDarab)
                    {
                        var hero = genRandoms[3];
                    }
                    for (int oszlop = 0; oszlop < szobaSize; oszlop++)
                    {
                        if (sor == 0 || sor == szobaSize - 1 || oszlop == 0 || oszlop == szobaSize - 1)
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    if (square < szobaDarab - 1)
                    {
                        Console.Write(" "); 
                    }
                }
                Console.WriteLine();
            }

            if (nextMap == true)
            {
                switch (counter)
                {
                    case 4:
                        break;
                }
                //Úgy tűnhet mintha itt is lefutna folyamatosan de nem, mert ha a külső nextMap érték megváltozik
                //akkor nem lép rá az if ágra
                genMap(genPlaces(), true);
                counter++;
            }
        }

        public void mapManager()
        {
            for (int i = 0; i < 4; i++)
            {
                //nextMap == true --> folyamatosan lefut egymás után az egész
                genMap(genPlaces(), true);
                
            }
        }
    }
}
