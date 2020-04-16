using Microsoft.Win32;
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
using System.Text;
using System.Windows.Forms;

namespace CpyFcDel.NET
{
    public partial class MainWindow : Form
    {
        private const string timeFormat = "yyyy-MM-dd HH:mm";

        private readonly ResourceManager res_man;

        private bool isTestRunning = false;

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
            label_sd.Text = res_man.GetString("source_dir");
            label_td.Text = res_man.GetString("target_dir");
            bt_set_sd.Text = res_man.GetString("browse");
            bt_set_td.Text = res_man.GetString("browse");
            gb_settings.Text = res_man.GetString("settings");
            gb_time.Text = res_man.GetString("info");
            bt_start.Text = res_man.GetString("start");
            label_start_t.Text = res_man.GetString("start_time");
            label_cur_t.Text = res_man.GetString("current_time");
            label_t_dur.Text = res_man.GetString("time_duration");
            label_count_limit.Text = res_man.GetString("count");
            label_counter.Text = res_man.GetString("counter");

            //TODO
            status.Text = "正常に作動";

            //set initial currnt time 
            cur_time.Text = DateTime.Now.ToString(timeFormat);

            //set timer for updating current time 
            timer = new Timer
            {
                Interval = 5000
            };

            timer.Tick += SystemTime_Change;
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void SystemTime_Change(object sender, EventArgs e)
        {
            cur_time.Text = DateTime.Now.ToString(timeFormat);
        }

        private void Bt_set_sd_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                source_dirs.Items.Add(folderBrowserDialog.SelectedPath);
                source_dirs.SelectedItem = folderBrowserDialog.SelectedPath;
            }
        }

        private void Bt_set_td_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                target_dirs.Items.Add(folderBrowserDialog.SelectedPath);
                target_dirs.SelectedItem = folderBrowserDialog.SelectedPath;
            }
        }

        private void Bt_start_Click(object sender, EventArgs e)
        {
            if (!isTestRunning)
            {
                if (!Directory.Exists(target_dirs.Text) || !Directory.Exists(source_dirs.Text))
                {
                    MessageBox.Show(res_man.GetString("error_info_1"), res_man.GetString("error_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    target_dirs.Text = source_dirs.Text = "";
                }
                else
                {
                    start_time.Text = DateTime.Now.ToString(timeFormat);
                    ((Button)sender).Text = res_man.GetString("stop");
                    isTestRunning = true;
                }
            }
            else
            {
                ((Button)sender).Text = res_man.GetString("start");
            }
            //TODO
        }

        private void Dirs_TextChanged(object sender, EventArgs e)
        {
            if (source_dirs.Text == ""　|| target_dirs.Text == "")
            {
                bt_start.Enabled = false;
            }
            else
            {
                bt_start.Enabled = true;
            }
            
        }
    }
}
