namespace Traveller2
{
    partial class AddWorldImage
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
            this.edDesc = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description for menu:";
            // 
            // edDesc
            // 
            this.edDesc.Location = new System.Drawing.Point(116, 6);
            this.edDesc.Name = "edDesc";
            this.edDesc.Size = new System.Drawing.Size(139, 20);
            this.edDesc.TabIndex = 1;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 43);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(104, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "load a file";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(3, 81);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(118, 13);
            this.lblFile.TabIndex = 3;
            this.lblFile.Text = "<no current file loaded>";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(6, 112);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(249, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "save image to world notes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddWorldImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 145);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.edDesc);
            this.Controls.Add(this.label1);
            this.Name = "AddWorldImage";
            this.Text = "AddWorldImage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edDesc;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnSave;
    }
}