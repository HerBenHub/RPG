using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace menutest
{
    class Game
    {
        public void Start()
        {
            Title = "Dark RPG";
            FoMenufuttatas();

        }
        private void FoMenufuttatas()
        {
            string prompt = @"
██████╗  █████╗ ██████╗ ██╗  ██╗    ██████╗ ██████╗  ██████╗ 
██╔══██╗██╔══██╗██╔══██╗██║ ██╔╝    ██╔══██╗██╔══██╗██╔════╝ 
██║  ██║███████║██████╔╝█████╔╝     ██████╔╝██████╔╝██║  ███╗
██║  ██║██╔══██║██╔══██╗██╔═██╗     ██╔══██╗██╔═══╝ ██║   ██║
██████╔╝██║  ██║██║  ██║██║  ██╗    ██║  ██║██║     ╚██████╔╝
╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝    ╚═╝  ╚═╝╚═╝      ╚═════╝                                                              

Üdvözöllek a játékban! Válassz a nyilakkal.";
            string[] opciok = { "új játék", "folytatás", "Kilépés" };
            Menu mainMenu = new Menu(prompt, opciok);
            int valasztott = mainMenu.futtatas();

            switch (valasztott) 
            {
                case 0:
                    Ujjatek();
                    break;
                case 1:
                    Folytatas();
                    break;
                case 2:
                    Kilepes();
                    break;

            }
        }

        private void Kilepes()
        {
            string prompt = "Biztosan ki szeretnél lépni?\n";
            string[] opciok = { "Igen", "Nem" };
            Menu E_Menu = new Menu(prompt, opciok);
            int E_valasztott = E_Menu.futtatas();

            BackgroundColor = ConsoleColor.Black;
            switch(E_valasztott)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    FoMenufuttatas();
                    break;
            }
        }

        private void Ujjatek()
        {
            WriteLine("\nMég nincs itt semmi");
            ReadKey(true);
            FoMenufuttatas();
        }

        private void Folytatas()
        {
            WriteLine("\nMég itt sincs semmi");
            ReadKey(true);
            FoMenufuttatas();
        }
    }
}
