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
using Microsoft.Xna.Framework;
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
        // The Content Builder and Content Manager are used to load textures
        // at runtime through the Content Pipeline
        ContentBuilder contentBuilder;
        ContentManager contentManager;

        int width;
        int height;
        Tile[,] board;

        string levelName;

        public Form1()
        {
            InitializeComponent();

            contentBuilder = new ContentBuilder();
            contentManager = new ContentManager(monsterControl1.Services,
                                                contentBuilder.OutputDirectory);

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
            System.Drawing.Rectangle area = new System.Drawing.Rectangle(X*32, Y*32, 32, 32);
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

            levelSpec.MapSize.X = height;
            levelSpec.MapSize.Y = width;

            // Set the background
            // TODO: implement a background field somewhere for this optional attribute


            // Create the list of all tile types
            Dictionary<string, string> tileToTextureDict = new Dictionary<string, string>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    // Then this is a child of someone and we want to add its info
                    tileToTextureDict.Add(childNode.Text, childNode.ImageKey);
                }
            }
            levelSpec.TileTypes = new TileContent[tileToTextureDict.Keys.Count];

            // Populate our tile content using the Dict of all tiles
            int index = 0;
            foreach (string tileName in tileToTextureDict.Keys)
            {
                TileContent tileContent = new TileContent();
                tileContent.Name = tileName;
                tileContent.Texture = new ExternalReference<Texture2DContent>("../../../../PlatformerGameContent/Tiles/" + tileToTextureDict[tileName]);
                levelSpec.TileTypes[index++] = tileContent;
            }

            // Create the TileMap of all tiles that make up the level
            levelSpec.TileArray = new TileMapContent[board.Length];
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
                    levelSpec.TileArray[index++] = tileMapContent;
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
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Load Level";

            fileDialog.Filter = "Level Files (*.xml)|*.xml|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadLevel(fileDialog.FileName);
            }
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

            // Set the level name, background image, and size
            // TODO: Should we also repopulate the TreeView based on the tiles included in the XML?
            NewLevel level = new NewLevel();
            levelName = levelSpec.Name;
            level.Name = levelName;
            height = (int)levelSpec.MapSize.X;
            width = (int)levelSpec.MapSize.Y;

            // Reset the background image so we can draw over it.
            pictureBox1.Image = new Bitmap(width * 32, height * 32);

            // Get the rest of the level information
            string bgimage = "";
            if (levelSpec.Background != null)
            {
                // if we even have a background texture, store it here
                bgimage = levelSpec.Background.Filename;
            }
            if (bgimage != "")
            {
                pictureBox1.BackgroundImage = new Bitmap(bgimage);
            }
            FormSet(width * 32, height * 32);


            // Create the list of all tile types
            Dictionary<string, string> tileToTextureDict = new Dictionary<string, string>();
            foreach (TileContent tileSpec in levelSpec.TileTypes)
            {
                tileToTextureDict.Add(tileSpec.Name, tileSpec.Texture.Filename);
            }
            tileToTextureDict.Add("Blank Tile", "");
            // TODO: add Blank Tile to official list when saving the data?

            // Load in the tile information and draw them.
            foreach (TileMapContent tileMapSpec in levelSpec.TileArray)
            {
                Tile tile = new Tile();
                tile.Name = tileMapSpec.Name;
                tile.TileType = tileMapSpec.Name;
                tile.ImageFile = tileToTextureDict[tile.Name];

                Vector2 position = tileMapSpec.Position;
                int posX = (int)position.X;
                int posY = (int)position.Y;
                board[posX, posY] = tile;

                if (tile.ImageFile != "")
                {
                    // only bother drawing if we actually have an image to draw, not blank.
                    Image pic = pictureBox1.Image;
                    if (pic == null)
                    {
                        pic = new Bitmap(width * 32, height * 32);
                    }
                    Graphics levelPic = Graphics.FromImage(pic);
                    Bitmap tileToDraw = new Bitmap(tile.ImageFile);
                    System.Drawing.Rectangle area = new System.Drawing.Rectangle(posY * 32, posX * 32, 32, 32);
                    levelPic.DrawImage(tileToDraw, area);
                    pictureBox1.Image = pic;
                    // TODO: Loop through tiles again to draw this stuff all at once?
                }
            }

            /*
            
            // Load the MonsterParts list from the MonsterContent's Parts array
            MonsterParts.Clear();
            partNames.Clear();
            foreach (MonsterPartContent partSpec in monsterSpec.Parts)
            {
                // Create a new monster part
                Texture2D texture = LoadTexture2D(partSpec.Texture.Filename);
                MonsterPart part = new MonsterPart(partSpec.Name, monsterControl1.GraphicsDevice, texture);

                // Set the part variables
                part.ParentIndex = partSpec.ParentIndex;
            }

            // Clear the heirarchy tree
            heirarchyTreeView.Nodes.Clear();

            // Populate the hierarchy tree from the MonsterPart list
            foreach (MonsterPart part in MonsterParts)
            {
                // Create the TreeView node
                TreeNode node = new TreeNode(part.Name);
                node.Name = part.Name;
                node.Tag = part;

                // Add the new part to the heirarchy treeview
                if (part.ParentIndex != -1)
                {
                    // Add the new part under the parent in the heirarchy
                    TreeNode[] nodeArr = heirarchyTreeView.Nodes.Find(MonsterParts[part.ParentIndex].Name, true);
                    if (nodeArr.Length > 0)
                        nodeArr[0].Nodes.Add(node);
                }
                else
                {
                    // add the part at the root
                    heirarchyTreeView.Nodes.Add(node);
                }
            }

            // Expand the heirarchy list
            heirarchyTreeView.ExpandAll();

            // Refresh the controls
            monsterControl1.Invalidate();
            timelineControl1.Invalidate();
             */


            Cursor = Cursors.Arrow;
        }

    }
}
