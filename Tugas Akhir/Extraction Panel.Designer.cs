namespace Tugas_Akhir
{
    partial class Extraction_Panel
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.targetDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MinSizeBox = new System.Windows.Forms.TextBox();
            this.MaxSizeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FilenameFilterBox = new System.Windows.Forms.TextBox();
            this.resizeButton = new System.Windows.Forms.Button();
            this.ExtractImage = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.reduceDimension = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(127, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(489, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.DoubleClick += new System.EventHandler(this.sourceFolder);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(126, 80);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(490, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.DoubleClick += new System.EventHandler(this.destinationFolder);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source Directory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Target Directory";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Crop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cropSelectedFiles);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(819, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceDirectoryToolStripMenuItem,
            this.targetDirectoryToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sourceDirectoryToolStripMenuItem
            // 
            this.sourceDirectoryToolStripMenuItem.Name = "sourceDirectoryToolStripMenuItem";
            this.sourceDirectoryToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.sourceDirectoryToolStripMenuItem.Text = "Source Directory";
            this.sourceDirectoryToolStripMenuItem.Click += new System.EventHandler(this.sourceFolder);
            // 
            // targetDirectoryToolStripMenuItem
            // 
            this.targetDirectoryToolStripMenuItem.Name = "targetDirectoryToolStripMenuItem";
            this.targetDirectoryToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.targetDirectoryToolStripMenuItem.Text = "Target Directory";
            this.targetDirectoryToolStripMenuItem.Click += new System.EventHandler(this.destinationFolder);
            // 
            // MinSizeBox
            // 
            this.MinSizeBox.Location = new System.Drawing.Point(153, 106);
            this.MinSizeBox.Name = "MinSizeBox";
            this.MinSizeBox.Size = new System.Drawing.Size(100, 20);
            this.MinSizeBox.TabIndex = 9;
            // 
            // MaxSizeBox
            // 
            this.MaxSizeBox.Location = new System.Drawing.Point(292, 106);
            this.MaxSizeBox.Name = "MaxSizeBox";
            this.MaxSizeBox.Size = new System.Drawing.Size(100, 20);
            this.MaxSizeBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Face Size";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(127, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(398, 108);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(133, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Histogram Equalization";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Min";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Max";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Data Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(260, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Filename Filter";
            // 
            // FilenameFilterBox
            // 
            this.FilenameFilterBox.Location = new System.Drawing.Point(344, 27);
            this.FilenameFilterBox.Name = "FilenameFilterBox";
            this.FilenameFilterBox.Size = new System.Drawing.Size(272, 20);
            this.FilenameFilterBox.TabIndex = 18;
            // 
            // resizeButton
            // 
            this.resizeButton.Location = new System.Drawing.Point(208, 144);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(75, 23);
            this.resizeButton.TabIndex = 19;
            this.resizeButton.Text = "Resize";
            this.resizeButton.UseVisualStyleBackColor = true;
            this.resizeButton.Click += new System.EventHandler(this.resize);
            // 
            // ExtractImage
            // 
            this.ExtractImage.Location = new System.Drawing.Point(623, 54);
            this.ExtractImage.Name = "ExtractImage";
            this.ExtractImage.Size = new System.Drawing.Size(75, 20);
            this.ExtractImage.TabIndex = 20;
            this.ExtractImage.Text = "Extract";
            this.ExtractImage.UseVisualStyleBackColor = true;
            this.ExtractImage.Click += new System.EventHandler(this.ExtractImage_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Black;
            this.progressBar1.Location = new System.Drawing.Point(292, 144);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(521, 23);
            this.progressBar1.TabIndex = 21;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.progressBarWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.updateBarWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.finishBarWork);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(623, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 19);
            this.button2.TabIndex = 22;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.saveDatatoDatabase);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(45, 144);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Resize X";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // reduceDimension
            // 
            this.reduceDimension.AutoSize = true;
            this.reduceDimension.Location = new System.Drawing.Point(704, 57);
            this.reduceDimension.Name = "reduceDimension";
            this.reduceDimension.Size = new System.Drawing.Size(109, 17);
            this.reduceDimension.TabIndex = 24;
            this.reduceDimension.Text = "reduce dimension";
            this.reduceDimension.UseVisualStyleBackColor = true;
            // 
            // Control_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 186);
            this.Controls.Add(this.reduceDimension);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ExtractImage);
            this.Controls.Add(this.resizeButton);
            this.Controls.Add(this.FilenameFilterBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MaxSizeBox);
            this.Controls.Add(this.MinSizeBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Control_Panel";
            this.Text = "Control Panel";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem targetDirectoryToolStripMenuItem;
        private System.Windows.Forms.TextBox MinSizeBox;
        private System.Windows.Forms.TextBox MaxSizeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox FilenameFilterBox;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.Button ExtractImage;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox reduceDimension;
    }
}