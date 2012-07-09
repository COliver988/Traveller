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
    public partial class LogForm : Form
    {
        public LogForm(List<string> results)
        {
            InitializeComponent();

            foreach (string line in results)
            {
                lvLog.Items.Add(line);
            }
        }
    }
}
