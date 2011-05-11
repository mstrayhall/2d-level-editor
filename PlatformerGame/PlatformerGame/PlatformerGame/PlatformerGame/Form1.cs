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
using Microsoft.Xna.Framework.Content.Pipeline.Audio;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using PlatformerGamePipeline;
using PlatformerGameLibrary;
#endregion

namespace PlatformerGame
{

    public partial class uxForm1 : Form
    {
        int width;
        int height;
        string bgimage;
        Bitmap backimage;
        Tile[,] board;
        string[] tiles;
        Bitmap[] tileImages;
        string[] tileTypes;
        int displayW;
        int displayH;
        int currentX;
        int currentY;
        string contentPath;
        string levelName;
        string levelSong;

        public uxForm1(IServiceProvider services)
        {
            InitializeComponent();
            
            width = 30;
            height = 20;
            displayW = 30;
            displayH = 20;
            currentX = 0;
            currentY = 0;
            bgimage = "";
            backimage = new Bitmap(width, height);
            if (width > displayW)
            {
                uxRight.Enabled = true;
            }
            if (height > displayH)
            {
                uxDown.Enabled = true;
            }
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../../PlatformerGameContent/Tiles/");
            contentPath = Path.GetFullPath(relativePath);
            tiles = new string[] { contentPath + "purple.png", contentPath + "red.png", contentPath + "black.png", contentPath + "blue.png", contentPath + "green.png" };
            tileTypes = new string[] { "Player", "WalkingEnemy", "Ground", "Platform", "LevelEnd" };
            tileImages = new Bitmap[5];
            for (int j = 0; j < tiles.Length; j++)
            {
                tileImages[j] = new Bitmap(tiles[j]);
            }
            
            board = new Tile[height, width];
            for(int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i,j] = new Tile();
                }
            }
            levelName = "";
        }

        public string[] Tiles
        {
            get
            {
                return tiles;
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
            PlaceTile(e);
        }

        private void PlaceTile(MouseEventArgs e)
        {
            TreeNode current = treeView1.SelectedNode;
            if (current.Level != 0)
            {
                pictureBox1.Capture = true;
                int X = (e.X / 32) + currentX;
                int Y = (e.Y / 32) + currentY;

                X = (int)MathHelper.Clamp(X, 0, width - 1);
                Y = (int)MathHelper.Clamp(Y, 0, height - 1);
                
                Image pic = pictureBox1.Image;
                if (pic == null)
                {
                    pic = new Bitmap(width * 32, height * 32);
                }
                Graphics level = Graphics.FromImage(pic);
                if (current.Name != "Delete")
                {
                    board[Y, X].ImageFile = tiles[current.SelectedImageIndex - 1];
                    board[Y, X].TileType = current.Name;
                    board[Y, X].ImageIndex = current.SelectedImageIndex - 1;
                    Bitmap tile = tileImages[current.SelectedImageIndex - 1];
                    System.Drawing.Rectangle area = new System.Drawing.Rectangle((X - currentX) * 32, (Y - currentY) * 32, 32, 32);
                    level.DrawImage(tile, area);
                    pictureBox1.Image = pic;
                }
                else
                {
                    board[Y, X].ImageFile = "";
                    board[Y, X].TileType = "Blank Tile";
                    board[Y, X].ImageIndex = -1;
                    if (bgimage != "")
                    {
                        Bitmap blah = new Bitmap(32, 32);
                        Graphics temp = Graphics.FromImage(blah);
                        System.Drawing.Rectangle ar1 = new System.Drawing.Rectangle(0, 0, 32, 32);
                        System.Drawing.Rectangle ar2 = new System.Drawing.Rectangle(X*32, Y*32, 32, 32);
                        temp.DrawImage(backimage, ar1, ar2, GraphicsUnit.Pixel);

                        Bitmap tile = new Bitmap(contentPath + "white.png");
                        System.Drawing.Rectangle area = new System.Drawing.Rectangle((X - currentX) * 32, (Y - currentY) * 32, 32, 32);
                        level.DrawImage(tile, area);
                        level.DrawImage(blah, area);
                    }
                    else
                    {
                        Bitmap tile = new Bitmap(contentPath + "white.png");
                        System.Drawing.Rectangle area = new System.Drawing.Rectangle((X - currentX) * 32, (Y - currentY) * 32, 32, 32);
                        level.DrawImage(tile, area);
                    }
                    pictureBox1.Image = pic;
                }
            }
        }

        private void newLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLevel newlevel = new NewLevel();
            DialogResult result = newlevel.ShowDialog();
            if (result == DialogResult.OK)
            {
                width = newlevel.FindWidth;
                height = newlevel.FindHeight;
                currentX = 0;
                currentY = 0;
                string bgimage = newlevel.FindImage;
                if (bgimage != "")
                {
                    pictureBox1.BackgroundImage = new Bitmap(bgimage);
                }
                else
                {
                    pictureBox1.BackgroundImage = null;
                }
                levelSong = "";
                this.Text = newlevel.FindName + " - DIY 2D Platformer";
                levelName = newlevel.FindName;
                int wid = width * 32;
                int heig = height * 32;
                displayW = width;
                displayH = height;
                if (wid > SystemInformation.PrimaryMonitorSize.Width * 4 / 5)
                {
                    wid = SystemInformation.PrimaryMonitorSize.Width * 4 / 5;
                    displayW = wid / 32;
                }
                if (heig > SystemInformation.PrimaryMonitorSize.Height * 4 / 5)
                {
                    heig = SystemInformation.PrimaryMonitorSize.Height * 4 / 5;
                    displayH = heig / 32;
                }
                FormSet(wid, heig);
                if (width > displayW)
                {
                    uxRight.Enabled = true;
                }
                if (height > displayH)
                {
                    uxDown.Enabled = true;
                }
            }
        }

        private void FormSet(int w, int h)
        {
            panel1.Height = h;
            panel1.Width = w;
            pictureBox1.Height = h;
            pictureBox1.Width = w;
            treeView1.Height = h - 60;
            treeView1.Left = w + 1;
            uxLeft.Left = w + 1;
            uxLeft.Top = h - 60 + 24;
            uxUp.Left = w + 1 + 39;
            uxUp.Top = h - 60 + 24;
            uxDown.Left = w + 1 + 39;
            uxDown.Top = h - 30 + 24;
            uxRight.Left = w + 1 + 39 + 89;
            uxRight.Top = h - 60 + 24;
            pictureBox1.Image = new Bitmap(w, h);
            board = new Tile[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j] = new Tile();
                }
            }
        }

        /// <summary>
        /// Opens a menu to save the level file.
        /// 
        /// Code based on a Windows Forms sample from create.msdn.com used with permission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string fileNameOnly = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                SaveLevel(fileDialog.FileName, fileNameOnly);
            }
        }

        private void SaveLevel(string fileName, string fileNameOnly)
        {
            Cursor = Cursors.WaitCursor;

            // Create the LevelContent object
            LevelContent levelSpec = new LevelContent();
            if (String.IsNullOrEmpty(levelName))
            {
                levelName = fileNameOnly;
            }
            levelSpec.Name = levelName;

            levelSpec.MapSize.X = height;
            levelSpec.MapSize.Y = width;

            // Set the background
            if (!String.IsNullOrEmpty(bgimage))
            {
                levelSpec.Background = new ExternalReference<Texture2DContent>(bgimage);
            }

            // Set the music
            if (!String.IsNullOrEmpty(levelSong))
            {
                levelSpec.LevelSong = new ExternalReference<SongContent>(levelSong);
            }

            // Create the list of all tile types
            Dictionary<string, string> tileToTextureDict = new Dictionary<string, string>();
            for (int a = 0; a < tileTypes.Length; a++)
            {
                // Then this is a child of someone and we want to add its info
                tileToTextureDict.Add(tileTypes[a], tiles[a]);
            }
            levelSpec.TileTypes = new TileContent[tileToTextureDict.Keys.Count];

            // Populate our tile content using the Dict of all tiles
            int index = 0;
            foreach (string tileName in tileToTextureDict.Keys)
            {
                TileContent tileContent = new TileContent();
                tileContent.Name = tileName;
                tileContent.Texture = new ExternalReference<Texture2DContent>(tileToTextureDict[tileName]);
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
                    tileMapContent.Position.X = i;
                    tileMapContent.Position.Y = j;
                    levelSpec.TileArray[index++] = tileMapContent;
                }
            }

            // Write the level specification
            XmlWriterSettings settingsWriter = new XmlWriterSettings();
            settingsWriter.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(fileName, settingsWriter))
            {
                IntermediateSerializer.Serialize<LevelContent>(writer, levelSpec, null);
            }


            // Check to see if we have already created an xnb file of a level with this same name.
            // It would be in the folder XNB_Files inside the folder we chose to save this level in.
            // We need to delete it so it will be rebuilt in game with the edits.
            string directoryPath = Path.GetDirectoryName(fileName) + "/XNB_Files";

            // If the XNB directory exists, check if a file of this level name is there.
            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath);
                foreach (string file in files)
                {
                    string nameOnly = Path.GetFileNameWithoutExtension(file);
                    if (nameOnly == fileNameOnly)
                    {
                        File.Delete(file);
                    }
                }
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
            NewLevel level = new NewLevel();
            levelName = levelSpec.Name;
            level.Name = levelName;
            this.Text = levelName + " - DIY 2D Platformer";
            height = (int)levelSpec.MapSize.X;
            width = (int)levelSpec.MapSize.Y;

            foreach(TileContent tilespec in levelSpec.TileTypes)
            {
                if (tilespec.Name == "Player")
                {
                    tiles[0] = tilespec.Texture.Filename;
                }
                if (tilespec.Name == "WalkingEnemy")
                {
                    tiles[1] = tilespec.Texture.Filename;
                }
                if (tilespec.Name == "Ground")
                {
                    tiles[2] = tilespec.Texture.Filename;
                }
                if (tilespec.Name == "Platform")
                {
                    tiles[3] = tilespec.Texture.Filename;
                }
                if (tilespec.Name == "LevelEnd")
                {
                    tiles[4] = tilespec.Texture.Filename;
                }
            }

            imageList1.Images.Clear();
            Image thing = new Bitmap(contentPath + "clear.png");
            imageList1.Images.Add(thing);
            for (int s = 0; s < tiles.Length; s++)
            {
                Bitmap pict = new Bitmap(tiles[s]);
                imageList1.Images.Add(pict);
                tileImages[s] = new Bitmap(pict, 32, 32);
            }
            treeView1.Update();

            if (levelSpec.LevelSong != null)
            {
                levelSong = levelSpec.LevelSong.Filename;
            }
            int wid = width * 32;
            int heig = height * 32;
            displayW = width;
            displayH = height;
            if (wid > SystemInformation.PrimaryMonitorSize.Width * 4 / 5)
            {
                wid = SystemInformation.PrimaryMonitorSize.Width * 4 / 5;
                displayW = wid / 32;
            }
            if (heig > SystemInformation.PrimaryMonitorSize.Height * 4 / 5)
            {
                heig = SystemInformation.PrimaryMonitorSize.Height * 4 / 5;
                displayH = heig / 32;
            }
            FormSet(wid, heig);

            if (width > displayW)
            {
                uxRight.Enabled = true;
            }
            if (height > displayH)
            {
                uxDown.Enabled = true;
            }

            // Reset the background image so we can draw over it.
            pictureBox1.Image = new Bitmap(displayW * 32, displayH * 32);

            // Get the rest of the level information
            bgimage = "";
            if (levelSpec.Background != null)
            {
                // if we even have a background texture, store it here
                bgimage = levelSpec.Background.Filename;
            } 
            if (bgimage != "")
            {
                backimage = new Bitmap(new Bitmap(bgimage), width * 32, height * 32);
                Bitmap area = new Bitmap(displayW * 32, displayH * 32);
                Graphics temp = Graphics.FromImage(area);
                System.Drawing.Rectangle first = new System.Drawing.Rectangle(0, 0, displayW * 32, displayH * 32);
                System.Drawing.Rectangle second = new System.Drawing.Rectangle(currentX * 32, currentY * 32, displayW * 32, displayH * 32);
                temp.DrawImage(backimage, second, first, GraphicsUnit.Pixel);
                pictureBox1.BackgroundImage = area;
            }

            // Create the list of all tile types
            Dictionary<string, string> tileToTextureDict = new Dictionary<string, string>();
            foreach (TileContent tileSpec in levelSpec.TileTypes)
            {
                tileToTextureDict.Add(tileSpec.Name, tileSpec.Texture.Filename);
            }
            tileToTextureDict.Add("Blank Tile", "");

            // Load in the tile information and draw them.
            foreach (TileMapContent tileMapSpec in levelSpec.TileArray)
            {
                Tile tile = new Tile();
                tile.Name = tileMapSpec.Name;
                tile.TileType = tileMapSpec.Name;
                tile.ImageFile = tileToTextureDict[tile.Name];
               
                
                if (tile.TileType == "Player")
                {
                    tile.ImageIndex = 0;
                }
                else if (tile.TileType == "LevelEnd")
                {
                    tile.ImageIndex = 4;
                }
                else if (tile.TileType == "Ground")
                {
                    tile.ImageIndex = 2;
                }
                else if (tile.TileType == "Platform")
                {
                    tile.ImageIndex = 3;
                }
                else if (tile.TileType == "WalkingEnemy")
                {
                    tile.ImageIndex = 1;
                }

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
                    Bitmap tileToDraw = tileImages[tile.ImageIndex];
                    System.Drawing.Rectangle area = new System.Drawing.Rectangle(posY * 32, posX * 32, 32, 32);
                    levelPic.DrawImage(tileToDraw, area);
                    pictureBox1.Image = pic;
                }
            }

            Cursor = Cursors.Arrow;
        }

        private void setContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetContent form = new SetContent();
            form.SetFields(tiles, bgimage, levelSong);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                imageList1.Images.Clear();
                tiles = form.Tiles;
                Image thing = new Bitmap(contentPath+"clear.png");
                imageList1.Images.Add(thing);
                for (int s = 0; s < tiles.Length; s++)
                {
                    Bitmap pict = new Bitmap(tiles[s]);
                    imageList1.Images.Add(pict);
                    tileImages[s] = new Bitmap(pict, 32, 32);
                }
                bgimage = form.BG;
                if (bgimage != "")
                {
                    backimage = new Bitmap(new Bitmap(bgimage), width*32, height*32);
                    Bitmap area = new Bitmap(displayW*32, displayH*32);
                    Graphics temp = Graphics.FromImage(area);
                    System.Drawing.Rectangle first = new System.Drawing.Rectangle(0, 0, displayW*32, displayH*32);
                    System.Drawing.Rectangle second = new System.Drawing.Rectangle(currentX * 32, currentY * 32, displayW * 32, displayH * 32);
                    temp.DrawImage(backimage, second, first, GraphicsUnit.Pixel);
                    pictureBox1.BackgroundImage = area;
                }
                levelSong = form.Music;
                UpdateBoard();
                Repaint();
                treeView1.Update();
            }
        }

        private void uxForm1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void uxLeft_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void uxForm1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                uxLeft_Click(sender, e);
            }
            if (e.KeyCode == Keys.Right)
            {
                uxRight_Click(sender, e);
            }
            if (e.KeyCode == Keys.Up)
            {
                uxUp_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                uxDown_Click(sender, e);
            }
        }

        private void uxLeft_Click(object sender, EventArgs e)
        {
            if (currentX > 0)
            {
                currentX--;
                uxRight.Enabled = true;
                Repaint();
            }
            else
            {
                uxLeft.Enabled = false;
            }
        }

        private void uxUp_Click(object sender, EventArgs e)
        {
            if (currentY > 0)
            {
                currentY--;
                uxDown.Enabled = true;
                Repaint();
            }
            else
            {
                uxUp.Enabled = false;
            }
        }

        private void uxDown_Click(object sender, EventArgs e)
        {
            if (currentY + displayH < height)
            {
                currentY++;
                uxUp.Enabled = true;
                Repaint();
            }
            else
            {
                uxDown.Enabled = false;
            }
        }

        private void uxRight_Click(object sender, EventArgs e)
        {
            if (currentX + displayW < width)
            {
                currentX++;
                uxLeft.Enabled = true;
                Repaint();
            }
            else
            {
                uxRight.Enabled = false;
            }
        }

        private void UpdateBoard()
        {
            for (int a = 0; a < displayH; a++)
            {
                for (int b = 0; b < displayW; b++)
                {
                    int i = a + currentY;
                    int j = b + currentX;
                    if (board[i, j].TileType == "Player")
                    {
                        board[i, j].ImageFile = tiles[0];
                        board[i, j].ImageIndex = 0;
                    }
                    else if (board[i, j].TileType == "LevelEnd")
                    {
                        board[i, j].ImageFile = tiles[4];
                        board[i, j].ImageIndex = 4;
                    }
                    else if (board[i, j].TileType == "Ground")
                    {
                        board[i, j].ImageFile = tiles[2];
                        board[i, j].ImageIndex = 2;
                    }
                    else if (board[i, j].TileType == "Platform")
                    {
                        board[i, j].ImageFile = tiles[3];
                        board[i, j].ImageIndex = 3;
                    }
                    else if (board[i, j].TileType == "WalkingEnemy")
                    {
                        board[i, j].ImageFile = tiles[1];
                        board[i, j].ImageIndex = 1;
                    }
                }
            }
        }

        private void Repaint()
        {
            Image pic = new Bitmap(displayW * 32, displayH * 32);
            Graphics level = Graphics.FromImage(pic);
            for (int a = 0; a < displayH; a++)
            {
                for (int b = 0; b < displayW; b++)
                {
                    int i = a + currentY;
                    int j = b + currentX;
                    if (board[i, j].ImageIndex >= 0)
                    {
                        Bitmap tile = tileImages[board[i, j].ImageIndex];
                        System.Drawing.Rectangle area = new System.Drawing.Rectangle(b * 32, a * 32, 32, 32);
                        level.DrawImage(tile, area);
                    }
                }
            }
            pictureBox1.Image = pic;
            if (currentX == 0)
            {
                uxLeft.Enabled = false;
            }
            if (currentX + displayW == width)
            {
                uxRight.Enabled = false;
            }
            if (currentY == 0)
            {
                uxUp.Enabled = false;
            }
            if (currentY + displayH == height)
            {
                uxDown.Enabled = false;
            }
            if (bgimage != "")
            {
                backimage = new Bitmap(new Bitmap(bgimage), width * 32, height * 32);
                Bitmap area = new Bitmap(displayW * 32, displayH * 32);
                Graphics temp = Graphics.FromImage(area);
                System.Drawing.Rectangle first = new System.Drawing.Rectangle(0, 0, displayW * 32, displayH * 32);
                System.Drawing.Rectangle second = new System.Drawing.Rectangle(currentX*32, currentY*32, displayW * 32, displayH * 32);
                temp.DrawImage(backimage, first, second, GraphicsUnit.Pixel);
                pictureBox1.BackgroundImage = area;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Capture == true)
            {
                PlaceTile(e);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Capture = false;
        }

        private void DeleteBackground_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = new Bitmap(displayW * 32, displayH * 32);
            bgimage = "";
            backimage = new Bitmap(width * 32, height * 32);
        }

        private void DeleteMusic_Click(object sender, EventArgs e)
        {
            levelSong = "";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
