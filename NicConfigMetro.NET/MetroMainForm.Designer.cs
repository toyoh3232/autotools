namespace NicConfigMetro.NET
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
            this.mtDet = new MetroFramework.Controls.MetroTile();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mtSet = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // mtDet
            // 
            this.mtDet.Location = new System.Drawing.Point(144, 75);
            this.mtDet.Name = "mtDet";
            this.mtDet.Size = new System.Drawing.Size(105, 33);
            this.mtDet.Style = "Blue";
            this.mtDet.TabIndex = 0;
            this.mtDet.Text = "Details";
            this.mtDet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 208);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(43, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Status";
            // 
            // mtSet
            // 
            this.mtSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtSet.Location = new System.Drawing.Point(24, 75);
            this.mtSet.Name = "mtSet";
            this.mtSet.Size = new System.Drawing.Size(105, 91);
            this.mtSet.Style = "Orange";
            this.mtSet.TabIndex = 2;
            this.mtSet.Text = "Set";
            this.mtSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtSet.Click += new System.EventHandler(this.mtSet_Click);
            // 
            // MetroMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 248);
            this.Controls.Add(this.mtSet);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mtDet);
            this.Name = "MetroMainForm";
            this.Resizable = false;
            this.Text = "IPSettings";
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTile mtDet;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTile mtSet;
    }
}