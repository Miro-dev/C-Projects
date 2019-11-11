using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermonArena
{
    public class Actor
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public int HealthBase { get; set; }

        public int DodgeStat { get; set; }

        public int StaminaBase { get; set; }

        public int AccuracyBase { get; set; }

        public int StaminaRegenBase { get; set; }

        public bool IsDead { get; set; }

        public int AttackSpeedBase { get; set; }

        // Properties
        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                // validate the input
                _health = value;
                if (value > HealthBase)
                {
                    _health = HealthBase;
                }

                if (value <= 0)
                {
                    _health = 0;
                }
            }
        }

        public int Stamina
        {
            get
            {
                return _stamina;
            }

            set
            {
                // validate the input
                _stamina = value;
                if (value > StaminaBase)
                {
                    _stamina = StaminaBase;
                }

                if (value <= 0)
                {
                    _stamina = 0;
                }
            }
        }

        public int StaminaRegen
        {
            get
            {
                return _staminaRegen;
            }

            set
            {
                // validate the input
                _staminaRegen = value;
                if (value > StaminaRegenBase)
                {
                    _staminaRegen = StaminaRegenBase;
                }

                if (value <= 0)
                {
                    _staminaRegen = 0;
                }
            }
        }

        public int AttackSpeed
        {
            get
            {
                return _attackSpeed;
            }

            set
            {
                // validate the input
                _attackSpeed = value;

                if (value <= 0)
                {
                    _attackSpeed = 0;
                }
            }
        }

        public int Accuracy
        {
            get
            {
                return _accuracy;
            }

            set
            {
                // validate the input
                _accuracy = value;
                if (value > this.AccuracyBase)
                {
                    this._accuracy = this.AccuracyBase;
                }

                if (value <= 0)
                {
                    this._accuracy = 0;
                }
            }
        }

        public Weapon CurrentWeapon { get; set; }

        public Armor CurrentArmor { get; set; }

        protected internal int _health;
        protected internal int _stamina;
        protected internal int _staminaRegen;
        protected internal int _attackSpeed;
        protected internal int _accuracy;

        public Actor(string name, int iD, int healthBase, int dodgeStat, int staminaBase, int accuracyBase, int staminaRegenBase, bool isDead, int attackSpeedBase, int health, int stamina, int staminaRegen, int attackSpeed, int accuracy, Weapon currentWeapon, Armor currentArmor)
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

        public virtual void Hit(Actor HitPlayer, int WithStamina)
        {
            do
            {
                int damageDone;


                Stamina -= this.CurrentWeapon.StaminaModifier;

                damageDone = (EntryPoint._rnd.Next(this.CurrentWeapon.DMG_MIN, this.CurrentWeapon.DMG_MAX) + WithStamina / 3) - EntryPoint._rnd.Next(HitPlayer.CurrentArmor.BLOCK_MIN, HitPlayer.CurrentArmor.BLOCK_MAX);
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

        public bool Dodge(Actor player)
        {
            // If you are not missed and you try to DODGE!

            // Can you dodge?
            if (Stamina < this.CurrentArmor.DodgeModifier + HealthBase * 20 / 100)
            {
                Console.WriteLine("{0} is too fatigued to dodge!", Name);
                return false;
            }

            // Is the Dodge successful?
            int PercentageToHit = DodgeStat - this.CurrentArmor.AttackSpeedModifier;
            int ValueDodge = EntryPoint._rnd.Next(100);
            if (ValueDodge < PercentageToHit)
            {
                player.Stamina -= (player.CurrentWeapon.StaminaModifier + player.StaminaRegen) * 2;
                Console.WriteLine("{0} Misses {1}! {0} Stamina - {2}. Stamina= {3}",
                    player.Name, Name, player.CurrentWeapon.StaminaModifier + player.StaminaRegen, player.Stamina);
                return true;
            }

            Console.WriteLine("{0} could not dodge {1}'s attack!",
                    Name, player.Name);
            return false;
        }

        public bool IsStaminaEnough(Actor player)
        {
            // Do you hit a stationary target?

            // Do you have enough stamina?
            if (player.Stamina < player.CurrentWeapon.StaminaModifier)
            {
                Console.WriteLine("{0} does not have enought stamina to swing {1}!", player.Name, player.CurrentWeapon.Name);
                return false;
            }

            return true;
        }

        public void Block(Actor player)
        {
            // Block an attack!
        }
    }
}
