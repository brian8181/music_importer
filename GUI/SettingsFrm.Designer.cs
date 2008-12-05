namespace music_importer
{
    partial class SettingsFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && ( components != null ))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.grp2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCircularSizeUnit = new System.Windows.Forms.ComboBox();
            this.cmbSplitSizeUnit = new System.Windows.Forms.ComboBox();
            this.upDownCircularSize = new System.Windows.Forms.NumericUpDown();
            this.cmbSplitTimeUnit = new System.Windows.Forms.ComboBox();
            this.upDownSplitTime = new System.Windows.Forms.NumericUpDown();
            this.rbSplitRun = new System.Windows.Forms.RadioButton();
            this.upDownSplitSize = new System.Windows.Forms.NumericUpDown();
            this.rbSplitTime = new System.Windows.Forms.RadioButton();
            this.rbCircular = new System.Windows.Forms.RadioButton();
            this.rbSplitSize = new System.Windows.Forms.RadioButton();
            this.reportOptionsCtrl = new music_importer.LogOptionsCtrl();
            this.logOptionsCtrl = new music_importer.LogOptionsCtrl();
            this.groupBox1.SuspendLayout();
            this.grpLogging.SuspendLayout();
            this.grp2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCircularSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitSize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SHA1 hash Generation";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(170, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(92, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Insert or is null";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(318, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(36, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "All";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(80, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Inserts Only";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.logOptionsCtrl);
            this.grpLogging.Location = new System.Drawing.Point(4, 199);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(446, 127);
            this.grpLogging.TabIndex = 3;
            this.grpLogging.TabStop = false;
            this.grpLogging.Text = "Logging";
            // 
            // grp2
            // 
            this.grp2.Controls.Add(this.reportOptionsCtrl);
            this.grp2.Location = new System.Drawing.Point(4, 332);
            this.grp2.Name = "grp2";
            this.grp2.Size = new System.Drawing.Size(446, 127);
            this.grp2.TabIndex = 4;
            this.grp2.TabStop = false;
            this.grp2.Text = "Reports";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(375, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(294, 465);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbCircularSizeUnit);
            this.groupBox2.Controls.Add(this.cmbSplitSizeUnit);
            this.groupBox2.Controls.Add(this.upDownCircularSize);
            this.groupBox2.Controls.Add(this.cmbSplitTimeUnit);
            this.groupBox2.Controls.Add(this.upDownSplitTime);
            this.groupBox2.Controls.Add(this.rbSplitRun);
            this.groupBox2.Controls.Add(this.upDownSplitSize);
            this.groupBox2.Controls.Add(this.rbSplitTime);
            this.groupBox2.Controls.Add(this.rbCircular);
            this.groupBox2.Controls.Add(this.rbSplitSize);
            this.groupBox2.Location = new System.Drawing.Point(4, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 128);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logger Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(384, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "(s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(384, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "(s)";
            // 
            // cmbCircularSizeUnit
            // 
            this.cmbCircularSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCircularSizeUnit.FormattingEnabled = true;
            this.cmbCircularSizeUnit.Items.AddRange(new object[] {
            "KB",
            "MB",
            "GB"});
            this.cmbCircularSizeUnit.Location = new System.Drawing.Point(296, 94);
            this.cmbCircularSizeUnit.Name = "cmbCircularSizeUnit";
            this.cmbCircularSizeUnit.Size = new System.Drawing.Size(82, 21);
            this.cmbCircularSizeUnit.TabIndex = 12;
            // 
            // cmbSplitSizeUnit
            // 
            this.cmbSplitSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSplitSizeUnit.FormattingEnabled = true;
            this.cmbSplitSizeUnit.Items.AddRange(new object[] {
            "KB",
            "MB",
            "GB"});
            this.cmbSplitSizeUnit.Location = new System.Drawing.Point(296, 72);
            this.cmbSplitSizeUnit.Name = "cmbSplitSizeUnit";
            this.cmbSplitSizeUnit.Size = new System.Drawing.Size(82, 21);
            this.cmbSplitSizeUnit.TabIndex = 11;
            // 
            // upDownCircularSize
            // 
            this.upDownCircularSize.Location = new System.Drawing.Point(170, 95);
            this.upDownCircularSize.Name = "upDownCircularSize";
            this.upDownCircularSize.Size = new System.Drawing.Size(120, 20);
            this.upDownCircularSize.TabIndex = 10;
            // 
            // cmbSplitTimeUnit
            // 
            this.cmbSplitTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSplitTimeUnit.FormattingEnabled = true;
            this.cmbSplitTimeUnit.Items.AddRange(new object[] {
            "Minute",
            "Hour",
            "Day",
            "Week",
            "Month",
            "Year"});
            this.cmbSplitTimeUnit.Location = new System.Drawing.Point(296, 49);
            this.cmbSplitTimeUnit.Name = "cmbSplitTimeUnit";
            this.cmbSplitTimeUnit.Size = new System.Drawing.Size(82, 21);
            this.cmbSplitTimeUnit.TabIndex = 9;
            // 
            // upDownSplitTime
            // 
            this.upDownSplitTime.Location = new System.Drawing.Point(170, 49);
            this.upDownSplitTime.Name = "upDownSplitTime";
            this.upDownSplitTime.Size = new System.Drawing.Size(120, 20);
            this.upDownSplitTime.TabIndex = 8;
            // 
            // rbSplitRun
            // 
            this.rbSplitRun.AutoSize = true;
            this.rbSplitRun.Location = new System.Drawing.Point(9, 26);
            this.rbSplitRun.Name = "rbSplitRun";
            this.rbSplitRun.Size = new System.Drawing.Size(141, 17);
            this.rbSplitRun.TabIndex = 7;
            this.rbSplitRun.TabStop = true;
            this.rbSplitRun.Text = "Split file before every run";
            this.rbSplitRun.UseVisualStyleBackColor = true;
            this.rbSplitRun.CheckedChanged += new System.EventHandler(this.rbSplitRun_CheckedChanged);
            // 
            // upDownSplitSize
            // 
            this.upDownSplitSize.Location = new System.Drawing.Point(170, 72);
            this.upDownSplitSize.Name = "upDownSplitSize";
            this.upDownSplitSize.Size = new System.Drawing.Size(120, 20);
            this.upDownSplitSize.TabIndex = 6;
            // 
            // rbSplitTime
            // 
            this.rbSplitTime.AutoSize = true;
            this.rbSplitTime.Location = new System.Drawing.Point(9, 49);
            this.rbSplitTime.Name = "rbSplitTime";
            this.rbSplitTime.Size = new System.Drawing.Size(85, 17);
            this.rbSplitTime.TabIndex = 5;
            this.rbSplitTime.TabStop = true;
            this.rbSplitTime.Text = "Split file after";
            this.rbSplitTime.UseVisualStyleBackColor = true;
            this.rbSplitTime.CheckedChanged += new System.EventHandler(this.rbSplitTime_CheckedChanged);
            // 
            // rbCircular
            // 
            this.rbCircular.AutoSize = true;
            this.rbCircular.Location = new System.Drawing.Point(9, 95);
            this.rbCircular.Name = "rbCircular";
            this.rbCircular.Size = new System.Drawing.Size(99, 17);
            this.rbCircular.TabIndex = 4;
            this.rbCircular.TabStop = true;
            this.rbCircular.Text = "Circular back at";
            this.rbCircular.UseVisualStyleBackColor = true;
            this.rbCircular.CheckedChanged += new System.EventHandler(this.rbCircular_CheckedChanged);
            // 
            // rbSplitSize
            // 
            this.rbSplitSize.AllowDrop = true;
            this.rbSplitSize.AutoSize = true;
            this.rbSplitSize.Location = new System.Drawing.Point(8, 72);
            this.rbSplitSize.Name = "rbSplitSize";
            this.rbSplitSize.Size = new System.Drawing.Size(73, 17);
            this.rbSplitSize.TabIndex = 3;
            this.rbSplitSize.TabStop = true;
            this.rbSplitSize.Text = "Split file at";
            this.rbSplitSize.UseVisualStyleBackColor = true;
            this.rbSplitSize.CheckedChanged += new System.EventHandler(this.rbSplitSize_CheckedChanged);
            // 
            // reportOptionsCtrl
            // 
            this.reportOptionsCtrl.Location = new System.Drawing.Point(8, 19);
            this.reportOptionsCtrl.Name = "reportOptionsCtrl";
            this.reportOptionsCtrl.Size = new System.Drawing.Size(425, 103);
            this.reportOptionsCtrl.TabIndex = 0;
            // 
            // logOptionsCtrl
            // 
            this.logOptionsCtrl.Location = new System.Drawing.Point(8, 19);
            this.logOptionsCtrl.Name = "logOptionsCtrl";
            this.logOptionsCtrl.Size = new System.Drawing.Size(425, 100);
            this.logOptionsCtrl.TabIndex = 0;
            // 
            // SettingsFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(452, 497);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grp2);
            this.Controls.Add(this.grpLogging);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsFrm";
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpLogging.ResumeLayout(false);
            this.grp2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCircularSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.GroupBox grp2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private LogOptionsCtrl reportOptionsCtrl;
        private LogOptionsCtrl logOptionsCtrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCircular;
        private System.Windows.Forms.RadioButton rbSplitSize;
        private System.Windows.Forms.RadioButton rbSplitRun;
        private System.Windows.Forms.NumericUpDown upDownSplitSize;
        private System.Windows.Forms.RadioButton rbSplitTime;
        private System.Windows.Forms.ComboBox cmbSplitSizeUnit;
        private System.Windows.Forms.NumericUpDown upDownCircularSize;
        private System.Windows.Forms.ComboBox cmbSplitTimeUnit;
        private System.Windows.Forms.NumericUpDown upDownSplitTime;
        private System.Windows.Forms.ComboBox cmbCircularSizeUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}