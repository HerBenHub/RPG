using System.Security.Cryptography;
using System.Text.Json;
using JsonManager;
using RitkasagManager;

using System.Xml.Serialization;
using Spectre.Console;

internal class Program
{
    public class Characters
    {
        public string name;
        public int hp;
        public int defense; //defense
        public string weapon; //attack
        public int damage;
        public string armour;
        public int priority;

        public Characters(string _name, int _hp, int _defense, string _weapon, string _armour, int _priority)
        {
            name = _name;
            hp = _hp;
            defense = _defense;
            weapon = _weapon;
            armour = _armour;
            priority = _priority;
        }
        //Static!!!
        public void levelUp()
        {
            hp += 10;
            defense += 10;
            
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
        public Fighter(string _name, int _hp, int _defense, string _weapon, string _armour, int _damage, int _priority) : base(_name, _hp, _defense, _weapon, _armour, _priority)
        {
            damage = _damage;
        }
    }
    
    public class Hero : Characters
    {
        public Hero(string _name, int _hp, int _defense, string _weapon, string _armour, int _priority) : base(_name, _hp, _defense, _weapon, _armour, _priority)
        {
            
        }
    }

    public static Hero chooseCharacter()
    {
        //Játékosok nevei
        Console.WriteLine("1. Fighter");
        Console.WriteLine("2. Ranger");
        Console.WriteLine("3. Sorcerer");
        Console.WriteLine("4. Rogue");
        Console.WriteLine();
        Console.WriteLine("Add meg melyik karakterrel szeretnél leni!");
        int character = Convert.ToInt16(Console.ReadLine());
        
        Hero hero;
        switch (character)
        {
            
            case 1:
                hero = new Hero("Fighter", 100, 100, "sima_kard", "", 10);
                break;
            case 2:
                hero = new Hero("Ranger", 80, 80, "íj", "nincs", 18);
                break;
            case 3:
                hero = new Hero("Sorcerer", 80, 80, "bot", "nincs", 18);
                break;
            case 4:
                hero = new Hero("Rogue", 80, 80, "penge", "nincs", 22);
                break;
        }
        
        //Local variable!!
        return hero;
        

    }

    public void charTools()
    {
        
    }

    public static void Main(string[] args)
    {

        Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
        
        Console.WriteLine(RitkasagSzamolo.LadaLootGenerate("legendary"));
        
        string fileName = @"../../../json/szovegek.json";
        // JsonOlvaso.ProcessJsonFile(fileName);

        chooseCharacter();
        foreach (var elemek in fegyverek)
        {
            Console.WriteLine(elemek.Value.nev);
        }
        //Értékek elérése
        Console.WriteLine(fegyverek["sima_kard"].sebzes);
        

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
//Játékmenet