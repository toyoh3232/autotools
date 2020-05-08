using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        private BindingList<string> ds = new BindingList<string>{"a","b","c"};

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = ds;
            comboBox2.DataSource = ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds.Add("d");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.Remove("a");
        }
    }
}
