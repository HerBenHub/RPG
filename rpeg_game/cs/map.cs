// using System.Collections;
// using System.Security.Cryptography;
// using System.Text.Json;
// using JsonManager;
// using RitkasagManager;
// using BattleManager;
// using System.Xml.Serialization;
// using Spectre.Console;
// using System.Runtime.InteropServices;
// using System.ComponentModel;
// using System.Runtime.InteropServices.JavaScript;
// using MainProgram;
// using System.Windows;
// using System.Drawing;


// namespace mapGenerate
// {
//     public class generateMap
//     {
//         //Dict deklarálása
//         static Dictionary<Program.Characters, Dictionary<int, int>> placeChars =
//             new Dictionary<Program.Characters, Dictionary<int, int>>();
        
//         static int sizeX = 10;
//         static int sizeY = 10;
        
//         private static void genPlaces(Program.Characters chars)
//         {
//             Random rnd = new Random();
            
//             int randomCharX = 0;
//             int randomCharY = 0;
            
//             if (placeChars.Keys.GetType() == typeof(Program.Enemy))
//             {
//                 //Map alsó része
//                 randomCharX = rnd.Next(sizeX - sizeX / 2, sizeY - 1);
//                 randomCharY = rnd.Next(sizeX - sizeX / 2, sizeY - 1);
//             }

//             else if (placeChars.Keys.GetType() == typeof(Program.Hero))
//             {
//                 //Map alsó része
//                 randomCharX = rnd.Next(sizeX - sizeX, (sizeY / 2) - 1);
//                 randomCharY = rnd.Next(sizeX - sizeY, (sizeY / 2) - 1);
//             }
            
//             if (placeChars != null)
//             {
//                 Dictionary<int, int> charPos = new Dictionary<int, int>();
//                 charPos.Add(randomCharX, randomCharY);
//                 placeChars.Add(chars, charPos);
//             }
//         }
        
//         public class MainWindow : Window 
//         {
//             public MainWindow()
//             {
//                 InitializeComponent();
//                 DrawSquareWithCharacter();
//             }
//         }
        

//         private static void DrawSquareWithCharacter()
//         {
            
//         }
//         private static void getPlaces(Program.Characters chars)
//         {
//             int charPrintX = 0;
//             int charPrintY = 0;
            
//             //Adatok kikérése
            
            
//             //Itt vizsgálom melyik karakter melyik pozícióban van
//             foreach (var charChar in placeChars)
//             {
//                 //Itt tudom elérni a hero karaktert pl
//                 //heroChar.Key
//                 foreach (var charData in charChar.Value)
//                 {
//                     //Itt érem el a dict-en belüli dict-et
//                     charPrintX = charData.Key;
//                     charPrintY = charData.Value;

//                     if (placeChars.Keys.GetType() == typeof(Program.Hero))
//                     {
//                         Rectangle character = new Rectangle();
//                         character.Width = 20;
//                         character.Height = 20;
//                         character.Fill = Brushes.Blue;
//                         double characterLeft = x * (canvas.Width / sizeX);
//                         double characterTop = y * (canvas.Height / sizeY);
//                         Canvas.SetLeft(character, characterLeft);
//                         Canvas.SetTop(character, characterTop);
//                         canvas.Children.Add(character);
//                     }
//                     else if (placeChars.Keys.GetType() == typeof(Program.Enemy))
//                     {
//                         Rectangle character = new Rectangle();
//                         character.Width = 20;
//                         character.Height = 20;
//                         character.Fill = Brushes.Red;
//                         double characterLeft = x * (canvas.Width / sizeX);
//                         double characterTop = y * (canvas.Height / sizeY);
//                         Canvas.SetLeft(character, characterLeft);
//                         Canvas.SetTop(character, characterTop);
//                         canvas.Children.Add(character);
//                     }
//                 }
//             }
            
//         }
        
//         //Később mehet private-nak
//         public static void genMap(Program.Characters chars)
//         {
//             Rectangle square = new Rectangle();
//             square.Width = 300;
//             square.Height = 300;
//             square.Fill = Brushes.LightBlue;
//             Canvas.SetLeft(square, 0); // A Canvas bal felső sarkába helyezzük a nagyobb négyzetet
//             Canvas.SetTop(square, 0);
//             canvas.Children.Add(square);
            
//             getPlaces(chars);
//         }

//         public static void moveChars(Program.Characters chars)
//         {
//             int charPrintX = 0;
//             int charPrintY = 0;
            
//             if (placeChars.Keys.GetType() == typeof(Program.Hero))
//             {
//                 var mozgas = AnsiConsole.Prompt(
//                     new SelectionPrompt<string>()
//                         .Title("Merre szeretnél mozogni?")
//                         .PageSize(10)
//                         .HighlightStyle(new Style(new Color(0, 128, 0)))
//                         .AddChoices("Fel!", "Le!", "Jobbra!", "Balra!")
//                         .AddChoices("[Red]Kilépés[/]"));
                
//                 //Ezt egy cikluson belül is meg lehet oldani!
//                 if (mozgas == "Fel!")
//                 {
//                     //Itt vizsgálom melyik karakter melyik pozícióban van
//                     foreach (var charChar in placeChars)
//                     {
//                         //Itt tudom elérni a hero karaktert
//                         //heroChar.Key
//                          foreach (var charData in charChar.Value)
//                         {
//                             //Itt érem el a dict-en belüli dict-et
//                             charPrintX = charData.Key;
//                             charPrintY -= charData.Value;
//                         }
//                     }
//                 }
//                 else if (mozgas == "Le!")
//                 {
//                     //Itt vizsgálom melyik karakter melyik pozícióban van
//                     foreach (var charChar in placeChars)
//                     {
//                         //Itt tudom elérni a hero karaktert
//                         //heroChar.Key
//                         foreach (var charData in charChar.Value)
//                         {
//                             //Itt érem el a dict-en belüli dict-et
//                             charPrintX = charData.Key;
//                             charPrintY += charData.Value;
//                         }
//                     }
//                 }
//                 else if (mozgas == "Jobbra!")
//                 {
//                     //Itt vizsgálom melyik karakter melyik pozícióban van
//                     foreach (var charChar in placeChars)
//                     {
//                         //Itt tudom elérni a hero karaktert
//                         //heroChar.Key
//                         foreach (var charData in charChar.Value)
//                         {
//                             //Itt érem el a dict-en belüli dict-et
//                             charPrintX += charData.Key;
//                             charPrintY = charData.Value;
//                         }
//                     }
//                 }
//                 else if (mozgas == "Balra!")
//                 {
//                     //Itt vizsgálom melyik karakter melyik pozícióban van
//                     foreach (var charChar in placeChars)
//                     {
//                         //Itt tudom elérni a hero karaktert
//                         //heroChar.Key
//                         foreach (var charData in charChar.Value)
//                         {
//                             //Itt érem el a dict-en belüli dict-et
//                             charPrintX -= charData.Key;
//                             charPrintY = charData.Value;
//                         }
//                     }
//                 }
//             }
//         }

//         // public static double inDistance(Program.Characters chars)
//         // {
//         //     //Ha a két érték (x, y) egyenlő, akkor tökmindegy melyik adat lesz a kereszt távolság
//         //     //StackOverFlow link dc!!
//         //     
//         //     var places = genMap(chars);
//         //
//         //     int charPrintX = places[0];
//         //     int charPrintY = places[1];
//         //     
//         //     //Távolság leellenőrzése
//         //     //Segéd távolság a pitagorasz tételhez
//         //     double tavolsagX = 0;
//         //     double tavolsagY = 0;
//         //     
//         //     //Fő távolság
//         //     double tavolsag = 0;
//         //
//         //     int referenciaErtek = 5;
//         //     bool tamadhatosag = false;
//         //     
//         //     //Egyenes távolság
//         //     if (placeChars.Keys.GetType() == placeChars.Keys.GetType(Program.Enemy))
//         //     {
//         //         tavolsagY = Math.Abs(heroPrintY) - Math.Abs(enemyPrintY);
//         //         tavolsag = tavolsagY;
//         //     }
//         //     else if(heroPrintY == enemyPrintY)
//         //     {
//         //         tavolsagX = Math.Abs(heroPrintX) - Math.Abs(enemyPrintX);
//         //         tavolsag = tavolsagX;
//         //     }
//         //     //Keresztbe lévő távolság
//         //     else if(heroPrintX != enemyPrintX && heroPrintY != enemyPrintY)
//         //     {
//         //         tavolsag = Math.Sqrt(Math.Pow(tavolsagX, 2) + Math.Pow(tavolsagY, 2));
//         //     }
//         //
//         //     if (Program.Character)
//         //     {
//         //         
//         //     }
//         // }
        
//         public static void mapManager(Program.Characters chars)
//         {
//             int palyaSzam = 1;
//             int szobaSzam = 1;
            
//             while (true)
//             {
//                 //genMap függvény meghívása
//                 genMap(chars);
                
//                 //Szoba, map számának a kiírása
//                 Console.WriteLine($"{palyaSzam} számú pálya");
//                 Console.WriteLine($"{szobaSzam} számú szoba");

//                 if (szobaSzam == 4)
//                 {
//                     palyaSzam++;
//                     szobaSzam = 1;
//                 }
//                 if (palyaSzam == 4)
//                 {
//                     Console.WriteLine("Játék vége!");
//                     break;
//                 }
//                 szobaSzam++;
//             }
//         }
//     }
// }