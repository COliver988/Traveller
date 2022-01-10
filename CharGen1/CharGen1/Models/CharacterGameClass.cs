using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CharGen1.Models
{
    class CharacterGameClass
    {
        public CharacterGameClass() { }

        public CharacterGameClass (string name, List<String> stats, int diceType = 6, int diceToRoll = 3)
        {
            Name = name;
            StatTags = stats;
            DiceType = diceType;
            DiceToRoll = diceToRoll;
        }

        public string Name { get; set; }
        public List<string> StatTags {get; set;}

        public int DiceType { get; set; }
        public int DiceToRoll { get; set; }

        public void Save()
        {
            string gameData = JsonConvert.SerializeObject(this);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "json files|(*.json)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog1.FileName, gameData);
        }

        public CharacterGameClass Load()
        {
            CharacterGameClass cc = new CharacterGameClass();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "json files|(*.json)|All files (*.*)|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string gameData = File.ReadAllText(openDialog.FileName);
                cc = JsonConvert.DeserializeObject<CharacterGameClass>(gameData);

            }
            return cc;
        }
    } 
}
