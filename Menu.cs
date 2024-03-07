using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace menutest
{
    internal class Menu
    {
        private int valasztott;
        private string[] Opciok;
        private string Prompt;

        public Menu(string prompt, string[] opciok)
        {
            Prompt = prompt;
            Opciok = opciok;
            valasztott = 0;
        }

        private void DisplayOpciok()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Opciok.Length; i++)
            {
                string jelenlegiopcio = Opciok[i];
                string jel;

                if (i == valasztott)
                {
                    // megváltozik a színe a kiválasztott index (opció) nek 
                    jel = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Gray;
                }
                else
                {
                    jel = " ";
                    ForegroundColor = ConsoleColor.Gray;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{jel}  {jelenlegiopcio}");
            }
            ResetColor();
        }

        public int futtatas()
        {
            ConsoleKey nyomott;
            //amíg a felhasználó nem nyomja meg az Entert addig nem megy tovább
            do
            {
                Clear();
                DisplayOpciok();
                ConsoleKeyInfo keyInfo = ReadKey(true);
                nyomott = keyInfo.Key;
                //ne menjen az érték minuszba
                if(nyomott == ConsoleKey.UpArrow)
                {
                    valasztott--;
                    if(valasztott == -1)
                    {
                        valasztott = Opciok.Length - 1;
                    }
                }
                else if (nyomott == ConsoleKey.DownArrow)
                {
                    //ne menjen túl a maximum indexen(opción)
                    valasztott++;
                    if (valasztott == Opciok.Length)
                    {
                        valasztott = 0;
                    }
                }
            } while (nyomott != ConsoleKey.Enter);

            return valasztott;
        }
    }
}
