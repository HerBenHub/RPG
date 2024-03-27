namespace test
{
    class tester
    {
        public static void testing()
        {
            int y = 10;
            int x = 10;
            
            int[,] numbers = new int[y,x];
            
            // az alatti for-t berakni egy for-ba ami x-ig megy
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    if (i == 8 && j == 4 || i == 5 && j == 5)
                    {
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.Write($"{numbers[0,i]} ");
                    }

                    // if (i == 5 && j == 5)
                    // {
                    //     Console.Write("# ");
                    // }
                    // else
                    // {
                    //     Console.Write($"{numbers[0,i]} ");
                    // }
                }

                Console.Write("\n");
            }
        }
    }
}


