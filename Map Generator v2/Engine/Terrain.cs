using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LocationEnvironment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CrossTime { get; set; }
        public int HealthCost { get; set; }
        public int HealthGain { get; set; }
        public bool CanForage { get; set; }

        public LocationEnvironment(int id,string name, string description, int crossTime, int healthCost,int healthGain,bool canForage)
        {
            ID = id;
            Name = name;
            Description = description;
            CrossTime = crossTime;
            HealthCost = healthCost;
            HealthGain = healthGain;
            CanForage = canForage;
        }
    }
}
