using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermonArena
{
    public class Player:Actor
    {
        public Player(string name, int iD, int healthBase, int dodgeStat, int staminaBase, int accuracyBase, int staminaRegenBase, bool isDead, int attackSpeedBase, int health, int stamina, int staminaRegen, int attackSpeed, int accuracy, Weapon currentWeapon, Armor currentArmor):base
            (name, iD, healthBase,dodgeStat,staminaBase,  accuracyBase, staminaRegenBase,  isDead,  attackSpeedBase, health, stamina,  staminaRegen,  attackSpeed,  accuracy,  currentWeapon, currentArmor)
        {
            Name = name;
            ID = iD;
            HealthBase = healthBase;
            DodgeStat = dodgeStat;
            StaminaBase = staminaBase;
            this.AccuracyBase = accuracyBase;
            StaminaRegenBase = staminaRegenBase;
            IsDead = isDead;
            this.AttackSpeedBase = attackSpeedBase;
            _health = health;
            _stamina = stamina;
            _staminaRegen = staminaRegen;
            _attackSpeed = attackSpeed;
            _accuracy = accuracy;
            this.CurrentWeapon = currentWeapon;
            this.CurrentArmor = currentArmor;
        }
    }
}
