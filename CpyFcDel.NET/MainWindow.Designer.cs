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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_sd = new System.Windows.Forms.Label();
            this.bt_set_sd = new System.Windows.Forms.Button();
            this.source_dirs = new System.Windows.Forms.ComboBox();
            this.label_td = new System.Windows.Forms.Label();
            this.target_dirs = new System.Windows.Forms.ComboBox();
            this.bt_set_td = new System.Windows.Forms.Button();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.ckb_rcache = new System.Windows.Forms.CheckBox();
            this.ckb_wcache = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.count = new System.Windows.Forms.Label();
            this.label_counter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_start = new System.Windows.Forms.Button();
            this.gb_time = new System.Windows.Forms.GroupBox();
            this.test_time = new System.Windows.Forms.Label();
            this.cur_time = new System.Windows.Forms.Label();
            this.start_time = new System.Windows.Forms.Label();
            this.label_t_dur = new System.Windows.Forms.Label();
            this.label_cur_t = new System.Windows.Forms.Label();
            this.label_start_t = new System.Windows.Forms.Label();
            this.label_count_limit = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.gb_settings.SuspendLayout();
            this.gb_time.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(802, 360);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 88);
            this.checkedListBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "CpyFcDel.NET V0.01";
            // 
            // label_sd
            // 
            this.label_sd.AutoSize = true;
            this.label_sd.Location = new System.Drawing.Point(12, 25);
            this.label_sd.Name = "label_sd";
            this.label_sd.Size = new System.Drawing.Size(25, 12);
            this.label_sd.TabIndex = 2;
            this.label_sd.Text = "text";
            // 
            // bt_set_sd
            // 
            this.bt_set_sd.Location = new System.Drawing.Point(373, 40);
            this.bt_set_sd.Name = "bt_set_sd";
            this.bt_set_sd.Size = new System.Drawing.Size(75, 23);
            this.bt_set_sd.TabIndex = 4;
            this.bt_set_sd.Text = "text";
            this.bt_set_sd.UseVisualStyleBackColor = true;
            this.bt_set_sd.Click += new System.EventHandler(this.Bt_set_sd_Click);
            // 
            // source_dirs
            // 
            this.source_dirs.FormattingEnabled = true;
            this.source_dirs.Location = new System.Drawing.Point(12, 40);
            this.source_dirs.Name = "source_dirs";
            this.source_dirs.Size = new System.Drawing.Size(355, 20);
            this.source_dirs.TabIndex = 5;
            this.source_dirs.TextChanged += new System.EventHandler(this.Dirs_TextChanged);
            // 
            // label_td
            // 
            this.label_td.AutoSize = true;
            this.label_td.Location = new System.Drawing.Point(12, 63);
            this.label_td.Name = "label_td";
            this.label_td.Size = new System.Drawing.Size(25, 12);
            this.label_td.TabIndex = 6;
            this.label_td.Text = "text";
            // 
            // target_dirs
            // 
            this.target_dirs.FormattingEnabled = true;
            this.target_dirs.Location = new System.Drawing.Point(12, 78);
            this.target_dirs.Name = "target_dirs";
            this.target_dirs.Size = new System.Drawing.Size(355, 20);
            this.target_dirs.TabIndex = 7;
            this.target_dirs.TextChanged += new System.EventHandler(this.Dirs_TextChanged);
            // 
            // bt_set_td
            // 
            this.bt_set_td.Location = new System.Drawing.Point(373, 78);
            this.bt_set_td.Name = "bt_set_td";
            this.bt_set_td.Size = new System.Drawing.Size(75, 23);
            this.bt_set_td.TabIndex = 8;
            this.bt_set_td.Text = "text";
            this.bt_set_td.UseVisualStyleBackColor = true;
            this.bt_set_td.Click += new System.EventHandler(this.Bt_set_td_Click);
            // 
            // gb_settings
            // 
            this.gb_settings.Controls.Add(this.textBox1);
            this.gb_settings.Controls.Add(this.checkBox3);
            this.gb_settings.Controls.Add(this.ckb_rcache);
            this.gb_settings.Controls.Add(this.ckb_wcache);
            this.gb_settings.Location = new System.Drawing.Point(12, 107);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Size = new System.Drawing.Size(144, 84);
            this.gb_settings.TabIndex = 9;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "text";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(29, 19);
            this.textBox1.TabIndex = 3;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 62);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(78, 16);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "count_limit";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // ckb_rcache
            // 
            this.ckb_rcache.AutoSize = true;
            this.ckb_rcache.Location = new System.Drawing.Point(6, 40);
            this.ckb_rcache.Name = "ckb_rcache";
            this.ckb_rcache.Size = new System.Drawing.Size(74, 16);
            this.ckb_rcache.TabIndex = 1;
            this.ckb_rcache.Text = "read_ache";
            this.ckb_rcache.UseVisualStyleBackColor = true;
            // 
            // ckb_wcache
            // 
            this.ckb_wcache.AutoSize = true;
            this.ckb_wcache.Location = new System.Drawing.Point(6, 18);
            this.ckb_wcache.Name = "ckb_wcache";
            this.ckb_wcache.Size = new System.Drawing.Size(83, 16);
            this.ckb_wcache.TabIndex = 0;
            this.ckb_wcache.Text = "write_cache";
            this.ckb_wcache.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(-7, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(475, 2);
            this.label2.TabIndex = 10;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(0, 208);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(37, 12);
            this.status.TabIndex = 11;
            this.status.Text = "status";
            // 
            // count
            // 
            this.count.AutoSize = true;
            this.count.Location = new System.Drawing.Point(410, 208);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(33, 12);
            this.count.TabIndex = 12;
            this.count.Text = "count";
            // 
            // label_counter
            // 
            this.label_counter.AutoSize = true;
            this.label_counter.Location = new System.Drawing.Point(371, 208);
            this.label_counter.Name = "label_counter";
            this.label_counter.Size = new System.Drawing.Size(27, 12);
            this.label_counter.TabIndex = 13;
            this.label_counter.Text = "text:";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(363, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(2, 30);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            // 
            // bt_start
            // 
            this.bt_start.Enabled = false;
            this.bt_start.Location = new System.Drawing.Point(373, 117);
            this.bt_start.Name = "bt_start";
            this.bt_start.Size = new System.Drawing.Size(75, 68);
            this.bt_start.TabIndex = 15;
            this.bt_start.Text = "text";
            this.bt_start.UseVisualStyleBackColor = true;
            this.bt_start.Click += new System.EventHandler(this.Bt_start_Click);
            // 
            // gb_time
            // 
            this.gb_time.Controls.Add(this.test_time);
            this.gb_time.Controls.Add(this.cur_time);
            this.gb_time.Controls.Add(this.start_time);
            this.gb_time.Controls.Add(this.label_t_dur);
            this.gb_time.Controls.Add(this.label_cur_t);
            this.gb_time.Controls.Add(this.label_start_t);
            this.gb_time.Location = new System.Drawing.Point(162, 107);
            this.gb_time.Name = "gb_time";
            this.gb_time.Size = new System.Drawing.Size(201, 84);
            this.gb_time.TabIndex = 16;
            this.gb_time.TabStop = false;
            this.gb_time.Text = "text";
            // 
            // test_time
            // 
            this.test_time.AutoSize = true;
            this.test_time.Location = new System.Drawing.Point(64, 62);
            this.test_time.Name = "test_time";
            this.test_time.Size = new System.Drawing.Size(51, 12);
            this.test_time.TabIndex = 5;
            this.test_time.Text = "test_time";
            // 
            // cur_time
            // 
            this.cur_time.AutoSize = true;
            this.cur_time.Location = new System.Drawing.Point(64, 41);
            this.cur_time.Name = "cur_time";
            this.cur_time.Size = new System.Drawing.Size(47, 12);
            this.cur_time.TabIndex = 4;
            this.cur_time.Text = "cur_time";
            // 
            // start_time
            // 
            this.start_time.AutoSize = true;
            this.start_time.Location = new System.Drawing.Point(64, 19);
            this.start_time.Name = "start_time";
            this.start_time.Size = new System.Drawing.Size(55, 12);
            this.start_time.TabIndex = 3;
            this.start_time.Text = "start_time";
            // 
            // label_t_dur
            // 
            this.label_t_dur.AutoSize = true;
            this.label_t_dur.Location = new System.Drawing.Point(8, 62);
            this.label_t_dur.Name = "label_t_dur";
            this.label_t_dur.Size = new System.Drawing.Size(25, 12);
            this.label_t_dur.TabIndex = 2;
            this.label_t_dur.Text = "text";
            // 
            // label_cur_t
            // 
            this.label_cur_t.AutoSize = true;
            this.label_cur_t.Location = new System.Drawing.Point(8, 41);
            this.label_cur_t.Name = "label_cur_t";
            this.label_cur_t.Size = new System.Drawing.Size(25, 12);
            this.label_cur_t.TabIndex = 1;
            this.label_cur_t.Text = "text";
            // 
            // label_start_t
            // 
            this.label_start_t.AutoSize = true;
            this.label_start_t.Location = new System.Drawing.Point(8, 19);
            this.label_start_t.Name = "label_start_t";
            this.label_start_t.Size = new System.Drawing.Size(25, 12);
            this.label_start_t.TabIndex = 0;
            this.label_start_t.Text = "text";
            // 
            // label_count_limit
            // 
            this.label_count_limit.AutoSize = true;
            this.label_count_limit.Location = new System.Drawing.Point(137, 173);
            this.label_count_limit.Name = "label_count_limit";
            this.label_count_limit.Size = new System.Drawing.Size(9, 12);
            this.label_count_limit.TabIndex = 4;
            this.label_count_limit.Text = "t";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 224);
            this.Controls.Add(this.label_count_limit);
            this.Controls.Add(this.gb_time);
            this.Controls.Add(this.bt_start);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_counter);
            this.Controls.Add(this.count);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gb_settings);
            this.Controls.Add(this.bt_set_td);
            this.Controls.Add(this.target_dirs);
            this.Controls.Add(this.label_td);
            this.Controls.Add(this.source_dirs);
            this.Controls.Add(this.bt_set_sd);
            this.Controls.Add(this.label_sd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_sd;
        private System.Windows.Forms.Button bt_set_sd;
        private System.Windows.Forms.ComboBox source_dirs;
        private System.Windows.Forms.Label label_td;
        private System.Windows.Forms.ComboBox target_dirs;
        private System.Windows.Forms.Button bt_set_td;
        private System.Windows.Forms.GroupBox gb_settings;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox ckb_rcache;
        private System.Windows.Forms.CheckBox ckb_wcache;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label count;
        private System.Windows.Forms.Label label_counter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_start;
        private System.Windows.Forms.GroupBox gb_time;
        private System.Windows.Forms.Label label_t_dur;
        private System.Windows.Forms.Label label_cur_t;
        private System.Windows.Forms.Label label_start_t;
        private System.Windows.Forms.Label test_time;
        private System.Windows.Forms.Label cur_time;
        private System.Windows.Forms.Label start_time;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label_count_limit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Timer timer;
    }
}

