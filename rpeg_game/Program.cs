using System.Xml.Serialization;

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
            //*0,1 hp
            hp += 10;
            defense += 10;
            weapon += 10;//actual weapon damage + 10
        }

        public void getValues()
        {
            //Import JSON
        }

        public void death()
        {
            Console.WriteLine("Meghaltál!");
        }
    }
    
    //Ha ügyesek vagyunk akkor jó lesz. Szóval itt öröklődnek a Character class-ból a
    //tulajdonsáhok tovább: Fighter, Enemy osztályba. Ahol csak az adott csoportra jellemző tulajdonságok lesznek
    //ott lehet látni pluszban még functionoket: public int damage. Mert ez csak az enemy tulajdonsága lesz
    
    public class Fighter : Characters
    {
        private int damage = 0;
        public Fighter(string _name, int _hp, int _defense, int _weapon, int _armour, int _damage) : base(_name, _hp, _defense, _weapon, _armour)
        {
            damage = _damage;
        }
    }
    
    public class Hero : Characters
    {
        public Hero(string _name, int _hp, int _defense, int _weapon, int _armour) : base(_name, _hp, _defense, _weapon, _armour)
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
//Menu mindig lefut egy érték átadás miatt, 
//Menut megcsinálni a main függvényben
//Kiválasztjuk a játékost majd ahhoz kapunk bizonyos felszereltségi szintet(armour, weapon, defense rate)