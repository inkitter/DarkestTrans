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
            this.TxtPathFile = new System.Windows.Forms.TextBox();
            this.BtnMakeFile = new System.Windows.Forms.Button();
            this.BtnLoadFile = new System.Windows.Forms.Button();
            this.BtnLoadOriginal = new System.Windows.Forms.Button();
            this.TxtCount = new System.Windows.Forms.TextBox();
            this.TxtStartCode = new System.Windows.Forms.TextBox();
            this.ChkSingleFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TxtInput
            // 
            this.TxtInput.Location = new System.Drawing.Point(3, 2);
            this.TxtInput.Multiline = true;
            this.TxtInput.Name = "TxtInput";
            this.TxtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtInput.Size = new System.Drawing.Size(428, 315);
            this.TxtInput.TabIndex = 0;
            this.TxtInput.WordWrap = false;
            // 
            // BtnToChar
            // 
            this.BtnToChar.Location = new System.Drawing.Point(587, 323);
            this.BtnToChar.Name = "BtnToChar";
            this.BtnToChar.Size = new System.Drawing.Size(123, 34);
            this.BtnToChar.TabIndex = 1;
            this.BtnToChar.Text = "To Char";
            this.BtnToChar.UseVisualStyleBackColor = true;
            this.BtnToChar.Click += new System.EventHandler(this.BtnToChar_Click);
            // 
            // TxtOutput
            // 
            this.TxtOutput.Location = new System.Drawing.Point(437, 2);
            this.TxtOutput.Multiline = true;
            this.TxtOutput.Name = "TxtOutput";
            this.TxtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtOutput.Size = new System.Drawing.Size(428, 315);
            this.TxtOutput.TabIndex = 2;
            // 
            // TxtPath
            // 
            this.TxtPath.Location = new System.Drawing.Point(12, 330);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.Size = new System.Drawing.Size(298, 21);
            this.TxtPath.TabIndex = 3;
            this.TxtPath.Text = "D:\\SteamLibrary\\steamapps\\common\\DarkestDungeon";
            // 
            // TxtPathFile
            // 
            this.TxtPathFile.Location = new System.Drawing.Point(12, 383);
            this.TxtPathFile.Name = "TxtPathFile";
            this.TxtPathFile.Size = new System.Drawing.Size(298, 21);
            this.TxtPathFile.TabIndex = 5;
            this.TxtPathFile.Text = "miscellaneous.string_table.xml";
            // 
            // BtnMakeFile
            // 
            this.BtnMakeFile.Location = new System.Drawing.Point(742, 323);
            this.BtnMakeFile.Name = "BtnMakeFile";
            this.BtnMakeFile.Size = new System.Drawing.Size(123, 34);
            this.BtnMakeFile.TabIndex = 6;
            this.BtnMakeFile.Text = "MakeFile";
            this.BtnMakeFile.UseVisualStyleBackColor = true;
            this.BtnMakeFile.Click += new System.EventHandler(this.BtnMakeFile_Click);
            // 
            // BtnLoadFile
            // 
            this.BtnLoadFile.Location = new System.Drawing.Point(316, 375);
            this.BtnLoadFile.Name = "BtnLoadFile";
            this.BtnLoadFile.Size = new System.Drawing.Size(123, 34);
            this.BtnLoadFile.TabIndex = 7;
            this.BtnLoadFile.Text = "Load File";
            this.BtnLoadFile.UseVisualStyleBackColor = true;
            this.BtnLoadFile.Click += new System.EventHandler(this.BtnLoadFile_Click);
            // 
            // BtnLoadOriginal
            // 
            this.BtnLoadOriginal.Location = new System.Drawing.Point(316, 323);
            this.BtnLoadOriginal.Name = "BtnLoadOriginal";
            this.BtnLoadOriginal.Size = new System.Drawing.Size(123, 34);
            this.BtnLoadOriginal.TabIndex = 8;
            this.BtnLoadOriginal.Text = "Load Original";
            this.BtnLoadOriginal.UseVisualStyleBackColor = true;
            this.BtnLoadOriginal.Click += new System.EventHandler(this.BtnLoadOriginal_Click);
            // 
            // TxtCount
            // 
            this.TxtCount.Location = new System.Drawing.Point(873, 2);
            this.TxtCount.Multiline = true;
            this.TxtCount.Name = "TxtCount";
            this.TxtCount.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtCount.Size = new System.Drawing.Size(156, 315);
            this.TxtCount.TabIndex = 9;
            this.TxtCount.WordWrap = false;
            // 
            // TxtStartCode
            // 
            this.TxtStartCode.Location = new System.Drawing.Point(493, 336);
            this.TxtStartCode.Name = "TxtStartCode";
            this.TxtStartCode.Size = new System.Drawing.Size(88, 21);
            this.TxtStartCode.TabIndex = 10;
            this.TxtStartCode.Text = "160";
            // 
            // ChkSingleFile
            // 
            this.ChkSingleFile.AutoSize = true;
            this.ChkSingleFile.Location = new System.Drawing.Point(445, 388);
            this.ChkSingleFile.Name = "ChkSingleFile";
            this.ChkSingleFile.Size = new System.Drawing.Size(90, 16);
            this.ChkSingleFile.TabIndex = 11;
            this.ChkSingleFile.Text = "Single File";
            this.ChkSingleFile.UseVisualStyleBackColor = true;
            this.ChkSingleFile.CheckedChanged += new System.EventHandler(this.ChkSingleFile_CheckedChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 416);
            this.Controls.Add(this.ChkSingleFile);
            this.Controls.Add(this.TxtStartCode);
            this.Controls.Add(this.TxtCount);
            this.Controls.Add(this.BtnLoadOriginal);
            this.Controls.Add(this.BtnLoadFile);
            this.Controls.Add(this.BtnMakeFile);
            this.Controls.Add(this.TxtPathFile);
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
        private System.Windows.Forms.TextBox TxtPathFile;
        private System.Windows.Forms.Button BtnMakeFile;
        private System.Windows.Forms.Button BtnLoadFile;
        private System.Windows.Forms.Button BtnLoadOriginal;
        private System.Windows.Forms.TextBox TxtCount;
        private System.Windows.Forms.TextBox TxtStartCode;
        private System.Windows.Forms.CheckBox ChkSingleFile;
    }
}

