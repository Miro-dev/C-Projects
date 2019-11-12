using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;
namespace Adventure
{

    public partial class Adventure : Form
    {
        private Player _player;
        private Monster _currentMonster;

        public Adventure()
        {
            InitializeComponent();

            _player = new Player(10,10,20,0,1);
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            lblMeatLeft.Text = _player.CurrentMeatLeft.ToString();
            lblCoins.Text = _player.Coins.ToString();
            lblEXP.Text = _player.EXPPoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void MoveTo(Location newLocation)
        {
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessages.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name +
                    " to enter this location." + Environment.NewLine;
                return;
            }

            _player.CurrentLocation = newLocation;

            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            _player.CurrentMeatLeft = _player.MaximumMeatLeft;

            lblMeatLeft.Text = _player.CurrentMeatLeft.ToString();

            if(newLocation.QuestAvailableHere != null)
            {
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                if (playerAlreadyHasQuest)
                {
                    if (!playerAlreadyCompletedQuest)
                    {
                        bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);

                        if (playerHasAllItemsToCompleteQuest)
                        {
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + newLocation.QuestAvailableHere.Name +
                                "'quest." + Environment.NewLine;

                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardEXPPoints.ToString() + " experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardCoins.ToString() + " coins" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                            _player.EXPPoints += newLocation.QuestAvailableHere.RewardEXPPoints;
                            _player.Coins += newLocation.QuestAvailableHere.RewardCoins;

                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);

                        }
                    }
                }
            
              else
              {
                rtbMessages.Text += "You receive the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;
                rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                foreach(QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                {
                    if (qci.Quantity == 1)
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                    }
                }
                rtbMessages.Text += Environment.NewLine;

                _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
              }
            }

            if(newLocation.MonsterLivingHere != null)
            {
                rtbMessages.Text += "You see a " + newLocation.MonsterLivingHere.Name + Environment.NewLine;

                Monster standartMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standartMonster.ID, standartMonster.Name, standartMonster.MaximumDamage,
                    standartMonster.RewardEXPPoints, standartMonster.RewardCoins, standartMonster.CurrentMeatLeft,
                    standartMonster.MaximumMeatLeft);

                foreach(LootItem lootItem in standartMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }
                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _currentMonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;
            }

            UpdateInventoryListUI();
            UpdateQuestListUI();
            UpdateWeaponListUI();
            UpdatePotionListUI();

        }

        private void UpdateInventoryListUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }
            }
        }

        private void UpdateQuestListUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListUI()
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }

            if (weapons.Count == 0)
            {
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListUI()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPotion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add((HealingPotion)inventoryItem.Details);
                    }
                }
            }

            if (healingPotions.Count == 0)
            {
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;

            int damageToMonster = RandomNumberGenerator.NumberBetween(currentWeapon.MinimumDamage,
                currentWeapon.MaximumDamage);

            _currentMonster.CurrentMeatLeft -= damageToMonster;

            // Display message
            rtbMessages.Text += "You hit the " + _currentMonster.Name + " for " + damageToMonster.ToString()
                + " points." + Environment.NewLine;

            // Check if the monster is dead
            if (_currentMonster.CurrentMeatLeft <= 0)
            {
                // Monster is Dead
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the" + _currentMonster.Name + Environment.NewLine;

                // Gve player experience points for killing the monster
                _player.EXPPoints += _currentMonster.RewardEXPPoints;
                rtbMessages.Text += "You receive " + _currentMonster.RewardEXPPoints.ToString() +
                    " experience points"+ Environment.NewLine;

                // Give player coins for killing the monster
                _player.Coins += _currentMonster.RewardCoins;
                rtbMessages.Text += "You receive" + _currentMonster.RewardCoins.ToString() +
                    " coins." + Environment.NewLine;

                // Get random loot item from the monster
                List<InventoryItem> lootedItems = new List<InventoryItem>();

                // Add items to the lootedItems list, comparing a random number to the drop percentage
                foreach(LootItem lootItem in _currentMonster.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                // If no items were randomly selected, then add the default loot item(s).
                if (lootedItems.Count == 0)
                {
                    foreach(LootItem lootItem in _currentMonster.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                // Add the looted items to the player's inventory
                foreach(InventoryItem inventoryItem in lootedItems)
                {
                    _player.AddItemToInventory(inventoryItem.Details);

                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " +
                            inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " +
                            inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }

                // Refresh player information and inventory controls
                lblMeatLeft.Text = _player.CurrentMeatLeft.ToString();
                lblCoins.Text = _player.Coins.ToString();
                lblEXP.Text = _player.EXPPoints.ToString();
                lblLevel.Text = _player.Level.ToString();

                UpdateInventoryListUI();
                UpdatePotionListUI();
                UpdateWeaponListUI();

                // Add a blank line to the messages box, just for appearance.
                rtbMessages.Text += Environment.NewLine;

                // Move plaer to current location (to heal player and create a new monster to fight)
                MoveTo(_player.CurrentLocation);
            }
            else
            {
                // Monster is still alive

                // Determine the amount of damage the monster does to the player
                int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);

                // Display message
                rtbMessages.Text += "The " + _currentMonster.Name + " did " + damageToPlayer.ToString() + " points of" +
                    "damage." + Environment.NewLine;

                // Substract damage from player
                _player.CurrentMeatLeft -= damageToPlayer;

                // Refresh player data in UI
                lblMeatLeft.Text = _player.CurrentMeatLeft.ToString();

                if (_player.CurrentMeatLeft <= 0)
                {
                    // Display message
                    rtbMessages.Text += "The " + _currentMonster.Name + " killed you " + Environment.NewLine;

                    // Move player to "Home"
                    MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                }
            }
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            // Get the currently selected potion from the combobox
            HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;

            // Add healing amount to the player's current Meat Left
            _player.CurrentMeatLeft = (_player.CurrentMeatLeft + potion.AmountToHeal);

            // CurrentMeatLeft cannot exceed player's MaximumMeatLeft
            if (_player.CurrentMeatLeft > _player.MaximumMeatLeft)
            {
                _player.CurrentMeatLeft = _player.MaximumMeatLeft;
            }

            // Remove the potion from the player's inventory
            foreach (InventoryItem ii in _player.Inventory)
            {
                if (ii.Details.ID == potion.ID)
                {
                    ii.Quantity--;
                    break;
                }
            }

            // Display message
            rtbMessages.Text += "You drink a " + potion.Name + Environment.NewLine;

            // Monster gets their turn to attack

            // Determine the amount of damage the monster does to the player
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);

            // Display message
            rtbMessages.Text += "The " + _currentMonster.Name + " did " + damageToPlayer.ToString() + " points of" +
                "damage." + Environment.NewLine;

            // Substract damage from player
            _player.CurrentMeatLeft -= damageToPlayer;

            if (_player.CurrentMeatLeft <= 0)
            {
                // Display message
                rtbMessages.Text += "The " + _currentMonster.Name + " killed you " + Environment.NewLine;

                // Move player to "Home"
                MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            }

            //Refresh player data in UI
            lblMeatLeft.Text = _player.CurrentMeatLeft.ToString();
            UpdateInventoryListUI();
            UpdatePotionListUI();
        }
    }
}
