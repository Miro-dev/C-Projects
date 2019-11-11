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


namespace Doran
{
    public partial class DoranRealm : Form
    {

        private Player _player;
        private Enemy _currentEnemy;



        public DoranRealm()
        {
            InitializeComponent();

            _player = new Player(25, 0, 80, 80, 80, 100, 100, 100, 5, 5, 5, 5, 5, 5, 10, 10, 20, 25, 25, 25, false, false);
            MoveTo(World.LocationByID(World.LOCATION_ID_FIELD));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_JAREX_SWORD), 1));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_CLUB), 1));
            UpdateWeaponListInUI();
            UpdateInventoryListInUI();

            /*_player.BaseHP = 120;
            _player.CurrentHP = 120;
            _player.BaseStamina = 100;
            _player.CurrentStamina = 100;

            _player.BaseDefense = 20;
            _player.BaseDodge = 30;

            _player.BaseDMG = 20;
            _player.BaseAccuracy = 20;

            _player.Gold = 45;
            _player.Experiance = 0;
            _player.Level = 1;*/

            lblHP.Text = _player.CurrentHP.ToString();
            lblStamina.Text = _player.CurrentStamina.ToString();
            lblExperiance.Text = _player.Experience.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();


            lblBaseHPP.Text = _player.BaseHP.ToString();
            lblMaxHPP.Text = _player.MaxHP.ToString();
            lblCurrentHPP.Text = _player.CurrentHP.ToString();

            lblBaseAccP.Text = _player.BaseAccuracy.ToString();
            lblMaxAccP.Text = _player.MaxAccuracy.ToString();
            lblCurrentAccP.Text = _player.CurrentAccuracy.ToString();

            lblBaseDefP.Text = _player.BaseDefense.ToString();
            lblMaxDefP.Text = _player.MaxDefense.ToString();
            lblCurrentDefP.Text = _player.CurrentDefense.ToString();

            lblBaseStaminaP.Text = _player.BaseStamina.ToString();
            lblMaxStaminaP.Text = _player.MaxStamina.ToString();
            lblCurrentStaminaP.Text = _player.CurrentStamina.ToString();

            lblCurDodgeP.Text = _player.CurrentDodge.ToString();

            lblCurDMGP.Text = _player.CurrentDMG.ToString();


            lblCurAccE.Text = _currentEnemy.CurrentAccuracy.ToString();
            lblCurHPE.Text = _currentEnemy.CurrentHP.ToString();
            lblCurDefE.Text = _currentEnemy.CurrentDefense.ToString();
            lblCurDodgeE.Text = _currentEnemy.CurrentDodge.ToString();
            lblCurDMGE.Text = _currentEnemy.CurrentDMG.ToString();
        }

        private void rtbMessages_TextChanged(object sender, EventArgs e)
        {
            rtbMessages.Text += Environment.NewLine;
        }

        private void ScrollToBottomOfMessages()
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            try
            {
                MoveTo(_player._currentLocation.LocationToNorth);
            }
            catch
            {
                rtbMessages.Text += Environment.NewLine + "No path " + btnNorth.Text + "ward of here." + Environment.NewLine;
            }
            ScrollToBottomOfMessages();


        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            try
            {
                MoveTo(_player._currentLocation.LocationToEast);
            }
            catch
            {
                rtbMessages.Text += Environment.NewLine + "No path " + btnEast.Text + "ward of here." + Environment.NewLine;
            }
            ScrollToBottomOfMessages();
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            try
            {
                MoveTo(_player._currentLocation.LocationToSouth);
            }
            catch
            {
                rtbMessages.Text += Environment.NewLine + "No path " + btnSouth.Text + "ward of here." + Environment.NewLine;
            }
            ScrollToBottomOfMessages();
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            try
            {
                MoveTo(_player._currentLocation.LocationToWest);
            }
            catch
            {
                rtbMessages.Text += Environment.NewLine + "No path " + btnWest.Text + "ward of here." + Environment.NewLine;
            }
            ScrollToBottomOfMessages();
        }


        private void Heal(int HealAmount)
        {
            _player.CurrentHP += HealAmount;
            if (_player.CurrentHP > _player.MaxHP)
            {
                _player.MaxHP += HealAmount * 20 / 100;
                _player.CurrentHP = _player.MaxHP;
            }
        }

        private int RowTheDice()
        {
            Random rnd = new Random();
            int DiceRow = rnd.Next(1, 7);
            return DiceRow;
        }

        private void MoveTo(Location newLocation)
        {
            //Implement the RowTheDice()

            if (!_player.HasRequiredItemToEnter(newLocation))
            {
                rtbMessages.Text += "You must have a " + newLocation.ItemNeededToEnter.Name +
                    " to enter this location." + Environment.NewLine;
                ScrollToBottomOfMessages();

                return;
            }

            _player._currentLocation = newLocation;

            rtbMessages.Text = newLocation.Name + Environment.NewLine + newLocation.Description + Environment.NewLine;

            if (newLocation.EnemyHere != null)
            {
                Enemy standartEnemy = World.EnemyByID(newLocation.EnemyHere.ID);

                _currentEnemy = new Enemy(standartEnemy.Name, standartEnemy.ID, standartEnemy.RewardXP, standartEnemy.RewardGold,
                    standartEnemy.BaseHP, standartEnemy.CurrentHP, standartEnemy.MaxHP, standartEnemy.BaseStamina, standartEnemy.MaxStamina,
                    standartEnemy.CurrentStamina, standartEnemy.BaseDefense, standartEnemy.MaxDefense, standartEnemy.CurrentDefense,
                    standartEnemy.BaseDodge, standartEnemy.MaxDodge, standartEnemy.CurrentDodge, standartEnemy.BaseDMG,
                    standartEnemy.MaxDMG, standartEnemy.CurrentDMG, standartEnemy.BaseAccuracy, standartEnemy.MaxAccuracy, standartEnemy.CurrentAccuracy,
                    standartEnemy.Aggressive, standartEnemy.IsDead);

                rtbMessages.Text += "You see a " + _currentEnemy.Name + " approaching!";
                ScrollToBottomOfMessages();
            }
            else
            {
                _currentEnemy = null;
            }
            UpdateInventoryListInUI();
        }

        //public int ChanceToHit { get; set; }

        //Hide and show boxes
        //Looting area. Should there be a loot item button?

        private void btnAttack_Click(object sender, EventArgs e)
        {
            try
            {
                Weapon _currentWeapon = (Weapon)cboWeapons.SelectedItem;

                if (_player.CurrentAccuracy > _currentEnemy.CurrentDodge)
                {

                    if (_currentEnemy.CurrentHP > 0)
                    {
                        int DamageTaken = -_currentEnemy.CurrentDefense + _player.CurrentDMG + _currentWeapon.MaxDamage;
                        _currentEnemy.CurrentHP -= DamageTaken;
                        rtbMessages.Text += "You hit the " + _currentEnemy.Name + " for " + DamageTaken + " points." + Environment.NewLine;
                        ScrollToBottomOfMessages();

                        if (_currentEnemy.CurrentHP <= 0)
                        {
                            Loot();
                            rtbMessages.Text += Environment.NewLine + "You slayed the " + _currentEnemy.Name + ". Looted " + _currentEnemy.RewardGold + " gold and " +
                                "gained " + _currentEnemy.RewardXP + " Experience." + Environment.NewLine;
                            ScrollToBottomOfMessages();
                            _player._currentLocation.EnemyHere = null;
                        }
                    }

                }
                else if (_player.CurrentAccuracy < _currentEnemy.CurrentDodge)
                {
                    rtbMessages.Text += "You missed " + _currentEnemy.Name + "." + Environment.NewLine;
                    ScrollToBottomOfMessages();
                }
                else if (_currentEnemy.CurrentAccuracy > _player.CurrentDodge)
                {
                    int DamageTaken = -_player.CurrentDefense + _currentEnemy.CurrentDMG;
                    _player.CurrentHP -= DamageTaken;
                    rtbMessages.Text += "You were hit by " + _currentEnemy.Name + " for " + DamageTaken + " points." + Environment.NewLine;
                    ScrollToBottomOfMessages();
                }

                UpdatePlayerStats();
                UpdateInventoryListInUI();
            }
            catch
            {
                rtbMessages.Text += "No enemy here." + Environment.NewLine;
                ScrollToBottomOfMessages();
            }
        }

        private void cboWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _player.CurrentWeapon = (Weapon)cboWeapons.SelectedItem;
        }

        private void UpdatePlayerStats()
        {
            lblHP.Text = _player.CurrentHP.ToString();
            lblStamina.Text = _player.CurrentStamina.ToString();
            lblExperiance.Text = _player.Experience.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();


            lblBaseHPP.Text = _player.BaseHP.ToString();
            lblMaxHPP.Text = _player.MaxHP.ToString();
            lblCurrentHPP.Text = _player.CurrentHP.ToString();

            lblBaseAccP.Text = _player.BaseAccuracy.ToString();
            lblMaxAccP.Text = _player.MaxAccuracy.ToString();
            lblCurrentAccP.Text = _player.CurrentAccuracy.ToString();

            lblBaseDefP.Text = _player.BaseDefense.ToString();
            lblMaxDefP.Text = _player.MaxDefense.ToString();
            lblCurrentDefP.Text = _player.CurrentDefense.ToString();

            lblBaseStaminaP.Text = _player.BaseStamina.ToString();
            lblMaxStaminaP.Text = _player.MaxStamina.ToString();
            lblCurrentStaminaP.Text = _player.CurrentStamina.ToString();

            lblCurDodgeP.Text = _player.CurrentDodge.ToString();


            lblCurAccE.Text = _currentEnemy.CurrentAccuracy.ToString();
            lblCurHPE.Text = _currentEnemy.CurrentHP.ToString();
            lblCurDefE.Text = _currentEnemy.CurrentDefense.ToString();
            lblCurDodgeE.Text = _currentEnemy.CurrentDodge.ToString();
        }

        private void UpdateInventoryListInUI()
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

        private void UpdateWeaponListInUI()
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
                btnAttack.Visible = false;
            }
            else
            {
                cboWeapons.SelectedIndexChanged -= cboWeapons_SelectedIndexChanged;
                cboWeapons.DataSource = weapons;
                cboWeapons.SelectedIndexChanged += cboWeapons_SelectedIndexChanged;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                if (_player.CurrentWeapon != null)
                {
                    cboWeapons.SelectedItem = _player.CurrentWeapon;
                }
                else
                {
                    cboWeapons.SelectedIndex = 0;
                }
            }
        }

        private void DoranRealm_Load(object sender, EventArgs e)
        {

        }

        private void btnHealP_Click(object sender, EventArgs e)
        {
            Heal(20);
            UpdatePlayerStats();
        }

        private void btnIncAccP_Click(object sender, EventArgs e)
        {
            _player.CurrentAccuracy += 5;
            UpdatePlayerStats();
        }

        private void btnDecAccP_Click(object sender, EventArgs e)
        {
            _player.CurrentAccuracy -= 5;
            UpdatePlayerStats();
        }

        private void btnHealE_Click(object sender, EventArgs e)
        {
            _currentEnemy.CurrentHP += 20;
            UpdatePlayerStats();
        }

        private void btnIncAccE_Click(object sender, EventArgs e)
        {
            _currentEnemy.CurrentAccuracy += 5;
            UpdatePlayerStats();
        }

        private void btnDecAccE_Click(object sender, EventArgs e)
        {
            _currentEnemy.CurrentAccuracy -= 5;
            UpdatePlayerStats();
        }

        private void Loot()
        {
            List<InventoryItem> lootedItems = new List<InventoryItem>();

            _player.Experience += _currentEnemy.RewardXP;
            _player.Gold += _currentEnemy.RewardGold;

            foreach (LootItem lootItem in _player._currentLocation.LootTableLocation)
            {
                lootedItems.Add(new InventoryItem(lootItem.Details, 1));
            }

            foreach (InventoryItem inventoryItem in lootedItems)
            {
                _player.AddItemToInventory(inventoryItem.Details);

                if (inventoryItem.Quantity == 1)
                {
                    rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name + Environment.NewLine;
                    ScrollToBottomOfMessages();
                }
                else
                {
                    rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.NamePlural + Environment.NewLine;
                    ScrollToBottomOfMessages();
                }

                _player._currentLocation.LootTableLocation.RemoveAt(0);
            }

            UpdateWeaponListInUI();
            UpdateInventoryListInUI();
        }

        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txbStamina_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void Restart_Click(object sender, EventArgs e)
        {

        }

        /*private void MapGenerator()
        {
            rtbMessages.Text += "Map Generator Started" + Environment.NewLine;
            ScrollToBottomOfMessages();

            x = 500;
            y = 200;
            TileCountNum = 1;

            Tiles.Add("500 200");

            for (int b = 0; b < 50; b++)
            {
                int North = 0;
                int South = 0;
                int East = 0;
                int West = 0;

                //Return here after break!!!

                Random rnd = new Random();
                int Direction = rnd.Next(0, 4);

                switch (Direction)
                {
                    case 0:
                        East= 35;
                        break;
                    case 1:
                        West= -35;
                        break;
                    case 2:
                        North= 35;
                        break;
                    case 3:
                        South= -35;          
                        break;
                }

                bool alreadyExist = Tiles.Contains(TileCoordinates);

                if (alreadyExist == false)
                {

                    Random RandomX = new Random();
                    int f = rnd.Next(1, 4);

                    for (int i = 0; i < f; i++)
                    {
                        y += East + West;
                        x += North + South;

                        if (x >= 500 && y >= 200)
                        {
                            PictureBox C = new PictureBox();
                            Controls.Add(C);
                            C.Name = "PicBox " + TileCountNum;
                            C.Location = new Point(x, y);
                            C.Height = 30;
                            C.Width = 30;
                            C.BackColor = Color.Aqua;

                            TileCountNum += 1;

                            string TileCoordinates = Convert.ToString(x + " " + y);
                            Tiles.Add(TileCoordinates);

                            rtbMessages.Text += "Box Created: " + C.Name + " with x: " + x + " and y: " + y + Environment.NewLine;
                            ScrollToBottomOfMessages();
                        }
                        else
                        {
                            TileCountNum += 1;
                            rtbMessages.Text += "Tile" + TileCountNum + " is out of range with x,y: " + Environment.NewLine + x + " , " + y + Environment.NewLine;
                            ScrollToBottomOfMessages();
                            break;
                        }
                    }
                }
                else if (alreadyExist == true)
                {
                    rtbMessages.Text += "List Tiles Contains Tile with coordinates " + TileCoordinates + Environment.NewLine;
                    ScrollToBottomOfMessages();
                    break;
                }
            }
        }*/

        private void btnMapGenerator_Click(object sender, EventArgs e)
        {
            MapGenerator(500, 600, 27, 500, 30);
        }

        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        public int TileSide { get; set; }
        public int StartingPointOfGrid_X { get; set; }
        public int StartingPointOfGrid_Y { get; set; }

        public void MapGenerator(int fieldWidth, int fieldHeight, int tileSide, int startingPointOfGrid_X, int startingPointOfGrid_Y)
        {
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
            TileSide = tileSide;
            StartingPointOfGrid_X = startingPointOfGrid_X;
            StartingPointOfGrid_Y = startingPointOfGrid_Y;
            int X = startingPointOfGrid_X;
            int Y = startingPointOfGrid_Y;

            int TileBorder = TileSide * 15 / 100;
            int TileFullSize = TileBorder + TileSide;
            int Rows = FieldHeight / TileFullSize;
            int Columns = FieldWidth / TileFullSize;

            int PicX;
            int PicY;

            int TileCountNum;

            Place CurrentPlace;


            int ROWS_0;
            int COLUMNS_0;
            int ROWS_1;
            int COLUMNS_1;


            int ForbiddenCreatedPlaceID;
            int CreatedPlaceID;
            int VariableForNewStart = 0;

            Random rndNewStart = new Random();

            int[,,] FieldGrid = new int[Rows, Columns, 2];
            Place[,] PlacesArray = new Place[Rows, Columns];
            Place[,] ForbiddenPlacesArray = new Place[Rows, Columns];

            List<Place> PlacesList = new List<Place>();

            rtbMessages.Text += Environment.NewLine + "Tile Border: " + TileBorder + " Rows: " + Rows + " Columns: " +
            Columns + Environment.NewLine + "For Width=" + FieldWidth + Environment.NewLine + "For Height=" + FieldHeight +
            Environment.NewLine + "TileSide=" + TileSide + Environment.NewLine;

            for (int i = 0; i < FieldGrid.GetLength(0); i++)
            {
                StartingPointOfGrid_X = X;
                for (int j = 0; j < FieldGrid.GetLength(1); j++)
                {
                    FieldGrid[i, j, 0] = StartingPointOfGrid_X;
                    rtbMessages.Text += Environment.NewLine + FieldGrid[i, j, 0] + " [" + i + ", " + j + ", " + 0 + "]";
                    StartingPointOfGrid_X += TileFullSize;
                }
            }

            for (int j = 0; j < FieldGrid.GetLength(1); j++)
            {
                StartingPointOfGrid_Y = Y;
                for (int i = 0; i < FieldGrid.GetLength(0); i++)
                {
                    FieldGrid[i, j, 1] = StartingPointOfGrid_Y;

                    StartingPointOfGrid_Y += TileFullSize;
                }
            }

            TileCountNum = 1;

            rtbMessages.Text += "GeneratePlaces Started" + Environment.NewLine;//------------------------------------------------------------------------
            ScrollToBottomOfMessages();

            ROWS_0 = 1;
            COLUMNS_0 = 1;
            ROWS_1 = 1;
            COLUMNS_1 = 1;
            StartingPointOfGrid_X = X;
            StartingPointOfGrid_Y = Y;

            Place Home = new Place(0, FieldGrid[ROWS_0, COLUMNS_0, 0], FieldGrid[ROWS_1, COLUMNS_1, 1]);

            PlacesArray[1, 1] = Home;
            CurrentPlace = Home;
            PlacesList.Add(Home);

            CreatedPlaceID = 1;
            ForbiddenCreatedPlaceID = 0;

            for (int k = 0; k < 90; k++)
            {
                if (VariableForNewStart > 4)
                {
                    PlacesList.Remove(CurrentPlace);
                    do
                    {
                        int NewBegginingPlace = rndNewStart.Next(PlacesList.Count);

                        CurrentPlace = PlacesList[NewBegginingPlace];

                        PlacesList.Remove(CurrentPlace);
                    }
                    while (CurrentPlace == null);
                }

                ROWS_0 = (CurrentPlace.Y - StartingPointOfGrid_Y) / TileFullSize;
                COLUMNS_0 = (CurrentPlace.X - StartingPointOfGrid_X) / TileFullSize;
                ROWS_1 = (CurrentPlace.Y - StartingPointOfGrid_Y) / TileFullSize;
                COLUMNS_1 = (CurrentPlace.X - StartingPointOfGrid_X) / TileFullSize;

                VariableForNewStart = 0;

                rtbMessages.Text += "New Beggining with Current Row: "
                    + ROWS_0 + ", Column: " + COLUMNS_0 + Environment.NewLine;
                ScrollToBottomOfMessages();

                for (int i = 0; i < 5; i++)
                {
                    this.Refresh();

                    rtbMessages.Text += "VariableForNewStart Count: (>4) " + VariableForNewStart + Environment.NewLine;
                    ScrollToBottomOfMessages();

                    Random rnd = new Random();
                    int Direction = rnd.Next(0, 4);

                    int RemoveFromList = 0;

                    switch (Direction)
                    {
                        case 0:
                            rtbMessages.Text += "From Left to Right (Colums)X+1" + Environment.NewLine + "Current Row: " + ROWS_0 + ", Column: " + COLUMNS_0 + Environment.NewLine;
                            // From Left to Right (Colums)X+1--------------------------------------------------------------------------------------------------------------------------------------------
                            COLUMNS_0 += 1;
                            COLUMNS_1 += 1;
                            if (COLUMNS_0 > Columns - 2)
                            {
                                COLUMNS_0 -= 1;
                                COLUMNS_1 -= 1;

                                VariableForNewStart += 1;

                                break;
                            }

                            if (ForbiddenPlacesArray[ROWS_0, COLUMNS_0] != null)
                            {
                                COLUMNS_0 -= 1;
                                COLUMNS_1 -= 1;

                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0 - 1] != null && PlacesArray[ROWS_0 - 1, COLUMNS_0] != null)
                            {
                                CreateForbiddenPlace();

                                COLUMNS_0 -= 1;
                                COLUMNS_1 -= 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 + 1, COLUMNS_0 - 1] != null && PlacesArray[ROWS_0 + 1, COLUMNS_0] != null)
                            {
                                CreateForbiddenPlace();

                                COLUMNS_0 -= 1;
                                COLUMNS_1 -= 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0 - 1] != null)//Upper Element Exists 0,                                                                                                                    //
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0 - 1, COLUMNS_0, 0], FieldGrid[ROWS_1 - 1, COLUMNS_1, 1]);

                                ForbiddenPlacesArray[ROWS_0 - 1, COLUMNS_0] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0 - 1, COLUMNS_0, 0];
                                PicY = FieldGrid[ROWS_1 - 1, COLUMNS_1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + (ROWS_0 - 1) + ", Column: " + COLUMNS_0 + " and ID: " + ForbiddenPlacesArray[ROWS_0 - 1, COLUMNS_0].ID + Environment.NewLine;
                            }

                            if (PlacesArray[ROWS_0 + 1, COLUMNS_0 - 1] != null)//Lower Element Exists 2,0
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0 + 1, COLUMNS_0, 0], FieldGrid[ROWS_1 + 1, COLUMNS_1, 1]);

                                ForbiddenPlacesArray[ROWS_0 + 1, COLUMNS_0] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0 + 1, COLUMNS_0, 0];
                                PicY = FieldGrid[ROWS_1 + 1, COLUMNS_1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + (ROWS_0 + 1) + ", Column: " + COLUMNS_0 + " and ID: " + ForbiddenPlacesArray[ROWS_0 + 1, COLUMNS_0].ID + Environment.NewLine;
                            }

                            CreatePlace();

                            if (RemoveFromList > 1)
                            {
                                PlacesList.Remove(CurrentPlace);
                            }
                            RemoveFromList = 0;
                            break;

                        case 1:
                            rtbMessages.Text += "From Right To Left (Colums)X-1" + Environment.NewLine + "Current Row: " + ROWS_0 + ", Column: " + COLUMNS_0 + Environment.NewLine;
                            // From Right To Left (Colums)X-1--------------------------------------------------------------------------------------------------------------------------------------------
                            COLUMNS_0 -= 1;
                            COLUMNS_1 -= 1;
                            if (COLUMNS_0 < 1)
                            {
                                COLUMNS_0 += 1;
                                COLUMNS_1 += 1;
                                VariableForNewStart += 1;
                                break;
                            }

                            if (ForbiddenPlacesArray[ROWS_0, COLUMNS_0] != null)
                            {
                                COLUMNS_0 += 1;
                                COLUMNS_1 += 1;
                                VariableForNewStart += 1;
                                break;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0] != null && PlacesArray[ROWS_0 - 1, COLUMNS_0 + 1] != null)
                            {
                                CreateForbiddenPlace();

                                COLUMNS_0 += 1;
                                COLUMNS_1 += 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 + 1, COLUMNS_0] != null && PlacesArray[ROWS_0 + 1, COLUMNS_0 + 1] != null)
                            {
                                CreateForbiddenPlace();

                                COLUMNS_0 += 1;
                                COLUMNS_1 += 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0 + 1] != null)//Upper Element Exists 0,2
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0 - 1, COLUMNS_0, 0], FieldGrid[ROWS_1 - 1, COLUMNS_1, 1]);

                                ForbiddenPlacesArray[ROWS_0 - 1, COLUMNS_0] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0 - 1, COLUMNS_0, 0];
                                PicY = FieldGrid[ROWS_1 - 1, COLUMNS_1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + (ROWS_0 - 1) + ", Column: " + COLUMNS_0 + " and ID: " + ForbiddenPlacesArray[ROWS_0 - 1, COLUMNS_0] + Environment.NewLine;
                            }

                            if (PlacesArray[ROWS_0 + 1, COLUMNS_0 + 1] != null)//Lower Element Exists 2,2
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0 + 1, COLUMNS_0, 0], FieldGrid[ROWS_1 + 1, COLUMNS_1, 1]);

                                ForbiddenPlacesArray[ROWS_0 + 1, COLUMNS_0] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0 + 1, COLUMNS_0, 0];
                                PicY = FieldGrid[ROWS_1 + 1, COLUMNS_1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + (ROWS_0 + 1) + ", Column: " + COLUMNS_0 + " and ID: " + ForbiddenPlacesArray[ROWS_0 + 1, COLUMNS_0].ID + Environment.NewLine;
                            }

                            CreatePlace();

                            if (RemoveFromList > 1)
                            {
                                PlacesList.Remove(CurrentPlace);
                            }
                            RemoveFromList = 0;
                            break;
                        case 2:
                            rtbMessages.Text += "From Bottom To Top Y" + Environment.NewLine + "Current Row: " + ROWS_0 + ", Column: " + COLUMNS_0 + Environment.NewLine;
                            // From Bottom To Top Y---------------------------------------------------------------------------------------------------------------------------------------------
                            ROWS_0 -= 1;
                            ROWS_1 -= 1;
                            if (ROWS_0 < 1)
                            {
                                ROWS_0 += 1;
                                ROWS_1 += 1;
                                VariableForNewStart += 1;
                                break;
                            }

                            if (ForbiddenPlacesArray[ROWS_0, COLUMNS_0] != null)
                            {
                                ROWS_0 += 1;
                                ROWS_1 += 1;
                                VariableForNewStart += 1;
                                break;
                            }

                            if (PlacesArray[ROWS_0, COLUMNS_0 - 1] != null && PlacesArray[ROWS_0 + 1, COLUMNS_0 - 1] != null)
                            {
                                CreateForbiddenPlace();

                                ROWS_0 += 1;
                                ROWS_1 += 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0, COLUMNS_0 + 1] != null && PlacesArray[ROWS_0 + 1, COLUMNS_0 + 1] != null)
                            {
                                CreateForbiddenPlace();

                                ROWS_0 += 1;
                                ROWS_1 += 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 + 1, COLUMNS_0 - 1] != null)//Left Element Exists 2,0
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0, COLUMNS_0 - 1, 0], FieldGrid[ROWS_1, COLUMNS_1 - 1, 1]);

                                ForbiddenPlacesArray[ROWS_0, COLUMNS_0 - 1] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0, COLUMNS_0 - 1, 0];
                                PicY = FieldGrid[ROWS_1, COLUMNS_1 - 1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + ROWS_0 + ", Column: " + (COLUMNS_0 - 1) + " and ID: " + ForbiddenPlacesArray[ROWS_0, COLUMNS_0 - 1].ID + Environment.NewLine;
                            }

                            if (PlacesArray[ROWS_0 + 1, COLUMNS_0 + 1] != null)//Right Element Exists 2,2
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0, COLUMNS_0 + 1, 0], FieldGrid[ROWS_1, COLUMNS_1 + 1, 1]);

                                ForbiddenPlacesArray[ROWS_0, COLUMNS_0 + 1] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0, COLUMNS_0 + 1, 0];
                                PicY = FieldGrid[ROWS_1, COLUMNS_1 + 1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + ROWS_0 + ", Column: " + (COLUMNS_0 + 1) + " and ID: " + ForbiddenPlacesArray[ROWS_0, COLUMNS_0 + 1].ID + Environment.NewLine;
                            }

                            CreatePlace();

                            if (RemoveFromList > 1)
                            {
                                PlacesList.Remove(CurrentPlace);
                            }
                            RemoveFromList = 0;
                            break;
                        case 3:
                            rtbMessages.Text += "From Top To Bottom Y+" + Environment.NewLine + "Current Row: " + ROWS_0 + ", Column: " + COLUMNS_0 + Environment.NewLine;
                            // From Top To Bottom Y+--------------------------------------------------------------------------------------------------------------------------------------------
                            ROWS_0 += 1;
                            ROWS_1 += 1;
                            if (ROWS_0 > Rows - 2)
                            {
                                ROWS_0 -= 1;
                                ROWS_1 -= 1;
                                VariableForNewStart += 1;
                                break;
                            }

                            if (ForbiddenPlacesArray[ROWS_0, COLUMNS_0] != null)
                            {
                                ROWS_0 -= 1;
                                ROWS_1 -= 1;
                                VariableForNewStart += 1;
                                break;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0 + 1] != null && PlacesArray[ROWS_0, COLUMNS_0 + 1] != null)
                            {
                                CreateForbiddenPlace();

                                ROWS_0 -= 1;
                                ROWS_1 -= 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0 - 1] != null && PlacesArray[ROWS_0, COLUMNS_0 - 1] != null)
                            {
                                CreateForbiddenPlace();

                                ROWS_0 -= 1;
                                ROWS_1 -= 1;
                                VariableForNewStart += 1;

                                break;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0 - 1] != null)//Left Element Exists 0,0
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0, COLUMNS_0 - 1, 0], FieldGrid[ROWS_1, COLUMNS_1 - 1, 1]);

                                ForbiddenPlacesArray[ROWS_0, COLUMNS_0 - 1] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0, COLUMNS_0 - 1, 0];
                                PicY = FieldGrid[ROWS_1, COLUMNS_1 - 1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + ROWS_0 + ", Column: " + (COLUMNS_0 - 1) + " and ID: " + ForbiddenPlacesArray[ROWS_0, COLUMNS_0 - 1].ID + Environment.NewLine;
                            }

                            if (PlacesArray[ROWS_0 - 1, COLUMNS_0 + 1] != null)//Upper Element Exists 0,2
                            {
                                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0, COLUMNS_0 + 1, 0], FieldGrid[ROWS_1, COLUMNS_1 + 1, 1]);

                                ForbiddenPlacesArray[ROWS_0, COLUMNS_0 + 1] = ForbiddenPlaceElement;

                                PicX = FieldGrid[ROWS_0, COLUMNS_0 + 1, 0];
                                PicY = FieldGrid[ROWS_1, COLUMNS_1 + 1, 1];
                                CreatePic(Color.Black);

                                ForbiddenCreatedPlaceID += 1;

                                RemoveFromList += 1;

                                rtbMessages.Text += "Forbidden Place Created on Row: " + ROWS_0 + ", Column: " + (COLUMNS_0 + 1) + " and ID: " + ForbiddenPlacesArray[ROWS_0, COLUMNS_0 + 1].ID + Environment.NewLine;
                            }
                            CreatePlace();

                            if (RemoveFromList > 1)
                            {
                                PlacesList.Remove(CurrentPlace);
                            }
                            RemoveFromList = 0;
                            break;
                    }
                }
                rtbMessages.Text += "Iteration Num: " + k;

                //------------------------------------------------------------------------------------------------------------------------------------------
            }

            void CreateFieldPics()
            {
                for (int j = 0; j < FieldGrid.GetLength(1); j++)
                {
                    for (int i = 0; i < FieldGrid.GetLength(0); i++)
                    {
                        PictureBox C = new PictureBox();
                        Controls.Add(C);
                        C.Name = "PicBox " + TileCountNum;
                        C.Location = new Point(FieldGrid[i, j, 0], FieldGrid[i, j, 1]);
                        C.Height = TileSide;
                        C.Width = TileSide;
                        C.BackColor = Color.Ivory;
                    }
                }
            }

            void CreatePlace()
            {
                Place TilePlace = new Place(CreatedPlaceID, FieldGrid[ROWS_0, COLUMNS_0, 0], FieldGrid[ROWS_1, COLUMNS_1, 1]);

                PlacesArray[ROWS_0, COLUMNS_0] = TilePlace;
                ForbiddenPlacesArray[ROWS_0, COLUMNS_0] = TilePlace;

                PlacesList.Add(TilePlace);

                CreatedPlaceID += 1;

                VariableForNewStart = 0;

                CurrentPlace = TilePlace;

                CreateLocationPic(Color.Brown);

                rtbMessages.Text += "Place Created on Row: " + ROWS_0 + ", Column: " + COLUMNS_0 + " and ID: " + PlacesArray[ROWS_0, COLUMNS_0].ID + " " + TilePlace + Environment.NewLine;
                rtbMessages.Text += "CreatedPlaceID: " + CreatedPlaceID + " PlaceListCount: " + PlacesList.Count + Environment.NewLine;
            }

            void CreateForbiddenPlace()
            {
                VariableForNewStart += 1;

                Place ForbiddenPlaceElement = new Place(ForbiddenCreatedPlaceID, FieldGrid[ROWS_0, COLUMNS_0, 0], FieldGrid[ROWS_1, COLUMNS_1, 1]);
                ForbiddenPlacesArray[ROWS_0, COLUMNS_0] = ForbiddenPlaceElement;
                PicX = FieldGrid[ROWS_0, COLUMNS_0, 0];
                PicY = FieldGrid[ROWS_1, COLUMNS_1, 1];

                CreatePic(Color.Black);

                ForbiddenCreatedPlaceID += 1;
            }

            void CreatePic(Color color)
            {
                PictureBox PicBox = new PictureBox();
                Controls.Add(PicBox);
                PicBox.Name = "PicBox " + ForbiddenCreatedPlaceID;
                PicBox.Location = new Point(PicX, PicY);
                PicBox.Height = TileSide - 10;
                PicBox.Width = TileSide - 10;
                PicBox.BackColor = color;
            }

            void CreateLocationPic(Color color)
            {
                PictureBox PicBox = new PictureBox();
                Controls.Add(PicBox);
                PicBox.Name = "PicBox " + ForbiddenCreatedPlaceID;
                PicBox.Location = new Point(FieldGrid[ROWS_0, COLUMNS_0, 0], FieldGrid[ROWS_1, COLUMNS_1, 1]);
                PicBox.Height = TileSide;
                PicBox.Width = TileSide;
                PicBox.BackColor = color;
            }

            CreateFieldPics();
        }
    }
}