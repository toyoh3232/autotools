using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using TM = CpyFcDel.NET.Localization.TranslationManager;

namespace CpyFcDel.NET
{
    public partial class MetroMainForm : MetroFramework.Forms.MetroForm
    {
        public MetroMainForm()
        {
            InitializeComponent();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, TM.Translate("start"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.metroTextBox1.BackColor = this.EffectiveBackColor;
        }
    }
}
