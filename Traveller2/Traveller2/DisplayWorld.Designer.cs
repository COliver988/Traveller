namespace Traveller2
{
    partial class DisplayWorld
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnJump = new System.Windows.Forms.Button();
            this.lblSEC = new System.Windows.Forms.Label();
            this.lblWorld = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvData = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.edNotes = new System.Windows.Forms.TextBox();
            this.edNPC = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnJump);
            this.panel1.Controls.Add(this.lblSEC);
            this.panel1.Controls.Add(this.lblWorld);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(497, 136);
            this.panel1.TabIndex = 0;
            // 
            // btnJump
            // 
            this.btnJump.Location = new System.Drawing.Point(410, 9);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(75, 23);
            this.btnJump.TabIndex = 3;
            this.btnJump.Text = "Jump";
            this.btnJump.UseVisualStyleBackColor = true;
            this.btnJump.Click += new System.EventHandler(this.btnJump_Click);
            // 
            // lblSEC
            // 
            this.lblSEC.AutoSize = true;
            this.lblSEC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSEC.Location = new System.Drawing.Point(28, 42);
            this.lblSEC.Name = "lblSEC";
            this.lblSEC.Size = new System.Drawing.Size(46, 17);
            this.lblSEC.TabIndex = 2;
            this.lblSEC.Text = "label1";
            // 
            // lblWorld
            // 
            this.lblWorld.AutoSize = true;
            this.lblWorld.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorld.Location = new System.Drawing.Point(12, 9);
            this.lblWorld.Name = "lblWorld";
            this.lblWorld.Size = new System.Drawing.Size(51, 20);
            this.lblWorld.TabIndex = 1;
            this.lblWorld.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(410, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 136);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(497, 276);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(489, 250);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.edNotes);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(489, 250);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "System Notes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvData
            // 
            this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvData.Location = new System.Drawing.Point(3, 3);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(483, 244);
            this.lvData.TabIndex = 1;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Data";
            this.columnHeader1.Width = 132;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 323;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.edNPC);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(489, 250);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "NPC Notes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // edNotes
            // 
            this.edNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edNotes.Location = new System.Drawing.Point(3, 3);
            this.edNotes.Multiline = true;
            this.edNotes.Name = "edNotes";
            this.edNotes.ReadOnly = true;
            this.edNotes.Size = new System.Drawing.Size(483, 244);
            this.edNotes.TabIndex = 0;
            // 
            // edNPC
            // 
            this.edNPC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edNPC.Location = new System.Drawing.Point(0, 0);
            this.edNPC.Multiline = true;
            this.edNPC.Name = "edNPC";
            this.edNPC.ReadOnly = true;
            this.edNPC.Size = new System.Drawing.Size(489, 250);
            this.edNPC.TabIndex = 0;
            // 
            // DisplayWorld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 412);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "DisplayWorld";
            this.Text = "DisplayWorld";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSEC;
        private System.Windows.Forms.Label lblWorld;
        private System.Windows.Forms.Button btnJump;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox edNotes;
        private System.Windows.Forms.TextBox edNPC;
    }
}