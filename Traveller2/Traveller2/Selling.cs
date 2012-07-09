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
    public partial class Selling : Form
    {
        public Form1 frm1;

        private Traveller.Starship.cargoDesc cargo;
        private Traveller.World world;
        private Traveller.Starship ship;

        public Selling()
        {
            InitializeComponent();
        }

        /// <summary>
        /// selling a cargo lot
        /// </summary>
        /// <param name="cargo">cargo structure - item we are selling</param>
        /// <param name="ship">Traveller starship - ship it is being sold from</param>
        /// <param name="world">Traveller world - world it is being sold on</param>
        public Selling(Traveller.Starship.cargoDesc cargo, Traveller.Starship ship, Traveller.World world)
        {
            InitializeComponent();

            lblCurrent.Text = world.SEC;
            lblSelling.Text = String.Format("{0} {1} dTons; Cr{2}", cargo.origCode, cargo.dtons, cargo.purchasePrice);
            lblDesc.Text = cargo.desc;
            lblOrigSystem.Text = cargo.origSEC;
            edDM.Text = ship.TradeDM.ToString();

            this.world = world;
            this.cargo = cargo;
            this.ship = ship;

            // and set the selling info
            this.cargo.sellingDate = this.ship.Date;
            this.cargo.destSEC = this.world.SEC;
        }

        // get the sale price info
        private void btnSell_Click(object sender, EventArgs e)
        {
            int DM = 0;
            int.TryParse(edDM.Text, out DM);

            Traveller.Trade trade = new Traveller.Trade(ship, world);
            cargo = trade.sell(cargo, world, DM);
            lblSellAt.Text = String.Format("{0} [actual value: {1}%]", cargo.sellingPrice.ToString(),
                cargo.avSell* 100.00);
        }

        // make the sale:
        // 1. update the cargo item in the ship file as sold
        // 2. set the dialog result to 'Ok' so we know to reload ship info
        private void btnSold_Click(object sender, EventArgs e)
        {
            ship.sellCargo(this.cargo);
            if (ship.ErrMessage.Length == 0)
            {
                ship.travelogue(String.Format("Cargo sold: {0} / {1} tons for Cr{2} ({3})",
                    cargo.origCode, cargo.dtons.ToString(), cargo.sellingPrice.ToString(), cargo.desc));
            }
            else
            {
                MessageBox.Show("Error in selling: " + ship.ErrMessage);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
