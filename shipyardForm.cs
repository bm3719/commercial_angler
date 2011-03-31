using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace commercial_angler
{
    public partial class shipyardForm : Form
    {
        public List<Ship> ShipList;
        public List<Weapon> WeaponList;
        public Character Player;
        public float shipTradeIn;
        public float weaponTradeIn;

        public shipyardForm(List<Ship> inShipList, List<Weapon> inWeaponList, Character inPlayer)
        {
            ShipList = inShipList;
            WeaponList = inWeaponList;
            Player = inPlayer;

            InitializeComponent();

            // disable maximize button
            this.MaximizeBox = false;

            // Fill ship combobox
            foreach (Ship s in ShipList)
            {
                shipComboBox.Items.Add(s.Name);
            }

            shipComboBox.SelectedIndex = 0;
            // Fill weapon combobox
            foreach (Weapon s in WeaponList)
            {
                weaponComboBox.Items.Add(s.Name);
            }

            weaponComboBox.SelectedIndex = 0;
        }

        private void upgradeShipButton_Click(object sender, EventArgs e)
        {
            if (shipComboBox.SelectedIndex >= 0)
            {
                float cost = ShipList.ToArray()[shipComboBox.SelectedIndex].Cost - (Player.CurrentShip.Cost / 2);
                if (cost > Player.Money)
                {
                    MessageBox.Show("You do not have enough money.");
                }
                else
                {
                    Player.Money = Player.Money - cost;
                    Player.CurrentShip = ShipList.ToArray()[shipComboBox.SelectedIndex].Copy();
                }
                UpdateLabels();
            }
        }

        private void upgradeWeaponButton_Click(object sender, EventArgs e)
        {
            if (shipComboBox.SelectedIndex >= 0)
            {
                float cost = (WeaponList.ToArray()[weaponComboBox.SelectedIndex].Price - (Player.CurrentWeapon.Price / 2));
                if (cost > Player.Money)
                {
                    MessageBox.Show("You do not have enough money.");
                }
                else
                {
                    Player.Money = Player.Money - cost;
                    Player.CurrentWeapon = WeaponList.ToArray()[weaponComboBox.SelectedIndex];
                }
                UpdateLabels();
            }
        }

        public DialogResult Display()
        {
            shipTradeInLabel.Text = (Player.CurrentShip.Cost / 2).ToString("C");
            weaponTradeInLabel.Text = (Player.CurrentWeapon.Price / 2).ToString("C");

            return base.ShowDialog();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void shipComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void weaponComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            if (shipComboBox.Items.Count > 0)
            {
                // ship labels
                float cost1 = ShipList.ToArray()[shipComboBox.SelectedIndex].Cost - (Player.CurrentShip.Cost / 2);
                upgradeShipButton.Enabled = (cost1 > Player.Money) ? false : true;
                shipHoldCapLabel.Text = ShipList.ToArray()[shipComboBox.SelectedIndex].HoldCap.ToString(); ;
                shipHPLabel.Text = ShipList.ToArray()[shipComboBox.SelectedIndex].HP.ToString();
                shipPriceLabel.Text = ShipList.ToArray()[shipComboBox.SelectedIndex].Cost.ToString("C");
                shipTotalLabel.Text = cost1.ToString("C");
                if (cost1 > Player.Money)
                {
                    shipTotalLabel.ForeColor = Color.Red;
                }
                else
                {
                    shipTotalLabel.ForeColor = Color.Black;
                }
                //dont allow purchase of already owned ships
                if (shipComboBox.SelectedIndex == (Player.CurrentShip.ID - 1))
                {
                    upgradeShipButton.Enabled = false;
                }
                if (shipComboBox.SelectedIndex < (Player.CurrentShip.ID - 1))
                {
                    upgradeShipButton.Text = "Downgrade Ship";
                }
                else if (shipComboBox.SelectedIndex > (Player.CurrentShip.ID - 1))
                {
                    upgradeShipButton.Text = "Upgrade Ship";
                }
                else 
                {
                    upgradeShipButton.Text = "Ship Owned";
                }
            }

            if (weaponComboBox.Items.Count > 0)
            {
                // weapon labels
                float cost2 = (WeaponList.ToArray()[weaponComboBox.SelectedIndex].Price - (Player.CurrentWeapon.Price / 2));
                upgradeWeaponButton.Enabled = (cost2 > Player.Money) ? false : true;
                weaponMaxDmgLabel.Text = WeaponList.ToArray()[weaponComboBox.SelectedIndex].MaxDmg.ToString();
                weaponMinDmgLabel.Text = WeaponList.ToArray()[weaponComboBox.SelectedIndex].MinDmg.ToString();
                weaponPriceLabel.Text = WeaponList.ToArray()[weaponComboBox.SelectedIndex].Price.ToString("C");
                weaponTotalLabel.Text = cost2.ToString("C");
                if (cost2 > Player.Money)
                {
                    weaponTotalLabel.ForeColor = Color.Red;
                }
                else
                {
                    weaponTotalLabel.ForeColor = Color.Black;
                }
                //dont allow purchase of already owned weapons
                if (weaponComboBox.SelectedIndex == (Player.CurrentWeapon.ID - 1))
                {
                    upgradeWeaponButton.Enabled = false;
                }
                if (weaponComboBox.SelectedIndex < (Player.CurrentWeapon.ID - 1))
                {
                    upgradeWeaponButton.Text = "Downgrade Weapon";
                }
                else if (weaponComboBox.SelectedIndex > (Player.CurrentWeapon.ID - 1))
                {
                    upgradeWeaponButton.Text = "Upgrade Weapon";
                }
                else
                {
                    upgradeWeaponButton.Text = "Weapon Owned";
                }
            }

        }
    }
}