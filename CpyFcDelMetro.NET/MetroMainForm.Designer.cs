using System.Drawing;

namespace CpyFcDelMetro.NET
{
    partial class MetroMainForm
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
            this.tbSrcDir = new MetroFramework.Controls.MetroTextBox();
            this.tbTgtDir = new MetroFramework.Controls.MetroTextBox();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile3 = new MetroFramework.Controls.MetroTile();
            this.metroTile5 = new MetroFramework.Controls.MetroTile();
            this.metroTile6 = new MetroFramework.Controls.MetroTile();
            this.metroTile7 = new MetroFramework.Controls.MetroTile();
            this.metroTile8 = new MetroFramework.Controls.MetroTile();
            this.metroTile4 = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // tbSrcDir
            // 
            this.tbSrcDir.FontSize = MetroFramework.Drawing.MetroFontSize.Medium;
            this.tbSrcDir.Location = new System.Drawing.Point(23, 94);
            this.tbSrcDir.Name = "tbSrcDir";
            this.tbSrcDir.Size = new System.Drawing.Size(328, 28);
            this.tbSrcDir.TabIndex = 0;
            this.tbSrcDir.Text = "tbSrcDir";
            // 
            // tbTgtDir
            // 
            this.tbTgtDir.FontSize = MetroFramework.Drawing.MetroFontSize.Medium;
            this.tbTgtDir.Location = new System.Drawing.Point(23, 138);
            this.tbTgtDir.Name = "tbTgtDir";
            this.tbTgtDir.Size = new System.Drawing.Size(328, 28);
            this.tbTgtDir.TabIndex = 1;
            this.tbTgtDir.Text = "tbTgtDir";
            // 
            // metroTile1
            // 
            this.metroTile1.Location = new System.Drawing.Point(314, 181);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(76, 76);
            this.metroTile1.TabIndex = 2;
            this.metroTile1.Text = "start";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroTile2
            // 
            this.metroTile2.Location = new System.Drawing.Point(357, 87);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(32, 32);
            this.metroTile2.TabIndex = 3;
            // 
            // metroTile3
            // 
            this.metroTile3.Location = new System.Drawing.Point(357, 131);
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(32, 32);
            this.metroTile3.TabIndex = 4;
            // 
            // metroTile5
            // 
            this.metroTile5.Location = new System.Drawing.Point(93, 181);
            this.metroTile5.Name = "metroTile5";
            this.metroTile5.Size = new System.Drawing.Size(136, 23);
            this.metroTile5.TabIndex = 6;
            this.metroTile5.Text = "appstatus";
            // 
            // metroTile6
            // 
            this.metroTile6.Location = new System.Drawing.Point(23, 181);
            this.metroTile6.Name = "metroTile6";
            this.metroTile6.Size = new System.Drawing.Size(64, 23);
            this.metroTile6.TabIndex = 7;
            this.metroTile6.Text = "time1";
            // 
            // metroTile7
            // 
            this.metroTile7.Location = new System.Drawing.Point(23, 210);
            this.metroTile7.Name = "metroTile7";
            this.metroTile7.Size = new System.Drawing.Size(64, 23);
            this.metroTile7.TabIndex = 8;
            this.metroTile7.Text = "time1";
            // 
            // metroTile8
            // 
            this.metroTile8.Location = new System.Drawing.Point(93, 210);
            this.metroTile8.Name = "metroTile8";
            this.metroTile8.Size = new System.Drawing.Size(136, 23);
            this.metroTile8.TabIndex = 7;
            this.metroTile8.Text = "appstatus";
            // 
            // metroTile4
            // 
            this.metroTile4.Location = new System.Drawing.Point(23, 299);
            this.metroTile4.Name = "metroTile4";
            this.metroTile4.Size = new System.Drawing.Size(136, 23);
            this.metroTile4.TabIndex = 8;
            this.metroTile4.Text = "appstatus";
            // 
            // MetroMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 336);
            this.Controls.Add(this.metroTile4);
            this.Controls.Add(this.metroTile8);
            this.Controls.Add(this.metroTile7);
            this.Controls.Add(this.metroTile5);
            this.Controls.Add(this.metroTile6);
            this.Controls.Add(this.metroTile3);
            this.Controls.Add(this.metroTile2);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.tbTgtDir);
            this.Controls.Add(this.tbSrcDir);
            this.Name = "MetroMainForm";
            this.Text = "CpyFcDel.NET";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tbSrcDir;
        private MetroFramework.Controls.MetroTextBox tbTgtDir;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroTile metroTile3;
        private MetroFramework.Controls.MetroTile metroTile5;
        private MetroFramework.Controls.MetroTile metroTile6;
        private MetroFramework.Controls.MetroTile metroTile7;
        private MetroFramework.Controls.MetroTile metroTile8;
        private MetroFramework.Controls.MetroTile metroTile4;
    }
}