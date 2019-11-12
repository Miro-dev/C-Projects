using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monster: LivingCreature
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardEXPPoints { get; set; }
        public int RewardCoins { get; set; }
        public List<LootItem> LootTable { get; set; }

        public Monster(int id, string name, int maximumDamage,
            int rewardEXPPoints, int rewardCoins, int currentMeatLeft,
            int maximumMeatLeft)
            :base(currentMeatLeft, maximumMeatLeft)
        {
            ID = id;
            Name = name;
            MaximumDamage = maximumDamage;
            RewardEXPPoints = rewardEXPPoints;
            RewardCoins = rewardCoins;

            LootTable = new List<LootItem>();
        }
    }
}
