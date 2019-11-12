using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Enemy: LivingCreature
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int RewardXP { get; set; }
        public int RewardGold { get; set; }
        public List<LootItem> LootTable { get; set; }

        public Enemy(string name, int id, int rewardXP, int rewardGold, int baseHP,int currentHP,int maxHP, int baseStamina,int maxStamina,int currentStamina, 
            int baseDefense,int maxDefense, int currentDefense, int baseDodge,int maxDodge,int currentDodge, int baseDMG,int maxDMG,int currentDMG, int baseAccuracy,
            int maxAccuracy, int currentAccuracy, bool aggressive, bool isDead):
            base(baseHP, maxHP, currentHP,baseStamina, maxStamina,currentStamina, baseDefense, maxDefense, 
                currentDefense, baseDodge,maxDodge,currentDodge, baseDMG,maxDMG,currentDMG, baseAccuracy,maxAccuracy,currentAccuracy, aggressive, isDead)
        {
            Name = name;
            ID = id;
            RewardXP = rewardXP;
            RewardGold = rewardGold;
        }

        /*
        public Weapon();
        public Armor();
        */
    }
}
