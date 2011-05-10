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
    public partial class SetContent : Form
    {
        public SetContent()
        {
            InitializeComponent();
        }

        public string BG
        {
            get
            {
                return uxBackgroundImage.Text;
            }
        }

        public string Music
        {
            get
            {
                return uxMusicFile.Text;
            }
        }

        public string[] Tiles
        {
            get
            {
                return new string[] { uxPlayerImage.Text, uxWalkingImage.Text, uxGroundImage.Text, uxPlatformImage.Text, uxLevelEndImage.Text  };
            }
        }

        public void SetFields(string[] stuff, string bg, string song)
        {
            uxPlayerImage.Text = stuff[0];
            uxLevelEndImage.Text = stuff[4];
            uxGroundImage.Text = stuff[2];
            uxPlatformImage.Text = stuff[3];
            uxWalkingImage.Text = stuff[1];
            uxBackgroundImage.Text = bg;
            uxMusicFile.Text = song;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    uxPlayerImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void uxWalkingBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    uxWalkingImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void uxLevelEndBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    uxLevelEndImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void uxGroundBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    uxGroundImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void uxPlatformBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    uxPlatformImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void uxBackgroundBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    uxBackgroundImage.Text = uxOpenFile.FileName;
                }
            }
        }

        private void uxMusicBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = uxOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (uxOpenFile.FileName != "")
                {
                    uxMusicFile.Text = uxOpenFile.FileName;
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
