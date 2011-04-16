using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlatformerGame
{
    public partial class NewLevel : Form
    {
        public NewLevel()
        {
            InitializeComponent();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
        public int FindWidth
        {
            get
            {
                return Convert.ToInt32(uxWidth.Value);
            }
        }
        public int FindHeight
        {
            get
            {
                return Convert.ToInt32(uxHeight.Value);
            }
        }
        public string FindImage
        {
            get
            {
                return uxBackground.Text;
            }
        }
        public string FindName
        {
            get
            {
                return uxLevelName.Text;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
