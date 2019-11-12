using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public PictureBox {get;set;}

        //public Quest QuestHere { get; set; }
        public Item ItemNeededToEnter { get; set; }
        public Enemy EnemyHere { get; set; }
        public List<LootItem> LootTableLocation { get; set; }
        public Location LocationToNorth { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToWest { get; set; }

        public Location(int id, string name, string description, Item itemNeededToEnter = null)
        {
            ID = id;
            Name = name;
            Description = description;
            //CrossTime = crossTime;
            //HealthCost = healthCost;
            //QuestHere = questHere;
            ItemNeededToEnter = itemNeededToEnter;
            LootTableLocation = new List<LootItem>();
        }
    }
}
