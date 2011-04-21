namespace _2DLevelEditor
{
    partial class SetImages
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PlayerImage = new System.Windows.Forms.TextBox();
            this.WalkingEnemyImage = new System.Windows.Forms.TextBox();
            this.LevelEndImage = new System.Windows.Forms.TextBox();
            this.GroundImage = new System.Windows.Forms.TextBox();
            this.PlatformImage = new System.Windows.Forms.TextBox();
            this.PlayerBrowse = new System.Windows.Forms.Button();
            this.WalkingBrowse = new System.Windows.Forms.Button();
            this.LevelEndBrowse = new System.Windows.Forms.Button();
            this.GroundBrowse = new System.Windows.Forms.Button();
            this.PlatformBrowse = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.uxOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image for Walking Enemy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Image for Player";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Image for Level End";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Image for Ground Block";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Image for Platform";
            // 
            // PlayerImage
            // 
            this.PlayerImage.Location = new System.Drawing.Point(146, 6);
            this.PlayerImage.Name = "PlayerImage";
            this.PlayerImage.Size = new System.Drawing.Size(160, 20);
            this.PlayerImage.TabIndex = 5;
            // 
            // WalkingEnemyImage
            // 
            this.WalkingEnemyImage.Location = new System.Drawing.Point(146, 32);
            this.WalkingEnemyImage.Name = "WalkingEnemyImage";
            this.WalkingEnemyImage.Size = new System.Drawing.Size(160, 20);
            this.WalkingEnemyImage.TabIndex = 6;
            // 
            // LevelEndImage
            // 
            this.LevelEndImage.Location = new System.Drawing.Point(146, 58);
            this.LevelEndImage.Name = "LevelEndImage";
            this.LevelEndImage.Size = new System.Drawing.Size(160, 20);
            this.LevelEndImage.TabIndex = 7;
            // 
            // GroundImage
            // 
            this.GroundImage.Location = new System.Drawing.Point(146, 84);
            this.GroundImage.Name = "GroundImage";
            this.GroundImage.Size = new System.Drawing.Size(160, 20);
            this.GroundImage.TabIndex = 8;
            // 
            // PlatformImage
            // 
            this.PlatformImage.Location = new System.Drawing.Point(146, 110);
            this.PlatformImage.Name = "PlatformImage";
            this.PlatformImage.Size = new System.Drawing.Size(160, 20);
            this.PlatformImage.TabIndex = 9;
            // 
            // PlayerBrowse
            // 
            this.PlayerBrowse.Location = new System.Drawing.Point(312, 4);
            this.PlayerBrowse.Name = "PlayerBrowse";
            this.PlayerBrowse.Size = new System.Drawing.Size(75, 23);
            this.PlayerBrowse.TabIndex = 10;
            this.PlayerBrowse.Text = "Browse...";
            this.PlayerBrowse.UseVisualStyleBackColor = true;
            this.PlayerBrowse.Click += new System.EventHandler(this.PlayerBrowse_Click);
            // 
            // WalkingBrowse
            // 
            this.WalkingBrowse.Location = new System.Drawing.Point(312, 29);
            this.WalkingBrowse.Name = "WalkingBrowse";
            this.WalkingBrowse.Size = new System.Drawing.Size(75, 23);
            this.WalkingBrowse.TabIndex = 11;
            this.WalkingBrowse.Text = "Browse...";
            this.WalkingBrowse.UseVisualStyleBackColor = true;
            this.WalkingBrowse.Click += new System.EventHandler(this.WalkingBrowse_Click);
            // 
            // LevelEndBrowse
            // 
            this.LevelEndBrowse.Location = new System.Drawing.Point(312, 56);
            this.LevelEndBrowse.Name = "LevelEndBrowse";
            this.LevelEndBrowse.Size = new System.Drawing.Size(75, 23);
            this.LevelEndBrowse.TabIndex = 12;
            this.LevelEndBrowse.Text = "Browse...";
            this.LevelEndBrowse.UseVisualStyleBackColor = true;
            this.LevelEndBrowse.Click += new System.EventHandler(this.LevelEndBrowse_Click);
            // 
            // GroundBrowse
            // 
            this.GroundBrowse.Location = new System.Drawing.Point(312, 82);
            this.GroundBrowse.Name = "GroundBrowse";
            this.GroundBrowse.Size = new System.Drawing.Size(75, 23);
            this.GroundBrowse.TabIndex = 13;
            this.GroundBrowse.Text = "Browse...";
            this.GroundBrowse.UseVisualStyleBackColor = true;
            this.GroundBrowse.Click += new System.EventHandler(this.GroundBrowse_Click);
            // 
            // PlatformBrowse
            // 
            this.PlatformBrowse.Location = new System.Drawing.Point(312, 108);
            this.PlatformBrowse.Name = "PlatformBrowse";
            this.PlatformBrowse.Size = new System.Drawing.Size(75, 23);
            this.PlatformBrowse.TabIndex = 14;
            this.PlatformBrowse.Text = "Browse...";
            this.PlatformBrowse.UseVisualStyleBackColor = true;
            this.PlatformBrowse.Click += new System.EventHandler(this.PlatformBrowse_Click);
            // 
            // button6
            // 
            this.button6.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button6.Location = new System.Drawing.Point(105, 136);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 15;
            this.button6.Text = "OK";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button7.Location = new System.Drawing.Point(220, 136);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 16;
            this.button7.Text = "Cancel";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // uxOpenFile
            // 
            this.uxOpenFile.FileName = "openFileDialog1";
            // 
            // SetImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 169);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.PlatformBrowse);
            this.Controls.Add(this.GroundBrowse);
            this.Controls.Add(this.LevelEndBrowse);
            this.Controls.Add(this.WalkingBrowse);
            this.Controls.Add(this.PlayerBrowse);
            this.Controls.Add(this.PlatformImage);
            this.Controls.Add(this.GroundImage);
            this.Controls.Add(this.LevelEndImage);
            this.Controls.Add(this.WalkingEnemyImage);
            this.Controls.Add(this.PlayerImage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SetImages";
            this.Text = "SetImages";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PlayerImage;
        private System.Windows.Forms.TextBox WalkingEnemyImage;
        private System.Windows.Forms.TextBox LevelEndImage;
        private System.Windows.Forms.TextBox GroundImage;
        private System.Windows.Forms.TextBox PlatformImage;
        private System.Windows.Forms.Button PlayerBrowse;
        private System.Windows.Forms.Button WalkingBrowse;
        private System.Windows.Forms.Button LevelEndBrowse;
        private System.Windows.Forms.Button GroundBrowse;
        private System.Windows.Forms.Button PlatformBrowse;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.OpenFileDialog uxOpenFile;
    }
}