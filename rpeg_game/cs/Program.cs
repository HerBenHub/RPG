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
using inventoryManager;
using mapGenerate;
using test;

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
            public Characters? eredeti;
            public int level;
            public bool canFly;

            public Characters(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage,
                int _priority, int _points, Characters? _eredeti, int _level, bool _canFly)
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
                hero.damage = Math.Round(hero.damage * 1.1,2);
                hero.eredeti.damage = Math.Round(hero.eredeti.damage * 1.1,2);
                hero.hp = (int)Math.Round(hero.hp * 1.1);
                hero.eredeti.hp += 10;

                hero.points = (int)Math.Round(hero.points * 1.1);
                hero.eredeti.points = (int)Math.Round(hero.eredeti.points * 1.1);

            }
        }

        public class Enemy : Characters
        {
            // private double damage = 0;
            public Enemy(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage,
                int _priority, int _points,
                Characters? _eredeti, int _level, bool _canFly) : base(_name, _hp, _defense, _weapon, _armour, _damage,
                _priority, _points, _eredeti, _level, _canFly)
            {

            }
        }

        public class Hero : Characters
        {
            //Itt private változó, aminek konstruktora a kapcsos zárójelben és Hero()-be is bele kell rakni
            public Hero(string _name, int _hp, int _defense, string _weapon, string _armour, double _damage,
                int _priority, int _points,
                Characters? _eredeti, int _level, bool _canFly) : base(_name, _hp, _defense, _weapon, _armour, _damage,
                _priority, _points, _eredeti, _level, _canFly)
            {
                //Itt csak a hero konstruktora, ami csak a herora fog vonatkozni
                
            }
        }

        public static List<Hero>? csapat = new List<Hero>();

        public static Hero? createCharacter(string className = "none")
        {
            if (className == "none")
            {
                className = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Add meg melyik karakterrel szeretnél lenni!")   
                        .PageSize(5)
                        .HighlightStyle(new Style(new Color(100,150,100)))
                        .AddChoices(["Fighter","Ranger","Sorcerer","Rogue"]));
            }


            Hero? hero = null;
            switch (className)
            {

                case "Fighter":
                    hero = new Hero("Fighter", 100, 100, "sima_kard", "sima_pancel", 1, 10, 25, null, 1, false);
                    break;
                case "Ranger":
                    hero = new Hero("Ranger", 80, 80, "íj", "nincs", 1, 18, 15, null, 1, false);
                    break;
                case "Sorcerer":
                    hero = new Hero("Sorcerer", 80, 80, "bot", "nincs", 1, 18, 15, null, 1, false);
                    break;
                case "Rogue":
                    hero = new Hero("Rogue", 80, 80, "penge", "nincs", 1, 22, 25, null, 1, false);
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
                    enemy = new Enemy("Demon", 90, 100, "nincs", "nincs", 120, 13, 25, null, 1, true);
                    break;
            }

            enemy.eredeti = new Enemy(enemy.name, enemy.hp, enemy.defense, enemy.weapon, enemy.armour, enemy.damage,enemy.priority, enemy.points, null, enemy.level, enemy.canFly);

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

        public static void Main(string[] args)
        {

            Dictionary<string, FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();

            //Console.WriteLine(RitkasagSzamolo.LadaLootGenerate("legendary"));

            // string fileName = @"json/szovegek.json";
            // JsonOlvaso.ProcessJsonFile(fileName);



            // Hero? player = createCharacter();
            //
            // Hero? seged1 = createCharacter("Ranger");
            // Enemy? enemy1 = createEnemy(6);
            //
            // player.name = "asbvbsa";
            // Items.inventory.Add("szeltoro");
            // Items.inventory.Add("nem_sima_pancel");
            
            // Console.WriteLine($"{player} {player.hp} {player.defense} {player.damage} {player.weapon} {player.armour}");
            // foreach (string item in Items.inventory)
            // {
            //     Console.WriteLine(item);
            // }
            
            // Items.EquipArmor(player, "nem_sima_pancel", pancelok);
            // Items.EquipSword(player, "szeltoro", fegyverek);

            // Console.WriteLine($"{player} {player.hp} {player.defense} {player.damage} {player.weapon} {player.armour}");
            // foreach (string item in Items.inventory)
            // {
            //     Console.WriteLine(item);
            // }

            //generateMap.genMap();
            tester.testing();

            // viewInventory.Tarhely(csapat);
            // viewInventory.Itemval(csapat);

            //Battles.StartBattle([player, seged1], [enemy1]);


            //Értékek elérése
            //////////////////////////////////////////////////////////

            // foreach (Hero tars in csapat)
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