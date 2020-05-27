﻿using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using TM = CpyFcDel.NET.TranslationManager;

namespace CpyFcDel.NET
{
    public partial class MainWindow : Form
    {
        private const string timeFormat = "yyyy-MM-dd HH:mm";

        private readonly ElapsedTimer elapsedTimer;

        private readonly System.Timers.Timer updateStsTimer, exitTimer;

        private Thread workingThread;

        private Options options;

        public MainWindow()
        {
            InitializeComponent();

            // initialize folderDialog
            folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false
            };

            // language localization for each control
            lbcSrcDir.Text = TM.Translate("source_dir");
            lbcTgtDir.Text = TM.Translate("target_dir");
            btSetSrcDir.Text = TM.Translate("browse...");
            btSetTgtDir.Text = TM.Translate("browse...");
            gbCacheSettings.Text = TM.Translate("cache_settings");
            gbOtherSettings.Text = TM.Translate("other_settings");
            gbTimeInfo.Text = TM.Translate("time_info");
            gbStatusInfo.Text = TM.Translate("status_info");
            btStart.Text = TM.Translate("start");
            lbcStartTime.Text = TM.Translate("start_time:");
            lbcPassedTime.Text = TM.Translate("time_duration:");
            lbcLimitCount.Text = TM.Translate("count");
            lbcCount.Text = TM.Translate("counter");
            ckbCountLimOn.Text = TM.Translate("count_limit_on");
            ckbRCacheOn.Text = TM.Translate("read_cache_on");
            ckbWCacheOn.Text = TM.Translate("write_cache_on");
            ckbAutoExit.Text = TM.Translate("auto_exit");
            // set default value
            foreach (var ctl in new Control[] { lbStartTime, lbPassedTime, lbStatus, lbCount, tbStatusInfo })
            {
                ctl.Text = "";
            }

            // set timer for cleaning app status for one time
            updateStsTimer = new System.Timers.Timer
            {
                Interval = 2000,
                AutoReset = false
            };
            updateStsTimer.Elapsed += (s, e) => this.Invoke(new Action(() => lbStatus.Text = ""));
            // set timer for deleting app info only for one time
            exitTimer = new System.Timers.Timer
            {
                Interval = 2000,
                AutoReset = false
            };
            exitTimer.Elapsed += (s, e) => this.Invoke(new Action(() => Application.Exit()));
            // set timer for updating test time
            elapsedTimer = new ElapsedTimer
            {
                Interval = 1000
            };
            elapsedTimer.Tick += (s, e) => lbPassedTime.Text = TimeSpan.FromSeconds(elapsedTimer.ElapsedSeconds).ToString();

        }
        
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // resize controls for language localization
            tbLimitCount.Location = new Point(ckbCountLimOn.Location.X + ckbCountLimOn.Size.Width + 3, tbLimitCount.Location.Y);
            lbcLimitCount.Location = new Point(tbLimitCount.Location.X + tbLimitCount.Size.Width + 3, lbcLimitCount.Location.Y);

            // left align
            var newX = new Control[] { lbcStartTime, lbcPassedTime }.Max(x => x.Location.X + x.Size.Width);
            lbStartTime.Location = new Point(newX + 3, lbStartTime.Location.Y);
            lbPassedTime.Location = new Point(newX + 3, lbPassedTime.Location.Y);

            // little fix for location of control when it is in engllish
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                lbcLimitCount.Location = new Point(lbcLimitCount.Location.X, ckbCountLimOn.Location.Y);

            // load options from file if exists or default options
            options = Options.Load();

            // parse result
            bool isSucceed = false;
            // parse commandline
            try
            {
                isSucceed = options.Parse(Environment.GetCommandLineArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, TM.Translate("error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // set data bindings for combobox
            cbSrcDirs.DataSource = options.SourceDirs;
            cbSrcDirs.SelectedIndex = options.CurrentSrcDirIndex;
            cbTgtDirs.DataSource = options.TargetDirs;
            cbTgtDirs.SelectedIndex = options.CurrentTgtDirIndex;
            
            //fill controls from options
            ckbAutoExit.Checked = options.IsAutoExit;
            ckbRCacheOn.Checked = options.IsReadCacheOn;
            ckbWCacheOn.Checked = options.IsWriteCacheOn;
            if (options.LimitCount is int count)
            {
                ckbCountLimOn.Checked = true;
                tbLimitCount.Text = count.ToString();
            }
            if (isSucceed) btStart.PerformClick();
        }

        private void BtSetSrcDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                cbSrcDirs.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtSetTgtDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                cbTgtDirs.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtStart_Click(object sender, EventArgs e)
        {
            // validate
             if (!Directory.Exists(cbTgtDirs.Text))
            {
                MessageBox.Show(TM.Translate("error_info_1"), TM.Translate("error_title"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTgtDirs.Text = "";
                return;
            }
            if (!Directory.Exists(cbSrcDirs.Text))
            {
                MessageBox.Show(TM.Translate("error_info_1"), TM.Translate("error_title"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbSrcDirs.Text = "";
                return;
            }

            int? limitCount = null;
            if (ckbCountLimOn.Checked)
            {
                try
                {
                    limitCount = int.Parse(tbLimitCount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(TM.Translate("error_info_2"), TM.Translate("error_title"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbLimitCount.Text = "";
                    return;
                }
                if (limitCount <= 0)
                {
                    MessageBox.Show(TM.Translate("error_info_3"), TM.Translate("error_title"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbLimitCount.Text = "";
                    return;
                }
            }

            // update options
            cbSrcDirs.SelectedIndex = options.AddSourceDir(cbSrcDirs.Text);
            cbTgtDirs.SelectedIndex = options.AddTargetDir(cbTgtDirs.Text);

            options.IsAutoExit = ckbAutoExit.Checked;
            options.IsReadCacheOn = ckbRCacheOn.Checked;
            options.IsWriteCacheOn = ckbWCacheOn.Checked;
            options.LimitCount = limitCount; 

            if (workingThread?.IsAlive ?? false)
            {
                workingThread.Abort();
                UpdateControls(false);
            }
            else
            {
                // set parameters passing to thread
                // pay attention to value type or reference type
                var tuple = Tuple.Create(string.Copy(options.SourceDirs[options.CurrentSrcDirIndex]), 
                    string.Copy(options.TargetDirs[options.CurrentTgtDirIndex]), options.IsReadCacheOn, options.IsWriteCacheOn, 
                    options.LimitCount, options.IsAutoExit);

                // initialize working thread
                // set background property 
                workingThread = new Thread(new ParameterizedThreadStart(DoWork))
                {
                    IsBackground = true
                };
                workingThread.Start(tuple);
                UpdateControls(true);

            }
        }

        // switch controls state 
        // corresponding to working thread
        private void UpdateControls(bool isThreadStart)
        {
            var ctrls = new Control[] { btSetSrcDir, btSetTgtDir, cbSrcDirs, cbTgtDirs, gbOtherSettings, gbCacheSettings};
            if (isThreadStart)
            {
                foreach (var ctl in ctrls)
                {
                    ctl.Enabled = false;
                }
                btStart.Text = TM.Translate("stop");
                lbCount.ForeColor = Color.Red;
                lbPassedTime.ForeColor = Color.Red;
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
                btStart.Text = TM.Translate("start");
                lbStatus.Text = "";
                tbStatusInfo.Text = "";
                lbCount.ForeColor = Color.Black;
                lbPassedTime.ForeColor = Color.Black;
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
                tbLimitCount.Text = "";
                ckbAutoExit.Enabled = false;
                ckbAutoExit.Checked = false;
                
            }
        }

        private void DoWork(object data)
        {
            var tuple = (Tuple<string, string, bool, bool, int?, bool>)data;
            var srcDir = tuple.Item1;
            var tgtDir = tuple.Item2;
            var isWCacheOn = tuple.Item3;
            var isRcacheOn = tuple.Item4;
            var count = tuple.Item5;
            var isAutoExit = tuple.Item6;

            var curCount = 1;
            var files = Directory.GetFiles(srcDir);
            while (true)
            {
                this.Invoke(new Action(() => lbCount.Text = curCount.ToString()));
                foreach (var file in files)
                {
                    var srcFileInfo = new FileInfo(file);
                    var tgtFileInfo = new FileInfo(Path.Combine(tgtDir, srcFileInfo.Name));

                    try
                    {
                        #region COPY

                        // update status to copy
                        this.Invoke(new Action(() => tbStatusInfo.Text = TM.Translate("copy:") + srcFileInfo.Name));

                        // copy
                        using (var readStream = new FileStream(srcFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.
                            ReadWrite, 4096, isRcacheOn ? FileOptions.None : (FileOptions)0x20000000))
                        using (var writeStream = new FileStream(tgtFileInfo.FullName, FileMode.Create,
                            FileAccess.ReadWrite, FileShare.ReadWrite, 4096, isWCacheOn ? FileOptions.None : FileOptions.WriteThrough))
                        {

                            int c = -1;
                            long bytes = 0;
                            while ((c = readStream.ReadByte()) != -1)
                            {
                                writeStream.WriteByte((byte)c);
                                // show copy progress when file is greater than 200MB
                                if (++bytes % (1024 * 1024) == 0 && readStream.Length > 1024 * 1024 * 200)
                                {
                                    var length = readStream.Length;
                                    this.BeginInvoke(new Action(() => UpdateAppStatus("copied:",
                                        string.Format("{0:d}M / {1:d}M", bytes / 1024 / 1024, length / 1024 / 1024),
                                        false)));
                                }
                            }
                            // clean app status
                            this.Invoke(new Action(() => lbStatus.Text = ""));
                        }


                        #endregion

                        #region COMPARE

                        // update status to compare
                        this.Invoke(new Action(() => tbStatusInfo.Text = TM.Translate("compare:") + srcFileInfo.Name));
                        // compare
                        CompareMd5(srcFileInfo, tgtFileInfo);
                        #endregion

                        #region DELETE
                        // update status to delete
                        this.Invoke(new Action(() => tbStatusInfo.Text = TM.Translate("delete:") + srcFileInfo.Name));
                        // delete
                        tgtFileInfo.Delete();
                    }
                    catch (ThreadAbortException)
                    {
                        return;
                    }
                    catch (Exception e)
                    {
                        // raise the error messagebox at main thread to block the UI (Modal)
                        if ((bool)this.Invoke(new Func<bool>(() => AskWhetherContinue(e.Message))))
                        {
                            continue;
                        }
                        else
                        {
                            // prepare ending this thread
                            this.Invoke(new Action(() => this.UpdateControls(false)));
                            return;
                        }
                    }
                    #endregion
                }
                // update counter
                curCount++;
                // since count is nullable type following is necessary
                // if (count == null) continue;
                count--;
                if (count == 0) break;
            }
            // prepare ending this thread
            this.Invoke(new Action(() => this.UpdateControls(false)));

            // end main thread if necessary
            // COMMENT
            // Invoke method is blocking. If the working thread is foreground,
            // following code fails (different to BeginInvoke() method)
            if (isAutoExit)
            {
                this.Invoke(new Action(() => this.UpdateAppStatus("app_info_1", "")));
                this.BeginInvoke(new Action(() => this.exitTimer.Start()));
            }
        }

        private void CompareMd5(FileInfo file1, FileInfo file2)
        {
            using (var md5Hash = MD5.Create())
            using (var srcFileStream = file1.OpenRead())
            using (var tgtFileStream = file2.OpenRead())
            {
                var md5Hash1 = GetMd5Hash(md5Hash, srcFileStream);
                var md5Hash2 = GetMd5Hash(md5Hash, tgtFileStream);
                if (md5Hash1 != md5Hash2)
                {
                    throw new Exception(TM.Translate("error_info_4"));
                }
            }
        }

        private string GetMd5Hash(MD5 md5Hash, Stream input)
        {

            // convert the input string to a byte array and compute the hash
            input.Position = 0;
            byte[] data = md5Hash.ComputeHash(input);

            // create a new Stringbuilder to collect the bytes
            // and create a string
            StringBuilder sBuilder = new StringBuilder();

            // loop through each byte of the hashed data
            // and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // return the hexadecimal string.
            return sBuilder.ToString();
        }

        private bool AskWhetherContinue(string msg)
        {
            elapsedTimer.Stop();
            if (DialogResult.Yes == MessageBox.Show(msg + TM.Translate("if_continue?"),
                TM.Translate("error_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Error))
            {
                elapsedTimer.Resume();
                return true;
            }
            return false;
            
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            options.Save();
        }

        private void lbProgramName_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, TM.Translate("cl_usage"), TM.Translate("cl_title"));
        }

        private void UpdateAppStatus(string statusName, string info = "", bool isAutoDeleted = true)
        {
            lbStatus.Text = TM.Translate(statusName) + (info==string.Empty ? "": info);
            if (isAutoDeleted)
                updateStsTimer.Start();
        }
    }
}
