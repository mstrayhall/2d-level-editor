using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _2DLevelEditor
{
    public partial class SetImages : Form
    {
        public SetImages()
        {
            InitializeComponent();
        }

        public string[] Images
        {
            get
            {
                return new string[] {PlayerImage.Text, LevelEndImage.Text, GroundImage.Text, PlatformImage.Text, WalkingEnemyImage.Text};
            }
        }

        private void PlayerBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    PlayerImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void WalkingBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    WalkingEnemyImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void LevelEndBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    LevelEndImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void GroundBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    GroundImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void PlatformBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    PlatformImage.Text = uxOpenFile.FileName;
                }
            }
        }
    }
}
