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
    public partial class ShowImage : Form
    {
        public ShowImage(string image, string desc)
        {
            InitializeComponent();
            pictureBox1.Load(image);
            this.Text = desc;
        }
    }
}
