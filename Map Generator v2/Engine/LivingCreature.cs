using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LivingCreature
    {
        public int BaseHP { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }

        public int BaseStamina { get; set; }
        public int MaxStamina { get; set; }
        public int CurrentStamina { get; set; }

        public int BaseDefense { get; set; }
        public int MaxDefense { get; set; }
        public int CurrentDefense { get; set; }

        public int BaseDodge { get; set; }
        public int MaxDodge { get; set; }
        public int CurrentDodge { get; set; }

        public int BaseDMG { get; set; }
        public int MaxDMG { get; set; }
        public int CurrentDMG { get; set; }

        public int BaseAccuracy { get; set; }
        public int MaxAccuracy { get; set; }
        public int CurrentAccuracy { get; set; }

        public bool Aggressive { get; set; }
        public bool Passing { get; set; }

        public bool IsDead { get; set; }

        public LivingCreature (int baseHP, int maxHP, int currentHP, int baseStamina, int maxStamina, int currentStamina, 
            int baseDefense, int maxDefense, int currentDefense, int baseDodge, int maxDodge, int currentDodge, 
            int baseDMG,int maxDMG,int currentDMG,int baseAccuracy,int maxAccuracy,int currentAccuracy, bool aggressive, bool isDead)
        {
            BaseHP = baseHP;
            MaxHP = maxHP;
            CurrentHP = currentHP;

            BaseStamina = baseStamina;
            MaxStamina = maxStamina;
            CurrentStamina = currentStamina;

            BaseDefense = baseDefense;
            MaxDefense = maxDefense;
            CurrentDefense = currentDefense;

            BaseDodge = baseDodge;
            MaxDodge = maxDodge;
            CurrentDodge = currentDodge;

            BaseDMG = baseDMG;
            MaxDMG = maxDMG;
            CurrentDMG = currentDMG;

            BaseAccuracy = baseAccuracy;
            MaxAccuracy = maxAccuracy;
            CurrentAccuracy = currentAccuracy;

            Aggressive = aggressive;
            IsDead = isDead;
        }
    }
}
