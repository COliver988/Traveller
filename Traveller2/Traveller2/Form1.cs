using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

namespace Traveller2
{
    public partial class Form1 : Form
    {
        public Traveller.World world;
        public Traveller.Starship ship;

        public Form1()
        {
            InitializeComponent();

            loadSupport();
            createMap();

            // and see if we've saved off a previous location
            if (Properties.Settings.Default.ShipDataFile.Length > 0)
            {
                showShip(Properties.Settings.Default.ShipDataFile);
            }
            else
            {
                MessageBox.Show("Please load or create a new ship file to play");
            }

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

            stStatus.Text = "ready";
        }

        private void loadSupport()
        {
            // set up support files
            lblCTTrade.Text = Properties.Settings.Default.CTTrade;
            lblT5Trade.Text = Properties.Settings.Default.T5Trade;
            lblMTTrade.Text = Properties.Settings.Default.MTTrade;
            lblCustomTrade.Text = Properties.Settings.Default.CUTrade;

            lblPortModifier.Text = Properties.Settings.Default.PortModifier;
            lblTLModifier.Text = Properties.Settings.Default.TLModifier;
        }

        // see about some sort of mapping
        private void createMap()
        {
            //TableLayoutPanel lp = new TableLayoutPanel();
            //lp.RowCount = 12;
            //lp.ColumnCount = 12;
            //panel2.Controls.Add(lp);
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            stStatus.Text = "saving current settings";
            if (ship != null)
            {
                ship.save();
                Properties.Settings.Default.Save();
            }

            // 1st see if we've a world already loaded, and save off any notes
            if (edNotes.Text.Length > 0 & world != null)
            {
                world.saveNotes(edNotes.Text, edNPC.Text);
            }

        }

        #region jump handling

        /// <summary>
        /// jump to a new system (or even the same)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>save any existing data, add a week to the current date
        /// and make the jump to the new system; save the current system</remarks>
        private void btnJump_Click(object sender, EventArgs e)
        {
            if (cbWorlds.SelectedIndex > 0)
            {
                makeJump(cbWorlds.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Please select a system to jump to");
                cbWorlds.Focus();
            }
        }

        public void makeJump(string secString)
        {

            // 1st see if we've a world already loaded, and save off any notes
            if (edNotes.Text.Length > 0)
            {
                world.saveNotes(edNotes.Text, edNPC.Text);
            }

            // our jump from travelogue
            if (world != null)
            {
                ship.travelogue("Prepping for jumping from " + world.Name);
            }

            // 1st see if we're in jump range
            Traveller.World newWorld = new Traveller.World();
            if (newWorld.isValidSEC(secString))
            {
                newWorld = new Traveller.World(secString, ship.Version);
                if (world == null)
                {
                    world = newWorld;
                }
                Traveller.TravUtils util = new Traveller.TravUtils();
                if (util.calcDistance(newWorld.Hex, world.Hex) <= ship.Jump)
                {
                    ship.makeJump(secString);
                    world = new Traveller.World(secString, ship.Version);
                    showShip(ship.Filename);
                }
                else
                {
                    MessageBox.Show("You can't jump that far!");
                }
            }
            else
            {
                MessageBox.Show(secString, "Invalid SEC format");
            }
        }

        private void showWorld(Traveller.World world)
        {
            Traveller.TravUtils util = new Traveller.TravUtils();
            lvData.Items.Clear();
            edNotes.Clear();
            edNPC.Clear();
            tabInfo.SelectedIndex = 0;  // reset to basic info

            ListViewItem li = new ListViewItem("Starport");
            li.SubItems.Add(world.Starport.ToString());
            lvData.Items.Add(li);
            li = new ListViewItem("Size");
            li.SubItems.Add(world.Size.ToString() + " " + world.descSize);
            lvData.Items.Add(li);
            li = new ListViewItem("Atmosphere");
            li.SubItems.Add(world.Atmosphere.ToString() + " " + world.descAtmo);
            lvData.Items.Add(li);
            li = new ListViewItem("Hydrographics");
            li.SubItems.Add(world.Hydrographics.ToString() + " " + world.descHydro);
            lvData.Items.Add(li);
            li = new ListViewItem("Population");
            li.SubItems.Add(world.Population.ToString() + " " + world.descPop);
            lvData.Items.Add(li);
            li = new ListViewItem("Government");
            li.SubItems.Add(world.Government.ToString() + " " + world.descGov);
            lvData.Items.Add(li);
            li = new ListViewItem("Law Level");
            li.SubItems.Add(world.LawLevel.ToString());
            lvData.Items.Add(li);
            li = new ListViewItem("Tech Level");
            li.SubItems.Add(world.TechLevel.ToString());
            lvData.Items.Add(li);

            li = new ListViewItem("Travel code");
            li.SubItems.Add(world.TravelCode);
            switch (world.TravelCode.ToLower())
            {
                case "red":
                    li.SubItems[0].BackColor = System.Drawing.Color.Red;
                    break;
                case "amber":
                    li.SubItems[0].BackColor = System.Drawing.Color.Yellow;
                    break;
                case "green":
                    li.SubItems[0].BackColor = System.Drawing.Color.Green;
                    break;
                default:
                    break;
            }
            lvData.Items.Add(li);

            li = new ListViewItem("Stellar info");
            li.SubItems.Add(world.Stellar);
            lvData.Items.Add(li);
            li = new ListViewItem("Gas giants");
            li.SubItems.Add(world.GasGiant.ToString());
            lvData.Items.Add(li);
            li = new ListViewItem("Asteroid belts");
            li.SubItems.Add(world.Belts.ToString());
            lvData.Items.Add(li);

            lbTradeCodes.Items.Clear();
            foreach (Traveller.World.stTrade tc in world.TradeClass)
            {
                lbTradeCodes.Items.Add(tc.code + " " + tc.desc);
            }

            if (world.Misc.Count > 0)
            {
                li = new ListViewItem(" ");
                li.SubItems.Add("Extensions");
                li.BackColor = System.Drawing.Color.DarkSalmon;
                lvData.Items.Add(li);
                foreach (string ext in world.Misc)
                {
                    string[] exts = ext.Split(new char[] { ':' });
                    li = new ListViewItem(exts[0]);
                    li.SubItems.Add(exts[1]);
                    lvData.Items.Add(li);
                }
            }

            lblAlliance.Text = world.Alliance + " " + world.descAlliance;

            // show the notes - for some reason it is doing odd on CR/LF
            string[] noteString = world.Notes.Split(new char[] { '\n' });
            StringBuilder newNotes = new StringBuilder();
            foreach (string s in noteString)
            {
                newNotes.Append(s + Environment.NewLine);
            }
            edNotes.Text = newNotes.ToString();

            lblSEC.Text = world.SEC;
            sbCurrentWorld.Text = ship.SectorName + "/" + world.Name;

            showJ6();
            showImage(this.world);

            // and the web page
            string sectorName = ship.SectorName.Replace(' ', '+');
            string url = String.Format("http://www.travellermap.com/iframe.htm?sector={0}&hex={1}",
                sectorName, world.Hex);
            //string url = String.Format("http://www.travellermap.com/JumpMap.aspx?Sector={0}&hex={1}&jump={2}&scale=48&options=48",
            //    sectorName, world.Hex, ship.Jump);
            webBrowser1.Navigate(url);

            loadImages();
        }

        // load any images up
        private void loadImages()
        {
            try
            {
                ((ToolStripMenuItem)menuStrip1.Items["tbImages"]).DropDownItems.Clear();
                if (this.world.Images != null)
                {
                    foreach (string image in this.world.Images)
                    {
                        string[] item = image.Split(new char[] { '|' });
                        ToolStripMenuItem mi = new ToolStripMenuItem(item[0] + ": " + item[1]);
                        ((ToolStripMenuItem)(menuStrip1.Items["tbImages"])).DropDownItems.Add(mi);
                        mi.Click += new EventHandler(subMenu_Click);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in image menu generation: " + ex.Message);
            }
        }

        /// <summary>
        /// load up any images added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            if (menuItem.Text.EndsWith("jpg"))
            {
                try
                {
                    string[] mi = menuItem.Text.Split(new char[] { ':' });
                    string myImage = mi[1];
                    if (mi.Count() > 2)
                    {
                        myImage = myImage + ":" + mi[2];
                    }
                    if (myImage.StartsWith(" "))
                        myImage = myImage.Substring(1);
                    ShowImage si = new ShowImage(myImage, mi[0]);
                    si.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in loading image: " + ex.Message);
                }
            }
        }

        // get the worlds within a J6 jump
        private void showJ6()
        {
            tabInfo.TabPages["tabPageJumpRange"].Text = String.Format("Jump {0} systems", ship.Jump);       
            List<string> systems = world.jumpRange(Properties.Settings.Default.Sector, ship.Jump);
            foreach (string sys in systems)
            {
                ListViewItem li = new ListViewItem(sys);
                Match worldMatch = world.worldRegex.Match(sys);
                string hex = worldMatch.Groups["hex"].Value;
                lvJ6.Items.Add(li);
            }
        }

        // find an image to match
        private void showImage(Traveller.World world)
        {
            string imageName = world.BerkaDesc + "World.jpg";

            try
            {
                pictureBox1.Load(imageName);
            }
            catch 
            {
            }
        }

        // show the ship's travelogue
        public void showTravelogue()
        {
            tvTravelogue.Nodes.Clear();
            if (ship.Log != null)
            {
                TreeNode rootNode = new TreeNode("Ship's log");
                foreach (Traveller.Starship.travelDesc item in ship.Log)
                {
                    TreeNode node = new TreeNode(item.date + " " + item.system);
                    foreach (string note in item.notes)
                    {
                        TreeNode subnode = new TreeNode(note);
                        node.Nodes.Add(subnode);
                    }
                    rootNode.Nodes.Add(node);
                }
                tvTravelogue.Nodes.Add(rootNode);
                rootNode.ExpandAll();
            }
        }

        private void showDate()
        {
            sbImperialDate.Text = ship.Date;
        }

        // show the system clicked
        private void lvJ6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string sec = lvJ6.SelectedItems[0].Text;
            Traveller.World dw = new Traveller.World(sec, ship.Version);
            DisplayWorld dispworld = new DisplayWorld(dw);
            dispworld.frm1 = this;
            dispworld.ShowDialog();
        }

        #endregion

        #region tool strip handling

        #region File

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void addTravelogueNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            Travelogue f = new Travelogue(ship.Date, ship.SEC);
            f.frm1 = this; 
            f.ShowDialog(this);
            showTravelogue();
        }

        private void openExistingShipFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadShip();
        }

        private void createNewShipDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShipData sd = new ShipData();
            sd.frm1 = this;
            if (sd.ShowDialog(this) == DialogResult.OK)
            {
                showShip(Properties.Settings.Default.ShipDataFile);
            }
        }

        /// <summary>
        /// load a new ship's data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadShipFile_Click(object sender, EventArgs e)
        {
            loadShip();
        }

        /// <summary>
        /// edit the current ships data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editCurrentShipsDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editShip();
        }

        private void editShip()
        {
            ShipData sd = new ShipData(this.ship);
            sd.frm1 = this;
            if (sd.ShowDialog(this) == DialogResult.OK)
            {
                showShip(Properties.Settings.Default.ShipDataFile);
            }
        }

        private void addWorldImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddWorldImage awi = new AddWorldImage(this.world);
            awi.ShowDialog();
        }

        #endregion

        #region Report
        // print the current cargo manifest; summary
        private void summaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ship.printManifest(false);
        }

        // print detailed cargo manifest
        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ship.printManifest(true);
        }

        private void cargoHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ship.printHistory();
        }

        private void showJumpMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sectorName = ship.SectorName.Replace(' ', '+');
            string url = String.Format("http://www.travellermap.com/JumpMap.aspx?Sector={0}&hex={1}&jump={2}&scale=48&options=48",
                sectorName, world.Hex, ship.Jump);
            Help.ShowHelp(this, url);
        }


        private void generateOriginCodesForAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportAllCodes();
        }

        /// <summary>
        /// generate a report of all trade codes
        /// </summary>
        private void reportAllCodes()
        {
            List<string> codes = new List<string>();
            foreach (string  line in cbWorlds.Items)
            {
                try
                {
                    Traveller.World tw = new Traveller.World(line, ship.Version);
                    Traveller.Trade trade = new Traveller.Trade(ship, tw);
                    List<Traveller.Starship.cargoDesc> cargoes = trade.findCargo();
                    string cc = String.Format("{0,-25} {1} [{2}] {3}",
                        tw.Name, tw.UWP, tw.Hex, cargoes[0].origCode);
                    codes.Add(cc);
                }
                catch (Exception ex)
                {
                    codes.Add("Error on line: " + line + ": " + ex.Message);
                }
            }
            SaveFileDialog sv = new SaveFileDialog();
            if (sv.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(sv.FileName, codes.ToArray());
            }
        }

        #endregion

        #region about

        // about box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        #endregion

        #endregion

        #region ship handling

        // load a ship from file
        private void loadShip()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "XML files (*.xml)|*.xml";
            if (of.ShowDialog(this) != DialogResult.Cancel)
            {
                showShip(of.FileName);
                lblShipFileName.Text = of.SafeFileName;
                Properties.Settings.Default.ShipDataFile = of.FileName;
                Properties.Settings.Default.Save();
            }
        }

        // load the file; verify it appears ok
        private void showShip(string shipdataname)
        {
            ship = new Traveller.Starship(shipdataname);
            if (ship.ErrMessage.Length > 0)
            {
                MessageBox.Show("Error in loading ship data: " + ship.ErrMessage);
                return;
            }

            lblShipData.Text = shipdataname;

            this.Text = "Traveller Trade for " + ship.Name;
            displayShip();
            loadSector();
        }

        // actually show the ship data
        private void displayShip()
        {
            string[] filename = ship.Filename.Split(new char[] { '/' });
            lblShipFileName.Text = filename[filename.Length - 1];
            lblShipName.Text = ship.Name;
            lblManPowJump.Text = String.Format("Man: {0}; Power: {1}; Jump: {2}",
                ship.Man, ship.Power, ship.Jump);
            lblCargo.Text = String.Format("Total tonnage: {0}; Current cargo: {1}; Avail: {2}",
                ship.Cargo, ship.CargoHeld, ship.Cargo - ship.CargoHeld);
            lblCredits.Text = ship.Credits.ToString();
            sbCredits.Text = "Current credits: Cr" + ship.Credits.ToString();
            showCargo(ship.Manifest);
            showSold(ship.Sold);

            // set the version
            switch (ship.Version)
            {
                case "CT":
                    sbVersion.Text = "Classic Traveller";
                    break;
                case "MT":
                    sbVersion.Text = "Mongoose Traveller";
                    break;
                case "T5":
                    sbVersion.Text = "T5 Traveller";
                    break;
                case "CU":
                    sbVersion.Text = "Custom Traveller";
                    break;
                default:
                    break;
            }
            showDate();
            showTravelogue();

            if (ship.SEC.Length > 0)
            {
                world = new Traveller.World(ship.SEC, ship.Version);
                showWorld(world);
            }
        }

        private void loadSector()
        {
            // clear out anything if we've got it
            cbWorlds.Items.Clear();

            // and load the sector file
            StreamReader rd = new StreamReader(ship.SECDataFile);
            string line = "";
            Traveller.World tw = new Traveller.World();

            while ((line = rd.ReadLine()) != null)
            {
                if (tw.isValidSEC(line))
                {
                    cbWorlds.Items.Add(line);
                }
            }
        }

        // show the current cargo
        private void showCargo(List<Traveller.Starship.cargoDesc> manifest)
        {
            lvShipCargo.Items.Clear();
            if (manifest != null)
            {
                foreach (Traveller.Starship.cargoDesc cargo in manifest)
                {
                    ListViewItem li = new ListViewItem(cargo.desc);
                    li.SubItems.Add(cargo.origCode);
                    li.SubItems.Add(cargo.purchasePrice.ToString());
                    li.SubItems.Add(cargo.dtons.ToString());
                    li.SubItems.Add(cargo.purchaseDate);
                    li.SubItems.Add(cargo.origSEC);
                    li.SubItems.Add(cargo.cargoID.ToString());
                    lvShipCargo.Items.Add(li);
                }
            }
        }

        // show the sold cargo
        private void showSold(List<Traveller.Starship.cargoDesc> manifest)
        {
            lvSold.Items.Clear();
            if (manifest != null)
            {
                foreach (Traveller.Starship.cargoDesc cargo in manifest)
                {
                    ListViewItem li = new ListViewItem(cargo.origCode);
                    li.SubItems.Add(cargo.desc);
                    li.SubItems.Add(cargo.sellingPrice.ToString());
                    li.SubItems.Add(cargo.sellingDate);
                    li.SubItems.Add(cargo.destSEC);
                    lvSold.Items.Add(li);
                }
            }
        }

        #endregion

        #region cargo handling

        // generate the cargo. T5 & CT return a single cargo; Mongoose multiple ones
        private void btnGenerateCargo_Click(object sender, EventArgs e)
        {
            if (ship == null | world == null)
            {
                MessageBox.Show("You must have a ship in port to search for cargo!");
                return;
            }

            Traveller.Trade trade = new Traveller.Trade(ship, world);
            List<Traveller.Starship.cargoDesc> cargoes = trade.findCargo();

            foreach (Traveller.Starship.cargoDesc cargo in cargoes)
            {
                ListViewItem li = new ListViewItem(cargo.origCode);
                li.SubItems.Add(cargo.purchasePrice.ToString());
                li.SubItems.Add(cargo.dtons.ToString());
                li.SubItems.Add(cargo.desc);
                li.SubItems.Add(cargo.basecostbuy.ToString());
                li.SubItems.Add(cargo.avBuy.ToString());
                lvAvailableCargo.Items.Add(li);
            }
        }

        private void btnPassengers_Click(object sender, EventArgs e)
        {
            Traveller.Trade trade = new Traveller.Trade(ship, world);
            int[] passengers = trade.passengers();
            int cost = 0;

            if (passengers[0] > 0)
            {
                cost = passengers[0] * 1000;
                ListViewItem li = new ListViewItem("Passengers - low");
                li.SubItems.Add(cost.ToString());
                li.SubItems.Add(passengers[0].ToString());
                li.SubItems.Add(String.Format("{0} low passengers", passengers[0]));
                lvAvailableCargo.Items.Add(li);
            }
            if (passengers[1] > 0)
            {
                cost = passengers[1] * 8000;
                ListViewItem li = new ListViewItem("Passengers - mid");
                li.SubItems.Add(cost.ToString());
                li.SubItems.Add(passengers[1].ToString());
                li.SubItems.Add(String.Format("{0} mid passengers", passengers[1]));
                lvAvailableCargo.Items.Add(li);
            }
            if (passengers[2] > 0)
            {
                cost = passengers[2] * 10000;
                ListViewItem li = new ListViewItem("Passengers - high");
                li.SubItems.Add(cost.ToString());
                li.SubItems.Add(passengers[2].ToString());
                li.SubItems.Add(String.Format("{0} high passengers", passengers[2]));
                lvAvailableCargo.Items.Add(li);
            }

        }

        // purchase checked items
        // note: if passengers, we need to select the quantity available
        private void btnBuy_Click(object sender, EventArgs e)
        {
            int tons = 0;
            int availableTons = ship.Cargo - ship.CargoHeld;

            foreach (ListViewItem li in lvAvailableCargo.Items)
            {
                if (li.Checked)
                {
                    tons = Convert.ToInt32(li.SubItems[2].Text);
                    if (tons <= availableTons)
                    {
                        availableTons -= tons;
                        ship.travelogue(String.Format("Cargo purchase: {0} / {1} tons for Cr{2} ({3})",
                            li.Text, li.SubItems[2].Text, li.SubItems[1].Text, li.SubItems[3].Text));
                        ship.CargoHeld += tons;

                        // and add to the cargo listing
                        Traveller.Starship.cargoDesc cd = new Traveller.Starship.cargoDesc();
                        cd.desc = li.SubItems[3].Text;
                        cd.origCode = li.Text;
                        cd.origSEC = ship.SEC;
                        cd.sellingPrice = 0;
                        cd.purchaseDate = ship.Date;
                        cd.dtons = Convert.ToInt32(li.SubItems[2].Text);
                        cd.purchasePrice = Convert.ToInt32(li.SubItems[1].Text);
                        int.TryParse(li.SubItems[4].Text, out cd.basecostbuy);
                        double.TryParse(li.SubItems[5].Text, out cd.avBuy);
                        ship.addCargo(cd);
                        ship.Credits -= cd.purchasePrice;
                    }
                    else
                    {
                        MessageBox.Show("Unable to purchase " + li.Text);
                    }
                }          
            }
            ship.save();
            showShip(ship.Filename);
        }

        // and now to sell off our stuff
        private void sellCheckedCargosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in lvShipCargo.Items)
            {
                if (li.Checked)
                {
                    Traveller.Starship.cargoDesc cargo = loadCargo(li);
                    Selling frm = new Selling(cargo, ship, world);
                    frm.frm1 = this;
                    if (frm.ShowDialog() == DialogResult.OK)
                        showShip(ship.Filename);
                }
            }
        }

        private void detaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ship.printManifest(false);
        }

        private void detailedManifestReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ship.printManifest(true);
        }

        // find the cargo based on the listview info
        private Traveller.Starship.cargoDesc loadCargo(ListViewItem li)
        {
            Traveller.Starship.cargoDesc cargo = new Traveller.Starship.cargoDesc();
            int cargoID = 0;
            int.TryParse(li.SubItems[6].Text, out cargoID);
            if (cargoID > 0)
            {
                cargo = ship.loadCargo(cargoID);
            }
            return cargo;
        }

        #endregion

        #region RSS feed
        private void travelogueRSSFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stStatus.Text = "generating RSS feed";

            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "RSS feed (*.xml)|*.xml";
            if (sv.ShowDialog() == DialogResult.Cancel)
                return;

            StreamWriter writer = new StreamWriter(sv.FileName);
            RSSFeeds.RSSFeedGenerator rss = new RSSFeeds.RSSFeedGenerator(writer);
            rss.WriteStartDocument();
            rss.WriteStartChannel("Ship Log for " + ship.Name, "", "A Travelogue",
                "copyright me", ship.Name);

            string lognote = "";
            foreach (Traveller.Starship.travelDesc item in ship.Log)
            {
                foreach (string note in item.notes)
                {
                    lognote += note + " ::: ";
                }
                rss.WriteItem(item.date,
                    "",
                    lognote, ship.Name, DateTime.Now, "Travelogue", "Traveller");
                lognote = "";
            }           

            rss.WriteEndChannel();
            rss.WriteEndDocument();
            rss.Close();

            stStatus.Text = "ready";
        }

        #endregion

        private void ckLimit_CheckStateChanged(object sender, EventArgs e)
        {
            cbWorlds.Items.Clear();
            if (ckLimit.Checked)
            {
                List<string> inRange = world.jumpRange(ship.SECDataFile, ship.Jump);
                foreach (string line in inRange)
                {
                    cbWorlds.Items.Add(line);                   
                }
            }
            else
            {
                loadSector();
            }
        }

        #region cbWorlds context menu

        private void showSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cbWorlds.SelectedText.Length > 0)
            {
                Traveller.World dw = new Traveller.World(cbWorlds.SelectedText, ship.Version);
                DisplayWorld dispworld = new DisplayWorld(dw);
                dispworld.frm1 = this;
                dispworld.ShowDialog();
            }
        }

        private void jumpToThisSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cbWorlds.SelectedIndex > 0)
            {
                makeJump(cbWorlds.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Please select a system to jump to");
                cbWorlds.Focus();
            }
        }

        #endregion

    }
}
