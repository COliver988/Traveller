using CharGen1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CharGen1
{
    public partial class Form1 : Form
    {

        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: game mechanic selector for which class rules to use
            CharacterGameClass myClass = GetCharacterClass();
            generate(myClass);
        }

        private CharacterGameClass GetCharacterClass()
        {
            List<string> tags = txt_tags.Text.Split(new [] { "\r\n" }, StringSplitOptions.None).ToList();
            int dType = 6;
            if (rb_D4.Checked)
                dType = 4;
            if (rb_D6.Checked)
                dType = 6;
            if (rb_D8.Checked)
                dType = 8;
            if (rb_D10.Checked)
                dType = 10;
            if (rb_D12.Checked)
                dType = 12;
            if (rb_D20.Checked)
                dType = 20;
            CharacterGameClass myClass = new CharacterGameClass(txt_Game.Text, tags, dType, int.Parse(txt_DiceToRoll.Text), cbHeroic.Checked);
            return myClass;
        }

        private void generate(CharacterGameClass myClass)
        {
            List<string> results = new List<string>() { myClass.Name };
            foreach (var stat in myClass.StatTags)
                results.Add(String.Format("{0} - {1}", stat, roll(myClass.DiceToRoll, myClass.DiceType, myClass.isHeroic)));
            txtResults.Text = String.Join(Environment.NewLine, results);
        }
        private int roll(int number, int sides = 6, bool isHeroic = false)
        {
            List<int> dieRolls = new List<int>();
            int dice = number + (isHeroic ? 1 : 0);   
            for (int i = 0; i < dice; i++)
                dieRolls.Add(r.Next(1, sides + 1));
            dieRolls.Sort();
            dieRolls.Reverse();
            return dieRolls.Take(number).Sum();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CharacterGameClass myClass = GetCharacterClass();
            myClass.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CharacterGameClass myClass = new CharacterGameClass();
            myClass = myClass.Load();
            SetCharacterClass(myClass);
        }

        private void SetCharacterClass(CharacterGameClass myClass)
        {
            txt_tags.Text = String.Join("\r\n", myClass.StatTags);
            txt_Game.Text = myClass.Name;
            txt_DiceToRoll.Text = myClass.DiceToRoll.ToString();
            cbHeroic.Checked = myClass.isHeroic;
            switch (myClass.DiceType)   
            {
                case 4:
                    rb_D4.Checked = true;
                    break;
                case 6:
                    rb_D6.Checked = true;
                    break;
                case 8:
                    rb_D8.Checked = true;
                    break;
                case 10:
                    rb_D10.Checked = true;
                    break;
                case 12:
                    rb_D12.Checked = true;
                    break;
                case 20:
                    rb_D20.Checked = true;
                    break;
                default:
                    break;
            }
        }
    }
}
