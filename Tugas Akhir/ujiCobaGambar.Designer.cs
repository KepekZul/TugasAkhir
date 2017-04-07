namespace Tugas_Akhir
{
    partial class ujiCobaGambar
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.MinBox = new System.Windows.Forms.TextBox();
            this.MaxBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pilihGambarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pPMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pGMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lainlainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pakaiHisteqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaborFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenShot = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(319, 300);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(223, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cari";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.deteksiWajah);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(304, 349);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Crop";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.cropImage);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(571, 290);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "pilih duplikat";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.pilihFolderDuplikat);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(571, 42);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(232, 110);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(571, 162);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(232, 93);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(571, 261);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 9;
            this.button7.Text = "delete";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.hapusFile);
            // 
            // MinBox
            // 
            this.MinBox.Location = new System.Drawing.Point(10, 349);
            this.MinBox.Name = "MinBox";
            this.MinBox.Size = new System.Drawing.Size(100, 20);
            this.MinBox.TabIndex = 11;
            // 
            // MaxBox
            // 
            this.MaxBox.Location = new System.Drawing.Point(117, 349);
            this.MaxBox.Name = "MaxBox";
            this.MaxBox.Size = new System.Drawing.Size(100, 20);
            this.MaxBox.TabIndex = 12;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pilihGambarToolStripMenuItem,
            this.lainlainToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(815, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pilihGambarToolStripMenuItem
            // 
            this.pilihGambarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bitmapToolStripMenuItem,
            this.pPMToolStripMenuItem,
            this.pGMToolStripMenuItem});
            this.pilihGambarToolStripMenuItem.Name = "pilihGambarToolStripMenuItem";
            this.pilihGambarToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.pilihGambarToolStripMenuItem.Text = "Pilih Gambar";
            // 
            // bitmapToolStripMenuItem
            // 
            this.bitmapToolStripMenuItem.Name = "bitmapToolStripMenuItem";
            this.bitmapToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.bitmapToolStripMenuItem.Text = "Bitmap";
            this.bitmapToolStripMenuItem.Click += new System.EventHandler(this.pilihGambar);
            // 
            // pPMToolStripMenuItem
            // 
            this.pPMToolStripMenuItem.Name = "pPMToolStripMenuItem";
            this.pPMToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.pPMToolStripMenuItem.Text = "PPM";
            this.pPMToolStripMenuItem.Click += new System.EventHandler(this.pilihGambarPPM);
            // 
            // pGMToolStripMenuItem
            // 
            this.pGMToolStripMenuItem.Name = "pGMToolStripMenuItem";
            this.pGMToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.pGMToolStripMenuItem.Text = "PGM";
            this.pGMToolStripMenuItem.Click += new System.EventHandler(this.pilihGambarPGM);
            // 
            // lainlainToolStripMenuItem
            // 
            this.lainlainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pakaiHisteqToolStripMenuItem,
            this.gaborFilterToolStripMenuItem});
            this.lainlainToolStripMenuItem.Name = "lainlainToolStripMenuItem";
            this.lainlainToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.lainlainToolStripMenuItem.Text = "Lain-lain";
            // 
            // pakaiHisteqToolStripMenuItem
            // 
            this.pakaiHisteqToolStripMenuItem.Name = "pakaiHisteqToolStripMenuItem";
            this.pakaiHisteqToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.pakaiHisteqToolStripMenuItem.Text = "Histogram Equalization";
            this.pakaiHisteqToolStripMenuItem.Click += new System.EventHandler(this.applyHisteq);
            // 
            // gaborFilterToolStripMenuItem
            // 
            this.gaborFilterToolStripMenuItem.Name = "gaborFilterToolStripMenuItem";
            this.gaborFilterToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.gaborFilterToolStripMenuItem.Text = "Gabor Filter";
            this.gaborFilterToolStripMenuItem.Click += new System.EventHandler(this.gaborFilter);
            // 
            // screenShot
            // 
            this.screenShot.Location = new System.Drawing.Point(571, 319);
            this.screenShot.Name = "screenShot";
            this.screenShot.Size = new System.Drawing.Size(75, 23);
            this.screenShot.TabIndex = 14;
            this.screenShot.Text = "Skrinsut";
            this.screenShot.UseVisualStyleBackColor = true;
            this.screenShot.Click += new System.EventHandler(this.screenShot_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(572, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "DRLDP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(337, 42);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(228, 213);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // ujiCobaGambar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 380);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.screenShot);
            this.Controls.Add(this.MaxBox);
            this.Controls.Add(this.MinBox);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ujiCobaGambar";
            this.Text = "ujikoding";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox MinBox;
        private System.Windows.Forms.TextBox MaxBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pilihGambarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pPMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pGMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lainlainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pakaiHisteqToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaborFilterToolStripMenuItem;
        private System.Windows.Forms.Button screenShot;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

