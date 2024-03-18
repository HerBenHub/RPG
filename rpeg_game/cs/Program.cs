using System.Security.Cryptography;
using System.Text.Json;
using JsonManager;
using RitkasagManager;
using BattleManager;

using System.Xml.Serialization;
using Spectre.Console;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;

namespace MainProgram
{
    public class Program
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
            public int points;
            public Hero? eredeti;
            public int level;
            public bool canFly;

            public Characters(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage,
                int _priority, int _points, Hero? _eredeti, int _level, bool _canFly)
            {
                name = _name;
                hp = _hp;
                defense = _defense;
                weapon = _weapon;
                armour = _armour;
                damage = _damage;
                priority = _priority;
                eredeti = _eredeti;
                points = _points;
                level = _level;
                canFly = _canFly;

            }

            //Static!!!
            public void levelUp(Hero? hero)
            {
                hero.level += 1;
                hero.damage = hero.damage * 1.1;
                hero.eredeti.damage = hero.eredeti.damage * 1.1;
                hero.hp = +10;
                hero.eredeti.hp += 10;

                Math.Round(hero.damage, 2);
                Math.Round(hero.eredeti.damage, 2);

            }

            public void death()
            {
                Console.WriteLine("Meghaltál!");
                bool fromStart = true;

            }
        }

        //Ha ügyesek vagyunk akkor jó lesz. Szóval itt öröklődnek a Character class-ból a
        //tulajdonsáhok tovább: Fighter, Enemy osztályba. Ahol csak az adott csoportra jellemző tulajdonságok lesznek
        //ott lehet látni pluszban még functionoket: public int damage. Mert ez csak az enemy tulajdonsága lesz

        public class Enemy : Characters
        {
            // private double damage = 0;
            public Enemy(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage,
                int _priority, int _points,
                Hero? _eredeti, int _level, bool _canFly) : base(_name, _hp, _defense, _weapon, _armour, _damage,
                _priority, _points, _eredeti, _level, _canFly)
            {

            }
        }

        public class Hero : Characters
        {
            public Hero(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage,
                int _priority, int _points,
                Hero? _eredeti, int _level, bool _canFly) : base(_name, _hp, _defense, _weapon, _armour, _damage,
                _priority, _points, _eredeti, _level, _canFly)
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
            switch (character)
            {

                case 1:
                    hero = new Hero("Fighter", 100, 100, "sima_kard", "nincs", 1, 10, 10, null, 1, false);
                    break;
                case 2:
                    hero = new Hero("Ranger", 80, 80, "íj", "nincs", 1, 18, 10, null, 1, false);
                    break;
                case 3:
                    hero = new Hero("Sorcerer", 80, 80, "bot", "nincs", 1, 18, 10, null, 1, false);
                    break;
                case 4:
                    hero = new Hero("Rogue", 80, 80, "penge", "nincs", 1, 22, 10, null, 1, false);
                    break;
            }

            hero.eredeti = new Hero(hero.name, hero.hp, hero.defense, hero.weapon, hero.armour, hero.damage,
                hero.priority, hero.points, null, 1, false);

            csapat?.Add(hero);

            return hero;
        }

        public static Enemy createEnemy(int chooseEnemy)
        {
            Enemy? enemy = null;

            switch (chooseEnemy) {
                case 1:
                    enemy = new Enemy("Goblin", 25, 20, "nincs", "nincs", 30, 10, 20, null, 1, false);
                    break;
                case 2:
                    enemy = new Enemy("Giant Rat", 20, 50, "nincs", "nincs", 20, 7,30 , null, 1, false);
                    break;
                case 3:
                    enemy = new Enemy("Giant Spider", 20, 80, "nincs", "nincs", 30, 7, 15, null, 1, false);
                    break;
                case 4:
                    enemy = new Enemy("Skeleton", 50, 90, "nincs", "nincs", 70, 8, 20, null, 1, false);
                    break;
                case 5:
                    enemy = new Enemy("Werewolf", 100, 95, "nincs", "nincs", 90, 12, 15, null, 1, false);
                    break;
                case 6:
                    enemy = new Enemy("Orc", 120, 130, "nincs", "nincs", 140, 11, 5, null, 1, false);
                    break;
                case 7:
                    enemy = new Enemy("Demon", 140, 100, "nincs", "nincs", 120, 13, 25, null, 1, true);
                    break;
            }

            return enemy;
        }

        public static Enemy miniBoss(int chooseEnemy)
        {
            //Mother spider generate
            int miniBossInt = 6;
            bool externalCondition = true;
            createEnemy(chooseEnemy);


            //Mother spider generate

            // createEnemy(miniBossInt, externalCondition);

            Enemy? motherSpider = createEnemy(chooseEnemy);
            motherSpider.name = "Broodmother";
            motherSpider.damage = 150;
            motherSpider.hp = 140;

            return motherSpider;
        }

        public static Enemy mainBoss()
        {
            Enemy? mainBoss;
            mainBoss = new Enemy("Dragon", 300, 160, "nincs", "nincs", 150, 20, 10, null, 5, true);
            return mainBoss;
        }

        public class Items
        {
            public static List<string> inventory = new List<string>();
            public static void EquipSword(Hero? hero, string weapon, Dictionary<string, FegyverAdatok>? fegyverek)
            {
                if (fegyverek[weapon].tipus == hero.eredeti.name)
                {
                    inventory.Add(hero.weapon);
                    inventory.Remove(weapon);

                    hero.weapon = weapon;
                }
                
            }
            public static void EquipArmor(Hero? hero, string armour, Dictionary<string, PancelAdatok>? pancelok)//armour file read!!
            {
    
                if (pancelok[armour].tipus == hero.eredeti.name)
                {

                    inventory.Add(hero.armour);
                    inventory.Remove(armour);

                    hero.defense = hero.eredeti.defense;
                    Console.WriteLine(hero.eredeti.defense);
                    hero.armour = armour;
                    //if?
                    hero.hp = (int)Math.Round(hero.hp * pancelok[armour].ertekek["hp"]);
                    hero.defense = (int)Math.Round(hero.defense + pancelok[armour].ertekek["defense"]);
                    hero.damage = (int)Math.Round(hero.damage * pancelok[armour].ertekek["damage"]);
                    Console.WriteLine(hero.eredeti.defense);
                    Console.WriteLine(hero.defense);
                }
            }
        }

        public static void Main(string[] args)
        {

            Dictionary<string, FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();

            //Console.WriteLine(RitkasagSzamolo.LadaLootGenerate("legendary"));

            // string fileName = @"json/szovegek.json";
            // JsonOlvaso.ProcessJsonFile(fileName);



            Hero? player = createCharacter();
            Hero? player_eredeti = player.eredeti;

            Hero? seged1 = createCharacter(2);
            Enemy? enemy1 = createEnemy(2);

            player.name = "asbvbsa";
        
            Items.EquipSword(player, "nagypenge", fegyverek);
            
            Battles.StartBattle([player, seged1], [enemy1]);


            //Értékek elérése
            //////////////////////////////////////////////////////////

            ///foreach (Hero tars in csapat)
            // {
            //     System.Console.WriteLine(tars.name);
            // }

            // System.Console.WriteLine(player?.name);
            // System.Console.WriteLine(player_eredeti?.name);

            // Console.WriteLine(fegyverek["sima_kard"].sebzes);
            // foreach (var elemek in fegyverek)
            // {
            //     Console.WriteLine(elemek.Value.nev);
            // }

            ////////////////////////////////////////////////////////


        }
    }
}