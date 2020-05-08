namespace CpyFcDel.NET
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.lbProgramName = new System.Windows.Forms.Label();
            this.lbcSrcDir = new System.Windows.Forms.Label();
            this.btSetSrcDir = new System.Windows.Forms.Button();
            this.cbSrcDirs = new System.Windows.Forms.ComboBox();
            this.lbcTgtDir = new System.Windows.Forms.Label();
            this.cbTgtDirs = new System.Windows.Forms.ComboBox();
            this.btSetTgtDir = new System.Windows.Forms.Button();
            this.gbCacheSettings = new System.Windows.Forms.GroupBox();
            this.ckbRCacheOn = new System.Windows.Forms.CheckBox();
            this.ckbWCacheOn = new System.Windows.Forms.CheckBox();
            this.lbcLimitCount = new System.Windows.Forms.Label();
            this.tbLimitCount = new System.Windows.Forms.TextBox();
            this.ckbCountLimOn = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.lbcCount = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.gbTimeInfo = new System.Windows.Forms.GroupBox();
            this.lbPassedTime = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.lbcPassedTime = new System.Windows.Forms.Label();
            this.lbcStartTime = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.gbOtherSettings = new System.Windows.Forms.GroupBox();
            this.ckbAutoExit = new System.Windows.Forms.CheckBox();
            this.gbStatusInfo = new System.Windows.Forms.GroupBox();
            this.tbStatusInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbCacheSettings.SuspendLayout();
            this.gbTimeInfo.SuspendLayout();
            this.gbOtherSettings.SuspendLayout();
            this.gbStatusInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProgramName
            // 
            this.lbProgramName.AutoSize = true;
            this.lbProgramName.Location = new System.Drawing.Point(162, 6);
            this.lbProgramName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProgramName.Name = "lbProgramName";
            this.lbProgramName.Size = new System.Drawing.Size(111, 12);
            this.lbProgramName.TabIndex = 1;
            this.lbProgramName.Text = "CpyFcDel.NET V0.01";
            // 
            // lbcSrcDir
            // 
            this.lbcSrcDir.AutoSize = true;
            this.lbcSrcDir.Location = new System.Drawing.Point(12, 25);
            this.lbcSrcDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbcSrcDir.Name = "lbcSrcDir";
            this.lbcSrcDir.Size = new System.Drawing.Size(25, 12);
            this.lbcSrcDir.TabIndex = 2;
            this.lbcSrcDir.Text = "text";
            // 
            // btSetSrcDir
            // 
            this.btSetSrcDir.Location = new System.Drawing.Point(401, 37);
            this.btSetSrcDir.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btSetSrcDir.Name = "btSetSrcDir";
            this.btSetSrcDir.Size = new System.Drawing.Size(74, 23);
            this.btSetSrcDir.TabIndex = 4;
            this.btSetSrcDir.Text = "text";
            this.btSetSrcDir.UseVisualStyleBackColor = true;
            this.btSetSrcDir.Click += new System.EventHandler(this.BtSetSrcDir_Click);
            // 
            // cbSrcDirs
            // 
            this.cbSrcDirs.FormattingEnabled = true;
            this.cbSrcDirs.Location = new System.Drawing.Point(12, 40);
            this.cbSrcDirs.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSrcDirs.Name = "cbSrcDirs";
            this.cbSrcDirs.Size = new System.Drawing.Size(386, 20);
            this.cbSrcDirs.TabIndex = 5;
            // 
            // lbcTgtDir
            // 
            this.lbcTgtDir.AutoSize = true;
            this.lbcTgtDir.Location = new System.Drawing.Point(12, 63);
            this.lbcTgtDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbcTgtDir.Name = "lbcTgtDir";
            this.lbcTgtDir.Size = new System.Drawing.Size(25, 12);
            this.lbcTgtDir.TabIndex = 6;
            this.lbcTgtDir.Text = "text";
            // 
            // cbTgtDirs
            // 
            this.cbTgtDirs.FormattingEnabled = true;
            this.cbTgtDirs.Location = new System.Drawing.Point(12, 77);
            this.cbTgtDirs.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbTgtDirs.Name = "cbTgtDirs";
            this.cbTgtDirs.Size = new System.Drawing.Size(386, 20);
            this.cbTgtDirs.TabIndex = 7;
            // 
            // btSetTgtDir
            // 
            this.btSetTgtDir.Location = new System.Drawing.Point(401, 74);
            this.btSetTgtDir.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btSetTgtDir.Name = "btSetTgtDir";
            this.btSetTgtDir.Size = new System.Drawing.Size(74, 23);
            this.btSetTgtDir.TabIndex = 8;
            this.btSetTgtDir.Text = "text";
            this.btSetTgtDir.UseVisualStyleBackColor = true;
            this.btSetTgtDir.Click += new System.EventHandler(this.BtSetTgtDir_Click);
            // 
            // gbCacheSettings
            // 
            this.gbCacheSettings.Controls.Add(this.ckbRCacheOn);
            this.gbCacheSettings.Controls.Add(this.ckbWCacheOn);
            this.gbCacheSettings.Location = new System.Drawing.Point(230, 103);
            this.gbCacheSettings.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbCacheSettings.Name = "gbCacheSettings";
            this.gbCacheSettings.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbCacheSettings.Size = new System.Drawing.Size(167, 62);
            this.gbCacheSettings.TabIndex = 9;
            this.gbCacheSettings.TabStop = false;
            this.gbCacheSettings.Text = "text";
            // 
            // ckbRCacheOn
            // 
            this.ckbRCacheOn.AutoSize = true;
            this.ckbRCacheOn.Checked = true;
            this.ckbRCacheOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRCacheOn.Location = new System.Drawing.Point(6, 42);
            this.ckbRCacheOn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbRCacheOn.Name = "ckbRCacheOn";
            this.ckbRCacheOn.Size = new System.Drawing.Size(96, 16);
            this.ckbRCacheOn.TabIndex = 1;
            this.ckbRCacheOn.Text = "ckbRCacheOn";
            this.ckbRCacheOn.UseVisualStyleBackColor = true;
            // 
            // ckbWCacheOn
            // 
            this.ckbWCacheOn.AutoSize = true;
            this.ckbWCacheOn.Checked = true;
            this.ckbWCacheOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbWCacheOn.Location = new System.Drawing.Point(6, 19);
            this.ckbWCacheOn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbWCacheOn.Name = "ckbWCacheOn";
            this.ckbWCacheOn.Size = new System.Drawing.Size(97, 16);
            this.ckbWCacheOn.TabIndex = 0;
            this.ckbWCacheOn.Text = "ckbWCacheOn";
            this.ckbWCacheOn.UseVisualStyleBackColor = true;
            // 
            // lbcLimitCount
            // 
            this.lbcLimitCount.AutoSize = true;
            this.lbcLimitCount.Location = new System.Drawing.Point(130, 23);
            this.lbcLimitCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbcLimitCount.Name = "lbcLimitCount";
            this.lbcLimitCount.Size = new System.Drawing.Size(9, 12);
            this.lbcLimitCount.TabIndex = 4;
            this.lbcLimitCount.Text = "t";
            // 
            // tbLimitCount
            // 
            this.tbLimitCount.Enabled = false;
            this.tbLimitCount.Location = new System.Drawing.Point(92, 19);
            this.tbLimitCount.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbLimitCount.Name = "tbLimitCount";
            this.tbLimitCount.Size = new System.Drawing.Size(35, 19);
            this.tbLimitCount.TabIndex = 3;
            // 
            // ckbCountLimOn
            // 
            this.ckbCountLimOn.AutoSize = true;
            this.ckbCountLimOn.Location = new System.Drawing.Point(8, 19);
            this.ckbCountLimOn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbCountLimOn.Name = "ckbCountLimOn";
            this.ckbCountLimOn.Size = new System.Drawing.Size(88, 16);
            this.ckbCountLimOn.TabIndex = 2;
            this.ckbCountLimOn.Text = "CountLimOn:";
            this.ckbCountLimOn.UseVisualStyleBackColor = true;
            this.ckbCountLimOn.CheckedChanged += new System.EventHandler(this.CkbCountLimOn_CheckedChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(0, 242);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 2);
            this.label2.TabIndex = 10;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbStatus.Location = new System.Drawing.Point(12, 248);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(47, 12);
            this.lbStatus.TabIndex = 11;
            this.lbStatus.Text = "lbStatus";
            // 
            // lbCount
            // 
            this.lbCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCount.Location = new System.Drawing.Point(370, 248);
            this.lbCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCount.Name = "lbCount";
            this.lbCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbCount.Size = new System.Drawing.Size(68, 12);
            this.lbCount.TabIndex = 12;
            this.lbCount.Text = "lbCount";
            this.lbCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbcCount
            // 
            this.lbcCount.AutoSize = true;
            this.lbcCount.Location = new System.Drawing.Point(447, 248);
            this.lbcCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbcCount.Name = "lbcCount";
            this.lbcCount.Size = new System.Drawing.Size(27, 12);
            this.lbcCount.TabIndex = 13;
            this.lbcCount.Text = "text:";
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(401, 106);
            this.btStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(74, 128);
            this.btStart.TabIndex = 15;
            this.btStart.Text = "text";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // gbTimeInfo
            // 
            this.gbTimeInfo.Controls.Add(this.lbPassedTime);
            this.gbTimeInfo.Controls.Add(this.lbStartTime);
            this.gbTimeInfo.Controls.Add(this.lbcPassedTime);
            this.gbTimeInfo.Controls.Add(this.lbcStartTime);
            this.gbTimeInfo.Location = new System.Drawing.Point(12, 167);
            this.gbTimeInfo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbTimeInfo.Name = "gbTimeInfo";
            this.gbTimeInfo.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbTimeInfo.Size = new System.Drawing.Size(213, 69);
            this.gbTimeInfo.TabIndex = 16;
            this.gbTimeInfo.TabStop = false;
            this.gbTimeInfo.Text = "text";
            // 
            // lbPassedTime
            // 
            this.lbPassedTime.AutoSize = true;
            this.lbPassedTime.Location = new System.Drawing.Point(50, 41);
            this.lbPassedTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPassedTime.Name = "lbPassedTime";
            this.lbPassedTime.Size = new System.Drawing.Size(76, 12);
            this.lbPassedTime.TabIndex = 5;
            this.lbPassedTime.Text = "lbPassedTime";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Location = new System.Drawing.Point(50, 19);
            this.lbStartTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(64, 12);
            this.lbStartTime.TabIndex = 3;
            this.lbStartTime.Text = "lbStartTime";
            // 
            // lbcPassedTime
            // 
            this.lbcPassedTime.AutoSize = true;
            this.lbcPassedTime.Location = new System.Drawing.Point(8, 41);
            this.lbcPassedTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbcPassedTime.Name = "lbcPassedTime";
            this.lbcPassedTime.Size = new System.Drawing.Size(27, 12);
            this.lbcPassedTime.TabIndex = 2;
            this.lbcPassedTime.Text = "text:";
            // 
            // lbcStartTime
            // 
            this.lbcStartTime.AutoSize = true;
            this.lbcStartTime.Location = new System.Drawing.Point(8, 19);
            this.lbcStartTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbcStartTime.Name = "lbcStartTime";
            this.lbcStartTime.Size = new System.Drawing.Size(27, 12);
            this.lbcStartTime.TabIndex = 0;
            this.lbcStartTime.Text = "text:";
            // 
            // gbOtherSettings
            // 
            this.gbOtherSettings.Controls.Add(this.ckbAutoExit);
            this.gbOtherSettings.Controls.Add(this.tbLimitCount);
            this.gbOtherSettings.Controls.Add(this.lbcLimitCount);
            this.gbOtherSettings.Controls.Add(this.ckbCountLimOn);
            this.gbOtherSettings.Location = new System.Drawing.Point(12, 103);
            this.gbOtherSettings.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbOtherSettings.Name = "gbOtherSettings";
            this.gbOtherSettings.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbOtherSettings.Size = new System.Drawing.Size(213, 62);
            this.gbOtherSettings.TabIndex = 17;
            this.gbOtherSettings.TabStop = false;
            this.gbOtherSettings.Text = "text";
            // 
            // ckbAutoExit
            // 
            this.ckbAutoExit.AutoSize = true;
            this.ckbAutoExit.Enabled = false;
            this.ckbAutoExit.Location = new System.Drawing.Point(8, 42);
            this.ckbAutoExit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbAutoExit.Name = "ckbAutoExit";
            this.ckbAutoExit.Size = new System.Drawing.Size(66, 16);
            this.ckbAutoExit.TabIndex = 5;
            this.ckbAutoExit.Text = "autoExit";
            this.ckbAutoExit.UseVisualStyleBackColor = true;
            // 
            // gbStatusInfo
            // 
            this.gbStatusInfo.Controls.Add(this.tbStatusInfo);
            this.gbStatusInfo.Location = new System.Drawing.Point(230, 167);
            this.gbStatusInfo.Margin = new System.Windows.Forms.Padding(2);
            this.gbStatusInfo.Name = "gbStatusInfo";
            this.gbStatusInfo.Padding = new System.Windows.Forms.Padding(2);
            this.gbStatusInfo.Size = new System.Drawing.Size(167, 69);
            this.gbStatusInfo.TabIndex = 18;
            this.gbStatusInfo.TabStop = false;
            this.gbStatusInfo.Text = "text";
            // 
            // tbStatusInfo
            // 
            this.tbStatusInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbStatusInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStatusInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbStatusInfo.Location = new System.Drawing.Point(4, 16);
            this.tbStatusInfo.Margin = new System.Windows.Forms.Padding(2);
            this.tbStatusInfo.Multiline = true;
            this.tbStatusInfo.Name = "tbStatusInfo";
            this.tbStatusInfo.ReadOnly = true;
            this.tbStatusInfo.Size = new System.Drawing.Size(148, 39);
            this.tbStatusInfo.TabIndex = 0;
            this.tbStatusInfo.TabStop = false;
            this.tbStatusInfo.Text = "info";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(368, 242);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 23);
            this.label1.TabIndex = 19;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(482, 264);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbStatusInfo);
            this.Controls.Add(this.gbOtherSettings);
            this.Controls.Add(this.gbTimeInfo);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.lbcCount);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbCacheSettings);
            this.Controls.Add(this.btSetTgtDir);
            this.Controls.Add(this.cbTgtDirs);
            this.Controls.Add(this.lbcTgtDir);
            this.Controls.Add(this.cbSrcDirs);
            this.Controls.Add(this.btSetSrcDir);
            this.Controls.Add(this.lbcSrcDir);
            this.Controls.Add(this.lbProgramName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "CpyFcDel.NET";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.gbCacheSettings.ResumeLayout(false);
            this.gbCacheSettings.PerformLayout();
            this.gbTimeInfo.ResumeLayout(false);
            this.gbTimeInfo.PerformLayout();
            this.gbOtherSettings.ResumeLayout(false);
            this.gbOtherSettings.PerformLayout();
            this.gbStatusInfo.ResumeLayout(false);
            this.gbStatusInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbProgramName;
        private System.Windows.Forms.Label lbcSrcDir;
        private System.Windows.Forms.Button btSetSrcDir;
        private System.Windows.Forms.ComboBox cbSrcDirs;
        private System.Windows.Forms.Label lbcTgtDir;
        private System.Windows.Forms.ComboBox cbTgtDirs;
        private System.Windows.Forms.Button btSetTgtDir;
        private System.Windows.Forms.GroupBox gbCacheSettings;
        private System.Windows.Forms.CheckBox ckbCountLimOn;
        private System.Windows.Forms.CheckBox ckbRCacheOn;
        private System.Windows.Forms.CheckBox ckbWCacheOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label lbcCount;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gbTimeInfo;
        private System.Windows.Forms.Label lbcPassedTime;
        private System.Windows.Forms.Label lbcStartTime;
        private System.Windows.Forms.Label lbPassedTime;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.TextBox tbLimitCount;
        private System.Windows.Forms.Label lbcLimitCount;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.GroupBox gbOtherSettings;
        private System.Windows.Forms.CheckBox ckbAutoExit;
        private System.Windows.Forms.GroupBox gbStatusInfo;
        private System.Windows.Forms.TextBox tbStatusInfo;
        private System.Windows.Forms.Label label1;
    }
}

