namespace _2DLevelEditor
{
    partial class NewLevel
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
            this.uxHeight = new System.Windows.Forms.NumericUpDown();
            this.uxWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.uxBackground = new System.Windows.Forms.TextBox();
            this.uxBrowseButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.uxLevelName = new System.Windows.Forms.TextBox();
            this.uxOKButton = new System.Windows.Forms.Button();
            this.uxCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.uxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Height In Tiles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width In Tiles";
            // 
            // uxHeight
            // 
            this.uxHeight.Location = new System.Drawing.Point(109, 28);
            this.uxHeight.Name = "uxHeight";
            this.uxHeight.Size = new System.Drawing.Size(90, 20);
            this.uxHeight.TabIndex = 4;
            this.uxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uxHeight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // uxWidth
            // 
            this.uxWidth.Location = new System.Drawing.Point(15, 28);
            this.uxWidth.Name = "uxWidth";
            this.uxWidth.Size = new System.Drawing.Size(85, 20);
            this.uxWidth.TabIndex = 5;
            this.uxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uxWidth.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.uxWidth.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Background Image";
            // 
            // uxBackground
            // 
            this.uxBackground.Location = new System.Drawing.Point(15, 77);
            this.uxBackground.Name = "uxBackground";
            this.uxBackground.Size = new System.Drawing.Size(146, 20);
            this.uxBackground.TabIndex = 7;
            // 
            // uxBrowseButton
            // 
            this.uxBrowseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxBrowseButton.Location = new System.Drawing.Point(167, 77);
            this.uxBrowseButton.Name = "uxBrowseButton";
            this.uxBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.uxBrowseButton.TabIndex = 8;
            this.uxBrowseButton.Text = "Browse...";
            this.uxBrowseButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Level Name";
            // 
            // uxLevelName
            // 
            this.uxLevelName.Location = new System.Drawing.Point(99, 106);
            this.uxLevelName.Name = "uxLevelName";
            this.uxLevelName.Size = new System.Drawing.Size(143, 20);
            this.uxLevelName.TabIndex = 10;
            // 
            // uxOKButton
            // 
            this.uxOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxOKButton.Location = new System.Drawing.Point(39, 132);
            this.uxOKButton.Name = "uxOKButton";
            this.uxOKButton.Size = new System.Drawing.Size(75, 23);
            this.uxOKButton.TabIndex = 11;
            this.uxOKButton.Text = "OK";
            this.uxOKButton.UseVisualStyleBackColor = true;
            this.uxOKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // uxCancelButton
            // 
            this.uxCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxCancelButton.Location = new System.Drawing.Point(141, 132);
            this.uxCancelButton.Name = "uxCancelButton";
            this.uxCancelButton.Size = new System.Drawing.Size(75, 23);
            this.uxCancelButton.TabIndex = 12;
            this.uxCancelButton.Text = "Cancel";
            this.uxCancelButton.UseVisualStyleBackColor = true;
            // 
            // NewLevel
            // 
            this.AcceptButton = this.uxOKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(255, 160);
            this.ControlBox = false;
            this.Controls.Add(this.uxCancelButton);
            this.Controls.Add(this.uxOKButton);
            this.Controls.Add(this.uxLevelName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uxBrowseButton);
            this.Controls.Add(this.uxBackground);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uxWidth);
            this.Controls.Add(this.uxHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLevel";
            this.ShowInTaskbar = false;
            this.Text = "New Level";
            ((System.ComponentModel.ISupportInitialize)(this.uxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown uxHeight;
        private System.Windows.Forms.NumericUpDown uxWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox uxBackground;
        private System.Windows.Forms.Button uxBrowseButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox uxLevelName;
        private System.Windows.Forms.Button uxOKButton;
        private System.Windows.Forms.Button uxCancelButton;

    }
}