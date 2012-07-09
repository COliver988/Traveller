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
    public partial class AddWorldImage : Form
    {
        private Traveller.World myworld;

        public AddWorldImage(Traveller.World world)
        {
            InitializeComponent();
            this.Text = "add image to " + world.Name;
            this.myworld = world;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "JPG files (*.jpg)|*.jpg|all files (*.*)|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                lblFile.Text = of.FileName;
                btnSave.Enabled = true;
            }
            else
            {
                lblFile.Text = "<no file loaded>";
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (edDesc.Text.Length > 0)
            {
                myworld.saveImage(lblFile.Text, edDesc.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a description for the menu");
                edDesc.Focus();
                return;
            }
        }
    }
}
