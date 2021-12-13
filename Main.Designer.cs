namespace AdventOfCode2021
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.cmbDaySelector = new System.Windows.Forms.ComboBox();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblResult1 = new System.Windows.Forms.Label();
            this.tbResult1 = new System.Windows.Forms.TextBox();
            this.tbResult2 = new System.Windows.Forms.TextBox();
            this.lblResult2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbDaySelector
            // 
            this.cmbDaySelector.FormattingEnabled = true;
            this.cmbDaySelector.Location = new System.Drawing.Point(12, 12);
            this.cmbDaySelector.Name = "cmbDaySelector";
            this.cmbDaySelector.Size = new System.Drawing.Size(149, 21);
            this.cmbDaySelector.TabIndex = 0;
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(167, 12);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(611, 368);
            this.lbLog.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 39);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(149, 23);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "RUN";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblResult1
            // 
            this.lblResult1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult1.Location = new System.Drawing.Point(167, 402);
            this.lblResult1.Name = "lblResult1";
            this.lblResult1.Size = new System.Drawing.Size(100, 23);
            this.lblResult1.TabIndex = 3;
            this.lblResult1.Text = "Result1:";
            // 
            // tbResult1
            // 
            this.tbResult1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbResult1.Location = new System.Drawing.Point(219, 399);
            this.tbResult1.Name = "tbResult1";
            this.tbResult1.ReadOnly = true;
            this.tbResult1.Size = new System.Drawing.Size(234, 20);
            this.tbResult1.TabIndex = 4;
            // 
            // tbResult2
            // 
            this.tbResult2.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResult2.Location = new System.Drawing.Point(534, 399);
            this.tbResult2.Name = "tbResult2";
            this.tbResult2.ReadOnly = true;
            this.tbResult2.Size = new System.Drawing.Size(244, 20);
            this.tbResult2.TabIndex = 6;
            // 
            // lblResult2
            // 
            this.lblResult2.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult2.Location = new System.Drawing.Point(482, 402);
            this.lblResult2.Name = "lblResult2";
            this.lblResult2.Size = new System.Drawing.Size(100, 23);
            this.lblResult2.TabIndex = 5;
            this.lblResult2.Text = "Result2:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 432);
            this.Controls.Add(this.tbResult2);
            this.Controls.Add(this.lblResult2);
            this.Controls.Add(this.tbResult1);
            this.Controls.Add(this.lblResult1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.cmbDaySelector);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnRun;

        private System.Windows.Forms.TextBox tbResult1;
        private System.Windows.Forms.Label lblResult1;
        private System.Windows.Forms.TextBox tbResult2;
        private System.Windows.Forms.Label lblResult2;

        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.ComboBox cmbDaySelector;
        private System.Windows.Forms.Button btn;

        private System.Windows.Forms.ListBox listBox1;

        private System.Windows.Forms.ComboBox comboBox1;

        #endregion
    }
}