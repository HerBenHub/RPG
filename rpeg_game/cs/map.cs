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
using System.Collections.Generic;

namespace mapGenerate
{
    public class generateMap
    {
        //Dict deklarálása
        static Dictionary<Program.Characters, Dictionary<int, int>> placeChars =
            new Dictionary<Program.Characters, Dictionary<int, int>>();
        
        static int sizeX = 10;
        static int sizeY = 10;
        
        private static void genPlaces(Program.Characters chars)
        {
            Random rnd = new Random();
            
            int randomCharX = 0;
            int randomCharY = 0;
            
            if (placeChars.Keys.GetType() == typeof(Program.Enemy))
            {
                //Map alsó része
                randomCharX = rnd.Next(sizeX - sizeX / 2, sizeY - 1);
                randomCharY = rnd.Next(sizeX - sizeX / 2, sizeY - 1);
            }

            else if (placeChars.Keys.GetType() == typeof(Program.Hero))
            {
                //Map alsó része
                randomCharX = rnd.Next(sizeX - sizeX, (sizeY / 2) - 1);
                randomCharY = rnd.Next(sizeX - sizeY, (sizeY / 2) - 1);
            }
            
            if (placeChars != null)
            {
                Dictionary<int, int> charPos = new Dictionary<int, int>();
                charPos.Add(randomCharX, randomCharY);
                placeChars.Add(chars, charPos);
            }
        }
        private static List<int> genMap(Program.Characters chars)
        {
            int charPrintX = 0;
            int charPrintY = 0;

            var canvas = new Canvas(sizeX, sizeY);
            for (var i = 0; i < canvas.Width; i++)
            {
                //Itt vizsgálom melyik karakter melyik pozícióban van
                foreach (var charChar in placeChars)
                {
                    //Itt tudom elérni a hero karaktert pl
                    //heroChar.Key
                    foreach (var charData in charChar.Value)
                    {
                        //Itt érem el a dict-en belüli dict-et
                        charPrintX = charData.Key;
                        charPrintY = charData.Value;

                        if (placeChars.Keys.GetType() == typeof(Program.Hero))
                        {
                            canvas.SetPixel(charPrintX, charPrintY, Color.White);
                        }
                        else if (placeChars.Keys.GetType() == typeof(Program.Enemy))
                        {
                            canvas.SetPixel(charPrintX, charPrintY, Color.Red);
                        }
                    }
                }
                //Keret
                canvas.SetPixel(i, 0, Color.Green);
                canvas.SetPixel(0, i, Color.Green);
                canvas.SetPixel(i, canvas.Height - 1, Color.Green);
                canvas.SetPixel(canvas.Width - 1, i, Color.Green);
            }
            AnsiConsole.Write(canvas);
            
            List<int> places = new List<int>();

            if (places.Any())
            {
                places.Clear();
            }
            
            if (placeChars.Keys.GetType() == typeof(Program.Hero))
            {
                places.Add(charPrintX);
                places.Add(charPrintY);
            }
            else if (placeChars.Keys.GetType() == typeof(Program.Enemy))
            {
                places.Add(charPrintX);
                places.Add(charPrintY);
            }
            return places;
        }
        public static void moveChars(Program.Characters chars)
        {
            int charPrintX = 0;
            int charPrintY = 0;

            if (placeChars.Keys.GetType() == typeof(Program.Hero))
            {
                var mozgas = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Merre szeretnél mozogni?")
                        .PageSize(10)
                        .HighlightStyle(new Style(new Color(0, 128, 0)))
                        .AddChoices("Fel!", "Le!", "Jobbra!", "Balra!")
                        .AddChoices("[Red]Kilépés[/]"));

                //Ezt egy cikluson belül is meg lehet oldani!
                if (mozgas == "Fel!")
                {
                    //Itt vizsgálom melyik karakter melyik pozícióban van
                    foreach (var charChar in placeChars)
                    {
                        //Itt tudom elérni a hero karaktert
                        //heroChar.Key
                        foreach (var charData in charChar.Value)
                        {
                            if (mozgas == "Fel!")
                            {
                                charPrintX = charData.Key;
                                charPrintY -= charData.Value;
                            }
                            else if (mozgas == "Le!")
                            {
                                charPrintX = charData.Key;
                                charPrintY += charData.Value;
                            }
                            else if (mozgas == "Jobbra!")
                            {
                                charPrintX += charData.Key;
                                charPrintY = charData.Value;
                            }
                            else if (mozgas == "Balra!")
                            {
                                charPrintX -= charData.Key;
                                charPrintY = charData.Value;
                            }
                        }
                    }
                }
            }
        }

        public static void inDistance(Program.Characters chars)
        {
            
            var places = genMap(chars);
        
            int HcharPrintX = places[0];
            int HcharPrintY = places[1];

            int EcharPrintX = places[2];
            int EcharPrintY = places[3];

            double tavolsag = 0;
            
            if (HcharPrintX == EcharPrintX || HcharPrintY == EcharPrintY)
            {
                //Egyenes távolság
                double dx = EcharPrintX - HcharPrintX;
                double dy = EcharPrintY - HcharPrintY;
                
                tavolsag = Math.Sqrt(dx * dx + dy * dy);
            }
            else
            {
                //Keresztbe távolság
                double dx = Math.Abs(EcharPrintX - HcharPrintX);
                double dy = Math.Abs(EcharPrintY - HcharPrintY);
                
                tavolsag = Math.Sqrt(dx * dx + dy * dy);
            }

        }
        
        public static void mapManager(Program.Characters chars)
        {
            int palyaSzam = 1;
            int szobaSzam = 1;
            
            while (true)
            {
                genPlaces(chars);
                //genMap függvény meghívása
                genMap(chars);
                
                //Szoba, map számának a kiírása
                Console.WriteLine($"{palyaSzam} számú pálya");
                Console.WriteLine($"{szobaSzam} számú szoba");

                if (szobaSzam == 4)
                {
                    palyaSzam++;
                    szobaSzam = 1;
                }
                if (palyaSzam == 4)
                {
                    Console.WriteLine("Játék vége!");
                    break;
                }
                szobaSzam++;
            }
        }
    }
}