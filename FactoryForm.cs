using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace commercial_angler
{
    public partial class FactoryForm : Form
    {
        private Character Player;

        public FactoryForm(Character inPlayer)
        {
            Player = inPlayer;

            InitializeComponent();

            // disable maximize button
            this.MaximizeBox = false;

            UpdateLabels();
            UpdateCargoListBox();
        }

        public DialogResult Display()
        {
            return base.ShowDialog();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sellSelectedButton_Click(object sender, EventArgs e)
        {
            if (cargoListBox.SelectedIndex >= 0)
            {
                Sell(cargoListBox.SelectedIndex);
            }

            UpdateLabels();
            UpdateCargoListBox();
        }

        private void sellAllButton_Click(object sender, EventArgs e)
        {
            if (cargoListBox.Items.Count > 0)
            {
                for (int i = cargoListBox.Items.Count - 1; i >= 0; i--)
                {
                    Sell(i);
                }
            }

            UpdateLabels();
            UpdateCargoListBox();
        }

        private void Sell(int ID)
        {
            float total = Player.CurrentShip.Cargo.ToArray()[ID].Quantity *
                        Player.CurrentShip.Cargo.ToArray()[ID].Value;

            Player.Money += total;

            // remove from cargo
            Player.CurrentShip.Cargo.Remove(Player.CurrentShip.Cargo.ToArray()[ID]);
        }

        private void UpdateLabels()
        {
            moneyLabel.Text = Player.Money.ToString("C");

            int selectedIndex = cargoListBox.SelectedIndex;
            if (Player.CurrentShip.Cargo.Count > 0)
            {
                if (selectedIndex >= 0)
                {
                    sellSelectedButton.Enabled = true;
                    nameLabel.Text = Player.CurrentShip.Cargo.ToArray()[selectedIndex].Name;
                    valueLabel.Text = Player.CurrentShip.Cargo.ToArray()[selectedIndex].Value.ToString("C");
                    quantityLabel.Text = Player.CurrentShip.Cargo.ToArray()[selectedIndex].Quantity.ToString();
                }
                else
                {
                    sellSelectedButton.Enabled = false;
                    nameLabel.Text = "";
                    valueLabel.Text = "";
                    quantityLabel.Text = "";
                }
                sellAllButton.Enabled = true;
            }
            else
            {
                nameLabel.Text = "";
                valueLabel.Text = "";
                quantityLabel.Text = "";
                sellSelectedButton.Enabled = false;
                sellAllButton.Enabled = false;
            }

        }

        private void UpdateCargoListBox()
        {
            int selectedIndex = cargoListBox.SelectedIndex;

            // update the cargo hold listbox
            cargoListBox.Items.Clear();
            if (Player.CurrentShip.Cargo.Count > 0)
            {
                foreach (Fish f in Player.CurrentShip.Cargo)
                {
                    cargoListBox.Items.Add("(" + f.Quantity.ToString() + ") " + f.Name);
                }
            }

            // restore sellected index
            if (selectedIndex >= 0 && cargoListBox.Items.Count > 0)
            {
                cargoListBox.SelectedIndex = selectedIndex;
            }
        }

        private void cargoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }
    }
}