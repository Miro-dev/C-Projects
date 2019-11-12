using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public int OccupyingSlots { get; set; }
        public string Description { get; set; }

        public Item(int id, string name, string namePlural, int occupyingSlots, string description)
        {
            ID = id;
            Name = name;
            NamePlural = namePlural;
            OccupyingSlots = occupyingSlots;
            Description = description;
        }
    }
}
