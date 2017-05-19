using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace FullTexttoChar
{
    public partial class FrmMain : Form
    {

        public FrmMain()
        {
            InitializeComponent();
        }
        Dictionary<char, uint> DictCharToCode = new Dictionary<char, uint>();
        Dictionary<char, uint> DictNewCode = new Dictionary<char, uint>();
        Dictionary<char, uint> CountDup = new Dictionary<char, uint>();
        GameFolder Gamefolder;
        private void BtnToChar_Click(object sender, EventArgs e)
        {
            TxtOutput.Clear();
            TxtCount.Clear();
            DictCharToCode.Clear();
            DictNewCode.Clear();
            CountDup.Clear();

            DictCharToCode =MakeCharToCodeDict(TxtInput.Text.ToCharArray());      
            DictNewCode = MakeNewCodeDict();

            StringBuilder AllStrTable = new StringBuilder();
            AllStrTable.Clear();
            foreach(char ch in DictCharToCode.Keys)
            {
                AllStrTable.Append(ch);
            }
            TxtOutput.Text = AllStrTable.ToString() + "\r\nCount: " + DictCharToCode.Count;

            StringBuilder StrCount = new StringBuilder();
            var order=CountDup.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (KeyValuePair<char,uint> ch in order)
            {
                StrCount.Append(ch.Key + "," + ch.Value+"\r\n");
            }
            TxtCount.Text = StrCount.ToString();

        }
        private Dictionary<char, uint> MakeCharToCodeDict(char[] CharArray)
        {
            Dictionary<char, uint> dict = new Dictionary<char, uint>();
            CountDup.Clear();
            foreach (char ch in CharArray)
            {
                uint chcode = Convert.ToUInt32(ch);
                if (chcode <160) { continue; }
                if (!dict.ContainsKey(ch))
                {
                    dict.Add(ch, chcode);
                    CountDup.Add(ch,1);
                }
                else
                {
                    uint ls = CountDup[ch];
                    CountDup.Remove(ch);
                    CountDup.Add(ch, ls + 1);
                }
            }
            return dict;
        }
        private Dictionary<char, uint> MakeNewCodeDict()
        {
            Dictionary<char, uint> dict = new Dictionary<char, uint>();
            uint StartCode = Convert.ToUInt32(TxtStartCode.Text);
            if (StartCode <= 127) { StartCode = 128; }
            foreach (char ch in DictCharToCode.Keys)
            {
                if (!dict.ContainsKey(ch))
                {
                    dict.Add(ch, StartCode);
                }
                StartCode++;
            }
            return dict;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadGameFolder();
        }

        private void LoadGameFolder()
        {
            Gamefolder = new GameFolder(TxtPath.Text);
            TxtOutput.Text = "Game Folder: " + Gamefolder.GamePath;
        }

        private void BtnLoadOriginal_Click(object sender, EventArgs e)
        {
            TxtInput.Clear();

            if (!Directory.Exists(Gamefolder.OriXMLFolder)) { return; }
            foreach (string file in Directory.GetFiles(Gamefolder.OriXMLFolder))
            {
                TxtInput.AppendText(File.ReadAllText(file));
            }
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            TxtInput.Clear();
            string filepath = Gamefolder.OriXMLFolder + TxtPathFile.Text;
            if (!File.Exists(filepath)) { return; }
            TxtInput.AppendText(File.ReadAllText(filepath));
        }

        private void BtnMakeFile_Click(object sender, EventArgs e)
        {
            if (!ChkSingleFile.Checked)
            {
                WriteXMLFolder();
                WriteFntFolder();
            }
            else
            {

            }
        }
        private void WriteXMLFolder()
        {
            if (!Directory.Exists(Gamefolder.OriXMLFolder)) { return; }
            foreach (string file in Directory.GetFiles(Gamefolder.OriXMLFolder))
            {
                File.WriteAllText(Gamefolder.GameXMLFolder+Path.GetFileName(file), MakeSingleFile(file));
                TxtOutput.AppendText("\r\n" + Gamefolder.GameXMLFolder + Path.GetFileName(file) + " xml Saved");
            }
        }
        private void WriteFntFolder()
        {
            if (!Directory.Exists(Gamefolder.OriFontFolder)) { return; }
            foreach (string file in Directory.GetFiles(Gamefolder.OriFontFolder, "*.fnt"))
            {
                string path = Gamefolder.GameFontFolder + Path.GetFileName(file);
                string wr = MakeFontFile(file);
                File.WriteAllText(path, wr);
                TxtOutput.AppendText("\r\n" + path + " fnt Saved");
            }
        }
        private string MakeSingleFile(string FilePath)
        {
            char[] FileCharArray = File.ReadAllText(FilePath).ToCharArray();
            StringBuilder outstr = new StringBuilder() ;
            for (int i=0; i<FileCharArray.Length; i++)
            {
                if (DictNewCode.ContainsKey(FileCharArray[i]))
                {
                   outstr.Append( Convert.ToChar(DictNewCode[FileCharArray[i]]));
                }
                else
                {
                    outstr.Append(FileCharArray[i]);
                }
            }

            return outstr.ToString();
            
        }
        private string MakeFontFile(string FilePath)
        {
            Dictionary<string, string> DictFontCode = new Dictionary<string, string>();
            foreach (char ch in DictCharToCode.Keys)
            {
                string pre = "char id=";
                DictFontCode.Add(pre + DictCharToCode[ch].ToString(), pre + DictNewCode[ch].ToString());
            }

            string[] FileLines = File.ReadAllLines(FilePath);
            StringBuilder NewFile=new StringBuilder();
            NewFile.Clear();

            for (int i =0;i<=99; i++)
            {
                NewFile.AppendLine(FileLines[i]);
            }

            foreach(string line in FileLines)
            {
                foreach (string key in DictFontCode.Keys)
                {
                    if (line.Contains(key))
                    {
                        NewFile.AppendLine(line.Replace(key, DictFontCode[key]));
                    }
                }
                
            }
            return NewFile.ToString();
        }

        private void ChkSingleFile_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSingleFile.Checked)
            {
                TxtPathFile.Enabled = true;
                BtnLoadFile.Enabled = true;
            }
            else
            {
                TxtPathFile.Enabled = false;
                BtnLoadFile.Enabled = false;
            }

            }
    }
}
