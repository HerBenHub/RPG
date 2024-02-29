internal class Program
{
    public class Characters
    {
        public string name;
        public int hp;
        public int defense; //defense
        public int weapon; //attack
        public int armour;

        public Characters(string _name, int _hp, int _defense, int _weapon, int _armour)
        {
            name = _name;
            hp = _hp;
            defense = _defense;
            weapon = _weapon;
            armour = _armour;
        }
        public void levelUp()
        {
            hp += 10;
            defense += 10;
            weapon += 10;//actual weapon damage + 10
        }

        public void death()
        {
            Console.WriteLine("Meghaltál!");
            
            
        }
    }
    
    //Ha ügyesek vagyunk akkor jó lesz. Szóval itt öröklődnek a Character class-ból a
    //tulajdonsáhok tovább: Mate, Enemy osztályba. Ahol csak az adott csoportra jellemző tulajdonságok lesznek
    //ott lehet látni pluszban még functionoket: public int damage. Mert ez csak az enemy tulajdonsága lesz
    
    public class Mate() : Characters
    {
        public void Fighter()
        {
            
        }

        public void Ranger()
        {
            
        }

        public void Sorcerer()
        {
            
        }

        public void Rogue()
        {

        }
    }

    public class Enemy : Characters
    {
        public int damage; //if enemy

        public void Goblin() //Void!!
        {

        }

        public void Rat()
        {
            
        }

        public void Sceleton()
        {
            
        }

        public void Orc()
        {
            
        }

        public void Werewolf()
        {
            
        }
        public void giantSpider()
        {
            
        }

        public void Demon()
        {
            
        }
}
    public static void Main(string[] args)
    {
        
    }
}
//AZ rpg játék...
//Feladatok kiosztása
//Csapatmunka összehozása (mikor, mennyit, mit)
//Következő: játékmenet kiötletelése (menet közben nagyban módusulhat)
//teszt