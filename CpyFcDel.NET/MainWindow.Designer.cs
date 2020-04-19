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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.lbProgramName = new System.Windows.Forms.Label();
            this.lbcSrcDir = new System.Windows.Forms.Label();
            this.btSetSrcDir = new System.Windows.Forms.Button();
            this.cbSrcDirs = new System.Windows.Forms.ComboBox();
            this.lbcTgtDir = new System.Windows.Forms.Label();
            this.cbTgtDirs = new System.Windows.Forms.ComboBox();
            this.btSetTgtDir = new System.Windows.Forms.Button();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.lbcLimitCount = new System.Windows.Forms.Label();
            this.tbLimitCount = new System.Windows.Forms.TextBox();
            this.ckbCountLimOn = new System.Windows.Forms.CheckBox();
            this.ckbRCacheOn = new System.Windows.Forms.CheckBox();
            this.ckbWCacheOn = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.lbcCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.gb_time = new System.Windows.Forms.GroupBox();
            this.lbPassedTime = new System.Windows.Forms.Label();
            this.lbCurTime = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.lbcPassedTime = new System.Windows.Forms.Label();
            this.lbcCurTime = new System.Windows.Forms.Label();
            this.lbcStartTime = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.gb_settings.SuspendLayout();
            this.gb_time.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(1337, 540);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(197, 114);
            this.checkedListBox1.TabIndex = 0;
            // 
            // lbProgramName
            // 
            this.lbProgramName.AutoSize = true;
            this.lbProgramName.Location = new System.Drawing.Point(326, 9);
            this.lbProgramName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbProgramName.Name = "lbProgramName";
            this.lbProgramName.Size = new System.Drawing.Size(166, 18);
            this.lbProgramName.TabIndex = 1;
            this.lbProgramName.Text = "CpyFcDel.NET V0.01";
            // 
            // lbcSrcDir
            // 
            this.lbcSrcDir.AutoSize = true;
            this.lbcSrcDir.Location = new System.Drawing.Point(20, 38);
            this.lbcSrcDir.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcSrcDir.Name = "lbcSrcDir";
            this.lbcSrcDir.Size = new System.Drawing.Size(37, 18);
            this.lbcSrcDir.TabIndex = 2;
            this.lbcSrcDir.Text = "text";
            // 
            // btSetSrcDir
            // 
            this.btSetSrcDir.Location = new System.Drawing.Point(670, 55);
            this.btSetSrcDir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btSetSrcDir.Name = "btSetSrcDir";
            this.btSetSrcDir.Size = new System.Drawing.Size(125, 34);
            this.btSetSrcDir.TabIndex = 4;
            this.btSetSrcDir.Text = "text";
            this.btSetSrcDir.UseVisualStyleBackColor = true;
            this.btSetSrcDir.Click += new System.EventHandler(this.BtSetSrcDir_Click);
            // 
            // cbSrcDirs
            // 
            this.cbSrcDirs.FormattingEnabled = true;
            this.cbSrcDirs.Location = new System.Drawing.Point(20, 60);
            this.cbSrcDirs.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cbSrcDirs.Name = "cbSrcDirs";
            this.cbSrcDirs.Size = new System.Drawing.Size(639, 26);
            this.cbSrcDirs.TabIndex = 5;
            this.cbSrcDirs.TextChanged += new System.EventHandler(this.Dirs_TextChanged);
            // 
            // lbcTgtDir
            // 
            this.lbcTgtDir.AutoSize = true;
            this.lbcTgtDir.Location = new System.Drawing.Point(20, 94);
            this.lbcTgtDir.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcTgtDir.Name = "lbcTgtDir";
            this.lbcTgtDir.Size = new System.Drawing.Size(37, 18);
            this.lbcTgtDir.TabIndex = 6;
            this.lbcTgtDir.Text = "text";
            // 
            // cbTgtDirs
            // 
            this.cbTgtDirs.FormattingEnabled = true;
            this.cbTgtDirs.Location = new System.Drawing.Point(20, 117);
            this.cbTgtDirs.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cbTgtDirs.Name = "cbTgtDirs";
            this.cbTgtDirs.Size = new System.Drawing.Size(639, 26);
            this.cbTgtDirs.TabIndex = 7;
            this.cbTgtDirs.TextChanged += new System.EventHandler(this.Dirs_TextChanged);
            // 
            // btSetTgtDir
            // 
            this.btSetTgtDir.Location = new System.Drawing.Point(670, 112);
            this.btSetTgtDir.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btSetTgtDir.Name = "btSetTgtDir";
            this.btSetTgtDir.Size = new System.Drawing.Size(125, 34);
            this.btSetTgtDir.TabIndex = 8;
            this.btSetTgtDir.Text = "text";
            this.btSetTgtDir.UseVisualStyleBackColor = true;
            this.btSetTgtDir.Click += new System.EventHandler(this.BtSetTgtDir_Click);
            // 
            // gb_settings
            // 
            this.gb_settings.Controls.Add(this.lbcLimitCount);
            this.gb_settings.Controls.Add(this.tbLimitCount);
            this.gb_settings.Controls.Add(this.ckbCountLimOn);
            this.gb_settings.Controls.Add(this.ckbRCacheOn);
            this.gb_settings.Controls.Add(this.ckbWCacheOn);
            this.gb_settings.Location = new System.Drawing.Point(20, 160);
            this.gb_settings.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gb_settings.Size = new System.Drawing.Size(299, 126);
            this.gb_settings.TabIndex = 9;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "text";
            // 
            // lbcLimitCount
            // 
            this.lbcLimitCount.AutoSize = true;
            this.lbcLimitCount.Location = new System.Drawing.Point(258, 103);
            this.lbcLimitCount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcLimitCount.Name = "lbcLimitCount";
            this.lbcLimitCount.Size = new System.Drawing.Size(14, 18);
            this.lbcLimitCount.TabIndex = 4;
            this.lbcLimitCount.Text = "t";
            // 
            // tbLimitCount
            // 
            this.tbLimitCount.Enabled = false;
            this.tbLimitCount.Location = new System.Drawing.Point(183, 93);
            this.tbLimitCount.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbLimitCount.Name = "tbLimitCount";
            this.tbLimitCount.Size = new System.Drawing.Size(55, 25);
            this.tbLimitCount.TabIndex = 3;
            // 
            // ckbCountLimOn
            // 
            this.ckbCountLimOn.AutoSize = true;
            this.ckbCountLimOn.Location = new System.Drawing.Point(10, 93);
            this.ckbCountLimOn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckbCountLimOn.Name = "ckbCountLimOn";
            this.ckbCountLimOn.Size = new System.Drawing.Size(158, 22);
            this.ckbCountLimOn.TabIndex = 2;
            this.ckbCountLimOn.Text = "ckbCountLimOn:";
            this.ckbCountLimOn.UseVisualStyleBackColor = true;
            this.ckbCountLimOn.CheckedChanged += new System.EventHandler(this.CkbCountLimOn_CheckedChanged);
            // 
            // ckbRCacheOn
            // 
            this.ckbRCacheOn.AutoSize = true;
            this.ckbRCacheOn.Checked = true;
            this.ckbRCacheOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRCacheOn.Location = new System.Drawing.Point(10, 60);
            this.ckbRCacheOn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckbRCacheOn.Name = "ckbRCacheOn";
            this.ckbRCacheOn.Size = new System.Drawing.Size(141, 22);
            this.ckbRCacheOn.TabIndex = 1;
            this.ckbRCacheOn.Text = "ckbRCacheOn";
            this.ckbRCacheOn.UseVisualStyleBackColor = true;
            // 
            // ckbWCacheOn
            // 
            this.ckbWCacheOn.AutoSize = true;
            this.ckbWCacheOn.Checked = true;
            this.ckbWCacheOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbWCacheOn.Location = new System.Drawing.Point(10, 27);
            this.ckbWCacheOn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckbWCacheOn.Name = "ckbWCacheOn";
            this.ckbWCacheOn.Size = new System.Drawing.Size(143, 22);
            this.ckbWCacheOn.TabIndex = 0;
            this.ckbWCacheOn.Text = "ckbWCacheOn";
            this.ckbWCacheOn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(3, 305);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(810, 3);
            this.label2.TabIndex = 10;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(0, 312);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(70, 18);
            this.lbStatus.TabIndex = 11;
            this.lbStatus.Text = "lbStatus";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(722, 312);
            this.lbCount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(66, 18);
            this.lbCount.TabIndex = 12;
            this.lbCount.Text = "lbCount";
            // 
            // lbcCount
            // 
            this.lbcCount.AutoSize = true;
            this.lbcCount.Location = new System.Drawing.Point(660, 312);
            this.lbcCount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcCount.Name = "lbcCount";
            this.lbcCount.Size = new System.Drawing.Size(41, 18);
            this.lbcCount.TabIndex = 13;
            this.lbcCount.Text = "text:";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(646, 308);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(3, 45);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            // 
            // btStart
            // 
            this.btStart.Enabled = false;
            this.btStart.Location = new System.Drawing.Point(670, 169);
            this.btStart.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(125, 117);
            this.btStart.TabIndex = 15;
            this.btStart.Text = "text";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // gb_time
            // 
            this.gb_time.Controls.Add(this.lbPassedTime);
            this.gb_time.Controls.Add(this.lbCurTime);
            this.gb_time.Controls.Add(this.lbStartTime);
            this.gb_time.Controls.Add(this.lbcPassedTime);
            this.gb_time.Controls.Add(this.lbcCurTime);
            this.gb_time.Controls.Add(this.lbcStartTime);
            this.gb_time.Location = new System.Drawing.Point(329, 160);
            this.gb_time.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gb_time.Name = "gb_time";
            this.gb_time.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gb_time.Size = new System.Drawing.Size(330, 126);
            this.gb_time.TabIndex = 16;
            this.gb_time.TabStop = false;
            this.gb_time.Text = "text";
            // 
            // lbPassedTime
            // 
            this.lbPassedTime.AutoSize = true;
            this.lbPassedTime.Location = new System.Drawing.Point(160, 93);
            this.lbPassedTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbPassedTime.Name = "lbPassedTime";
            this.lbPassedTime.Size = new System.Drawing.Size(112, 18);
            this.lbPassedTime.TabIndex = 5;
            this.lbPassedTime.Text = "lbPassedTime";
            // 
            // lbCurTime
            // 
            this.lbCurTime.AutoSize = true;
            this.lbCurTime.Location = new System.Drawing.Point(160, 62);
            this.lbCurTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbCurTime.Name = "lbCurTime";
            this.lbCurTime.Size = new System.Drawing.Size(85, 18);
            this.lbCurTime.TabIndex = 4;
            this.lbCurTime.Text = "lbCurTime";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Location = new System.Drawing.Point(160, 28);
            this.lbStartTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(96, 18);
            this.lbStartTime.TabIndex = 3;
            this.lbStartTime.Text = "lbStartTime";
            // 
            // lbcPassedTime
            // 
            this.lbcPassedTime.AutoSize = true;
            this.lbcPassedTime.Location = new System.Drawing.Point(13, 93);
            this.lbcPassedTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcPassedTime.Name = "lbcPassedTime";
            this.lbcPassedTime.Size = new System.Drawing.Size(41, 18);
            this.lbcPassedTime.TabIndex = 2;
            this.lbcPassedTime.Text = "text:";
            // 
            // lbcCurTime
            // 
            this.lbcCurTime.AutoSize = true;
            this.lbcCurTime.Location = new System.Drawing.Point(13, 62);
            this.lbcCurTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcCurTime.Name = "lbcCurTime";
            this.lbcCurTime.Size = new System.Drawing.Size(41, 18);
            this.lbcCurTime.TabIndex = 1;
            this.lbcCurTime.Text = "text:";
            // 
            // lbcStartTime
            // 
            this.lbcStartTime.AutoSize = true;
            this.lbcStartTime.Location = new System.Drawing.Point(13, 28);
            this.lbcStartTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcStartTime.Name = "lbcStartTime";
            this.lbcStartTime.Size = new System.Drawing.Size(41, 18);
            this.lbcStartTime.TabIndex = 0;
            this.lbcStartTime.Text = "text:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 336);
            this.Controls.Add(this.gb_time);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbcCount);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gb_settings);
            this.Controls.Add(this.btSetTgtDir);
            this.Controls.Add(this.cbTgtDirs);
            this.Controls.Add(this.lbcTgtDir);
            this.Controls.Add(this.cbSrcDirs);
            this.Controls.Add(this.btSetSrcDir);
            this.Controls.Add(this.lbcSrcDir);
            this.Controls.Add(this.lbProgramName);
            this.Controls.Add(this.checkedListBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainWindow";
            this.Text = "CpyFcDel.NET";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.gb_settings.ResumeLayout(false);
            this.gb_settings.PerformLayout();
            this.gb_time.ResumeLayout(false);
            this.gb_time.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label lbProgramName;
        private System.Windows.Forms.Label lbcSrcDir;
        private System.Windows.Forms.Button btSetSrcDir;
        private System.Windows.Forms.ComboBox cbSrcDirs;
        private System.Windows.Forms.Label lbcTgtDir;
        private System.Windows.Forms.ComboBox cbTgtDirs;
        private System.Windows.Forms.Button btSetTgtDir;
        private System.Windows.Forms.GroupBox gb_settings;
        private System.Windows.Forms.CheckBox ckbCountLimOn;
        private System.Windows.Forms.CheckBox ckbRCacheOn;
        private System.Windows.Forms.CheckBox ckbWCacheOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label lbcCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.GroupBox gb_time;
        private System.Windows.Forms.Label lbcPassedTime;
        private System.Windows.Forms.Label lbcCurTime;
        private System.Windows.Forms.Label lbcStartTime;
        private System.Windows.Forms.Label lbPassedTime;
        private System.Windows.Forms.Label lbCurTime;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.TextBox tbLimitCount;
        private System.Windows.Forms.Label lbcLimitCount;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

