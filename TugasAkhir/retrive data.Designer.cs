namespace Tugas_Akhir
{
    partial class retrive_data
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.templateQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeForEachDatasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.learnPCAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.learnSelectedDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serializeToTestingObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformPCAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(489, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(507, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 65);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(683, 342);
            this.dataGridView1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateQueryToolStripMenuItem,
            this.oerationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(708, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // templateQueryToolStripMenuItem
            // 
            this.templateQueryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeForEachDatasetToolStripMenuItem});
            this.templateQueryToolStripMenuItem.Name = "templateQueryToolStripMenuItem";
            this.templateQueryToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.templateQueryToolStripMenuItem.Text = "Template Query";
            // 
            // sizeForEachDatasetToolStripMenuItem
            // 
            this.sizeForEachDatasetToolStripMenuItem.Name = "sizeForEachDatasetToolStripMenuItem";
            this.sizeForEachDatasetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sizeForEachDatasetToolStripMenuItem.Text = "size for each dataset";
            this.sizeForEachDatasetToolStripMenuItem.Click += new System.EventHandler(this.sizeForEachDatasetToolStripMenuItem_Click);
            // 
            // oerationToolStripMenuItem
            // 
            this.oerationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.learnPCAToolStripMenuItem,
            this.learnSelectedDataToolStripMenuItem,
            this.serializeToTestingObjectToolStripMenuItem,
            this.transformPCAToolStripMenuItem});
            this.oerationToolStripMenuItem.Name = "oerationToolStripMenuItem";
            this.oerationToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.oerationToolStripMenuItem.Text = "Operation";
            // 
            // learnPCAToolStripMenuItem
            // 
            this.learnPCAToolStripMenuItem.Name = "learnPCAToolStripMenuItem";
            this.learnPCAToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.learnPCAToolStripMenuItem.Text = "Learn PCA";
            this.learnPCAToolStripMenuItem.Click += new System.EventHandler(this.learnPca);
            // 
            // learnSelectedDataToolStripMenuItem
            // 
            this.learnSelectedDataToolStripMenuItem.Name = "learnSelectedDataToolStripMenuItem";
            this.learnSelectedDataToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.learnSelectedDataToolStripMenuItem.Text = "Serialize to training object";
            this.learnSelectedDataToolStripMenuItem.Click += new System.EventHandler(this.learnSelectedDataToolStripMenuItem_Click);
            // 
            // serializeToTestingObjectToolStripMenuItem
            // 
            this.serializeToTestingObjectToolStripMenuItem.Name = "serializeToTestingObjectToolStripMenuItem";
            this.serializeToTestingObjectToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.serializeToTestingObjectToolStripMenuItem.Text = "Serialize to testing object";
            this.serializeToTestingObjectToolStripMenuItem.Click += new System.EventHandler(this.serializeToTestingObjectToolStripMenuItem_Click);
            // 
            // transformPCAToolStripMenuItem
            // 
            this.transformPCAToolStripMenuItem.Name = "transformPCAToolStripMenuItem";
            this.transformPCAToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.transformPCAToolStripMenuItem.Text = "Transform PCA";
            // 
            // retrive_data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 419);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "retrive_data";
            this.Text = "retrive_data";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem templateQueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeForEachDatasetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oerationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem learnPCAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem learnSelectedDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serializeToTestingObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformPCAToolStripMenuItem;
    }
}