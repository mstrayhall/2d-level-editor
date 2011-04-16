#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using PlatformerGamePipeline;
using PlatformerGameLibrary;
#endregion

namespace PlatformerGame
{

    public partial class Form1 : Form
    {
        int width;
        int height;
        Tile[,] board;

        string levelName;

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
                    board[i, j].TileType = "Blank Tile";
                }
            }

            levelName = "";
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
            board[Y, X].ImageFile = current.SelectedImageKey;
            board[Y, X].TileType = current.Text;
            Image pic = pictureBox1.Image;
            if (pic == null)
            {
                pic = new Bitmap(width*32, height*32);
            }
            Graphics level = Graphics.FromImage(pic);
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../../PlatformerGameContent/Tiles/");
            string contentPath = Path.GetFullPath(relativePath);
            Bitmap tile = new Bitmap(contentPath + current.SelectedImageKey);
            //Bitmap tile = new Bitmap("../../../../PlatformerGameContent/Tiles" + current.SelectedImageKey);
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
                // TODO: do we need this?
                //this.Text = newlevel.FindName;
                levelName = newlevel.FindName;
                FormSet(width*32, height*32);
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

        // TODO: add credit for this code
        private void SaveMenuClicked(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            // By default, go to directory containing content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;
            fileDialog.FileName = levelName + ".xml";
            fileDialog.Title = "Save Level";
            fileDialog.Filter = "Level Files (*.xml)|*.xml|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLevel(fileDialog.FileName);
            }
        }

        private void SaveLevel(string filename)
        {
            Cursor = Cursors.WaitCursor;

            // Create the LevelContent object
            LevelContent levelSpec = new LevelContent();
            levelSpec.Name = levelName;

            // Set the background
            // TODO: implement a background field somewhere for this optional attribute


            // Create the list of all tile types
            Dictionary<string, string> tileToTextureDict = new Dictionary<string, string>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                //if (node.Parent != null)
                foreach (TreeNode childNode in node.Nodes)
                {
                    // Then this is a child of someone and we want to add its info
                    tileToTextureDict.Add(childNode.Name, childNode.ImageKey);
                }
            }
            levelSpec.TileTypes = new TileContent[tileToTextureDict.Keys.Count];

            // Populate our tile content using the Dict of all tiles
            int index = 0;
            foreach (string tileName in tileToTextureDict.Keys)
            {
                TileContent tileContent = new TileContent();
                tileContent.Name = tileName;
                tileContent.Texture = new ExternalReference<Texture2DContent>("Content/Tiles/" + tileToTextureDict[tileName]);
                levelSpec.TileTypes[index++] = tileContent;
            }

            // Create the TileMap of all tiles that make up the level
            levelSpec.TileMap = new TileMapContent[board.Length];
            index = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Tile curTile = board[i,j];
                    TileMapContent tileMapContent = new TileMapContent();
                    tileMapContent.Name = curTile.TileType;
                    // TODO: get rid of tileType everywhere and replace it with Name field
                    tileMapContent.Position.X = i;
                    tileMapContent.Position.Y = j;
                    levelSpec.TileMap[index++] = tileMapContent;
                }
            }

            // Write the level specification
            XmlWriterSettings settingsWriter = new XmlWriterSettings();
            settingsWriter.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filename, settingsWriter))
            {
                IntermediateSerializer.Serialize<LevelContent>(writer, levelSpec, null);
            }

            Cursor = Cursors.Arrow;
        }

        private void OpenMenuClicked(object sender, EventArgs e)
        {
            // Let the user specify a level to load and make a new form with this data.
            // This includes the level dimensions, name displayed somewhere, background, tiles


            // Get the file to load
            string filename = "";
            LoadLevel(filename);

        }

        private void LoadLevel(string filename)
        {
            Cursor = Cursors.WaitCursor;

            // Read in the level specification
            LevelContent levelSpec;
            XmlReaderSettings settingsReader = new XmlReaderSettings();
            using (XmlReader reader = XmlReader.Create(filename, settingsReader))
            {
                levelSpec = IntermediateSerializer.Deserialize<LevelContent>(reader, null);
            }



            Cursor = Cursors.Arrow;
        }

    }
}
