namespace Traveller2
{
    partial class Selling
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSelling = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOrigSystem = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edDM = new System.Windows.Forms.MaskedTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnSold = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSellAt = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current System:";
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.Location = new System.Drawing.Point(124, 9);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(52, 18);
            this.lblCurrent.TabIndex = 1;
            this.lblCurrent.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lot being sold:";
            // 
            // lblSelling
            // 
            this.lblSelling.AutoSize = true;
            this.lblSelling.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelling.Location = new System.Drawing.Point(124, 36);
            this.lblSelling.Name = "lblSelling";
            this.lblSelling.Size = new System.Drawing.Size(52, 18);
            this.lblSelling.TabIndex = 3;
            this.lblSelling.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Origination system:";
            // 
            // lblOrigSystem
            // 
            this.lblOrigSystem.AutoSize = true;
            this.lblOrigSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigSystem.Location = new System.Drawing.Point(124, 89);
            this.lblOrigSystem.Name = "lblOrigSystem";
            this.lblOrigSystem.Size = new System.Drawing.Size(52, 18);
            this.lblOrigSystem.TabIndex = 5;
            this.lblOrigSystem.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "DMs (Broker, etc)";
            // 
            // edDM
            // 
            this.edDM.Location = new System.Drawing.Point(127, 120);
            this.edDM.Mask = "00";
            this.edDM.Name = "edDM";
            this.edDM.Size = new System.Drawing.Size(34, 20);
            this.edDM.TabIndex = 7;
            this.toolTip1.SetToolTip(this.edDM, "enter the accumulate DMs to apply (3 = +3, -2 = -2, etc)");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "enter the accumulate DMs to apply (3 = +3, -2 = -2, etc)";
            // 
            // btnSell
            // 
            this.btnSell.Location = new System.Drawing.Point(15, 181);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(187, 23);
            this.btnSell.TabIndex = 9;
            this.btnSell.Text = "determine selling price";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // btnSold
            // 
            this.btnSold.Location = new System.Drawing.Point(230, 181);
            this.btnSold.Name = "btnSold";
            this.btnSold.Size = new System.Drawing.Size(187, 23);
            this.btnSold.TabIndex = 10;
            this.btnSold.Text = "make the sale";
            this.btnSold.UseVisualStyleBackColor = true;
            this.btnSold.Click += new System.EventHandler(this.btnSold_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Selling price:";
            // 
            // lblSellAt
            // 
            this.lblSellAt.AutoSize = true;
            this.lblSellAt.Location = new System.Drawing.Point(126, 152);
            this.lblSellAt.Name = "lblSellAt";
            this.lblSellAt.Size = new System.Drawing.Size(82, 13);
            this.lblSellAt.TabIndex = 12;
            this.lblSellAt.Text = "<start haggling>";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(126, 64);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(46, 18);
            this.lblDesc.TabIndex = 13;
            this.lblDesc.Text = "label7";
            // 
            // Selling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 216);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblSellAt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSold);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.edDM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblOrigSystem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSelling);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Selling";
            this.Text = "Selling";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSelling;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOrigSystem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox edDM;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnSold;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSellAt;
        private System.Windows.Forms.Label lblDesc;
    }
}