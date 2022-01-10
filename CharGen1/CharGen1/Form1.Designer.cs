
namespace CharGen1
{
    partial class Form1
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
            this.txtResults = new System.Windows.Forms.TextBox();
            this.btn_generate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_tags = new System.Windows.Forms.TextBox();
            this.txt_DiceToRoll = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rb_D20 = new System.Windows.Forms.RadioButton();
            this.rb_D12 = new System.Windows.Forms.RadioButton();
            this.rb_D10 = new System.Windows.Forms.RadioButton();
            this.rb_D8 = new System.Windows.Forms.RadioButton();
            this.rb_D6 = new System.Windows.Forms.RadioButton();
            this.rb_D4 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Game = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(25, 73);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(333, 352);
            this.txtResults.TabIndex = 0;
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(25, 23);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(97, 23);
            this.btn_generate.TabIndex = 1;
            this.btn_generate.Text = "Generate";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_tags);
            this.panel1.Controls.Add(this.txt_DiceToRoll);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rb_D20);
            this.panel1.Controls.Add(this.rb_D12);
            this.panel1.Controls.Add(this.rb_D10);
            this.panel1.Controls.Add(this.rb_D8);
            this.panel1.Controls.Add(this.rb_D6);
            this.panel1.Controls.Add(this.rb_D4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_Game);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(396, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 399);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 27);
            this.button1.TabIndex = 12;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Characteristics";
            // 
            // txt_tags
            // 
            this.txt_tags.Location = new System.Drawing.Point(186, 225);
            this.txt_tags.Multiline = true;
            this.txt_tags.Name = "txt_tags";
            this.txt_tags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_tags.Size = new System.Drawing.Size(164, 155);
            this.txt_tags.TabIndex = 3;
            // 
            // txt_DiceToRoll
            // 
            this.txt_DiceToRoll.Location = new System.Drawing.Point(186, 184);
            this.txt_DiceToRoll.Name = "txt_DiceToRoll";
            this.txt_DiceToRoll.Size = new System.Drawing.Size(74, 22);
            this.txt_DiceToRoll.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Number of dice to roll";
            // 
            // rb_D20
            // 
            this.rb_D20.AutoSize = true;
            this.rb_D20.Location = new System.Drawing.Point(179, 125);
            this.rb_D20.Name = "rb_D20";
            this.rb_D20.Size = new System.Drawing.Size(52, 20);
            this.rb_D20.TabIndex = 8;
            this.rb_D20.TabStop = true;
            this.rb_D20.Text = "D20";
            this.rb_D20.UseVisualStyleBackColor = true;
            // 
            // rb_D12
            // 
            this.rb_D12.AutoSize = true;
            this.rb_D12.Location = new System.Drawing.Point(179, 98);
            this.rb_D12.Name = "rb_D12";
            this.rb_D12.Size = new System.Drawing.Size(52, 20);
            this.rb_D12.TabIndex = 7;
            this.rb_D12.TabStop = true;
            this.rb_D12.Text = "D12";
            this.rb_D12.UseVisualStyleBackColor = true;
            // 
            // rb_D10
            // 
            this.rb_D10.AutoSize = true;
            this.rb_D10.Location = new System.Drawing.Point(179, 71);
            this.rb_D10.Name = "rb_D10";
            this.rb_D10.Size = new System.Drawing.Size(52, 20);
            this.rb_D10.TabIndex = 6;
            this.rb_D10.TabStop = true;
            this.rb_D10.Text = "D10";
            this.rb_D10.UseVisualStyleBackColor = true;
            // 
            // rb_D8
            // 
            this.rb_D8.AutoSize = true;
            this.rb_D8.Location = new System.Drawing.Point(111, 124);
            this.rb_D8.Name = "rb_D8";
            this.rb_D8.Size = new System.Drawing.Size(45, 20);
            this.rb_D8.TabIndex = 5;
            this.rb_D8.TabStop = true;
            this.rb_D8.Text = "D8";
            this.rb_D8.UseVisualStyleBackColor = true;
            // 
            // rb_D6
            // 
            this.rb_D6.AutoSize = true;
            this.rb_D6.Location = new System.Drawing.Point(111, 97);
            this.rb_D6.Name = "rb_D6";
            this.rb_D6.Size = new System.Drawing.Size(45, 20);
            this.rb_D6.TabIndex = 4;
            this.rb_D6.TabStop = true;
            this.rb_D6.Text = "D6";
            this.rb_D6.UseVisualStyleBackColor = true;
            // 
            // rb_D4
            // 
            this.rb_D4.AutoSize = true;
            this.rb_D4.Location = new System.Drawing.Point(111, 70);
            this.rb_D4.Name = "rb_D4";
            this.rb_D4.Size = new System.Drawing.Size(45, 20);
            this.rb_D4.TabIndex = 3;
            this.rb_D4.TabStop = true;
            this.rb_D4.Text = "D4";
            this.rb_D4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dice type";
            // 
            // txt_Game
            // 
            this.txt_Game.Location = new System.Drawing.Point(83, 22);
            this.txt_Game.Name = "txt_Game";
            this.txt_Game.Size = new System.Drawing.Size(277, 22);
            this.txt_Game.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(21, 319);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 27);
            this.button2.TabIndex = 13;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.txtResults);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rb_D6;
        private System.Windows.Forms.RadioButton rb_D4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Game;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_D10;
        private System.Windows.Forms.RadioButton rb_D8;
        private System.Windows.Forms.TextBox txt_DiceToRoll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_D20;
        private System.Windows.Forms.RadioButton rb_D12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_tags;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

