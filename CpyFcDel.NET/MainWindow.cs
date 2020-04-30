﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CpyFcDel.NET
{
    public partial class MainWindow : Form
    {
        private const string timeFormat = "yyyy-MM-dd HH:mm";

        private readonly ResourceManager res_man;

        private readonly ElapsedTimer elapsedTimer;

        private readonly System.Timers.Timer timer;

        private Thread workingThread;

        public MainWindow()
        {
            InitializeComponent();

            //initialize folderDialog
            folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false
            };
            // load languge resource
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            res_man = new ResourceManager(name + ".lang_" + lang, Assembly.GetExecutingAssembly());

            //language localization for each control
            lbcSrcDir.Text = res_man.GetString("source_dir");
            lbcTgtDir.Text = res_man.GetString("target_dir");
            btSetSrcDir.Text = res_man.GetString("browse...");
            btSetTgtDir.Text = res_man.GetString("browse...");
            gbCacheSettings.Text = res_man.GetString("cache_settings");
            gbOtherSettings.Text = res_man.GetString("other_settings");
            gbTimeInfo.Text = res_man.GetString("time_info");
            gbStatusInfo.Text = res_man.GetString("status_info");
            btStart.Text = res_man.GetString("start");
            lbcStartTime.Text = res_man.GetString("start_time:");
            lbcPassedTime.Text = res_man.GetString("time_duration:");
            lbcLimitCount.Text = res_man.GetString("count");
            lbcCount.Text = res_man.GetString("counter:");
            ckbCountLimOn.Text = res_man.GetString("count_limit_on");
            ckbRCacheOn.Text = res_man.GetString("read_cache_on");
            ckbWCacheOn.Text = res_man.GetString("write_cache_on");
            ckbAutoExit.Text = res_man.GetString("auto_exit");
            //set default value
            foreach (var ctl in new Control[] { lbStartTime, lbPassedTime, lbStatus, lbCount, tbStatusInfo})
            {
                ctl.Text = "";
            }
            //set timder for deleting app info only for one time
            timer = new System.Timers.Timer
            {
                Interval = 2000,
                AutoReset = false
            };
            timer.Elapsed += (s, e) => this.Invoke(new Action(() => lbStatus.Text = ""));
            //set timer for updating test time
            elapsedTimer = new ElapsedTimer
            {
                Interval = 1000
            };
            elapsedTimer.Tick += (s, e) => lbPassedTime.Text = TimeSpan.FromSeconds(elapsedTimer.ElapsedSeconds).ToString();

        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            //resize controls for language localization
            tbLimitCount.Location = new Point(ckbCountLimOn.Location.X + ckbCountLimOn.Size.Width + 3, tbLimitCount.Location.Y);
            lbcLimitCount.Location = new Point(tbLimitCount.Location.X + tbLimitCount.Size.Width + 3, lbcLimitCount.Location.Y);
            //left align
            var newX = new Control[] { lbcStartTime, lbcPassedTime }.Max(x => x.Location.X + x.Size.Width);
            lbStartTime.Location = new Point(newX + 3, lbStartTime.Location.Y);
            lbPassedTime.Location = new Point(newX + 3, lbPassedTime.Location.Y);
            //little fix for location of control when it is in engllish
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                lbcLimitCount.Location = new Point(lbcLimitCount.Location.X, ckbCountLimOn.Location.Y);
        }

        private void BtSetSrcDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                cbSrcDirs.Items.Add(folderBrowserDialog.SelectedPath);
                cbSrcDirs.SelectedItem = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtSetTgtDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                cbTgtDirs.Items.Add(folderBrowserDialog.SelectedPath);
                cbTgtDirs.SelectedItem = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtStart_Click(object sender, EventArgs e)
        {
            // Validate
            if (!Directory.Exists(cbTgtDirs.Text))
            {
                MessageBox.Show(res_man.GetString("error_info_1"), res_man.GetString("error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTgtDirs.Text = "";
                return;
            }
            if (!Directory.Exists(cbSrcDirs.Text)) 
            {
                MessageBox.Show(res_man.GetString("error_info_1"), res_man.GetString("error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbSrcDirs.Text = "";
                return;
            }
            int limitCount = -1;
            if (ckbCountLimOn.Checked)
            {
                try
                {
                    limitCount = int.Parse(tbLimitCount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(res_man.GetString("error_info_2"), res_man.GetString("error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbLimitCount.Text = "";
                    return;
                }
                if (limitCount <= 0)
                {
                    MessageBox.Show(res_man.GetString("error_info_3"), res_man.GetString("error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbLimitCount.Text = "";
                    return;
                }
            }

            if (workingThread?.IsAlive ?? false)
            {
                workingThread.Abort();
                UpdateControls(false);
                
            }
            else
            {
                //set thread parameters
                var tuple = Tuple.Create(cbSrcDirs.Text, cbTgtDirs.Text, ckbRCacheOn.Checked, ckbWCacheOn.Checked, ckbCountLimOn.Checked, limitCount, ckbAutoExit.Checked);
                //initialize working thread
                workingThread = new Thread(new ParameterizedThreadStart(DoWork));
                //set background property 
                workingThread.IsBackground = true;
                workingThread.Start(tuple);
                UpdateControls(true);
                
            }
        }

        private void Dirs_TextChanged(object sender, EventArgs e)
        {
            if (cbSrcDirs.Text == ""　|| cbTgtDirs.Text == "")
            {
                btStart.Enabled = false;
            }
            else
            {
                btStart.Enabled = true;
            }
        }

        // switch controls state 
        // corresponding to working thread
        private void UpdateControls(bool isThreadStart)
        {
            var ctrls = new Control[] { btSetSrcDir, btSetTgtDir, cbSrcDirs, cbTgtDirs };
            if (isThreadStart)
            {
                foreach (var ctl in ctrls)
                {
                    ctl.Enabled = false;
                }
                btStart.Text = res_man.GetString("stop");
                lbStartTime.Text = DateTime.Now.ToString(timeFormat);
                lbPassedTime.Text = TimeSpan.FromSeconds(0).ToString();
                elapsedTimer.Start();
            }
            
            else
            {
                foreach (var ctl in ctrls)
                {
                    ctl.Enabled = Enabled;
                }
                btStart.Text = res_man.GetString("start");
                elapsedTimer.Stop();
            }
            
        }

        private void CkbCountLimOn_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCountLimOn.Checked)
            {
                tbLimitCount.Enabled = true;
                ckbAutoExit.Enabled = true;
            }
            else
            {
                tbLimitCount.Enabled = false;
                ckbAutoExit.Enabled = false;
            }
        }

        private void LbStatus_TextChanged(object sender, EventArgs e)
        {
            timer?.Start();
        }


        private void DoWork(object data)
        {
            var tuple = (Tuple<string, string, bool, bool, bool, int, bool>)data;
            var srcDir = tuple.Item1;
            var tgtDir = tuple.Item2;
            var isWCacheOn = tuple.Item3;
            var isRcacheOn = tuple.Item4;
            var isCountLimitOn = tuple.Item5;
            var count = tuple.Item6;
            var isAutoExit = tuple.Item7;

            var curCount = 1;
            var files = Directory.GetFiles(srcDir);
            while (true)
            {
                this.Invoke(new Action(() => lbCount.Text = curCount.ToString()));
                // TODO
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    // TODO

                    // copy

                    Thread.Sleep(100);
                    // update status
                    this.Invoke(new Action(() => tbStatusInfo.Text = res_man.GetString("copy:") + fileInfo.Name));

                    //compare
                    Thread.Sleep(100);
                    // update status
                    this.Invoke(new Action(() => tbStatusInfo.Text = res_man.GetString("compare:") + fileInfo.Name));
                    //delete
                    Thread.Sleep(100);
                    // update status
                    this.Invoke(new Action(() => tbStatusInfo.Text = res_man.GetString("delete:") + fileInfo.Name));
                }
                //update counter
                curCount++;
                if (!isCountLimitOn) continue;
                count--;
                if (count == 0) break;
            }
            this.Invoke(new Action(() => this.UpdateControls(false)));
            // comment
            // Invoke method is blocking. If the working thread is foreground,
            // following code fails (different to BeginInvoke() method)
            if (isAutoExit)
            {
                this.Invoke(new Action(() => lbStatus.Text = res_man.GetString("app_info_1")));
                Thread.Sleep(2000);
                this.Invoke(new Action(() => Application.Exit()));
            }

        }
    }
}
