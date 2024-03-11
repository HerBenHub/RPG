using System.Security.Cryptography;
using System.Text.Json;
using JsonManager;
using RitkasagManager;

using System.Xml.Serialization;
using Spectre.Console;
using System.Runtime.InteropServices;
using System.ComponentModel;

internal class Program
{
    public class Characters
    {
        public string name;
        public int hp;
        public int defense; //defense
        public string weapon; //attack
        public double damage;
        public string armour;
        public int priority;
        public Hero? eredeti;

        public Characters(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage, int _priority, Hero? _eredeti)
        {
            name = _name;
            hp = _hp;
            defense = _defense;
            weapon = _weapon;
            armour = _armour;
            damage = _damage;
            priority = _priority;
            eredeti = _eredeti;
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
    
    public class Enemy : Characters
    {
        // private double damage = 0;
        public Enemy(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage, int _priority, Hero? _eredeti) : base(_name, _hp, _defense, _weapon, _armour, _damage, _priority, _eredeti)
        {
            damage = _damage;
        }
    }
    
    public class Hero : Characters
    {
        public Hero(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage, int _priority, Hero? _eredeti) : base(_name, _hp, _defense, _weapon, _armour, _damage, _priority, _eredeti)
        {
            
        }
    }

    static List<Hero>? csapat = new List<Hero>();
    public static Hero? createCharacter(int classnumber = 0)
    {
        int character = classnumber;

        if (classnumber == 0)
        {
            Console.WriteLine("1. Fighter");
            Console.WriteLine("2. Ranger");
            Console.WriteLine("3. Sorcerer");
            Console.WriteLine("4. Rogue");
            Console.WriteLine("\nAdd meg melyik karakterrel szeretnél lenni!");
            character = Convert.ToInt16(Console.ReadLine());
        }

        Hero? hero = null;
        switch (character){
            
            case 1:
                hero = new Hero("Fighter", 100, 100, "sima_kard", "nincs", 1, 10, null);
                break;
            case 2:
                hero = new Hero("Ranger", 80, 80, "íj", "nincs", 1, 18, null);
                break;
            case 3:
                hero = new Hero("Sorcerer", 80, 80, "bot", "nincs", 1, 18, null);
                break;
            case 4:
                hero = new Hero("Rogue", 80, 80, "penge", "nincs", 1, 22, null);
                break;
        }

        hero.eredeti = new Hero(hero.name, hero.hp, hero.defense, hero.weapon, hero.armour, hero.damage, hero.priority, null);

        csapat?.Add(hero);

        return hero;
        

    }

    public void charTools()
    {
        
    }

    public static void Main(string[] args)
    {

        Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
        
        Console.WriteLine(RitkasagSzamolo.LadaLootGenerate("legendary"));
        
        // string fileName = @"json/szovegek.json";
        // JsonOlvaso.ProcessJsonFile(fileName);
        
        

        Hero? player = createCharacter();
        Hero? player_eredeti = player.eredeti; 

        Hero? seged1 = createCharacter(2);

        foreach (Hero tars in csapat)
        {
            System.Console.WriteLine(tars.name);
        }

        player.name = "asbvbsa";

        System.Console.WriteLine(player?.name);
        System.Console.WriteLine(player_eredeti?.name);

        //Értékek elérése
        ////////////////////////////////////////////////////////
        // Console.WriteLine(fegyverek["sima_kard"].sebzes);
        // foreach (var elemek in fegyverek)
        // {
        //     Console.WriteLine(elemek.Value.nev);
        // }
        ////////////////////////////////////////////////////////
        

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