using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Traveller2
{
    public partial class ShipData : Form
    {
        public Form1 frm1;
        private bool isNew = true;      // default to a new ship
        private Traveller.Starship myShip = null;

        public ShipData()
        {
            InitializeComponent();
            this.isNew = false;
        }

        public ShipData(Traveller.Starship ship)
        {
            InitializeComponent();

            loadShip(ship);
            this.myShip = ship;
            btnAdd.Text = "update current ship";
            this.isNew = true;
        }

        private void loadShip(Traveller.Starship ship)
        {
            edName.Text = ship.Name;
            edMan.Value = ship.Man;
            edPower.Value = ship.Power;
            edJump.Value = ship.Jump;
            edCargo.Text = ship.Cargo.ToString();
            edMonthly.Text = ship.MonthlyCosts.ToString();
            edPerJump.Text = ship.PerJumpCost.ToString();
            edCredits.Text = ship.Credits.ToString();
            switch (ship.Version)
            {
                case "CT":
                    rbCT.Checked = true;
                    break;
                case "T5":
                    rbT5.Checked = true;
                    break;
                case "MT":
                    rbMongoose.Checked = true;
                    break;
                case "CU":
                    rbCustom.Checked = true;
                    break;
                default:
                    break;
            }

            rbCT.Enabled = false;
            rbT5.Enabled = false;
            rbMongoose.Enabled = false;
            rbCustom.Enabled = false;
            edName.Enabled = false;

            edDay.Text = ship.Day.ToString();
            edYear.Text = ship.Year.ToString();

            lblDataFile.Text = ship.SECDataFile;
            edSectorName.Text = ship.SectorName;
            edTradeDM.Value = ship.TradeDM;           
            loadDataFile(ship.SECDataFile);
            cbWorld.SelectedIndex = cbWorld.FindString(ship.SEC, 0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!validateData())
            {
                return;
            }

            if (this.isNew)
            {
                loadData();
                this.myShip.save();
                MessageBox.Show("Successfully updated " + this.myShip.Name);
            }
            else
            {
                addNewShip();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void addNewShip()
        {
            int cargo = 0;
            int.TryParse(edCargo.Text, out cargo);
            int monthly = 0;
            int.TryParse(edMonthly.Text, out monthly);
            int perjump = 0;
            int.TryParse(edPerJump.Text, out perjump);
            int credits = 0;
            int.TryParse(edCredits.Text, out credits);
            string version = "CT";      // default
            if (rbCustom.Checked)
                version = "CU";
            if (rbMongoose.Checked)
                version = "MT";
            if (rbT5.Checked)
                version = "T5";
            Traveller.Starship ship = new Traveller.Starship(edName.Text, (int)edMan.Value,
                (int)edPower.Value, (int)edJump.Value, cargo, monthly, perjump,
                Convert.ToInt32(edDay.Text), Convert.ToInt32(edYear.Text), credits, version, lblDataFile.Text,
                cbWorld.SelectedItem.ToString(), edSectorName.Text, (int)edTradeDM.Value, ckIllegal.Checked);
            Properties.Settings.Default.ShipDataFile = edName.Text + ".xml";
            Properties.Settings.Default.Save();
        }

        private bool validateData()
        {
            if (lblDataFile.Text.Length == 0)
            {
                MessageBox.Show("Please select an SEC file for travelling in");
                return false;
            }

            if (cbWorld.Items.Count == 0)
            {
                MessageBox.Show("Please select a valid world to start on!");
                cbWorld.Focus();
                return false;
            }

            if (edName.Text.Length == 0)
            {
                MessageBox.Show("Please enter a valid ship name");
                edName.Focus();
                return false;
            }

            if (edCargo.Text.Length == 0)
            {
                MessageBox.Show("Please enter a valid cargo capacity in dTons");
                edCargo.Focus();
                return false;
            }

            if (edSectorName.Text.Length == 0)
            {
                MessageBox.Show("Please enter a valid sector name for TravellerMap API");
                edSectorName.Focus();
                return false;
            }

            int i = 0;
            int.TryParse(edDay.Text, out i);
            if (i < 0 | i > 365)
            {
                MessageBox.Show("Imperial day ranges from 0..365)");
                edDay.Focus();
                return false;
            }

            if (!rbCT.Checked & !rbT5.Checked & !rbMongoose.Checked & !rbCustom.Checked)
            {
                MessageBox.Show("Please select a version of Traveller to use");
                return false;
            }

            if (cbWorld.SelectedItem.ToString().Length == 0)
            {
                MessageBox.Show("Please select a system to start in");
                cbWorld.Focus();
                return false;
            }

            return true;

        }

        // pick a file
        private void btnDataFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "SEC files (*.sec)|*.sec|all file (*.*)|*.*";
            if (of.ShowDialog() != DialogResult.Cancel)
            {
                lblDataFile.Text = of.SafeFileName;
                loadDataFile(of.FileName);
            }
        }

        private void loadDataFile(string filename)
        {

            // and load our world picker
            cbWorld.Items.Clear();
            StreamReader rd = new StreamReader(filename);
            string line = "";
            Traveller.World tw = new Traveller.World();

            while ((line = rd.ReadLine()) != null)
            {
                if (tw.isValidSEC(line))
                {
                    cbWorld.Items.Add(line);
                }
            }
        }

        /// <summary>
        /// get the data off the screen that we are allwoed to save
        /// </summary>
        private void loadData()
        {
            this.myShip.Name = edName.Text;
            int intVal = 0;
            int.TryParse(edCargo.Text, out intVal);
            if (intVal > 0)
                this.myShip.Cargo = intVal;
            int.TryParse(edDay.Text, out intVal);
            this.myShip.Day = intVal;
            int.TryParse(edYear.Text, out intVal);
            this.myShip.Year = intVal;
            int.TryParse(edCredits.Text, out intVal);
            this.myShip.Credits = intVal;
            int.TryParse(edMonthly.Text, out intVal);
            this.myShip.MonthlyCosts = intVal;
            int.TryParse(edPerJump.Text, out intVal);
            this.myShip.PerJumpCost = intVal;
        }
    }
}
