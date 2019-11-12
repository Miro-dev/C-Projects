using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Enemy> Enemies = new List<Enemy>();
        public static readonly List<Location> Locations = new List<Location>();
        //public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<LocationEnvironment> LocationEnvironments = new List<LocationEnvironment>();

        public const int ITEM_ID_JAREX_SWORD = 1;
        public const int ITEM_ID_NOTE = 2;
        public const int ITEM_ID_CLUB = 3;
        public const int ITEM_ID_GOLD = 4;

        public const int ENEMY_ID_BANDIT = 1;
        public const int ENEMY_ID_WOLF = 2;

        public const int LOCATION_ID_FIELD = 1;
        public const int LOCATION_ID_FORTRESS = 2;
        public const int LOCATION_ID_FOREST = 3;
        public const int LOCATION_ID_FOREST1 = 4;
        public const int LOCATION_ID_FOREST2 = 5;
        public const int LOCATION_ID_FOREST3 = 6;
        public const int LOCATION_ID_FOREST4 = 7;
        public const int LOCATION_ID_FOREST5 = 8;

        //public const int QUEST_ID_KILL_BANDIT = 1;

        public const int LOCATION_ENVIRONMENT_ID_ROCKY = 1;

        static World()
        {
            PopulateItems();
            PopulateEnemies();
            PopulateLocations();
            //PopulateQuests();
            //PopulateLocationEnvironment();
        }

        private static void PopulateItems()
        {
            /*Random*/
            Items.Add(new Weapon(ITEM_ID_JAREX_SWORD,"Jarex Sword","Jarex Swords",5,"A sword from the East Empires.",15,8,25,25,1,15,20));
            Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 6, "A club.", 30, 0, 18, 25, 2, 15, 20));
            //Item MailA = new Item();
            Items.Add(new Item(ITEM_ID_NOTE, "Council Decree", "Council Decrees", 1, "Its a Decree from the High Council"));
            Items.Add(new Item(ITEM_ID_GOLD, "Gold","Gold", 0, "Currency of the Seven Kingdoms"));
        }

        private static void PopulateEnemies()
        {
            /*Random*/
            Enemy Bandit = new Enemy("Bandit", ENEMY_ID_BANDIT, 20, 30, 120, 120, 120, 100, 100, 100, 15, 15, 15, 10, 10, 10, 20, 20, 20, 15, 15, 15, true,false);
            Enemy Wolf = new Enemy("Wolf", ENEMY_ID_WOLF,20,5, 60, 60,60, 80, 80, 80, 10, 10, 10, 10, 25, 10, 25, 25, 25, 25, 25, 25, true, false);
            //rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
            Enemies.Add(Bandit); // Added to List
            Enemies.Add(Wolf);
        }

        private static void PopulateLocations()
        {
            /*Random*/
            Location field = new Location(LOCATION_ID_FIELD,"Field","It's a grassy field");
            field.EnemyHere = EnemyByID(ENEMY_ID_WOLF);
            field.LootTableLocation.Add(new LootItem(ItemByID(ITEM_ID_NOTE), 100,3));

            Location fortress = new Location(LOCATION_ID_FORTRESS, "Fortress", "It's a ruined Fortress", ItemByID(ITEM_ID_NOTE));
            fortress.EnemyHere = EnemyByID(ENEMY_ID_BANDIT);
            //rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));

            Location forest = new Location(LOCATION_ID_FOREST, "Forest", "It's a forest");

            Locations.Add(field);
            Locations.Add(fortress);// Added to Lisт
            Locations.Add(forest);

            field.LocationToEast = fortress;

            fortress.LocationToWest = field;

            fortress.LocationToNorth = forest;

            forest.LocationToSouth = fortress;

        }

        /*private static void PopulateQuests()
        {
            Random
            Quest KillTheBandit = new Quest(QUEST_ID_KILL_BANDIT,"Kill the Bandit!","Just kill the bandit.",30,10);
            //rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));

            Quests.Add(KillTheBandit); // Added to List
        }

        private static void PopulateLocationEnvironment()
        {
            //Random
            LocationEnvironment Rocky = new LocationEnvironment(LOCATION_ENVIRONMENT_ID_ROCKY, "Rocky Terrain","Rocky terrain: +5q, -12dodge",5,0,0,true);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));

            LocationEnvironments.Add(Rocky);
        }*/

        public static Item ItemByID(int id)
        {
            foreach (Item item in Items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static Enemy EnemyByID(int id)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.ID == id)
                {
                    return enemy;
                }
            }

            return null;
        }

        /*public static Quest QuestByID(int id)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }

            return null;
        }*/

        public static Location LocationByID(int id)
        {
            foreach (Location location in Locations)
            {
                if (location.ID == id)
                {
                    return location;
                }
            }

            return null;
        }

        /*public static LocationEnvironment TerrainByID(int id)
        {
            foreach (LocationEnvironment locationEnvironment in LocationEnvironments)
            {
                if (locationEnvironment.ID == id)
                {
                    return locationEnvironment;
                }
            }
            return null;

        }*/
    } 
}
