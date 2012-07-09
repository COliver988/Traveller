using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Traveller2
{
    public partial class DisplayWorld : Form
    {
        public Form1 frm1;
        private Traveller.World world;

        public DisplayWorld(Traveller.World world)
        {
            InitializeComponent();

            this.world = world;
            lblSEC.Text = world.SEC;
            lblWorld.Text = world.Name;

            this.Text = "System Data for " + world.Name;
         
            showData(world);
        }

        private void showData(Traveller.World world)
        {
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

            li = new ListViewItem("Trade codes");
            string trades= "";
            foreach (Traveller.World.stTrade tc in world.TradeClass)
            {
                trades += tc.code + " " + tc.desc + " ";
            }
            li.SubItems.Add(trades);
            lvData.Items.Add(li);

            li = new ListViewItem("Alliances");
            li.SubItems.Add(world.Alliance + " " + world.descAlliance);
            lvData.Items.Add(li);

            edNotes.Text = world.Notes;
            edNPC.Text = world.NPCNotes;

            showImage(world);
        }

        // find an image to match
        private void showImage(Traveller.World world)
        {
            switch (world.Atmosphere)
            {
                case '0':
                case '1':
                    pictureBox1.Load("space001.jpg");
                    break;
                case '2':
                    pictureBox1.Load("space006.jpg");
                    break;
                case '3':
                case '4':
                    pictureBox1.Load("space002.jpg");
                    break;
                case '5':
                case '6':
                case '7':
                    pictureBox1.Load("space005.jpg");
                    break;
                case '8':
                case '9':
                    pictureBox1.Load("space004.jpg");
                    break;
                case 'A':
                    pictureBox1.Load("space006.jpg");
                    break;
                default:
                    pictureBox1.Load("space007.jpg");
                    break;
            }
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            frm1.makeJump(this.world.SEC);
        }
    }
}
