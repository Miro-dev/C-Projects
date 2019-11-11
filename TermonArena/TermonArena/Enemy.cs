using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermonArena
{
    public class Enemy : Actor
    {
        public Enemy(string name, int iD, int healthBase, int dodgeStat, int staminaBase, int accuracyBase, int staminaRegenBase, bool isDead, int attackSpeedBase, int health, int stamina, int staminaRegen, int attackSpeed, int accuracy,int toGetHitInterval, Weapon currentWeapon, Armor currentArmor, int hitPoint) : base
            (name, iD, healthBase, dodgeStat, staminaBase, accuracyBase, staminaRegenBase, isDead, attackSpeedBase, health, stamina, staminaRegen, attackSpeed, accuracy, currentWeapon, currentArmor)
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
            ToGetHitInterval = toGetHitInterval;
            HitPoint = hitPoint;
        }

        public int ToGetHitInterval { get; set; }
        public int HitPoint { get; set; }

        public override void Hit(Actor HitPlayer, int WithStamina)
        {
            Console.WriteLine("Enemy Hits!");

            do
            {
                int damageDone;


                Stamina -= this.CurrentWeapon.StaminaModifier;

                damageDone = EntryPoint._rnd.Next(this.CurrentWeapon.DMG_MIN, this.CurrentWeapon.DMG_MAX) - EntryPoint._rnd.Next(HitPlayer.CurrentArmor.BLOCK_MIN, HitPlayer.CurrentArmor.BLOCK_MAX);
                if (damageDone < 0)
                {
                    damageDone = 0;
                }

                HitPlayer.Health -= damageDone;
                HitPlayer.Stamina -= damageDone * 2;
                Console.WriteLine("{0} hits {1} with {2}, for {3} dmg and burns {4} stamina!", Name, HitPlayer.Name, this.CurrentWeapon.Name, damageDone, damageDone * 2);

                if (damageDone >= HitPlayer.Health * 15 / 100 && damageDone <= HitPlayer.Health * 30 / 100)
                {
                    HitPlayer.AttackSpeed += 25;
                    Console.WriteLine("Punkturing the armor. Defending player AS = {0} + 25", HitPlayer.AttackSpeed);
                }
                else if (damageDone <= HitPlayer.Health * 15 / 100 && damageDone >= 0)
                {
                    Console.WriteLine("Glancing blow\n 15% = {0}", HitPlayer.Health * 15 / 100);
                }
                else if (damageDone >= HitPlayer.Health * 30 / 100 && damageDone <= HitPlayer.Health * 60 / 100)
                {
                    HitPlayer.AttackSpeed += 40;
                    Console.WriteLine("Shattering the armor. Defending player AS = {0} + 40\n 30% = {1}\n 60% = {2}", HitPlayer.AttackSpeed, HitPlayer.Health * 30 / 100, HitPlayer.Health * 60 / 100);
                }
                else if (damageDone >= HitPlayer.Health * 60 / 100)
                {
                    HitPlayer.AttackSpeed += 80;
                    Console.WriteLine("Crushing the body. Defending player AS = {0} + 80\n 60%: {1}", HitPlayer.AttackSpeed, HitPlayer.Health * 60 / 100);
                }

                if (HitPlayer.Health < 0 && HitPlayer.Health > -20)
                {
                    Console.WriteLine(HitPlayer.Name + " is dead!");
                }
                else if (HitPlayer.Health < -20)
                {
                    Console.WriteLine(HitPlayer.Name + " is Splatered!");
                }

                if (HitPlayer.Health > 0)
                {
                    Console.WriteLine("\n{0}\nHP: {1}\nStamina: {2}/{8}\nAttackSpeed: {3}/{9}\n\n{4}\nHP: {5}\nStamina: {6}/{10}\nAttackSpeed: {7}/{11}\n\n"
                        , HitPlayer.Name, HitPlayer.Health, HitPlayer.Stamina, HitPlayer.AttackSpeed,
                        Name, Health, Stamina, this.AttackSpeed
                        , HitPlayer.StaminaBase, HitPlayer.AttackSpeedBase,
                        StaminaBase, this.AttackSpeedBase);
                    Console.ReadLine();
                }

                break;
            } while (true);
        }
    }
}
