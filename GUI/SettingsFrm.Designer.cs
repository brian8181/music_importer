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
            this.logDirectory = new Utility.GUI.DirBrowser();
            this.dirBrowser2 = new Utility.GUI.DirBrowser();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.btnShowLogs = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearReports = new System.Windows.Forms.Button();
            this.btnShowReports = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpLogging.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SHA1 hash Generation";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(149, 33);
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
            this.radioButton2.Location = new System.Drawing.Point(266, 33);
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
            this.radioButton1.Location = new System.Drawing.Point(44, 33);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(80, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Inserts Only";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // logDirectory
            // 
            this.logDirectory.Label = "Directory";
            this.logDirectory.Location = new System.Drawing.Point(6, 17);
            this.logDirectory.Name = "logDirectory";
            this.logDirectory.Size = new System.Drawing.Size(334, 34);
            this.logDirectory.TabIndex = 1;
            // 
            // dirBrowser2
            // 
            this.dirBrowser2.Label = "Directory";
            this.dirBrowser2.Location = new System.Drawing.Point(6, 19);
            this.dirBrowser2.Name = "dirBrowser2";
            this.dirBrowser2.Size = new System.Drawing.Size(334, 34);
            this.dirBrowser2.TabIndex = 2;
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.btnClearLogs);
            this.grpLogging.Controls.Add(this.btnShowLogs);
            this.grpLogging.Controls.Add(this.logDirectory);
            this.grpLogging.Location = new System.Drawing.Point(4, 91);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(346, 86);
            this.grpLogging.TabIndex = 3;
            this.grpLogging.TabStop = false;
            this.grpLogging.Text = "Logging";
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Location = new System.Drawing.Point(209, 57);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(86, 23);
            this.btnClearLogs.TabIndex = 3;
            this.btnClearLogs.Text = "Clear Logs";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // btnShowLogs
            // 
            this.btnShowLogs.Location = new System.Drawing.Point(64, 57);
            this.btnShowLogs.Name = "btnShowLogs";
            this.btnShowLogs.Size = new System.Drawing.Size(86, 23);
            this.btnShowLogs.TabIndex = 2;
            this.btnShowLogs.Text = "Show Logs";
            this.btnShowLogs.UseVisualStyleBackColor = true;
            this.btnShowLogs.Click += new System.EventHandler(this.btnShowLogs_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearReports);
            this.groupBox3.Controls.Add(this.btnShowReports);
            this.groupBox3.Controls.Add(this.dirBrowser2);
            this.groupBox3.Location = new System.Drawing.Point(4, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(346, 88);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reports";
            // 
            // btnClearReports
            // 
            this.btnClearReports.Location = new System.Drawing.Point(209, 59);
            this.btnClearReports.Name = "btnClearReports";
            this.btnClearReports.Size = new System.Drawing.Size(86, 23);
            this.btnClearReports.TabIndex = 5;
            this.btnClearReports.Text = "Clear Reports";
            this.btnClearReports.UseVisualStyleBackColor = true;
            this.btnClearReports.Click += new System.EventHandler(this.btnClearReports_Click);
            // 
            // btnShowReports
            // 
            this.btnShowReports.Location = new System.Drawing.Point(64, 59);
            this.btnShowReports.Name = "btnShowReports";
            this.btnShowReports.Size = new System.Drawing.Size(86, 23);
            this.btnShowReports.TabIndex = 4;
            this.btnShowReports.Text = "Show Reports";
            this.btnShowReports.UseVisualStyleBackColor = true;
            this.btnShowReports.Click += new System.EventHandler(this.btnShowReports_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(275, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(194, 279);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SettingsFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(353, 306);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
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
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private Utility.GUI.DirBrowser logDirectory;
        private Utility.GUI.DirBrowser dirBrowser2;
        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.Button btnShowLogs;
        private System.Windows.Forms.Button btnClearReports;
        private System.Windows.Forms.Button btnShowReports;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}