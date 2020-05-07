using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CpyFcDel.NET
{
    public partial class MainWindow : Form
    {
        private const string timeFormat = "yyyy-MM-dd HH:mm";

        private readonly ResourceManager resManager;

        private readonly ElapsedTimer elapsedTimer;

        private readonly System.Timers.Timer updateStsTimer, exitTimer;

        private Thread workingThread;

        public MainWindow()
        {
            InitializeComponent();

            // initialize folderDialog
            folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false
            };
            // load languge resource
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            resManager = new ResourceManager(name + ".lang_" + lang, Assembly.GetExecutingAssembly());

            // language localization for each control
            lbcSrcDir.Text = resManager.GetString("source_dir");
            lbcTgtDir.Text = resManager.GetString("target_dir");
            btSetSrcDir.Text = resManager.GetString("browse...");
            btSetTgtDir.Text = resManager.GetString("browse...");
            gbCacheSettings.Text = resManager.GetString("cache_settings");
            gbOtherSettings.Text = resManager.GetString("other_settings");
            gbTimeInfo.Text = resManager.GetString("time_info");
            gbStatusInfo.Text = resManager.GetString("status_info");
            btStart.Text = resManager.GetString("start");
            lbcStartTime.Text = resManager.GetString("start_time:");
            lbcPassedTime.Text = resManager.GetString("time_duration:");
            lbcLimitCount.Text = resManager.GetString("count");
            lbcCount.Text = resManager.GetString("counter");
            ckbCountLimOn.Text = resManager.GetString("count_limit_on");
            ckbRCacheOn.Text = resManager.GetString("read_cache_on");
            ckbWCacheOn.Text = resManager.GetString("write_cache_on");
            ckbAutoExit.Text = resManager.GetString("auto_exit");
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
                MessageBox.Show(resManager.GetString("error_info_1"), resManager.GetString("error_title"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTgtDirs.Text = "";
                return;
            }
            if (!Directory.Exists(cbSrcDirs.Text))
            {
                MessageBox.Show(resManager.GetString("error_info_1"), resManager.GetString("error_title"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(resManager.GetString("error_info_2"), resManager.GetString("error_title"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbLimitCount.Text = "";
                    return;
                }
                if (limitCount <= 0)
                {
                    MessageBox.Show(resManager.GetString("error_info_3"), resManager.GetString("error_title"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // set thread parameters
                var tuple = Tuple.Create(cbSrcDirs.Text, cbTgtDirs.Text, ckbRCacheOn.Checked, ckbWCacheOn.Checked, 
                    ckbCountLimOn.Checked, limitCount, ckbAutoExit.Checked);
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

        private void Dirs_TextChanged(object sender, EventArgs e)
        {
            if (cbSrcDirs.Text == "" || cbTgtDirs.Text == "")
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
                btStart.Text = resManager.GetString("stop");
                // TODO
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
                btStart.Text = resManager.GetString("start");
                tbStatusInfo.Text = "";
                // TODO
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
                ckbAutoExit.Enabled = false;
            }
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
                foreach (var file in files)
                {
                    var srcFileInfo = new FileInfo(file);
                    var tgtFileInfo = new FileInfo(Path.Combine(tgtDir, srcFileInfo.Name));

                    #region COPY
                    // update status to copy
                    this.Invoke(new Action(() => tbStatusInfo.Text = resManager.GetString("copy:") + srcFileInfo.Name));
                    // copy
                    try
                    {
                        using (var readStream = new FileStream(srcFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.
                            ReadWrite, 4096, isRcacheOn ? FileOptions.None : (FileOptions)0x20000000))
                        using (var writeStream = new FileStream(tgtFileInfo.FullName, FileMode.Create,
                            FileAccess.ReadWrite, FileShare.ReadWrite, 4096, isWCacheOn ? FileOptions.None : FileOptions.WriteThrough ))
                        {

                            int c = -1;
                            long bytes = 0;
                            while ((c = readStream.ReadByte()) != -1)
                            {
                                writeStream.WriteByte((byte)c);
                                // show copy progress when file is greater than 200MB
                                if (++bytes % (1024 * 1024) == 0 && readStream.Length > 1024 * 1024 * 200)
                                {
                                    this.BeginInvoke(new Action(() => UpdateAppStatus("copied:",
                                        string.Format("{0:d}M / {1:d}M", bytes / 1024 / 1024, readStream.Length / 1024 / 1024),
                                        false)));
                                }
                            }
                            // clean app status
                            this.Invoke(new Action(() => lbStatus.Text = ""));
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        return;
                    }
                    catch (Exception e)
                    {
                        if (HandleorIgnoreException(e))
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

                    #region COMPARE
                    // update status to compare
                    this.Invoke(new Action(() => tbStatusInfo.Text = resManager.GetString("compare:") + srcFileInfo.Name));
                    // compare
                    try
                    {
                        CompareMd5(srcFileInfo, tgtFileInfo);
                    }
                    catch (ThreadAbortException)
                    {
                        return;
                    }
                    catch (Exception e)
                    {
                        if (HandleorIgnoreException(e))
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

                    #region DELETE
                    // update status to delete
                    this.Invoke(new Action(() => tbStatusInfo.Text = resManager.GetString("delete:") + srcFileInfo.Name));
                    // delete
                    try
                    {
                        tgtFileInfo.Delete();
                    }
                    catch (ThreadAbortException)
                    {
                        return;
                    }
                    catch (Exception e)
                    {
                        if (HandleorIgnoreException(e))
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
                //update counter
                curCount++;
                if (!isCountLimitOn) continue;
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
                    //TODO
                    throw new Exception();
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

        private bool HandleorIgnoreException(Exception e)
        {
            return DialogResult.Yes == MessageBox.Show(e.Message + "\n" +resManager.GetString("if_continue?"), 
                resManager.GetString("error_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        private void UpdateAppStatus(string statusName, string info = "", bool isAutoDeleted = true)
        {
            lbStatus.Text = resManager.GetString(statusName) + (info==string.Empty ? "": info);
            if (isAutoDeleted)
                updateStsTimer.Start();
        }
    }
}
