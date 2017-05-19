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
        Dictionary<char, uint> DictAllCode = new Dictionary<char, uint>();
        Dictionary<char, uint> DictOldCode = new Dictionary<char, uint>();
        Dictionary<char, uint> DictNewCode = new Dictionary<char, uint>();
        
        Dictionary<char, uint> DictCountDup = new Dictionary<char, uint>();
        GameFolder Gamefolder;
        private void BtnToChar_Click(object sender, EventArgs e)
        {
            TxtOutput.Clear();
            TxtCount.Clear();

            Tuple<Dictionary<char, uint>, Dictionary<char, uint>> tp = Functions.MakeOldCodeDict(TxtInput.Text.ToCharArray());
            DictOldCode = tp.Item1;
            DictCountDup = tp.Item2;

            ShowLastDict(DictOldCode);
        }
        private void ShowLastDict(Dictionary<char, uint> dict)
        {
            StringBuilder AllStrTable = new StringBuilder();
            AllStrTable.Clear();
            foreach (char ch in dict.Keys)
            {
                AllStrTable.Append(ch);
            }
            TxtOutput.Text = AllStrTable.ToString() + "\r\nCount: " + dict.Count;

            StringBuilder StrCount = new StringBuilder();
            var order = DictCountDup.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (KeyValuePair<char, uint> ch in order)
            {
                StrCount.Append(ch.Key + "," + ch.Value + "\r\n");
            }
            TxtCount.Text = StrCount.ToString();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadGameFolder();
            RefreshFileList();
            MakeAllDict();
        }

        private void MakeAllDict()
        {
            StringBuilder str = new StringBuilder();
            foreach(string file in LstFiles.Items)
            {
                str.Append(File.ReadAllText(file));
            }
            Tuple<Dictionary<char, uint>, Dictionary<char, uint>> tp = Functions.MakeOldCodeDict(str.ToString().ToCharArray());
            DictAllCode = tp.Item1;
            DictCountDup = tp.Item2;
            DictNewCode = Functions.MakeNewCodeDict(DictAllCode, Convert.ToUInt32(TxtStartCode.Text));
        }

        private void LoadGameFolder()
        {
            Gamefolder = new GameFolder(TxtPath.Text);
            Log("Game Folder Set: " + Gamefolder.GamePath);
        }
        private void RefreshFileList()
        {
            if (!Directory.Exists(Gamefolder.OriXMLFolder)) { Log("No Original XML Folder"); return; }
            foreach (string file in Directory.GetFiles(Gamefolder.OriXMLFolder))
            {
                LstFiles.Items.Add(file);
            }
            Log("Original XML Loaded");
        }
        private void Log(string logtext)
        {
            TxtOutput.AppendText("\r\n");
            TxtOutput.AppendText(logtext);
        }

        private void BtnLoadSelectedFiles_Click(object sender, EventArgs e)
        {
            TxtInput.Clear();
            List<string> FilesToLoad = new List<string>();

            foreach (var file in LstFiles.SelectedItems)
            {
                FilesToLoad.Add(file.ToString());
            }
            TxtInput.Text =Functions.LoadFiles(FilesToLoad);

            TxtOutput.Clear();
            TxtCount.Clear();

            Tuple<Dictionary<char, uint>, Dictionary<char, uint>> tp = Functions.MakeOldCodeDict(TxtInput.Text.ToCharArray());
            DictOldCode = tp.Item1;
            DictCountDup = tp.Item2;

            ShowLastDict(DictOldCode);
        }
        

        private void BtnMakeFile_Click(object sender, EventArgs e)
        {
            WriteXMLFolder();
            // 编辑XML并写入

            WriteFntFolder();
            // 编辑Fnt并写入
        }
        private void WriteXMLFolder()
        {
            if (!Directory.Exists(Gamefolder.OriXMLFolder)) { return; }
            List<string> FilesToSave = new List<string>();
            foreach (var file in LstFiles.SelectedItems)
            {
                FilesToSave.Add(file.ToString());
            }
            Functions.WriteXMLFileFolder(Gamefolder.OriXMLFolder, Gamefolder.GameXMLFolder, FilesToSave, DictNewCode,!ChkUseOldCode.Checked);
            Log("Files saved: " );
        }
        private void WriteFntFolder()
        {
            string OutputFilePath = "";
            if (LstFiles.SelectedItems.Count == 0) { Log("No File Selected"); return; }
            // 如果没选择文件则提示
            if (LstFiles.SelectedItems.Count == 1)
            {
                string file = LstFiles.SelectedItem.ToString();
                OutputFilePath = Gamefolder.GameFontFolder + Path.GetFileNameWithoutExtension(file).Replace(".string_table", "")+".fnt";
            }
            // 如果选择了一个文件就用这个文件名
            else
            {
                OutputFilePath = Gamefolder.GameFontFolder + "14.fnt";
            }
            // 如果选择了多个文件就用14这个文件名
            
            Log("Output File: " + OutputFilePath);
            //if (!Directory.Exists(Path.GetDirectoryName(OutputFilePath))) { Log("Original Folder not Found"); return; }
            if (!File.Exists(Gamefolder.OriFontFile)) { Log("Original fnt File not Found"); return; }

            Functions.WriteFntFileWithCode(Gamefolder.OriFontFile, OutputFilePath, DictOldCode, DictNewCode,!ChkUseOldCode.Checked);
        }

        private void BtnSetOutputFolder_Click(object sender, EventArgs e)
        {
            LoadGameFolder();
        }

        private void ChkUseOldCode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnMakeAllDict_Click(object sender, EventArgs e)
        {
            MakeAllDict();
            ShowLastDict(DictAllCode);
            Log("New Dict Made");
        }
    }
}
