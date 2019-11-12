using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{

    public class Player : LivingCreature
    {
        public int Gold { get; set; }
        public int Experience { get; set; }
        public int Level
        {
            get { return ((Experience / 100) + 1); }
        }

        public Location _currentLocation { get; set; }
        public Weapon _currentWeapon { get; set; }

        public Weapon CurrentWeapon { get; set; }

        public List<InventoryItem> Inventory { get; set; }

        
        //public List<PlayerQuest> Quests { get; set; }

        public Player(int gold,int experience, int baseHP, int currentHP, int maxHP, int baseStamina, int maxStamina, int currentStamina,
            int baseDefense, int maxDefense, int currentDefense, int baseDodge, int maxDodge, int currentDodge, int baseDMG, int maxDMG, int currentDMG, int baseAccuracy,
            int maxAccuracy, int currentAccuracy, bool aggressive, bool isDead) :
            base(baseHP, maxHP, currentHP, baseStamina, maxStamina, currentStamina, baseDefense, maxDefense,
                currentDefense, baseDodge, maxDodge, currentDodge, baseDMG, maxDMG, currentDMG, baseAccuracy, maxAccuracy, currentAccuracy, aggressive, isDead)
        {
            Gold = gold;
            Experience = experience;

            Inventory = new List<InventoryItem>();
            //Quests = new List<PlayerQuest>(); 
        }

        public bool HasRequiredItemToEnter(Location location)
        {
            if (location.ItemNeededToEnter == null)
            {
                return true;
            }

            // See if the player has the inventory item
            return Inventory.Exists(ii => ii.Details.ID == location.ItemNeededToEnter.ID);
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == itemToAdd.ID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    ii.Quantity++;

                    return; // We added the item, and are done, so get out of this function
                }
            }

            // They didn't have the item, so add it to their inventory, with a quantity of 1
            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }
    }
}

//you are here do the MoveTo function
