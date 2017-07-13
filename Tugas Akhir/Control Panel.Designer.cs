namespace Tugas_Akhir
{
    partial class Control_Panel
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
            this.extract_form = new System.Windows.Forms.Button();
            this.testing_form = new System.Windows.Forms.Button();
            this.get_data = new System.Windows.Forms.Button();
            this.testing_panel = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extract_form
            // 
            this.extract_form.Location = new System.Drawing.Point(12, 39);
            this.extract_form.Name = "extract_form";
            this.extract_form.Size = new System.Drawing.Size(100, 23);
            this.extract_form.TabIndex = 0;
            this.extract_form.Text = "Data Extraction";
            this.extract_form.UseVisualStyleBackColor = true;
            this.extract_form.Click += new System.EventHandler(this.extract_form_Click);
            // 
            // testing_form
            // 
            this.testing_form.Location = new System.Drawing.Point(12, 69);
            this.testing_form.Name = "testing_form";
            this.testing_form.Size = new System.Drawing.Size(100, 23);
            this.testing_form.TabIndex = 1;
            this.testing_form.Text = "Testing Panel";
            this.testing_form.UseVisualStyleBackColor = true;
            this.testing_form.Click += new System.EventHandler(this.testing_form_Click);
            // 
            // get_data
            // 
            this.get_data.Location = new System.Drawing.Point(12, 99);
            this.get_data.Name = "get_data";
            this.get_data.Size = new System.Drawing.Size(100, 23);
            this.get_data.TabIndex = 2;
            this.get_data.Text = "Retrieve Data";
            this.get_data.UseVisualStyleBackColor = true;
            this.get_data.Click += new System.EventHandler(this.get_data_Click);
            // 
            // testing_panel
            // 
            this.testing_panel.Location = new System.Drawing.Point(12, 129);
            this.testing_panel.Name = "testing_panel";
            this.testing_panel.Size = new System.Drawing.Size(100, 23);
            this.testing_panel.TabIndex = 3;
            this.testing_panel.Text = "Feature Classifier";
            this.testing_panel.UseVisualStyleBackColor = true;
            this.testing_panel.Click += new System.EventHandler(this.testing_panel_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Demo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Control_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.testing_panel);
            this.Controls.Add(this.get_data);
            this.Controls.Add(this.testing_form);
            this.Controls.Add(this.extract_form);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Control_Panel";
            this.Text = "Control_Panel";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button extract_form;
        private System.Windows.Forms.Button testing_form;
        private System.Windows.Forms.Button get_data;
        private System.Windows.Forms.Button testing_panel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}