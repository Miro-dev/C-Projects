namespace TermonArena
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Timers;
    using System.Diagnostics;

    public class Weapon
    {
        public Weapon(string name, int iD, WeaponType weaponType, DamageType damageType, int dMG_MIN, int dMG_MAX, int attackSpeed, int staminaModifier, int blockstat)
        {
            this.Name = name;
            this.ID = iD;
            this.weaponType = weaponType;
            this.damageType = damageType;
            this.DMG_MIN = dMG_MIN;
            this.DMG_MAX = dMG_MAX;
            this.AttackSpeed = attackSpeed;
            this.StaminaModifier = staminaModifier;
            this.BlockStat = blockstat;
        }

        public string Name { get; set; }

        private int ID { get; set; }

        public WeaponType weaponType { get; set; }

        public DamageType damageType { get; set; }

        public int DMG_MIN { get; set; }

        public int DMG_MAX { get; set; }

        public int AttackSpeed { get; set; }

        public int StaminaModifier { get; set; }

        public int BlockStat { get; set; }
    }

    public class Armor
    {
        public Armor(int iD, ArmorType armorType, int bLOCK_MIN, int bLOCK_MAX, int attackSpeedModifier, int dodgeModifier, int staminaModifier)
        {
            ID = iD;
            this.armorType = armorType;
            this.BLOCK_MIN = bLOCK_MIN;
            this.BLOCK_MAX = bLOCK_MAX;
            this.AttackSpeedModifier = attackSpeedModifier;
            DodgeModifier = dodgeModifier;
            StaminaModifier = staminaModifier;
        }

        private int ID { get; set; }

        public ArmorType armorType { get; set; }

        public int BLOCK_MIN { get; set; }

        public int BLOCK_MAX { get; set; }

        public int AttackSpeedModifier { get; set; }

        public int DodgeModifier { get; set; }

        public int StaminaModifier { get; set; }
    }

    class State
    {
        public int Dodge { get; set; }

        public int StaminaModifier { get; set; }

        public int StaminaRegen { get; set; }

        public int AttackSpeed { get; set; }
    }

    // number * percentage / 100
    public enum ArmorType
    {
        Mail, Leather, Cloth
    }

    public enum DamageType
    {
        Blunt, Sharp, Magic
    }

    public enum WeaponType
    {
        Sword, Hammer, Sickel, Stilleto, Staff, Pike
    }

    public class EntryPoint
    {
        private static int StaminaUsed = 0;

        public static readonly Random _rnd = new Random();

        public static readonly Stopwatch stopWatch = new Stopwatch();

        private static int CounterChange = 1;

        public static void ChangeStaminaAndAttackSpeed(Player Player1, Enemy Player2)
        {
            if (Player1.AttackSpeed > Player1.AttackSpeedBase)
            {
                Player1.AttackSpeed -= 1;
            }

            if (Player2.AttackSpeed > Player2.AttackSpeedBase)
            {
                Player2.AttackSpeed -= 1;
            }

            Player2.Stamina += Player2.StaminaRegen;
            Player1.Stamina += Player1.StaminaRegen;
        }

        enum Distance { Touching, Close, Far }

        static int StaminaUsedCheck(int stamina, Actor actor)
        {
            do
            {
                if (stamina > actor.Stamina)
                {
                    Console.WriteLine("Stamina MAX.");
                    stamina = actor.Stamina;
                    StaminaUsed = stamina;
                    return stamina;
                }

                if (stamina <= 0)
                {
                    Console.WriteLine("Can't hit with 0 Stamina");
                    StaminaUsed = stamina;
                    return 0;
                }
                return 0;
            } while (stamina <= 0);
        }

        public static int AttackTiming(Actor actor)
        {
            Console.WriteLine("The attacker is: " + actor.Name);
            Console.WriteLine($"Land your attack! You have the {actor.AttackSpeed*100} sec Point. Press key when ready.");
            Console.WriteLine($"Write used stamina. Out of {actor.Stamina} and prepare!");
            StaminaUsedCheck(Int32.Parse(Console.ReadLine()), actor);
            Console.WriteLine("START!");
            stopWatch.Restart();
            Console.ReadKey();
            stopWatch.Stop();
            Console.WriteLine($"Your attack: {stopWatch.ElapsedMilliseconds} with precision point: {actor.AttackSpeed * 100} !");
            return (int)stopWatch.ElapsedMilliseconds;// find a way to parse this
        }

        public static int DecisionAttackType(int attackTime, Player player, Enemy enemy)
        {
            if (attackTime > enemy.HitPoint - enemy.ToGetHitInterval && attackTime < enemy.HitPoint + enemy.ToGetHitInterval)
            {
                //Hit
                return 1;
            }

            if (attackTime < enemy.HitPoint - enemy.ToGetHitInterval && attackTime > enemy.HitPoint - enemy.ToGetHitInterval*4)
            {
                //Dodge
                return 2;
            }

            if (attackTime > enemy.HitPoint + enemy.ToGetHitInterval && attackTime < enemy.HitPoint - enemy.ToGetHitInterval * 4)
            {
                //Block
                return 3;
            }

            //Parry
            return 0;
        }

        public static int WhoHits(Player Player1, Enemy Player2, string distance)
        {
            for (int Counter = CounterChange; Counter < 1000; Counter++)
            {
                Console.WriteLine("Counter is {0}!", CounterChange);
                Console.WriteLine("CurrentASP1: {0}!", Player1.AttackSpeed);
                Console.WriteLine("CurrentASP2: {0}!\n", Player2.AttackSpeed);
                if (Counter % Player1.AttackSpeed == 0 && Counter % Player2.AttackSpeed == 0)
                {
                    return 0;
                }

                if (Counter % Player1.AttackSpeed == 0)
                {
                    ChangeStaminaAndAttackSpeed(Player1, Player2);
                    CounterChange++;
                    return 1;
                }

                if (Counter % Player2.AttackSpeed == 0)
                {
                    ChangeStaminaAndAttackSpeed(Player1, Player2);
                    CounterChange++;
                    return 2;
                }

                ChangeStaminaAndAttackSpeed(Player1, Player2);

                CounterChange++;
            }

            return 0;
        }

        public static void Main()
        {
            Weapon HammerOfOdin = new Weapon("Gurdol's Hammer of Despair", 1, WeaponType.Hammer, DamageType.Blunt, 120, 350, 8, 150, 150);
            Weapon SwordOfAkatosh = new Weapon("Elondriel's Sword of Finess", 2, WeaponType.Sword, DamageType.Sharp, 90, 220, 5, 110, 120);

            Armor MailOfDisdain = new Armor(1, ArmorType.Mail, 50, 150, 5, 25, 5);
            Armor JacketOfBelief = new Armor(2, ArmorType.Leather, 35, 50, 3, 18, 3);

            Player P1 = new Player("Xorakk Player", 1, 700, 20, 700, 20, 5, false, 25, 700, 700, 5, 25, 25, HammerOfOdin, MailOfDisdain);
            Enemy E1 = new Enemy("Ellisar Enemy", 2, 570, 70, 600, 30, 7, false, 20, 570, 600, 7, 20, 30,500, SwordOfAkatosh, JacketOfBelief,3000);

            // This happens before any fight session!
            P1.AttackSpeedBase += P1.CurrentArmor.AttackSpeedModifier + P1.CurrentWeapon.AttackSpeed;
            E1.AttackSpeedBase += E1.CurrentArmor.AttackSpeedModifier + E1.CurrentWeapon.AttackSpeed;
            P1.AttackSpeed = P1.AttackSpeedBase;
            E1.AttackSpeed = E1.AttackSpeedBase;

            do
            {
                int WhoHitsValue = WhoHits(P1, E1, "sss");

                if (WhoHitsValue == 1)
                {
                    switch(DecisionAttackType(AttackTiming(P1), P1, E1))
                    {
                        case 1:
                            Console.WriteLine("You HIT!");
                            P1.Hit(E1, StaminaUsed);
                            break;
                        case 2:
                            Console.WriteLine("Enemy DODGE!");
                            E1.Dodge(P1);
                            break;
                        case 3:
                            Console.WriteLine("Enemy BLOCK!");
                            E1.Block(P1);
                            break;
                        case 0:
                            Console.WriteLine("HIT FROM PARRY!");
                            E1.Hit(P1, _rnd.Next(0, E1.Stamina));
                            break;
                    }
                }
                else if (WhoHitsValue == 2)
                {
                    Console.WriteLine("You are getting hit!");
                    switch (DecisionAttackType(AttackTiming(E1), P1, E1))
                    {
                        case 1:
                            Console.WriteLine("HIT!");
                            E1.Hit(P1, StaminaUsed);
                            break;
                        case 2:
                            Console.WriteLine("DODGE!");
                            P1.Dodge(E1);
                            break;
                        case 3:
                            Console.WriteLine("BLOCK!");
                            P1.Block(E1);
                            break;
                        case 0:
                            Console.WriteLine("HIT FROM PARRY!");
                            P1.Hit(E1, _rnd.Next(0, E1.Stamina));
                            break;
                    }
                }
            } while (P1.Health > 0 && E1.Health > 0);
        }
    }
}
