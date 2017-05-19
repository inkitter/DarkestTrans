namespace FullTexttoChar
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtInput = new System.Windows.Forms.TextBox();
            this.BtnToChar = new System.Windows.Forms.Button();
            this.TxtOutput = new System.Windows.Forms.TextBox();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.BtnMakeFile = new System.Windows.Forms.Button();
            this.BtnLoadSelectedFiles = new System.Windows.Forms.Button();
            this.TxtCount = new System.Windows.Forms.TextBox();
            this.TxtStartCode = new System.Windows.Forms.TextBox();
            this.LstFiles = new System.Windows.Forms.ListBox();
            this.BtnSetOutputFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ChkUseOldCode = new System.Windows.Forms.CheckBox();
            this.BtnMakeAllDict = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtInput
            // 
            this.TxtInput.Location = new System.Drawing.Point(282, 2);
            this.TxtInput.Multiline = true;
            this.TxtInput.Name = "TxtInput";
            this.TxtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtInput.Size = new System.Drawing.Size(424, 268);
            this.TxtInput.TabIndex = 0;
            this.TxtInput.WordWrap = false;
            // 
            // BtnToChar
            // 
            this.BtnToChar.Location = new System.Drawing.Point(445, 301);
            this.BtnToChar.Name = "BtnToChar";
            this.BtnToChar.Size = new System.Drawing.Size(123, 34);
            this.BtnToChar.TabIndex = 1;
            this.BtnToChar.Text = "To Char";
            this.BtnToChar.UseVisualStyleBackColor = true;
            this.BtnToChar.Click += new System.EventHandler(this.BtnToChar_Click);
            // 
            // TxtOutput
            // 
            this.TxtOutput.Location = new System.Drawing.Point(183, 341);
            this.TxtOutput.Multiline = true;
            this.TxtOutput.Name = "TxtOutput";
            this.TxtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtOutput.Size = new System.Drawing.Size(523, 285);
            this.TxtOutput.TabIndex = 2;
            // 
            // TxtPath
            // 
            this.TxtPath.Location = new System.Drawing.Point(103, 271);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.Size = new System.Drawing.Size(474, 21);
            this.TxtPath.TabIndex = 3;
            this.TxtPath.Text = "D:\\SteamLibrary\\steamapps\\common\\DarkestDungeon";
            // 
            // BtnMakeFile
            // 
            this.BtnMakeFile.Location = new System.Drawing.Point(583, 301);
            this.BtnMakeFile.Name = "BtnMakeFile";
            this.BtnMakeFile.Size = new System.Drawing.Size(123, 34);
            this.BtnMakeFile.TabIndex = 6;
            this.BtnMakeFile.Text = "MakeFile";
            this.BtnMakeFile.UseVisualStyleBackColor = true;
            this.BtnMakeFile.Click += new System.EventHandler(this.BtnMakeFile_Click);
            // 
            // BtnLoadSelectedFiles
            // 
            this.BtnLoadSelectedFiles.Location = new System.Drawing.Point(1, 298);
            this.BtnLoadSelectedFiles.Name = "BtnLoadSelectedFiles";
            this.BtnLoadSelectedFiles.Size = new System.Drawing.Size(169, 34);
            this.BtnLoadSelectedFiles.TabIndex = 8;
            this.BtnLoadSelectedFiles.Text = "Load Selected Files";
            this.BtnLoadSelectedFiles.UseVisualStyleBackColor = true;
            this.BtnLoadSelectedFiles.Click += new System.EventHandler(this.BtnLoadSelectedFiles_Click);
            // 
            // TxtCount
            // 
            this.TxtCount.Location = new System.Drawing.Point(1, 338);
            this.TxtCount.Multiline = true;
            this.TxtCount.Name = "TxtCount";
            this.TxtCount.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtCount.Size = new System.Drawing.Size(176, 288);
            this.TxtCount.TabIndex = 9;
            this.TxtCount.WordWrap = false;
            // 
            // TxtStartCode
            // 
            this.TxtStartCode.Location = new System.Drawing.Point(386, 309);
            this.TxtStartCode.Name = "TxtStartCode";
            this.TxtStartCode.Size = new System.Drawing.Size(56, 21);
            this.TxtStartCode.TabIndex = 10;
            this.TxtStartCode.Text = "160";
            // 
            // LstFiles
            // 
            this.LstFiles.FormattingEnabled = true;
            this.LstFiles.ItemHeight = 12;
            this.LstFiles.Location = new System.Drawing.Point(1, 2);
            this.LstFiles.Name = "LstFiles";
            this.LstFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstFiles.Size = new System.Drawing.Size(275, 268);
            this.LstFiles.TabIndex = 12;
            // 
            // BtnSetOutputFolder
            // 
            this.BtnSetOutputFolder.Location = new System.Drawing.Point(583, 271);
            this.BtnSetOutputFolder.Name = "BtnSetOutputFolder";
            this.BtnSetOutputFolder.Size = new System.Drawing.Size(123, 21);
            this.BtnSetOutputFolder.TabIndex = 13;
            this.BtnSetOutputFolder.Text = "Set Output Folder";
            this.BtnSetOutputFolder.UseVisualStyleBackColor = true;
            this.BtnSetOutputFolder.Click += new System.EventHandler(this.BtnSetOutputFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "Output Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "StartFontCode";
            // 
            // ChkUseOldCode
            // 
            this.ChkUseOldCode.AutoSize = true;
            this.ChkUseOldCode.Location = new System.Drawing.Point(274, 298);
            this.ChkUseOldCode.Name = "ChkUseOldCode";
            this.ChkUseOldCode.Size = new System.Drawing.Size(96, 16);
            this.ChkUseOldCode.TabIndex = 16;
            this.ChkUseOldCode.Text = "Use Old Code";
            this.ChkUseOldCode.UseVisualStyleBackColor = true;
            this.ChkUseOldCode.CheckedChanged += new System.EventHandler(this.ChkUseOldCode_CheckedChanged);
            // 
            // BtnMakeAllDict
            // 
            this.BtnMakeAllDict.Location = new System.Drawing.Point(183, 306);
            this.BtnMakeAllDict.Name = "BtnMakeAllDict";
            this.BtnMakeAllDict.Size = new System.Drawing.Size(80, 24);
            this.BtnMakeAllDict.TabIndex = 17;
            this.BtnMakeAllDict.Text = "ReMake Dict";
            this.BtnMakeAllDict.UseVisualStyleBackColor = true;
            this.BtnMakeAllDict.Click += new System.EventHandler(this.BtnMakeAllDict_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 629);
            this.Controls.Add(this.BtnMakeAllDict);
            this.Controls.Add(this.ChkUseOldCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnSetOutputFolder);
            this.Controls.Add(this.LstFiles);
            this.Controls.Add(this.TxtStartCode);
            this.Controls.Add(this.TxtCount);
            this.Controls.Add(this.BtnLoadSelectedFiles);
            this.Controls.Add(this.BtnMakeFile);
            this.Controls.Add(this.TxtPath);
            this.Controls.Add(this.TxtOutput);
            this.Controls.Add(this.BtnToChar);
            this.Controls.Add(this.TxtInput);
            this.Name = "FrmMain";
            this.Text = "Full Text To Char";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtInput;
        private System.Windows.Forms.Button BtnToChar;
        private System.Windows.Forms.TextBox TxtOutput;
        private System.Windows.Forms.TextBox TxtPath;
        private System.Windows.Forms.Button BtnMakeFile;
        private System.Windows.Forms.Button BtnLoadSelectedFiles;
        private System.Windows.Forms.TextBox TxtCount;
        private System.Windows.Forms.TextBox TxtStartCode;
        private System.Windows.Forms.ListBox LstFiles;
        private System.Windows.Forms.Button BtnSetOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ChkUseOldCode;
        private System.Windows.Forms.Button BtnMakeAllDict;
    }
}

