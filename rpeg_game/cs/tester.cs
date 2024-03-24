namespace test
{
    class tester
    {
        public static void testing()
        {
            int sizeX = 5;
            int sizeY = 5;
            
            //Sor
            for (int i = sizeY; i >= 1; i--)
            {
                //Oszlop
                for (int j = 0; j < sizeX; j++)
                {

                    //Itt kell megvizsgálnom, hogy hero vagy enemy kirajzolás lesz e, esetleg mindkettő
                    //Meg kell vizsgálnom, hogy melyik pozícióba szeretném őket kirajzolni
                    //Mindenkihez tartozik egy x és y adat (a belső (második) ciklusban az indexelés miatt eltérés lehet a pozíció adatoknál)
                    //Pl.: x = 3, y = 3 --> a valóságban: x = 2, y = 3 (vagy valami ilyesmi, lehet fordítva, most nem tudok tesztelni)

                    if (i == 3 && j == 3)
                    {
                        Console.Write("L ");
                    }
                    //Ha ki van rajzolva az "L " akkor már nem kellene kirajzolni az "# "-t
                    Console.Write("# ");
                    
                }
                Console.Write("\n");
            }
        }
    }
}

