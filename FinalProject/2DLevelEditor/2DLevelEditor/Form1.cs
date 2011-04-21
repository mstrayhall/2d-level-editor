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

    public partial class Form1 : Form
    {
        int width;
        int height;
        Tile[,] board;
        string[] images;
        public Form1()
        {
            InitializeComponent();
            width = 30;
            height = 20;
            board = new Tile[height, width];
            for(int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i,j] = new Tile();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void solidPlatformToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode current = treeView1.SelectedNode;
            int X = e.X / 32;
            int Y = e.Y / 32;
            board[Y, X].ImageFile = images[current.SelectedImageIndex];
            board[Y, X].TileType = current.Text;
            Image pic = pictureBox1.Image;
            if (pic == null)
            {
                pic = new Bitmap(width*32, height*32);
            }
            Graphics level = Graphics.FromImage(pic);
            Bitmap tile = new Bitmap(images[current.SelectedImageIndex]);
            Rectangle area = new Rectangle(X*32, Y*32, 32, 32);
            level.DrawImage(tile, area);
            pictureBox1.Image = pic;
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLevel newlevel = new NewLevel();
            DialogResult result = newlevel.ShowDialog();
            if (result == DialogResult.OK)
            {
                width = newlevel.FindWidth;
                height = newlevel.FindHeight;
                string bgimage = newlevel.FindImage;
                if (bgimage != "")
                {
                    pictureBox1.BackgroundImage = new Bitmap(bgimage);
                }
                this.Text = newlevel.FindName;
                FormSet(width*32, height*32);
                pictureBox1.Image = new Bitmap(width * 32, height * 32);
            }
        }

        private void FormSet(int w, int h)
        {
            panel1.Height = h;
            panel1.Width = w;
            pictureBox1.Height = h;
            pictureBox1.Width = w;
            treeView1.Height = h;
            treeView1.Left = w + 1;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void SetImage_Click(object sender, EventArgs e)
        {

        }

        private void setImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetImages form = new SetImages();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                imageList1.Images.Clear();
                images = form.Images;
                foreach (string s in images)
                {
                    Image pict = new Bitmap(s);
                    imageList1.Images.Add(pict);
                }
                Image pic = pictureBox1.Image;
                if (pic == null)
                {
                    pic = new Bitmap(width * 32, height * 32);
                }
                Graphics level = Graphics.FromImage(pic);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (board[i, j].TileType == "Player")
                        {
                            board[i, j].ImageFile = images[0];
                        }
                        else if (board[i, j].TileType == "LevelEnd")
                        {
                            board[i, j].ImageFile = images[1];
                        }
                        else if (board[i, j].TileType == "Ground")
                        {
                            board[i, j].ImageFile = images[2];
                        }
                        else if (board[i, j].TileType == "Platform")
                        {
                            board[i, j].ImageFile = images[3];
                        }
                        else if (board[i, j].TileType == "WalkingEnemy")
                        {
                            board[i, j].ImageFile = images[4];
                        }
                        if (board[i, j].ImageFile != "")
                        {
                            Bitmap tile = new Bitmap(board[i, j].ImageFile);
                            Rectangle area = new Rectangle(j * 32, i * 32, 32, 32);
                            level.DrawImage(tile, area);
                        }
                    }
                }
                pictureBox1.Image = pic;
            }
        }
    }
}
