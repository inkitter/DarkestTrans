using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DarkestTrans
{

    public partial class FrmTranslator : Form
    {
        private void Mainfrm_Load(object sender, EventArgs e)
        {
            FormInit();
            FunRefresh();
            UserDictInitialize();
            SetToolTip();
            //ComBOldVersionInitialize();
        }

        private void SetToolTip()
        {
            ToolTiptt.AutomaticDelay = 100;
            ToolTiptt.AutoPopDelay = 10000;
            ToolTiptt.InitialDelay = 200;
            ToolTiptt.ReshowDelay = 200;
            ToolTiptt.SetToolTip(LabHelp, "Double Click: Open Github Web \n https://github.com/inkitter/ \n Ctrl + ↓: Find Line to Translate \n Ctrl + ←: Refresh API Textbox");
        }

        private void UserDictInitialize()
        {
            if (!File.Exists(StaticVars.UserDictCSV))
            {
                return;
            }
            var reader = new StreamReader(File.OpenRead(StaticVars.UserDictCSV), Encoding.UTF8, true);
            try
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (!String.IsNullOrEmpty(values[0]) && !String.IsNullOrEmpty(values[1]))
                    {
                        if (!UserDict.ContainsKey(values[0].ToLower()))
                        {
                            UserDict.Add(values[0].ToLower(), values[1]);
                        }
                    }
                }
            }
            catch { }
            reader.Close();
        }

        List<XMLdata> XMLText;
        Dictionary<string, string> UserDict = new Dictionary<string, string>();
        ToolTip ToolTiptt = new ToolTip();


        private void BtnSave_Click(object sender, EventArgs e) { FuncSave(); }
        private void FuncSave()
        {
            XMLTools.SaveMod(LstFiles.Text, XMLText);
            BtnSave.Enabled = false;
            // 保存文件
        }

        private void LstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstFiles.Enabled == true)
            {
               XMLText=XMLTools.ReadFile(LstFiles.Text);
               LoadtoDataGrid();
            }
        }

        private void FormInit()
        {

        }
        private void FunRefresh()
        {
            string[] stringList = Directory.GetFiles(StaticVars.DIREN,"*.xml");
            
            if (stringList.Length > 0)
            {
                foreach (string str in stringList)
                {
                    LstFiles.Items.Add(Path.GetFileName(str));
                }
                LstFiles.SelectedIndex = 0;
                //从eng目录读取文件并载入listbox，默认选择第一个文件。
            }
            else
            {
                LstFiles.Items.Add("No XML File in directory");
                LstFiles.Enabled = false;
                BtnApply.Enabled = false;
                BtnAPItochnBox.Enabled = false;
            }
        }

        private void LoadtoDataGrid()
        {
            
            DfData.ClearSelection();
            DfData.DataSource = XMLText;
            // 将对象映射到datagrid里。

            DfData.Columns[0].Width = 200;
            DfData.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            DfData.Columns[1].Width = 300;
            DfData.Columns[2].Width = 300;

            //DfData.Columns[0].HeaderText = "Variable Name";
            //DfData.Columns[1].HeaderText = "FROM";
            //DfData.Columns[2].HeaderText = "To";
            //DfData.Columns[3].HeaderText = CombOldVersion.Text;
            //DfData.Columns[4].HeaderText = "Save Line Preview";


            // 寻找原文与译文内容一致的，标记颜色，醒目便于确认需要翻译的部分。
            DfRefresh();
        }

        private void DfRefresh()
        {

            foreach (DataGridViewRow row in DfData.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
                
                if (XMLText.ElementAt(row.Index).SameInToAndFrom())
                {
                     row.Cells[2].Style.BackColor = Color.LightCyan; 
                }
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            TxtCHN.Text = XMLTools.RemoveReturnMark(TxtCHN.Text);
            XMLText.ElementAt(DfData.CurrentRow.Index).ApplyLine(TxtCHN.Text);
            DfData.Refresh();
            //将文本框内容移除换行符，放回变量，并刷新datagrid。
            BtnSave.Enabled = true;
            //做过修改，保存按钮可以使用了。
        }

        private void DfData_SelectionChanged(object sender, EventArgs e)
        {
            if (DfData.CurrentRow.Selected == true)
            {
                Showintxt(DfData.CurrentRow.Index);
            }
        }

        private void Showintxt(int id)
        {
            string engtext = XMLText.ElementAt(id).ContentEng;
            if (UserDict.Count>0)
            {
                TxtENG.Text = XMLTools.ReplaceWithUserDict(engtext, UserDict);
            }
            else
            {
                TxtENG.Text = engtext;
            }

            TxtCHN.Text = XMLText.ElementAt(id).ContentDest;

            BtnApply.Enabled = true;
            GetAPITranslation();
        }

        private async void GetAPITranslation()
        {
            TxtAPI.Clear();
            BtnAPItochnBox.Enabled = false;
            Task<string> GetTranslationTask = new Task<string>(FuncAsyncGetTranslation);
            try
            {
                GetTranslationTask.Start();
                TxtAPI.Text = await GetTranslationTask;
                GetTranslationTask.Dispose();
            }
            catch
            {
                TxtAPI.Text = "Nothing";
                GetTranslationTask.Dispose();
            }
            BtnAPItochnBox.Enabled = true;

            string FuncAsyncGetTranslation()
            {
                return XMLTools.GetTranslatedTextFromAPI(XMLTools.RemoveReturnMark(TxtENG.Text));
            }
        }
          

        public FrmTranslator()
        {
            InitializeComponent();
        }
        private void Mainfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void TxtCHN_Enter(object sender, EventArgs e)
        {
            TxtCHN.SelectAll();
        }
        private void TxtCHN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                TxtCHN.SelectAll();
            }
        }

        private void TxtENG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                TxtENG.SelectAll();
            }
            if (e.KeyCode == Keys.Left && e.Modifiers == Keys.Control)
            {
                GetAPITranslation();
            }
        }

        private void TxtAPI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                TxtAPI.SelectAll();
            }
        }

        private void TxtCHN_DoubleClick(object sender, EventArgs e)
        {
            TxtCHN.SelectAll();
        }

        private void TxtAPI_DoubleClick(object sender, EventArgs e)
        {
            TxtAPI.SelectAll();
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(StaticVars.DIRCN + LstFiles.Text);
        }

        private void BtnOpenFileOriginal_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(StaticVars.DIREN + LstFiles.Text);
        }
        private void TxtCHN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void Dfdata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
            {
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down && e.Modifiers == Keys.Control)
            {
                e.Handled = true;
            }
        }
        private void BtnAPItochnBox_Click(object sender, EventArgs e)
        {
            TxtCHN.Text = TxtAPI.Text;
        }

        private void Translatorfrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control)
            {
                BtnApply_Click(sender, e);
                return;
            }
            if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
            {
                BtnAPItochnBox_Click(sender, e);
                return;
            }
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                BtnSave_Click(sender, e);
                return;
            }
            if (e.KeyCode == Keys.Down && e.Modifiers == Keys.Control)
            {
                for (int i = DfData.CurrentRow.Index; i < DfData.RowCount; i++)
                {
                    if (i + 1 <= DfData.RowCount - 1)
                    {
                        DfData.CurrentCell = DfData[DfData.CurrentCell.ColumnIndex, i + 1];
                    }
                    else
                    {
                        DfData.CurrentCell = DfData[DfData.CurrentCell.ColumnIndex, 0];
                    }
                }
            }
        }

        private void BtnOpenBrowser_Click(object sender, EventArgs e)
        {
            if (RadioBaidu.Checked)
            {
                XMLTools.OpenWithBrowser(TxtENG.Text, "Baidu");
            }
            if (RadioGoogle.Checked)
            {
                XMLTools.OpenWithBrowser(TxtENG.Text, "Google");
            }
        }
        private void FuncInsertSign(string SignToInsert)
        {
            if (TxtCHN.SelectedText == "")
            {
                TxtCHN.SelectedText = "§"+ SignToInsert;
            }
            else
            {
                TxtCHN.SelectedText = "§"+ SignToInsert + TxtCHN.SelectedText + "§!";
            }
        }

        private void LabColorR_Click(object sender, EventArgs e)
        {
            FuncInsertSign("R");
        }

        private void LabColorG_Click(object sender, EventArgs e)
        {
            FuncInsertSign("G");
        }

        private void LabColorB_Click(object sender, EventArgs e)
        {
            FuncInsertSign("B");
        }

        private void LabColorH_Click(object sender, EventArgs e)
        {
            FuncInsertSign("H");
        }

        private void LabColorL_Click(object sender, EventArgs e)
        {
            FuncInsertSign("L");
        }

        private void LabColorS_Click(object sender, EventArgs e)
        {
            FuncInsertSign("S");
        }

        private void LabColorM_Click(object sender, EventArgs e)
        {
            FuncInsertSign("M");
        }

        private void LabColorGx_Click(object sender, EventArgs e)
        {
            FuncInsertSign("g");
        }

        private void LabColorW_Click(object sender, EventArgs e)
        {
            FuncInsertSign("W");
        }

        private void LabColorY_Click(object sender, EventArgs e)
        {
            FuncInsertSign("Y");
        }

        private void LabColorE_Click(object sender, EventArgs e)
        {
            FuncInsertSign("E");
        }

        private void LabColorEnd_Click(object sender, EventArgs e)
        {
            TxtCHN.SelectedText = "§!";
        }

        private void LabHelp_DoubleClick(object sender, EventArgs e)
        {
            XMLTools.OpenWithBrowser("", "Help");
        }

        private void CombOldVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FrmTranslator_SizeChanged(object sender, EventArgs e)
        {
            DfData.Height = Height - 295;
            DfData.Width = Width - 215;
            LstFiles.Height = Height - 295;
            TxtCHN.Width = Width - 260;
            TxtENG.Width = Width - 260;
            TxtAPI.Width = Width - 260;
        }

        private void ChkSimplifiedChinese_CheckedChanged(object sender, EventArgs e)
        {
            ChkTraditionalChinese.Checked = false;
        }

        private void ChkTraditionalChinese_CheckedChanged(object sender, EventArgs e)
        {
            ChkSimplifiedChinese.Checked = false;
        }

        private void TxtFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int i = DfData.CurrentRow.Index; i < DfData.RowCount; i++)
                {
                    if (i + 1 <= DfData.RowCount - 1)
                    {
                        DfData.CurrentCell = DfData[DfData.CurrentCell.ColumnIndex, i + 1];
                    }
                    else
                    {
                        DfData.CurrentCell = DfData[DfData.CurrentCell.ColumnIndex, 0];
                    }
                    if (XMLTools.RegexContainsWord(XMLText.ElementAt(DfData.CurrentRow.Index).ContentEng,TxtFind.Text)) { break; }
                }
            }
        }

        private void TxtFind_Enter(object sender, EventArgs e)
        {
            TxtFind.SelectAll();
        }

        private void TxtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                TxtFind.SelectAll();
            }
        }

        private void BtnAllToSimpChn_Click(object sender, EventArgs e)
        {

        }

        private void LstFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void LstFiles_KeyUp(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void LstFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void BtnMergeAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Merge All Files ?", "Merge All", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                LstFiles.Enabled = false;
                for (int i = 0; i < LstFiles.Items.Count; i++)
                {
                    LstFiles.SelectedIndex = i;
                }
                LstFiles.Enabled = true;
            }
        }

        private void BtnReWithDict_Click(object sender, EventArgs e)
        {
            XMLText=XMLTools.ReplaceWithDict(XMLText,UserDict);
            DfData.Refresh();
            BtnSave.Enabled = true;
        }
    }
}
