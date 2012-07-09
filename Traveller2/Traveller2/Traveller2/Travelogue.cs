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
    public partial class Travelogue : Form
    {
        public Form1 frm1;

        public Travelogue()
        {
            InitializeComponent();

        }

        public Travelogue(string date, string sec)
        {
            InitializeComponent();
            lblDate.Text = date;
            lblSystem.Text = sec;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (edNote.Text.Length > 0)
            {
                frm1.ship.travelogue(edNote.Text);
                frm1.showTravelogue();
            }
            else
            {
                MessageBox.Show("No message added");
            }
            this.Close();
        }
    }
}
