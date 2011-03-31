using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace commercial_angler
{
    public partial class GameForm : Form
    {
        public List<Weapon> WeaponList = new List<Weapon>();
        public List<Fish> FishList = new List<Fish>();
        public List<Ship> ShipList = new List<Ship>();
        public Character Player;
        private Fish CurrentEnemy;
        private shipyardForm ShipYardDialog;
        private FactoryForm FactoryDialog;
        private Bitmap[] shipBmpArr = new Bitmap[22];
        private Bitmap[] fishBmpArr = new Bitmap[44];
        private Bitmap attackBmp;
        private Bitmap water1Bmp;
        private Bitmap water2Bmp;
        private Bitmap portBmp;
        private Bitmap bloodBmp;
        private Bitmap aboutBmp;

        StreamReader sr;
        private Thread patrolThread;
        //readonly object pauseLock = new object();

        // state flags
        bool inPort;
        bool inCombat;
        bool gameStarted;
        bool isDead;
        bool waterSwitch = true;

        public Graphics g;

        public GameForm()
        {
            InitializeComponent();
            isDead = false;
            inPort = false;
            inCombat = false;
            gameStarted = false;
            patrolThread = new Thread(new ThreadStart(PatrolAnimation));

            //load the game data
            LoadData();

            // disable maximize button
            this.MaximizeBox = false;

            UpdateControls();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if ((patrolThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                || (patrolThread.ThreadState == System.Threading.ThreadState.Running))
            {
                patrolThread.Abort();
            }
            else if (patrolThread.ThreadState == System.Threading.ThreadState.Suspended)
            {
                patrolThread.Resume();
                patrolThread.Abort();
            }

            base.OnClosing(e);
        }

        // reads angler.dat and loads the data into lists
        // note: dat file needs to be of the format 
        //  #section
        //  #comment
        //  element1,...,elementn
        //  #end section
        public void LoadData()
        {
            try
            {
                // get a reference to the current assembly
                Assembly a = Assembly.GetExecutingAssembly();
                
                // get a list of resource names from the manifest
                string[] resNames = a.GetManifestResourceNames();

                foreach (string res in resNames)
                {
                    if (res.EndsWith("angler.dat"))
                    {
                        // attach to stream to the resource in the manifest
                        sr = new StreamReader(a.GetManifestResourceStream(res));
                    }
                    else if (res.EndsWith(".bmp"))
                    {
                        // NOTE: ship bmps are named shipn.bmp, where n is the ship ID
                        if (res.Contains("ship"))
                        {
                            String index = res.ToString().Substring(res.ToString().IndexOf('p') + 1);
                            index = index.Substring(0, index.IndexOf('.'));
                            shipBmpArr[int.Parse(index) - 1] = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                            shipBmpArr[int.Parse(index) - 1].MakeTransparent(shipBmpArr[int.Parse(index) - 1].GetPixel(1, 1));
                        }
                        // NOTE: fish bmps are named fishn.bmp, where n is the fish ID
                        else if (res.Contains("fish"))
                        {
                            String index = res.ToString().Substring(res.ToString().IndexOf('h') + 1);
                            index = index.Substring(0, index.IndexOf('.'));
                            fishBmpArr[int.Parse(index) - 1] = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                            fishBmpArr[int.Parse(index) - 1].MakeTransparent(fishBmpArr[int.Parse(index) - 1].GetPixel(1, 1));
                        }
                        else if (res.Contains("attack"))
                        {
                            attackBmp = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                            attackBmp.MakeTransparent(attackBmp.GetPixel(1, 1));
                        }
                        else if (res.Contains("blood"))
                        {
                            bloodBmp = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                            bloodBmp.MakeTransparent(bloodBmp.GetPixel(1, 1));
                        }
                        else if (res.Contains("water1"))
                        {
                            water1Bmp = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                        }
                        else if (res.Contains("water2"))
                        {
                            water2Bmp = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                        }
                        else if (res.Contains("port"))
                        {
                            portBmp = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                        }
                        else if (res.Contains("about"))
                        {
                            aboutBmp = Bitmap.FromStream(a.GetManifestResourceStream(res)) as Bitmap;
                        }
                    }
                }

                while (sr.Peek() > -1)
                {
                    String s = sr.ReadLine();
                    if (s[0] == '#')
                    {
                        s = s.Substring(1);
                        int ID, HoldCap, ShipHP, AC, MinDmg, MaxDmg, HitBonus, Level, BaseHP;
                        String Name;
                        float Price, Weight;

                        switch (s)
                        {
                            case "vessels":     // id,name,price,holdcap,shiphp,ac
                                // skip one line
                                s = sr.ReadLine();
                                s = sr.ReadLine();
                                while (s[0] != '#' && s.Length > 1)
                                {
                                    ID = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Name = s.Substring(0, s.IndexOf(','));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Price = float.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    HoldCap = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    ShipHP = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    AC = int.Parse(s);

                                    ShipList.Add(new Ship(ID, Name, Price, HoldCap, ShipHP, AC));
                                    s = sr.ReadLine();
                                }
                                break;
                            case "weapons":     // id,name,price,mindmg,maxdmg,weight,hitbonus
                                // skip one line
                                s = sr.ReadLine();
                                s = sr.ReadLine();
                                while (s[0] != '#' && s.Length > 1)
                                {
                                    ID = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Name = s.Substring(0, s.IndexOf(','));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Price = float.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    MinDmg = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    MaxDmg = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Weight = float.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    HitBonus = int.Parse(s);

                                    WeaponList.Add(new Weapon(ID, Name, Price, MinDmg, MaxDmg, Weight, HitBonus));
                                    s = sr.ReadLine();
                                }
                                break;
                            case "fish":     // id,name,level,basehp,mindmg,maxdmg,ac,weight,value
                                // skip one line
                                s = sr.ReadLine();
                                s = sr.ReadLine();
                                while (s[0] != '#' && s.Length > 1)
                                {
                                    ID = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Name = s.Substring(0, s.IndexOf(','));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Level = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    BaseHP = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    MinDmg = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    MaxDmg = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    AC = int.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Weight = float.Parse(s.Substring(0, s.IndexOf(',')));
                                    s = s.Substring(s.IndexOf(',') + 1);
                                    Price = float.Parse(s);

                                    FishList.Add(new Fish(ID, Name, Level, BaseHP, MinDmg, MaxDmg, AC, Weight, Price));
                                    s = sr.ReadLine();
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading data file: " + e.Message);
                statusToolStripStatusLabel.ForeColor = Color.Red;
                statusToolStripStatusLabel.Text = "Initialization Error!";
            }
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Pen myPen = new Pen(Color.Gray);
            SolidBrush myBrush = new SolidBrush(Color.Black);
            g.FillRectangle(myBrush, 20, 40, 595, 190);
            g.DrawRectangle(myPen, 22, 42, 590, 185);
            myBrush.Color = Color.SteelBlue;
            g.FillRectangle(myBrush, 23, 43, 589, 184);

            // draw the splash screen            
            if (!gameStarted)
            {
                myBrush.Color = Color.DarkKhaki;
                RectangleF rect = new RectangleF(new PointF(30, 60), new SizeF(350, 60));
                g.DrawString("Commercial Angler", new Font("Arial", 26), myBrush, rect, new StringFormat(StringFormatFlags.NoWrap));
                myBrush.Color = Color.DarkGray;
                rect = new RectangleF(new PointF(268, 105), new SizeF(65, 15));
                g.DrawString("Version 0.1", new Font("Arial", 8), myBrush, rect, new StringFormat(StringFormatFlags.NoWrap));
                myBrush.Color = Color.Black;
                rect = new RectangleF(new PointF(490, 205), new SizeF(205, 15));
                g.DrawString("©2007 Bruce C. Miller", new Font("Arial", 8), myBrush, rect, new StringFormat(StringFormatFlags.NoWrap));

                // ship img
                g.DrawImage(shipBmpArr[16], new RectangleF(new PointF(334, 71), new SizeF(261, 123)));
                g.Flush();
                g.Dispose();
            }
            else
            {
                CycleDrawArea();
            }
        }

        private void CycleDrawArea()
        {
            g = this.CreateGraphics();
            g.ResetClip();

            SolidBrush myBrush = new SolidBrush(Color.SteelBlue);
            g.FillRectangle(myBrush, 23, 43, 589, 184);

            // background
            if ((inPort) && (portBmp != null))
            {
                g.DrawImage(portBmp, new RectangleF(new PointF(23, 43), new SizeF(589, 184)));
            }
            else if ((water1Bmp != null) && (water2Bmp != null))
            {
                if (waterSwitch)
                {
                    g.DrawImage(water1Bmp, new RectangleF(new PointF(23, 43), new SizeF(589, 184)));
                }
                else
                {
                    g.DrawImage(water2Bmp, new RectangleF(new PointF(23, 43), new SizeF(589, 184)));
                }
            }
            
            if (inPort) // draw ship docked at port
            {
                if (shipBmpArr[Player.CurrentShip.ID - 1] != null)
                {
                    g.DrawImage(shipBmpArr[Player.CurrentShip.ID - 1], new RectangleF(new PointF(188, 54), new SizeF(261, 123)));
                }
            }
            else  // draw player ship and enemy
            {
                if (shipBmpArr[Player.CurrentShip.ID - 1] != null)
                {
                    if (Player.CurrentShip.HP > 0)
                    {
                        // Player still alive, draw ship
                        g.DrawImage(shipBmpArr[Player.CurrentShip.ID - 1], new RectangleF(new PointF(40, 71), new SizeF(261, 123)));
                    }
                    else
                    {
                        AddStatusLine("Player dead");
                        // draw the blood pool, if available
                        if (bloodBmp != null)
                        {
                            AddStatusLine("drawing blood bmp");
                            g.DrawImage(bloodBmp, new RectangleF(new PointF(40, 71), new SizeF(261, 123)));
                        }
                    }

                }
                if ((CurrentEnemy != null) && (CurrentEnemy.ID >= 0))
                {
                    if ((fishBmpArr[CurrentEnemy.ID - 1] != null) && (inCombat && !isDead))
                    {
                        g.DrawImage(fishBmpArr[CurrentEnemy.ID - 1], new RectangleF(new PointF(317, 43), new SizeF(294, 184)));
                    }
                }
            }

            g.Flush();
            g.Dispose();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ensure timer thread suspended
            if ((patrolThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                || (patrolThread.ThreadState == System.Threading.ThreadState.Running))
            {
                patrolThread.Suspend();
            }

            try
            {
                Player = new Character("Bruce C. Miller", 0.0f, WeaponList.ToArray()[0], ShipList.ToArray()[0].Copy());
                inPort = true;
                inCombat = false;
                isDead = false;
                gameStarted = true;

                statusTextBox.Clear();
                AddStatusLine("New Game Started!");
                AddStatusLine("You are docked at Hyannis Port, Massachusetts in the Nantucket Sound.");

                UpdateLabels();
                UpdateControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resource Corruption: " + ex.Message);
                statusToolStripStatusLabel.ForeColor = Color.Red;
                statusToolStripStatusLabel.Text = "Resource Corruption!";
            }
        }

        // starts a new game with the player with cash and upgraded to the level 5 weapon and ship 
        private void quickStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ensure timer thread suspended
            if ((patrolThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                || (patrolThread.ThreadState == System.Threading.ThreadState.Running))
            {
                patrolThread.Suspend();
            }

            try
            {
                Player = new Character("Bruce C. Miller", 200.00f, WeaponList.ToArray()[4], ShipList.ToArray()[4].Copy());
                inPort = true;
                inCombat = false;
                isDead = false;
                gameStarted = true;

                statusTextBox.Clear();
                AddStatusLine("New Quickstart Game Started!");
                AddStatusLine("You are docked at Hyannis Port, Massachusetts in the Nantucket Sound.");

                UpdateLabels();
                UpdateControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resource Corruption: " + ex.Message);
                statusToolStripStatusLabel.ForeColor = Color.Red;
                statusToolStripStatusLabel.Text = "Resource Corruption!";
            }
        }

        // updates information labels
        private void UpdateLabels()
        {
            // player name
            nameLabel.Text = Player.Name;
            // ship type
            shipLabel.Text = Player.CurrentShip.Name;
            // amount of money
            moneyLabel.Text = Player.Money.ToString("C");
            // weight of current cargo / max weight supported
            cargoLabel.Text = Player.CurrentShip.Cargo.GetWeight().ToString("0.00") + "/" + Player.CurrentShip.HoldCap.ToString("0.00") + " lbs"; 
            // weapon weilded
            weaponLabel.Text = Player.CurrentWeapon.Name;
            // current ship hp / max ship hp
            healthLabel.Text = Player.CurrentShip.CurrentHP + "/" + Player.CurrentShip.HP + " HP";

            // update the cargo hold listbox
            cargoListBox.Items.Clear();
            if (Player.CurrentShip.Cargo.Count > 0)
            {
                foreach (Fish f in Player.CurrentShip.Cargo)
                {
                    cargoListBox.Items.Add("(" + f.Quantity.ToString() + ") " + f.Name);
                }
            }
            CycleDrawArea();
        }

        private void UpdateControls()
        {
            saveGameToolStripMenuItem.Enabled = gameStarted ? true : false;
            if (gameStarted)
            {
                attackButton.Enabled = (inCombat && !isDead) ? true : false;
                shipyardButton.Enabled = inPort ? true : false;
                factoryButton.Enabled = inPort ? true : false;
                returnButton.Enabled = !inPort ? true : false;
                dumpButton.Enabled = (Player.CurrentShip.Cargo.Count > 0) ? true : false;
                patrolButton.Enabled = !(!inPort && !inCombat) ? true : false;
                if (inPort)
                {
                    statusToolStripStatusLabel.ForeColor = Color.Green;
                    statusToolStripStatusLabel.Text = "In Port";
                }
                else if (inCombat)
                {
                    statusToolStripStatusLabel.ForeColor = Color.Black;
                    statusToolStripStatusLabel.Text = "At Sea";
                }
                else
                {
                    statusToolStripStatusLabel.ForeColor = Color.Black;
                    statusToolStripStatusLabel.Text = "On Patrol";
                }
                    
                // TODO: enable these later
                saveGameToolStripMenuItem.Enabled = false;
                loadGameToolStripMenuItem.Enabled = false;
            }
            else
            {
                attackButton.Enabled = false;
                patrolButton.Enabled = false;
                returnButton.Enabled = false;
                dumpButton.Enabled = false;
                factoryButton.Enabled = false;
                shipyardButton.Enabled = false;
                statusToolStripStatusLabel.Text = "Not Playing (Click File | New to start a new game)";
                statusToolStripStatusLabel.ForeColor = Color.Gray;
                // TODO: enable these later
                saveGameToolStripMenuItem.Enabled = false;
                loadGameToolStripMenuItem.Enabled = false;
            }
        }

        private void AddStatusLine(String msg)
        {
            if (statusTextBox.Text == "")
            {
                statusTextBox.Text = msg;
            }
            else
            {
                statusTextBox.Text = statusTextBox.Text + "\r\n" + msg;
            }
            statusTextBox.SelectionStart = statusTextBox.Text.Length;
            statusTextBox.ScrollToCaret();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox(aboutBmp);
            ab.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // start patrolling from port or continue patrolling after sighting
        private void patrolButton_Click(object sender, EventArgs e)
        {
            if (inPort)
            {
                AddStatusLine("Sailing out from port...");
            }

            // start the patrol thread
            if (patrolThread.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                patrolThread.Start();
            }
            else
            {
                patrolThread.Resume();
            }
            inPort = false;
            inCombat = false;
            isDead = false;
            UpdateControls();
        }

        public void InitiateEncounter()
        {
            // get random enemy 
            // NOTE: This formula slightly weights the spawn such that uber mobs don't appear until the ship is upgraded.
            CurrentEnemy = FishList.ToArray()[(new Random()).Next(0, FishList.Count + 1 - (ShipList.Count - Player.CurrentShip.ID))];

            if (StartsWithVowel(CurrentEnemy.Name))
            {
                AddStatusLine("You encounter an " + CurrentEnemy.Name + "!");
            }
            else
            {
                AddStatusLine("You encounter a " + CurrentEnemy.Name + "!");
            }

            // warn player if creature is considerably stronger
            int powerDiffMetric = (CurrentEnemy.Level - ((Player.CurrentShip.ID + Player.CurrentWeapon.ID) / 2));
            if (powerDiffMetric >= 10)
            {
                AddStatusLine("Your irrepressible fear causes you to deficate yourself.");
            }
            else if (powerDiffMetric >= 8)
            {
                AddStatusLine("Gazing upon this mighty beast paralyzes you with mortal fear.");
            }
            else if (powerDiffMetric >= 6)
            {
                AddStatusLine("You shutter in fear at the sight of this powerful creature.");
            }
            else if (powerDiffMetric >= 4)
            {
                AddStatusLine("He looks pretty tough.");
            }

            inPort = false;
            inCombat = true;
            isDead = false;
            UpdateControls();
            UpdateLabels();
            patrolThread.Suspend();
        }

        private bool StartsWithVowel(String s)
        {
            Char[] Vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            for (int i = 0; i < Vowels.Length; i++)
            {
                if (s.ToLower()[0] == Vowels[i])
                {
                    return true;
                }
            }
            return false;
        }

        // animate the draw area to show movement
        public void PatrolAnimation()
        {
            while (true)
            {
                // TODO: change this to animate the draw area for this random amount of time.
                int random = (new Random()).Next(2, 11);
                for (int i = 0; i < random; i++)
                {
                    AddStatusLine("Patrolling the Northern Atlantic...");
                    // animate the background a few steps
                    for (int j = 0; j < 4; j++)
                    {
                        waterSwitch = !waterSwitch;
                        CycleDrawArea();
                        Thread.Sleep(800);
                    }
                }

                // this will suspend the thread
                InitiateEncounter();
            }

        }

        // initiate combat
        private void attackButton_Click(object sender, EventArgs e)
        {
            // disable attack button to prevent queueing attacks
            attackButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            AddStatusLine("You attack the " + CurrentEnemy.Name + " with your " + Player.CurrentWeapon.Name + ".");

            // TODO: move these procedures to the Character and Fish classes
            // attack fish
            Thread.Sleep(800);
            int missRoll = new Random().Next(0, 100) + 1;
            // better weapons hit more often
            bool miss = (missRoll <= (WeaponList.Count - Player.CurrentWeapon.ID));
            int dmg = (new Random()).Next(Player.CurrentWeapon.MinDmg, Player.CurrentWeapon.MaxDmg + 1);

            if (miss)
            {
                AddStatusLine("You miss.");
            }
            else
            {
                // attack graphic 
                g = this.CreateGraphics();
                g.DrawImage(attackBmp, new RectangleF(new PointF(334, 71), new SizeF(261, 123)));

                if (dmg == 0)
                {
                    AddStatusLine("You hit the " + CurrentEnemy.Name + ", but cause no damage.");
                }
                else
                {
                    AddStatusLine("You hit the " + CurrentEnemy.Name + ", causing " + dmg.ToString() + "HP of damage.");
                    CurrentEnemy.BaseHP = CurrentEnemy.BaseHP - dmg;
                }
                Thread.Sleep(600);
                CycleDrawArea();
            }

            // check if creature is dead
            if (CurrentEnemy.BaseHP <= 0)
            {
                AddStatusLine("You successfully slaughtered the " + CurrentEnemy.Name + "!");
                Thread.Sleep(150);
                if ((Player.CurrentShip.Cargo.GetWeight() + CurrentEnemy.Weight) > Player.CurrentShip.HoldCap)
                {
                    AddStatusLine("Your cargo hold doesn't have enough space for the " + CurrentEnemy.Name + "'s corpse!");
                }
                else
                {
                    AddStatusLine("You load the " + CurrentEnemy.Name + "'s lifeless corpse into your cargo hold.");
                    Player.CurrentShip.Cargo.AddCargo(CurrentEnemy);
                }
                isDead = true;
            }
            else // fish is still alive and now attacks
            {
                Thread.Sleep(350);
                dmg = (new Random()).Next(CurrentEnemy.MinDmg, CurrentEnemy.MaxDmg + 1); ;
                AddStatusLine("The " + CurrentEnemy.Name + " attacks!");
                missRoll = new Random().Next(0, 100) + 1;
                // tougher fish hit more often
                miss = (missRoll <= (FishList.Count - CurrentEnemy.ID));
                Thread.Sleep(800);
                if (miss)
                {
                    AddStatusLine("The " + CurrentEnemy.Name + " misses.");
                }
                else
                {
                    // attack graphic 
                    g = this.CreateGraphics();
                    g.DrawImage(attackBmp, new RectangleF(new PointF(40, 71), new SizeF(261, 123)));

                    if (dmg == 0)
                    {
                        AddStatusLine("The " + CurrentEnemy.Name + " hits you, but cause no damage.");
                    }
                    else
                    {
                        AddStatusLine("The " + CurrentEnemy.Name + " hits you, causing " + dmg.ToString() + "HP of damage.");
                        Player.CurrentShip.CurrentHP -= dmg;
                    }
                    Thread.Sleep(600);
                    CycleDrawArea();

                    // Check if player is dead
                    if (Player.CurrentShip.CurrentHP <= 0)
                    {
                        UpdateLabels();
                        AddStatusLine("Your vessel has been destroyed, leaving you helplessly adrift.");
                        Thread.Sleep(350);
                        AddStatusLine("The " + CurrentEnemy.Name + " eats you.");
                        AddStatusLine("Game Over.");
                        gameStarted = false;
                    }
                }
            }
            // re-enable attack button
            attackButton.Enabled = true;
            this.Cursor = Cursors.Default;     

            inPort = false;
            inCombat = true;
            UpdateLabels();
            UpdateControls();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            // disable return to port button
            returnButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            // Stop patrolling if currently doing so
            if ((patrolThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                || (patrolThread.ThreadState == System.Threading.ThreadState.Running))
            {
                patrolThread.Suspend();
            }

            AddStatusLine("Returning to port...");
            // animate the background a few steps
            for (int j = 0; j < 6; j++)
            {
                waterSwitch = !waterSwitch;
                CycleDrawArea();
                Thread.Sleep(800);
            }
            AddStatusLine("You have arrived safely back at Hyannis Port, Massachusetts.");

            inPort = true;
            inCombat = false;
            isDead = false;
            UpdateControls();

            // recharge HP
            if (Player.CurrentShip.CurrentHP < Player.CurrentShip.HP)
            {
                AddStatusLine("You repair the damage to your ship.");
                Player.CurrentShip.CurrentHP = Player.CurrentShip.HP;
            }
            UpdateLabels();
            this.Cursor = Cursors.Default;
        }

        private void dumpButton_Click(object sender, EventArgs e)
        {
            AddStatusLine("You scuttle your cargo into the ocean.");
            Player.CurrentShip.Cargo.Clear();

            UpdateControls();
            UpdateLabels();
        }

        private void factoryButton_Click(object sender, EventArgs e)
        {
            FactoryDialog = new FactoryForm(Player);
            FactoryDialog.Display();
            UpdateLabels();
        }

        private void shipyardButton_Click(object sender, EventArgs e)
        {
            ShipYardDialog = new shipyardForm(ShipList, WeaponList, Player);
            ShipYardDialog.Display();
            UpdateLabels();
        }
    }
}