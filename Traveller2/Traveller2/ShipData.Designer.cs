namespace Traveller2
{
    partial class ShipData
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.edName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.edCargo = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.edMonthly = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.edDay = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.edYear = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rbCT = new System.Windows.Forms.RadioButton();
            this.rbT5 = new System.Windows.Forms.RadioButton();
            this.rbMongoose = new System.Windows.Forms.RadioButton();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.edCredits = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.edPerJump = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbWorld = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblDataFile = new System.Windows.Forms.Label();
            this.btnDataFile = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.edTradeDM = new System.Windows.Forms.NumericUpDown();
            this.edMan = new System.Windows.Forms.NumericUpDown();
            this.edPower = new System.Windows.Forms.NumericUpDown();
            this.edJump = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.edSectorName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.ckIllegal = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.edTradeDM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edJump)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ship Name:";
            // 
            // edName
            // 
            this.edName.Location = new System.Drawing.Point(126, 6);
            this.edName.Name = "edName";
            this.edName.Size = new System.Drawing.Size(211, 20);
            this.edName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Manuever";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Power";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Jump";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cargo (dTon capacity)";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(123, 591);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(134, 23);
            this.btnAdd.TabIndex = 999;
            this.btnAdd.Text = "create ship\'s file";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // edCargo
            // 
            this.edCargo.Location = new System.Drawing.Point(126, 133);
            this.edCargo.Mask = "00000";
            this.edCargo.Name = "edCargo";
            this.edCargo.Size = new System.Drawing.Size(50, 20);
            this.edCargo.TabIndex = 50;
            this.edCargo.ValidatingType = typeof(int);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Monthly costs";
            // 
            // edMonthly
            // 
            this.edMonthly.Location = new System.Drawing.Point(126, 164);
            this.edMonthly.Mask = "0000000";
            this.edMonthly.Name = "edMonthly";
            this.edMonthly.Size = new System.Drawing.Size(50, 20);
            this.edMonthly.TabIndex = 60;
            this.edMonthly.ValidatingType = typeof(int);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Current date";
            // 
            // edDay
            // 
            this.edDay.Location = new System.Drawing.Point(126, 262);
            this.edDay.Mask = "000";
            this.edDay.Name = "edDay";
            this.edDay.Size = new System.Drawing.Size(28, 20);
            this.edDay.TabIndex = 80;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(159, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "-";
            // 
            // edYear
            // 
            this.edYear.Location = new System.Drawing.Point(179, 262);
            this.edYear.Mask = "0000";
            this.edYear.Name = "edYear";
            this.edYear.Size = new System.Drawing.Size(37, 20);
            this.edYear.TabIndex = 90;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 296);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Version";
            // 
            // rbCT
            // 
            this.rbCT.AutoSize = true;
            this.rbCT.Location = new System.Drawing.Point(125, 294);
            this.rbCT.Name = "rbCT";
            this.rbCT.Size = new System.Drawing.Size(58, 17);
            this.rbCT.TabIndex = 100;
            this.rbCT.TabStop = true;
            this.rbCT.Text = "Classic";
            this.rbCT.UseVisualStyleBackColor = true;
            // 
            // rbT5
            // 
            this.rbT5.AutoSize = true;
            this.rbT5.Location = new System.Drawing.Point(125, 317);
            this.rbT5.Name = "rbT5";
            this.rbT5.Size = new System.Drawing.Size(38, 17);
            this.rbT5.TabIndex = 110;
            this.rbT5.TabStop = true;
            this.rbT5.Text = "T5";
            this.rbT5.UseVisualStyleBackColor = true;
            // 
            // rbMongoose
            // 
            this.rbMongoose.AutoSize = true;
            this.rbMongoose.Location = new System.Drawing.Point(125, 340);
            this.rbMongoose.Name = "rbMongoose";
            this.rbMongoose.Size = new System.Drawing.Size(119, 17);
            this.rbMongoose.TabIndex = 120;
            this.rbMongoose.TabStop = true;
            this.rbMongoose.Text = "Mongoose Traveller";
            this.rbMongoose.UseVisualStyleBackColor = true;
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(126, 363);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(91, 17);
            this.rbCustom.TabIndex = 130;
            this.rbCustom.TabStop = true;
            this.rbCustom.Text = "Custom tables";
            this.rbCustom.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 237);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Initial Credits";
            // 
            // edCredits
            // 
            this.edCredits.Location = new System.Drawing.Point(126, 234);
            this.edCredits.Mask = "0000000";
            this.edCredits.Name = "edCredits";
            this.edCredits.Size = new System.Drawing.Size(60, 20);
            this.edCredits.TabIndex = 70;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 200);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 141;
            this.label11.Text = "per jump costs:";
            // 
            // edPerJump
            // 
            this.edPerJump.Location = new System.Drawing.Point(126, 197);
            this.edPerJump.Mask = "0000000";
            this.edPerJump.Name = "edPerJump";
            this.edPerJump.Size = new System.Drawing.Size(50, 20);
            this.edPerJump.TabIndex = 65;
            this.edPerJump.ValidatingType = typeof(int);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(203, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 13);
            this.label12.TabIndex = 143;
            this.label12.Text = "(monthly mortgage, maintenance)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(203, 200);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 13);
            this.label13.TabIndex = 144;
            this.label13.Text = "(fuel, life support, etc)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1, 464);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 145;
            this.label14.Text = "Start world";
            // 
            // cbWorld
            // 
            this.cbWorld.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbWorld.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbWorld.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWorld.FormattingEnabled = true;
            this.cbWorld.Location = new System.Drawing.Point(126, 455);
            this.cbWorld.Name = "cbWorld";
            this.cbWorld.Size = new System.Drawing.Size(252, 22);
            this.cbWorld.TabIndex = 146;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(0, 387);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 1000;
            this.label15.Text = "SEC data file:";
            // 
            // lblDataFile
            // 
            this.lblDataFile.AutoSize = true;
            this.lblDataFile.Location = new System.Drawing.Point(123, 387);
            this.lblDataFile.Name = "lblDataFile";
            this.lblDataFile.Size = new System.Drawing.Size(39, 13);
            this.lblDataFile.TabIndex = 1001;
            this.lblDataFile.Text = "<pick>";
            // 
            // btnDataFile
            // 
            this.btnDataFile.Location = new System.Drawing.Point(126, 414);
            this.btnDataFile.Name = "btnDataFile";
            this.btnDataFile.Size = new System.Drawing.Size(134, 23);
            this.btnDataFile.TabIndex = 140;
            this.btnDataFile.Text = "browse for file";
            this.btnDataFile.UseVisualStyleBackColor = true;
            this.btnDataFile.Click += new System.EventHandler(this.btnDataFile_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(2, 525);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 1002;
            this.label16.Text = "Buy/Sell DM";
            // 
            // edTradeDM
            // 
            this.edTradeDM.Location = new System.Drawing.Point(123, 523);
            this.edTradeDM.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.edTradeDM.Name = "edTradeDM";
            this.edTradeDM.Size = new System.Drawing.Size(39, 20);
            this.edTradeDM.TabIndex = 1003;
            // 
            // edMan
            // 
            this.edMan.Location = new System.Drawing.Point(126, 32);
            this.edMan.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.edMan.Name = "edMan";
            this.edMan.Size = new System.Drawing.Size(37, 20);
            this.edMan.TabIndex = 1004;
            // 
            // edPower
            // 
            this.edPower.Location = new System.Drawing.Point(126, 63);
            this.edPower.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.edPower.Name = "edPower";
            this.edPower.Size = new System.Drawing.Size(37, 20);
            this.edPower.TabIndex = 1005;
            // 
            // edJump
            // 
            this.edJump.Location = new System.Drawing.Point(126, 96);
            this.edJump.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.edJump.Name = "edJump";
            this.edJump.Size = new System.Drawing.Size(37, 20);
            this.edJump.TabIndex = 1006;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(203, 525);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(102, 13);
            this.label17.TabIndex = 1007;
            this.label17.Text = "(Trader, Broker, etc)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(0, 493);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 1008;
            this.label18.Text = "Sector name";
            // 
            // edSectorName
            // 
            this.edSectorName.Location = new System.Drawing.Point(125, 490);
            this.edSectorName.Name = "edSectorName";
            this.edSectorName.Size = new System.Drawing.Size(253, 20);
            this.edSectorName.TabIndex = 1009;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(2, 558);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 13);
            this.label19.TabIndex = 1010;
            this.label19.Text = "Allow illegals (Mongoose)";
            // 
            // ckIllegal
            // 
            this.ckIllegal.AutoSize = true;
            this.ckIllegal.Location = new System.Drawing.Point(126, 557);
            this.ckIllegal.Name = "ckIllegal";
            this.ckIllegal.Size = new System.Drawing.Size(169, 17);
            this.ckIllegal.TabIndex = 1011;
            this.ckIllegal.Text = "(checked means do not re-roll)";
            this.ckIllegal.UseVisualStyleBackColor = true;
            // 
            // ShipData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 617);
            this.Controls.Add(this.ckIllegal);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.edSectorName);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.edJump);
            this.Controls.Add(this.edPower);
            this.Controls.Add(this.edMan);
            this.Controls.Add(this.edTradeDM);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnDataFile);
            this.Controls.Add(this.lblDataFile);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cbWorld);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.edPerJump);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.edCredits);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rbCustom);
            this.Controls.Add(this.rbMongoose);
            this.Controls.Add(this.rbT5);
            this.Controls.Add(this.rbCT);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.edYear);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.edDay);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.edMonthly);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.edCargo);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edName);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "ShipData";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ShipData";
            ((System.ComponentModel.ISupportInitialize)(this.edTradeDM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edJump)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.MaskedTextBox edCargo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox edMonthly;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox edDay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox edYear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbCT;
        private System.Windows.Forms.RadioButton rbT5;
        private System.Windows.Forms.RadioButton rbMongoose;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox edCredits;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MaskedTextBox edPerJump;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbWorld;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblDataFile;
        private System.Windows.Forms.Button btnDataFile;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown edTradeDM;
        private System.Windows.Forms.NumericUpDown edMan;
        private System.Windows.Forms.NumericUpDown edPower;
        private System.Windows.Forms.NumericUpDown edJump;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox edSectorName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox ckIllegal;
    }
}