namespace Mono_PlatformerGame
{
    partial class uxForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Walking Enemy", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Enemies", -2, -2, new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Ground", 3, 3);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Platform", 4, 4);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Level End", 5, 5);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Platforms", -2, -2, new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Player", 1, 1);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Player", -2, -2, new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Erase Tile");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Delete", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uxForm1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMusic = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uxLeft = new System.Windows.Forms.Button();
            this.uxDirectionsImages = new System.Windows.Forms.ImageList(this.components);
            this.uxRight = new System.Windows.Forms.Button();
            this.uxUp = new System.Windows.Forms.Button();
            this.uxDown = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1131, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLevelToolStripMenuItem,
            this.openLevelToolStripMenuItem,
            this.saveLevelToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newLevelToolStripMenuItem
            // 
            this.newLevelToolStripMenuItem.Name = "newLevelToolStripMenuItem";
            this.newLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newLevelToolStripMenuItem.Text = "New Level";
            this.newLevelToolStripMenuItem.Click += new System.EventHandler(this.newLevelToolStripMenuItem_Click);
            // 
            // openLevelToolStripMenuItem
            // 
            this.openLevelToolStripMenuItem.Name = "openLevelToolStripMenuItem";
            this.openLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openLevelToolStripMenuItem.Text = "Open Level";
            this.openLevelToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuClicked);
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveLevelToolStripMenuItem.Text = "Save Level";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.SaveMenuClicked);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setContentToolStripMenuItem,
            this.DeleteBackground,
            this.DeleteMusic});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // setContentToolStripMenuItem
            // 
            this.setContentToolStripMenuItem.Name = "setContentToolStripMenuItem";
            this.setContentToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.setContentToolStripMenuItem.Text = "Set Content";
            this.setContentToolStripMenuItem.Click += new System.EventHandler(this.setContentToolStripMenuItem_Click);
            // 
            // DeleteBackground
            // 
            this.DeleteBackground.Name = "DeleteBackground";
            this.DeleteBackground.Size = new System.Drawing.Size(174, 22);
            this.DeleteBackground.Text = "Delete Background";
            this.DeleteBackground.Click += new System.EventHandler(this.DeleteBackground_Click);
            // 
            // DeleteMusic
            // 
            this.DeleteMusic.Name = "DeleteMusic";
            this.DeleteMusic.Size = new System.Drawing.Size(174, 22);
            this.DeleteMusic.Text = "Delete Music";
            this.DeleteMusic.Click += new System.EventHandler(this.DeleteMusic_Click);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Control;
            this.treeView1.FullRowSelect = true;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(963, 24);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "WalkingEnemy";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "Walking Enemy";
            treeNode2.ImageIndex = -2;
            treeNode2.Name = "Enemies";
            treeNode2.SelectedImageIndex = -2;
            treeNode2.Text = "Enemies";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "Ground";
            treeNode3.SelectedImageIndex = 3;
            treeNode3.Text = "Ground";
            treeNode4.ImageIndex = 4;
            treeNode4.Name = "Platform";
            treeNode4.SelectedImageIndex = 4;
            treeNode4.Text = "Platform";
            treeNode5.ImageIndex = 5;
            treeNode5.Name = "LevelEnd";
            treeNode5.SelectedImageIndex = 5;
            treeNode5.Text = "Level End";
            treeNode6.ImageIndex = -2;
            treeNode6.Name = "Platforms";
            treeNode6.SelectedImageIndex = -2;
            treeNode6.Text = "Platforms";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "Player";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Text = "Player";
            treeNode8.ImageIndex = -2;
            treeNode8.Name = "Player";
            treeNode8.SelectedImageIndex = -2;
            treeNode8.Text = "Player";
            treeNode9.Name = "Delete";
            treeNode9.Text = "Erase Tile";
            treeNode10.Name = "Delete";
            treeNode10.Text = "Delete";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6,
            treeNode8,
            treeNode10});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(168, 574);
            this.treeView1.TabIndex = 3;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "clear.png");
            this.imageList1.Images.SetKeyName(1, "purple.png");
            this.imageList1.Images.SetKeyName(2, "red.png");
            this.imageList1.Images.SetKeyName(3, "black.png");
            this.imageList1.Images.SetKeyName(4, "blue.png");
            this.imageList1.Images.SetKeyName(5, "green.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 640);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(960, 637);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // uxLeft
            // 
            this.uxLeft.Enabled = false;
            this.uxLeft.ImageIndex = 2;
            this.uxLeft.ImageList = this.uxDirectionsImages;
            this.uxLeft.Location = new System.Drawing.Point(963, 600);
            this.uxLeft.MaximumSize = new System.Drawing.Size(40, 60);
            this.uxLeft.MinimumSize = new System.Drawing.Size(40, 60);
            this.uxLeft.Name = "uxLeft";
            this.uxLeft.Size = new System.Drawing.Size(40, 60);
            this.uxLeft.TabIndex = 5;
            this.uxLeft.UseVisualStyleBackColor = true;
            this.uxLeft.Click += new System.EventHandler(this.uxLeft_Click);
            this.uxLeft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uxLeft_KeyDown);
            // 
            // uxDirectionsImages
            // 
            this.uxDirectionsImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("uxDirectionsImages.ImageStream")));
            this.uxDirectionsImages.TransparentColor = System.Drawing.Color.Transparent;
            this.uxDirectionsImages.Images.SetKeyName(0, "up.png");
            this.uxDirectionsImages.Images.SetKeyName(1, "down.png");
            this.uxDirectionsImages.Images.SetKeyName(2, "left.png");
            this.uxDirectionsImages.Images.SetKeyName(3, "right.png");
            // 
            // uxRight
            // 
            this.uxRight.Enabled = false;
            this.uxRight.ImageIndex = 3;
            this.uxRight.ImageList = this.uxDirectionsImages;
            this.uxRight.Location = new System.Drawing.Point(1091, 600);
            this.uxRight.MaximumSize = new System.Drawing.Size(40, 60);
            this.uxRight.MinimumSize = new System.Drawing.Size(40, 60);
            this.uxRight.Name = "uxRight";
            this.uxRight.Size = new System.Drawing.Size(40, 60);
            this.uxRight.TabIndex = 6;
            this.uxRight.UseVisualStyleBackColor = true;
            this.uxRight.Click += new System.EventHandler(this.uxRight_Click);
            // 
            // uxUp
            // 
            this.uxUp.Enabled = false;
            this.uxUp.ImageIndex = 0;
            this.uxUp.ImageList = this.uxDirectionsImages;
            this.uxUp.Location = new System.Drawing.Point(1002, 600);
            this.uxUp.MaximumSize = new System.Drawing.Size(90, 30);
            this.uxUp.MinimumSize = new System.Drawing.Size(90, 30);
            this.uxUp.Name = "uxUp";
            this.uxUp.Size = new System.Drawing.Size(90, 30);
            this.uxUp.TabIndex = 7;
            this.uxUp.UseVisualStyleBackColor = true;
            this.uxUp.Click += new System.EventHandler(this.uxUp_Click);
            // 
            // uxDown
            // 
            this.uxDown.Enabled = false;
            this.uxDown.ImageIndex = 1;
            this.uxDown.ImageList = this.uxDirectionsImages;
            this.uxDown.Location = new System.Drawing.Point(1002, 630);
            this.uxDown.MaximumSize = new System.Drawing.Size(90, 30);
            this.uxDown.MinimumSize = new System.Drawing.Size(90, 30);
            this.uxDown.Name = "uxDown";
            this.uxDown.Size = new System.Drawing.Size(90, 30);
            this.uxDown.TabIndex = 8;
            this.uxDown.UseVisualStyleBackColor = true;
            this.uxDown.Click += new System.EventHandler(this.uxDown_Click);
            // 
            // uxForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1131, 662);
            this.Controls.Add(this.uxDown);
            this.Controls.Add(this.uxUp);
            this.Controls.Add(this.uxRight);
            this.Controls.Add(this.uxLeft);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "uxForm1";
            this.Text = "DIY 2D Platformer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uxForm1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.uxForm1_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem setContentToolStripMenuItem;
        private System.Windows.Forms.Button uxLeft;
        private System.Windows.Forms.ImageList uxDirectionsImages;
        private System.Windows.Forms.Button uxRight;
        private System.Windows.Forms.Button uxUp;
        private System.Windows.Forms.Button uxDown;
        private System.Windows.Forms.ToolStripMenuItem DeleteBackground;
        private System.Windows.Forms.ToolStripMenuItem DeleteMusic;
    }
}

