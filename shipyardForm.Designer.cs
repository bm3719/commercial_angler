namespace commercial_angler
{
    partial class shipyardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(shipyardForm));
            this.upgradeWeaponButton = new System.Windows.Forms.Button();
            this.upgradeShipButton = new System.Windows.Forms.Button();
            this.shipComboBox = new System.Windows.Forms.ComboBox();
            this.weaponComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.shipHoldCapLabel = new System.Windows.Forms.Label();
            this.shipHPLabel = new System.Windows.Forms.Label();
            this.shipPriceLabel = new System.Windows.Forms.Label();
            this.shipTradeInLabel = new System.Windows.Forms.Label();
            this.shipTotalLabel = new System.Windows.Forms.Label();
            this.weaponMinDmgLabel = new System.Windows.Forms.Label();
            this.weaponMaxDmgLabel = new System.Windows.Forms.Label();
            this.weaponPriceLabel = new System.Windows.Forms.Label();
            this.weaponTradeInLabel = new System.Windows.Forms.Label();
            this.weaponTotalLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // upgradeWeaponButton
            // 
            this.upgradeWeaponButton.Location = new System.Drawing.Point(230, 216);
            this.upgradeWeaponButton.Name = "upgradeWeaponButton";
            this.upgradeWeaponButton.Size = new System.Drawing.Size(122, 23);
            this.upgradeWeaponButton.TabIndex = 4;
            this.upgradeWeaponButton.Text = "Purchase Weapon";
            this.upgradeWeaponButton.UseVisualStyleBackColor = true;
            this.upgradeWeaponButton.Click += new System.EventHandler(this.upgradeWeaponButton_Click);
            // 
            // upgradeShipButton
            // 
            this.upgradeShipButton.Location = new System.Drawing.Point(56, 216);
            this.upgradeShipButton.Name = "upgradeShipButton";
            this.upgradeShipButton.Size = new System.Drawing.Size(122, 23);
            this.upgradeShipButton.TabIndex = 3;
            this.upgradeShipButton.Text = "Purchase Ship";
            this.upgradeShipButton.UseVisualStyleBackColor = true;
            this.upgradeShipButton.Click += new System.EventHandler(this.upgradeShipButton_Click);
            // 
            // shipComboBox
            // 
            this.shipComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.shipComboBox.FormattingEnabled = true;
            this.shipComboBox.Location = new System.Drawing.Point(36, 56);
            this.shipComboBox.Name = "shipComboBox";
            this.shipComboBox.Size = new System.Drawing.Size(156, 21);
            this.shipComboBox.TabIndex = 1;
            this.shipComboBox.SelectedIndexChanged += new System.EventHandler(this.shipComboBox_SelectedIndexChanged);
            // 
            // weaponComboBox
            // 
            this.weaponComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.weaponComboBox.FormattingEnabled = true;
            this.weaponComboBox.Location = new System.Drawing.Point(217, 56);
            this.weaponComboBox.Name = "weaponComboBox";
            this.weaponComboBox.Size = new System.Drawing.Size(156, 21);
            this.weaponComboBox.TabIndex = 2;
            this.weaponComboBox.SelectedIndexChanged += new System.EventHandler(this.weaponComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Available Ships:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Available Weapons:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Total: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Price: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Max Dmg: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Min Dmg: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(203, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Trade-in Value: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Trade-in Value: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Hold Capacity: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(57, 117);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Ship HP: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(71, 143);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Price: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(71, 190);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Total: ";
            // 
            // shipHoldCapLabel
            // 
            this.shipHoldCapLabel.AutoSize = true;
            this.shipHoldCapLabel.Location = new System.Drawing.Point(115, 92);
            this.shipHoldCapLabel.Name = "shipHoldCapLabel";
            this.shipHoldCapLabel.Size = new System.Drawing.Size(27, 13);
            this.shipHoldCapLabel.TabIndex = 17;
            this.shipHoldCapLabel.Text = "N/A";
            // 
            // shipHPLabel
            // 
            this.shipHPLabel.AutoSize = true;
            this.shipHPLabel.Location = new System.Drawing.Point(115, 117);
            this.shipHPLabel.Name = "shipHPLabel";
            this.shipHPLabel.Size = new System.Drawing.Size(27, 13);
            this.shipHPLabel.TabIndex = 18;
            this.shipHPLabel.Text = "N/A";
            // 
            // shipPriceLabel
            // 
            this.shipPriceLabel.AutoSize = true;
            this.shipPriceLabel.Location = new System.Drawing.Point(115, 143);
            this.shipPriceLabel.Name = "shipPriceLabel";
            this.shipPriceLabel.Size = new System.Drawing.Size(27, 13);
            this.shipPriceLabel.TabIndex = 19;
            this.shipPriceLabel.Text = "N/A";
            // 
            // shipTradeInLabel
            // 
            this.shipTradeInLabel.AutoSize = true;
            this.shipTradeInLabel.Location = new System.Drawing.Point(115, 167);
            this.shipTradeInLabel.Name = "shipTradeInLabel";
            this.shipTradeInLabel.Size = new System.Drawing.Size(27, 13);
            this.shipTradeInLabel.TabIndex = 20;
            this.shipTradeInLabel.Text = "N/A";
            // 
            // shipTotalLabel
            // 
            this.shipTotalLabel.AutoSize = true;
            this.shipTotalLabel.Location = new System.Drawing.Point(115, 190);
            this.shipTotalLabel.Name = "shipTotalLabel";
            this.shipTotalLabel.Size = new System.Drawing.Size(27, 13);
            this.shipTotalLabel.TabIndex = 21;
            this.shipTotalLabel.Text = "N/A";
            // 
            // weaponMinDmgLabel
            // 
            this.weaponMinDmgLabel.AutoSize = true;
            this.weaponMinDmgLabel.Location = new System.Drawing.Point(290, 92);
            this.weaponMinDmgLabel.Name = "weaponMinDmgLabel";
            this.weaponMinDmgLabel.Size = new System.Drawing.Size(27, 13);
            this.weaponMinDmgLabel.TabIndex = 22;
            this.weaponMinDmgLabel.Text = "N/A";
            // 
            // weaponMaxDmgLabel
            // 
            this.weaponMaxDmgLabel.AutoSize = true;
            this.weaponMaxDmgLabel.Location = new System.Drawing.Point(290, 117);
            this.weaponMaxDmgLabel.Name = "weaponMaxDmgLabel";
            this.weaponMaxDmgLabel.Size = new System.Drawing.Size(27, 13);
            this.weaponMaxDmgLabel.TabIndex = 23;
            this.weaponMaxDmgLabel.Text = "N/A";
            // 
            // weaponPriceLabel
            // 
            this.weaponPriceLabel.AutoSize = true;
            this.weaponPriceLabel.Location = new System.Drawing.Point(290, 143);
            this.weaponPriceLabel.Name = "weaponPriceLabel";
            this.weaponPriceLabel.Size = new System.Drawing.Size(27, 13);
            this.weaponPriceLabel.TabIndex = 24;
            this.weaponPriceLabel.Text = "N/A";
            // 
            // weaponTradeInLabel
            // 
            this.weaponTradeInLabel.AutoSize = true;
            this.weaponTradeInLabel.Location = new System.Drawing.Point(290, 167);
            this.weaponTradeInLabel.Name = "weaponTradeInLabel";
            this.weaponTradeInLabel.Size = new System.Drawing.Size(27, 13);
            this.weaponTradeInLabel.TabIndex = 25;
            this.weaponTradeInLabel.Text = "N/A";
            // 
            // weaponTotalLabel
            // 
            this.weaponTotalLabel.AutoSize = true;
            this.weaponTotalLabel.Location = new System.Drawing.Point(290, 190);
            this.weaponTotalLabel.Name = "weaponTotalLabel";
            this.weaponTotalLabel.Size = new System.Drawing.Size(27, 13);
            this.weaponTotalLabel.TabIndex = 26;
            this.weaponTotalLabel.Text = "N/A";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(145, 258);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(122, 23);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Return to Dock";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // shipyardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 293);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.weaponTotalLabel);
            this.Controls.Add(this.weaponTradeInLabel);
            this.Controls.Add(this.weaponPriceLabel);
            this.Controls.Add(this.weaponMaxDmgLabel);
            this.Controls.Add(this.weaponMinDmgLabel);
            this.Controls.Add(this.shipTotalLabel);
            this.Controls.Add(this.shipTradeInLabel);
            this.Controls.Add(this.shipPriceLabel);
            this.Controls.Add(this.shipHPLabel);
            this.Controls.Add(this.shipHoldCapLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.weaponComboBox);
            this.Controls.Add(this.shipComboBox);
            this.Controls.Add(this.upgradeShipButton);
            this.Controls.Add(this.upgradeWeaponButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "shipyardForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nyannis Shipyard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button upgradeWeaponButton;
        private System.Windows.Forms.Button upgradeShipButton;
        private System.Windows.Forms.ComboBox shipComboBox;
        private System.Windows.Forms.ComboBox weaponComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label shipHoldCapLabel;
        private System.Windows.Forms.Label shipHPLabel;
        private System.Windows.Forms.Label shipPriceLabel;
        private System.Windows.Forms.Label shipTradeInLabel;
        private System.Windows.Forms.Label shipTotalLabel;
        private System.Windows.Forms.Label weaponMinDmgLabel;
        private System.Windows.Forms.Label weaponMaxDmgLabel;
        private System.Windows.Forms.Label weaponPriceLabel;
        private System.Windows.Forms.Label weaponTradeInLabel;
        private System.Windows.Forms.Label weaponTotalLabel;
        private System.Windows.Forms.Button exitButton;
    }
}