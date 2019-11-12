using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Weapon: Item
    {
        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }
        public int MaxDurability { get; set; }
        public int CurrentDurability { get; set; }
        public int DamageType { get; set; }//1=Sharp, 2=Blunt
        public int Crit { get; set; }
        public int Block { get; set; }

        public Weapon(int id, string name,string namePlural,int occupyingSlots,string description,int maxDamage,int minDamage, 
            int maxDurability,int currentDurabilitystring, int damageType, int crit, int block):base(id, name, namePlural,occupyingSlots, description)
        {
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            MaxDurability = MaxDurability;
            CurrentDurability = currentDurabilitystring;
            DamageType = damageType;
            Crit = crit;
            Block = block;
        }
    }
}
