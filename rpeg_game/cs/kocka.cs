using DamageManager;

namespace Kocka
{
    class kockaDobas
    {
        public int Dobas()
        {
            
            //Csak példa, hogy ne legyen error
            //int kapottSzam = 0;
            //return kapottSzam; 

            Random roll = new Random();
            int Dobas = roll.Next(1,21);
        }
    }
}

