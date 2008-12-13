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
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.logOptionsCtrl = new music_importer.LogOptionsCtrl();
            this.grp2 = new System.Windows.Forms.GroupBox();
            this.reportOptionsCtrl = new music_importer.LogOptionsCtrl();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbNever = new System.Windows.Forms.RadioButton();
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
            this.rbMediaInsert_Only = new System.Windows.Forms.RadioButton();
            this.rbMediaAlways = new System.Windows.Forms.RadioButton();
            this.rbMediaInsert_Or_Nulls = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSHA1_Never = new System.Windows.Forms.RadioButton();
            this.grpLogging.SuspendLayout();
            this.grp2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCircularSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.logOptionsCtrl);
            this.grpLogging.Location = new System.Drawing.Point(4, 218);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(446, 127);
            this.grpLogging.TabIndex = 3;
            this.grpLogging.TabStop = false;
            this.grpLogging.Text = "Logging";
            // 
            // logOptionsCtrl
            // 
            this.logOptionsCtrl.Location = new System.Drawing.Point(8, 19);
            this.logOptionsCtrl.Name = "logOptionsCtrl";
            this.logOptionsCtrl.Size = new System.Drawing.Size(425, 100);
            this.logOptionsCtrl.TabIndex = 0;
            // 
            // grp2
            // 
            this.grp2.Controls.Add(this.reportOptionsCtrl);
            this.grp2.Location = new System.Drawing.Point(4, 351);
            this.grp2.Name = "grp2";
            this.grp2.Size = new System.Drawing.Size(446, 127);
            this.grp2.TabIndex = 4;
            this.grp2.TabStop = false;
            this.grp2.Text = "Reports";
            // 
            // reportOptionsCtrl
            // 
            this.reportOptionsCtrl.Location = new System.Drawing.Point(8, 19);
            this.reportOptionsCtrl.Name = "reportOptionsCtrl";
            this.reportOptionsCtrl.Size = new System.Drawing.Size(425, 103);
            this.reportOptionsCtrl.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(375, 484);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(294, 484);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbNever);
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
            this.groupBox2.Size = new System.Drawing.Size(446, 147);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logger Settings";
            // 
            // rbNever
            // 
            this.rbNever.AutoSize = true;
            this.rbNever.Checked = true;
            this.rbNever.Location = new System.Drawing.Point(13, 26);
            this.rbNever.Name = "rbNever";
            this.rbNever.Size = new System.Drawing.Size(78, 17);
            this.rbNever.TabIndex = 17;
            this.rbNever.TabStop = true;
            this.rbNever.Text = "Do not split";
            this.rbNever.UseVisualStyleBackColor = true;
            this.rbNever.CheckedChanged += new System.EventHandler(this.rbNever_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(319, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "(s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "(s)";
            // 
            // cmbCircularSizeUnit
            // 
            this.cmbCircularSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCircularSizeUnit.FormattingEnabled = true;
            this.cmbCircularSizeUnit.Location = new System.Drawing.Point(246, 115);
            this.cmbCircularSizeUnit.Name = "cmbCircularSizeUnit";
            this.cmbCircularSizeUnit.Size = new System.Drawing.Size(67, 21);
            this.cmbCircularSizeUnit.TabIndex = 12;
            // 
            // cmbSplitSizeUnit
            // 
            this.cmbSplitSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSplitSizeUnit.FormattingEnabled = true;
            this.cmbSplitSizeUnit.Location = new System.Drawing.Point(246, 93);
            this.cmbSplitSizeUnit.Name = "cmbSplitSizeUnit";
            this.cmbSplitSizeUnit.Size = new System.Drawing.Size(67, 21);
            this.cmbSplitSizeUnit.TabIndex = 11;
            // 
            // upDownCircularSize
            // 
            this.upDownCircularSize.Location = new System.Drawing.Point(144, 116);
            this.upDownCircularSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.upDownCircularSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownCircularSize.Name = "upDownCircularSize";
            this.upDownCircularSize.Size = new System.Drawing.Size(77, 20);
            this.upDownCircularSize.TabIndex = 10;
            this.upDownCircularSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.upDownCircularSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbSplitTimeUnit
            // 
            this.cmbSplitTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSplitTimeUnit.FormattingEnabled = true;
            this.cmbSplitTimeUnit.Location = new System.Drawing.Point(246, 70);
            this.cmbSplitTimeUnit.Name = "cmbSplitTimeUnit";
            this.cmbSplitTimeUnit.Size = new System.Drawing.Size(67, 21);
            this.cmbSplitTimeUnit.TabIndex = 9;
            // 
            // upDownSplitTime
            // 
            this.upDownSplitTime.Location = new System.Drawing.Point(144, 70);
            this.upDownSplitTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.upDownSplitTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownSplitTime.Name = "upDownSplitTime";
            this.upDownSplitTime.Size = new System.Drawing.Size(77, 20);
            this.upDownSplitTime.TabIndex = 8;
            this.upDownSplitTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.upDownSplitTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbSplitRun
            // 
            this.rbSplitRun.AutoSize = true;
            this.rbSplitRun.Location = new System.Drawing.Point(13, 49);
            this.rbSplitRun.Name = "rbSplitRun";
            this.rbSplitRun.Size = new System.Drawing.Size(107, 17);
            this.rbSplitRun.TabIndex = 7;
            this.rbSplitRun.Text = "Split on every run";
            this.rbSplitRun.UseVisualStyleBackColor = true;
            this.rbSplitRun.CheckedChanged += new System.EventHandler(this.rbSplitRun_CheckedChanged);
            // 
            // upDownSplitSize
            // 
            this.upDownSplitSize.Location = new System.Drawing.Point(144, 93);
            this.upDownSplitSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.upDownSplitSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownSplitSize.Name = "upDownSplitSize";
            this.upDownSplitSize.Size = new System.Drawing.Size(77, 20);
            this.upDownSplitSize.TabIndex = 6;
            this.upDownSplitSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.upDownSplitSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbSplitTime
            // 
            this.rbSplitTime.AutoSize = true;
            this.rbSplitTime.Location = new System.Drawing.Point(13, 72);
            this.rbSplitTime.Name = "rbSplitTime";
            this.rbSplitTime.Size = new System.Drawing.Size(69, 17);
            this.rbSplitTime.TabIndex = 5;
            this.rbSplitTime.Text = "Split after";
            this.rbSplitTime.UseVisualStyleBackColor = true;
            this.rbSplitTime.CheckedChanged += new System.EventHandler(this.rbSplitTime_CheckedChanged);
            // 
            // rbCircular
            // 
            this.rbCircular.AutoSize = true;
            this.rbCircular.Location = new System.Drawing.Point(13, 118);
            this.rbCircular.Name = "rbCircular";
            this.rbCircular.Size = new System.Drawing.Size(90, 17);
            this.rbCircular.TabIndex = 4;
            this.rbCircular.Text = "Circle back at";
            this.rbCircular.UseVisualStyleBackColor = true;
            this.rbCircular.CheckedChanged += new System.EventHandler(this.rbCircular_CheckedChanged);
            // 
            // rbSplitSize
            // 
            this.rbSplitSize.AllowDrop = true;
            this.rbSplitSize.AutoSize = true;
            this.rbSplitSize.Location = new System.Drawing.Point(13, 95);
            this.rbSplitSize.Name = "rbSplitSize";
            this.rbSplitSize.Size = new System.Drawing.Size(57, 17);
            this.rbSplitSize.TabIndex = 3;
            this.rbSplitSize.Text = "Split at";
            this.rbSplitSize.UseVisualStyleBackColor = true;
            this.rbSplitSize.CheckedChanged += new System.EventHandler(this.rbSplitSize_CheckedChanged);
            // 
            // rbMediaInsert_Only
            // 
            this.rbMediaInsert_Only.AutoSize = true;
            this.rbMediaInsert_Only.Checked = true;
            this.rbMediaInsert_Only.Location = new System.Drawing.Point(211, 19);
            this.rbMediaInsert_Only.Name = "rbMediaInsert_Only";
            this.rbMediaInsert_Only.Size = new System.Drawing.Size(80, 17);
            this.rbMediaInsert_Only.TabIndex = 0;
            this.rbMediaInsert_Only.TabStop = true;
            this.rbMediaInsert_Only.Text = "Inserts Only";
            this.rbMediaInsert_Only.UseVisualStyleBackColor = true;
            // 
            // rbMediaAlways
            // 
            this.rbMediaAlways.AutoSize = true;
            this.rbMediaAlways.Location = new System.Drawing.Point(27, 19);
            this.rbMediaAlways.Name = "rbMediaAlways";
            this.rbMediaAlways.Size = new System.Drawing.Size(58, 17);
            this.rbMediaAlways.TabIndex = 1;
            this.rbMediaAlways.Text = "Always";
            this.rbMediaAlways.UseVisualStyleBackColor = true;
            // 
            // rbMediaInsert_Or_Nulls
            // 
            this.rbMediaInsert_Or_Nulls.AutoSize = true;
            this.rbMediaInsert_Or_Nulls.Location = new System.Drawing.Point(327, 19);
            this.rbMediaInsert_Or_Nulls.Name = "rbMediaInsert_Or_Nulls";
            this.rbMediaInsert_Or_Nulls.Size = new System.Drawing.Size(92, 17);
            this.rbMediaInsert_Or_Nulls.TabIndex = 2;
            this.rbMediaInsert_Or_Nulls.Text = "Insert or is null";
            this.rbMediaInsert_Or_Nulls.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSHA1_Never);
            this.groupBox1.Controls.Add(this.rbMediaInsert_Or_Nulls);
            this.groupBox1.Controls.Add(this.rbMediaAlways);
            this.groupBox1.Controls.Add(this.rbMediaInsert_Only);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SHA1 Generation for media only";
            // 
            // rbSHA1_Never
            // 
            this.rbSHA1_Never.AutoSize = true;
            this.rbSHA1_Never.Location = new System.Drawing.Point(121, 19);
            this.rbSHA1_Never.Name = "rbSHA1_Never";
            this.rbSHA1_Never.Size = new System.Drawing.Size(54, 17);
            this.rbSHA1_Never.TabIndex = 3;
            this.rbSHA1_Never.Text = "Never";
            this.rbSHA1_Never.UseVisualStyleBackColor = true;
            // 
            // SettingsFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(452, 513);
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
            this.grpLogging.ResumeLayout(false);
            this.grp2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCircularSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSplitSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.GroupBox grp2;
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
        private System.Windows.Forms.RadioButton rbNever;
        private System.Windows.Forms.RadioButton rbMediaInsert_Only;
        private System.Windows.Forms.RadioButton rbMediaAlways;
        private System.Windows.Forms.RadioButton rbMediaInsert_Or_Nulls;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSHA1_Never;
    }
}