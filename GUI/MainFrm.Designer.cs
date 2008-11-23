// Music Importer imports ID3 tags to a MySql database.
// Copyright (C) 2008  Brian Preston

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace music_importer
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.cbOptimize = new System.Windows.Forms.CheckBox();
            this.cbTags = new System.Windows.Forms.CheckBox();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.cbClean = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bl1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSchema = new System.Windows.Forms.TextBox();
            this.txtSQLite = new System.Windows.Forms.TextBox();
            this.txtMySql = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbScanLocations = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowseRoot = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.lblMask = new System.Windows.Forms.Label();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtArtMask = new System.Windows.Forms.TextBox();
            this.btnBrowseArt = new System.Windows.Forms.Button();
            this.lbArtRoot = new System.Windows.Forms.Label();
            this.txtArtLoc = new System.Windows.Forms.TextBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.cbCreateDB = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbDirectory = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbDirectory_label = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.art_large = new System.Windows.Forms.NumericUpDown();
            this.art_small = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.art_xsmall = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbElapsedTime = new System.Windows.Forms.Label();
            this.tt_btnStart = new System.Windows.Forms.ToolTip(this.components);
            this.tt_create = new System.Windows.Forms.ToolTip(this.components);
            this.tt_mm_message = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lbFilesScanned_label = new System.Windows.Forms.Label();
            this.lbFilesScanned = new System.Windows.Forms.Label();
            this.linkReport = new System.Windows.Forms.LinkLabel();
            this.cbGenerateThumbs = new BKP.Online.GUI.ImageCheckBox();
            this.cbSH_Pass = new BKP.Online.GUI.ImageCheckBox();
            this.cbSH_User = new BKP.Online.GUI.ImageCheckBox();
            this.cbPlaylist = new BKP.Online.GUI.ImageCheckBox();
            this.cbMysql = new BKP.Online.GUI.ImageCheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.art_large)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.art_small)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.art_xsmall)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbOptimize
            // 
            this.cbOptimize.AutoSize = true;
            this.cbOptimize.Location = new System.Drawing.Point(12, 104);
            this.cbOptimize.Name = "cbOptimize";
            this.cbOptimize.Size = new System.Drawing.Size(101, 17);
            this.cbOptimize.TabIndex = 3;
            this.cbOptimize.Text = "Optimize Tables";
            this.toolTip.SetToolTip(this.cbOptimize, "Mysql Optimize Tables.");
            this.cbOptimize.UseVisualStyleBackColor = true;
            // 
            // cbTags
            // 
            this.cbTags.AutoSize = true;
            this.cbTags.Location = new System.Drawing.Point(12, 52);
            this.cbTags.Name = "cbTags";
            this.cbTags.Size = new System.Drawing.Size(88, 17);
            this.cbTags.TabIndex = 1;
            this.cbTags.Text = "Update Tags";
            this.toolTip.SetToolTip(this.cbTags, "Scans Updates / Inserts file tags.");
            this.cbTags.UseVisualStyleBackColor = true;
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Location = new System.Drawing.Point(12, 78);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(44, 17);
            this.cbLog.TabIndex = 2;
            this.cbLog.Text = "Log";
            this.toolTip.SetToolTip(this.cbLog, "Turn Logging On / Off.");
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // cbClean
            // 
            this.cbClean.AutoSize = true;
            this.cbClean.Location = new System.Drawing.Point(12, 132);
            this.cbClean.Name = "cbClean";
            this.cbClean.Size = new System.Drawing.Size(70, 17);
            this.cbClean.TabIndex = 5;
            this.cbClean.Text = "Clean Up";
            this.toolTip.SetToolTip(this.cbClean, "Clean database of orphaned entries.");
            this.cbClean.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.bl1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbSH_Pass);
            this.groupBox1.Controls.Add(this.cbSH_User);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbPlaylist);
            this.groupBox1.Controls.Add(this.txtSchema);
            this.groupBox1.Controls.Add(this.cbMysql);
            this.groupBox1.Controls.Add(this.txtSQLite);
            this.groupBox1.Controls.Add(this.txtMySql);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 195);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MySql Database";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Image = global::music_importer.Properties.Resources.mono_powered;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(328, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // bl1
            // 
            this.bl1.AutoSize = true;
            this.bl1.Location = new System.Drawing.Point(12, 133);
            this.bl1.Name = "bl1";
            this.bl1.Size = new System.Drawing.Size(39, 13);
            this.bl1.TabIndex = 1;
            this.bl1.Text = "MySql:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "SQLite:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Schema:";
            // 
            // txtSchema
            // 
            this.txtSchema.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSchema.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSchema.Location = new System.Drawing.Point(75, 76);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(139, 20);
            this.txtSchema.TabIndex = 2;
            // 
            // txtSQLite
            // 
            this.txtSQLite.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSQLite.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSQLite.Location = new System.Drawing.Point(75, 157);
            this.txtSQLite.Name = "txtSQLite";
            this.txtSQLite.Size = new System.Drawing.Size(346, 20);
            this.txtSQLite.TabIndex = 8;
            // 
            // txtMySql
            // 
            this.txtMySql.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtMySql.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtMySql.Enabled = false;
            this.txtMySql.Location = new System.Drawing.Point(75, 130);
            this.txtMySql.Name = "txtMySql";
            this.txtMySql.Size = new System.Drawing.Size(346, 20);
            this.txtMySql.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(424, 106);
            this.label6.MaximumSize = new System.Drawing.Size(479, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = ":";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(436, 103);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(40, 20);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "3306";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(75, 49);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(139, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // txtAddress
            // 
            this.txtAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtAddress.Location = new System.Drawing.Point(75, 103);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(346, 20);
            this.txtAddress.TabIndex = 4;
            this.txtAddress.Text = "localhost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Password:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(75, 22);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(139, 20);
            this.txtUser.TabIndex = 0;
            this.txtUser.Text = "root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "User:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(553, 156);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbScanLocations
            // 
            this.lbScanLocations.FormattingEnabled = true;
            this.lbScanLocations.HorizontalScrollbar = true;
            this.lbScanLocations.Location = new System.Drawing.Point(13, 55);
            this.lbScanLocations.Name = "lbScanLocations";
            this.lbScanLocations.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbScanLocations.Size = new System.Drawing.Size(615, 95);
            this.lbScanLocations.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 665);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(637, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 16;
            // 
            // lbMessage
            // 
            this.lbMessage.AutoEllipsis = true;
            this.lbMessage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage.Location = new System.Drawing.Point(74, 644);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(565, 18);
            this.lbMessage.TabIndex = 17;
            this.lbMessage.Text = "Ready.";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(568, 696);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Start";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBrowseRoot);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtRoot);
            this.groupBox2.Controls.Add(this.lblMask);
            this.groupBox2.Controls.Add(this.txtMask);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.lbScanLocations);
            this.groupBox2.Location = new System.Drawing.Point(2, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(644, 213);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scan Locations";
            // 
            // btnBrowseRoot
            // 
            this.btnBrowseRoot.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseRoot.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseRoot.Image")));
            this.btnBrowseRoot.Location = new System.Drawing.Point(601, 18);
            this.btnBrowseRoot.Name = "btnBrowseRoot";
            this.btnBrowseRoot.Size = new System.Drawing.Size(27, 23);
            this.btnBrowseRoot.TabIndex = 37;
            this.toolTip.SetToolTip(this.btnBrowseRoot, "Browse folders.");
            this.btnBrowseRoot.UseVisualStyleBackColor = true;
            this.btnBrowseRoot.Click += new System.EventHandler(this.btnBrowseRoot_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Music Root:";
            // 
            // txtRoot
            // 
            this.txtRoot.AutoCompleteCustomSource.AddRange(new string[] {
            "*.*",
            "*.mp3",
            "*.wma",
            "*.ogg;*.flac",
            "*.mp3;*.wma;*.ogg;*.flac"});
            this.txtRoot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtRoot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtRoot.Location = new System.Drawing.Point(80, 21);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(515, 20);
            this.txtRoot.TabIndex = 0;
            this.txtRoot.TextChanged += new System.EventHandler(this.txtRoot_TextChanged);
            // 
            // lblMask
            // 
            this.lblMask.AutoSize = true;
            this.lblMask.Location = new System.Drawing.Point(11, 188);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(55, 13);
            this.lblMask.TabIndex = 29;
            this.lblMask.Text = "File Mask:";
            // 
            // txtMask
            // 
            this.txtMask.AutoCompleteCustomSource.AddRange(new string[] {
            "*.*",
            "*.mp3",
            "*.wma",
            "*.ogg;*.flac",
            "*.mp3;*.wma;*.ogg;*.flac"});
            this.txtMask.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtMask.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtMask.Location = new System.Drawing.Point(80, 185);
            this.txtMask.Name = "txtMask";
            this.txtMask.Size = new System.Drawing.Size(548, 20);
            this.txtMask.TabIndex = 5;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(474, 156);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(394, 156);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Art Mask:";
            // 
            // txtArtMask
            // 
            this.txtArtMask.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtArtMask.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtArtMask.Location = new System.Drawing.Point(75, 46);
            this.txtArtMask.Name = "txtArtMask";
            this.txtArtMask.Size = new System.Drawing.Size(553, 20);
            this.txtArtMask.TabIndex = 2;
            // 
            // btnBrowseArt
            // 
            this.btnBrowseArt.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseArt.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseArt.Image")));
            this.btnBrowseArt.Location = new System.Drawing.Point(601, 69);
            this.btnBrowseArt.Name = "btnBrowseArt";
            this.btnBrowseArt.Size = new System.Drawing.Size(27, 23);
            this.btnBrowseArt.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnBrowseArt, "Browse folders.");
            this.btnBrowseArt.UseVisualStyleBackColor = true;
            this.btnBrowseArt.Click += new System.EventHandler(this.btnBrowseArt_Click);
            // 
            // lbArtRoot
            // 
            this.lbArtRoot.AutoSize = true;
            this.lbArtRoot.Location = new System.Drawing.Point(5, 74);
            this.lbArtRoot.Name = "lbArtRoot";
            this.lbArtRoot.Size = new System.Drawing.Size(67, 13);
            this.lbArtRoot.TabIndex = 26;
            this.lbArtRoot.Text = "Art Location:";
            // 
            // txtArtLoc
            // 
            this.txtArtLoc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtArtLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtArtLoc.Location = new System.Drawing.Point(75, 72);
            this.txtArtLoc.Name = "txtArtLoc";
            this.txtArtLoc.Size = new System.Drawing.Size(520, 20);
            this.txtArtLoc.TabIndex = 3;
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.cbCreateDB);
            this.grpOptions.Controls.Add(this.cbClean);
            this.grpOptions.Controls.Add(this.cbLog);
            this.grpOptions.Controls.Add(this.cbTags);
            this.grpOptions.Controls.Add(this.cbOptimize);
            this.grpOptions.Location = new System.Drawing.Point(492, 13);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(154, 194);
            this.grpOptions.TabIndex = 1;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Scan Options";
            // 
            // cbCreateDB
            // 
            this.cbCreateDB.AutoSize = true;
            this.cbCreateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCreateDB.ForeColor = System.Drawing.Color.Red;
            this.cbCreateDB.Location = new System.Drawing.Point(12, 26);
            this.cbCreateDB.Name = "cbCreateDB";
            this.cbCreateDB.Size = new System.Drawing.Size(108, 17);
            this.cbCreateDB.TabIndex = 0;
            this.cbCreateDB.Text = "(Re)Create DB";
            this.cbCreateDB.UseVisualStyleBackColor = true;
            this.cbCreateDB.CheckedChanged += new System.EventHandler(this.cbCreateDB_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(410, 696);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.toolTip.SetToolTip(this.btnCancel, "Cancle / Finished.");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbDirectory
            // 
            this.lbDirectory.AutoEllipsis = true;
            this.lbDirectory.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDirectory.Location = new System.Drawing.Point(74, 619);
            this.lbDirectory.Name = "lbDirectory";
            this.lbDirectory.Size = new System.Drawing.Size(565, 18);
            this.lbDirectory.TabIndex = 21;
            this.lbDirectory.Text = "None.";
            this.lbDirectory.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 644);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "Messages: ";
            // 
            // lbDirectory_label
            // 
            this.lbDirectory_label.AutoEllipsis = true;
            this.lbDirectory_label.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDirectory_label.Location = new System.Drawing.Point(3, 619);
            this.lbDirectory_label.Name = "lbDirectory_label";
            this.lbDirectory_label.Size = new System.Drawing.Size(62, 18);
            this.lbDirectory_label.TabIndex = 23;
            this.lbDirectory_label.Text = "Directory:";
            this.lbDirectory_label.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 595);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 25;
            this.label9.Text = "Status: ";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoEllipsis = true;
            this.lbStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(74, 595);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(565, 18);
            this.lbStatus.TabIndex = 24;
            this.lbStatus.Text = "Ready.";
            // 
            // cmbPriority
            // 
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(87, 696);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(121, 21);
            this.cmbPriority.TabIndex = 3;
            this.toolTip.SetToolTip(this.cmbPriority, "Set thread priority.");
            this.cmbPriority.SelectedIndexChanged += new System.EventHandler(this.cmbPriority_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 701);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Thread Priority:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Large:";
            // 
            // art_large
            // 
            this.art_large.Location = new System.Drawing.Point(75, 102);
            this.art_large.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.art_large.Name = "art_large";
            this.art_large.Size = new System.Drawing.Size(56, 20);
            this.art_large.TabIndex = 5;
            this.toolTip.SetToolTip(this.art_large, "Set thumbnail size.");
            this.art_large.Value = new decimal(new int[] {
            225,
            0,
            0,
            0});
            this.art_large.ValueChanged += new System.EventHandler(this.art_large_ValueChanged);
            // 
            // art_small
            // 
            this.art_small.Location = new System.Drawing.Point(249, 102);
            this.art_small.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.art_small.Name = "art_small";
            this.art_small.Size = new System.Drawing.Size(56, 20);
            this.art_small.TabIndex = 6;
            this.toolTip.SetToolTip(this.art_small, "Set thumbnail size.");
            this.art_small.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.art_small.ValueChanged += new System.EventHandler(this.art_small_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(208, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Small:";
            // 
            // art_xsmall
            // 
            this.art_xsmall.Location = new System.Drawing.Point(423, 102);
            this.art_xsmall.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.art_xsmall.Name = "art_xsmall";
            this.art_xsmall.Size = new System.Drawing.Size(56, 20);
            this.art_xsmall.TabIndex = 7;
            this.toolTip.SetToolTip(this.art_xsmall, "Set thumbnail size.");
            this.art_xsmall.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.art_xsmall.ValueChanged += new System.EventHandler(this.art_xsmall_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(372, 106);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "X-Small:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.cbGenerateThumbs);
            this.groupBox3.Controls.Add(this.art_xsmall);
            this.groupBox3.Controls.Add(this.txtArtMask);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtArtLoc);
            this.groupBox3.Controls.Add(this.art_small);
            this.groupBox3.Controls.Add(this.lbArtRoot);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.btnBrowseArt);
            this.groupBox3.Controls.Add(this.art_large);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(2, 431);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(644, 134);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Album Art";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 37;
            this.label18.Text = "Create:";
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(489, 696);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "&Pause";
            this.toolTip.SetToolTip(this.btnPause, "Pause current scan.");
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // label17
            // 
            this.label17.AutoEllipsis = true;
            this.label17.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 568);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 18);
            this.label17.TabIndex = 31;
            this.label17.Text = "Start Time: ";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoEllipsis = true;
            this.lbStartTime.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStartTime.Location = new System.Drawing.Point(74, 568);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(94, 18);
            this.lbStartTime.TabIndex = 30;
            this.lbStartTime.Text = "0:00:00";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(217, 568);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 18);
            this.label19.TabIndex = 33;
            this.label19.Text = "Elapsed: ";
            // 
            // lbElapsedTime
            // 
            this.lbElapsedTime.AutoEllipsis = true;
            this.lbElapsedTime.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbElapsedTime.Location = new System.Drawing.Point(279, 568);
            this.lbElapsedTime.Name = "lbElapsedTime";
            this.lbElapsedTime.Size = new System.Drawing.Size(94, 18);
            this.lbElapsedTime.TabIndex = 32;
            this.lbElapsedTime.Text = "0:00:00";
            // 
            // tt_btnStart
            // 
            this.tt_btnStart.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tt_btnStart.ToolTipTitle = "Start";
            // 
            // tt_create
            // 
            this.tt_create.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.tt_create.ToolTipTitle = "Create Database";
            // 
            // tt_mm_message
            // 
            this.tt_mm_message.IsBalloon = true;
            this.tt_mm_message.ShowAlways = true;
            this.tt_mm_message.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tt_mm_message.ToolTipTitle = "MediaMonkey";
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            this.toolTip.ShowAlways = true;
            // 
            // lbFilesScanned_label
            // 
            this.lbFilesScanned_label.AutoEllipsis = true;
            this.lbFilesScanned_label.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFilesScanned_label.Location = new System.Drawing.Point(422, 568);
            this.lbFilesScanned_label.Name = "lbFilesScanned_label";
            this.lbFilesScanned_label.Size = new System.Drawing.Size(91, 18);
            this.lbFilesScanned_label.TabIndex = 35;
            this.lbFilesScanned_label.Text = "Files Scanned: ";
            this.lbFilesScanned_label.Visible = false;
            // 
            // lbFilesScanned
            // 
            this.lbFilesScanned.AutoEllipsis = true;
            this.lbFilesScanned.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFilesScanned.Location = new System.Drawing.Point(512, 568);
            this.lbFilesScanned.Name = "lbFilesScanned";
            this.lbFilesScanned.Size = new System.Drawing.Size(94, 18);
            this.lbFilesScanned.TabIndex = 34;
            this.lbFilesScanned.Text = "0";
            this.lbFilesScanned.Visible = false;
            // 
            // linkReport
            // 
            this.linkReport.AutoSize = true;
            this.linkReport.Location = new System.Drawing.Point(422, 644);
            this.linkReport.Name = "linkReport";
            this.linkReport.Size = new System.Drawing.Size(65, 13);
            this.linkReport.TabIndex = 36;
            this.linkReport.TabStop = true;
            this.linkReport.Text = "View Report";
            this.linkReport.Visible = false;
            this.linkReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkReport_LinkClicked);
            // 
            // cbGenerateThumbs
            // 
            this.cbGenerateThumbs.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbGenerateThumbs.Location = new System.Drawing.Point(75, 17);
            this.cbGenerateThumbs.Name = "cbGenerateThumbs";
            this.cbGenerateThumbs.Size = new System.Drawing.Size(32, 26);
            this.cbGenerateThumbs.TabIndex = 1;
            this.toolTip.SetToolTip(this.cbGenerateThumbs, "Enable / disable thumbnail creation.");
            this.cbGenerateThumbs.UseVisualStyleBackColor = true;
            this.cbGenerateThumbs.CheckedChanged += new System.EventHandler(this.cbGenerateThumbs_CheckedChanged);
            // 
            // cbSH_Pass
            // 
            this.cbSH_Pass.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbSH_Pass.Checked = true;
            this.cbSH_Pass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSH_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSH_Pass.Image = ((System.Drawing.Image)(resources.GetObject("cbSH_Pass.Image")));
            this.cbSH_Pass.Location = new System.Drawing.Point(218, 46);
            this.cbSH_Pass.Name = "cbSH_Pass";
            this.cbSH_Pass.Size = new System.Drawing.Size(32, 26);
            this.cbSH_Pass.TabIndex = 10;
            this.toolTip.SetToolTip(this.cbSH_Pass, "Show / Hide user password.");
            this.cbSH_Pass.UseVisualStyleBackColor = true;
            this.cbSH_Pass.CheckedChanged += new System.EventHandler(this.cbSH_Pass_CheckedChanged);
            // 
            // cbSH_User
            // 
            this.cbSH_User.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbSH_User.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSH_User.Image = ((System.Drawing.Image)(resources.GetObject("cbSH_User.Image")));
            this.cbSH_User.Location = new System.Drawing.Point(218, 19);
            this.cbSH_User.Name = "cbSH_User";
            this.cbSH_User.Size = new System.Drawing.Size(32, 26);
            this.cbSH_User.TabIndex = 9;
            this.toolTip.SetToolTip(this.cbSH_User, "Show / Hide user name.");
            this.cbSH_User.UseVisualStyleBackColor = true;
            this.cbSH_User.CheckedChanged += new System.EventHandler(this.cbSH_User_CheckedChanged);
            // 
            // cbPlaylist
            // 
            this.cbPlaylist.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbPlaylist.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPlaylist.Image = ((System.Drawing.Image)(resources.GetObject("cbPlaylist.Image")));
            this.cbPlaylist.Location = new System.Drawing.Point(436, 154);
            this.cbPlaylist.Name = "cbPlaylist";
            this.cbPlaylist.Size = new System.Drawing.Size(32, 26);
            this.cbPlaylist.TabIndex = 12;
            this.toolTip.SetToolTip(this.cbPlaylist, "Enable / disable SQlite connection string.");
            this.cbPlaylist.UseVisualStyleBackColor = true;
            this.cbPlaylist.CheckedChanged += new System.EventHandler(this.cbPlaylist_CheckedChanged);
            // 
            // cbMysql
            // 
            this.cbMysql.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbMysql.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMysql.Image = ((System.Drawing.Image)(resources.GetObject("cbMysql.Image")));
            this.cbMysql.Location = new System.Drawing.Point(436, 127);
            this.cbMysql.Name = "cbMysql";
            this.cbMysql.Size = new System.Drawing.Size(32, 26);
            this.cbMysql.TabIndex = 11;
            this.toolTip.SetToolTip(this.cbMysql, "Enable / disable MySql connection string.");
            this.cbMysql.UseVisualStyleBackColor = true;
            this.cbMysql.CheckedChanged += new System.EventHandler(this.cbMysql_CheckedChanged);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 722);
            this.Controls.Add(this.linkReport);
            this.Controls.Add(this.lbFilesScanned_label);
            this.Controls.Add(this.lbFilesScanned);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lbElapsedTime);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lbStartTime);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbPriority);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.lbDirectory_label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbDirectory);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrm";
            this.RightToLeftLayout = true;
            this.Text = "Music Importer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.art_large)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.art_small)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.art_xsmall)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbOptimize;
        private System.Windows.Forms.CheckBox cbTags;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.CheckBox cbClean;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbScanLocations;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox cbCreateDB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lbDirectory;
        private System.Windows.Forms.TextBox txtMySql;
        private System.Windows.Forms.TextBox txtSQLite;
        private System.Windows.Forms.Button btnBrowseArt;
        private System.Windows.Forms.Label lbArtRoot;
        private System.Windows.Forms.TextBox txtArtLoc;
        private System.Windows.Forms.TextBox txtSchema;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label bl1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbDirectory_label;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMask;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtArtMask;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown art_xsmall;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown art_small;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown art_large;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbElapsedTime;
        private System.Windows.Forms.ToolTip tt_btnStart;
        private System.Windows.Forms.ToolTip tt_create;
        private System.Windows.Forms.ToolTip tt_mm_message;
        private System.Windows.Forms.Button btnBrowseRoot;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolTip toolTip;
        private BKP.Online.GUI.ImageCheckBox cbPlaylist;
        private BKP.Online.GUI.ImageCheckBox cbMysql;
        private BKP.Online.GUI.ImageCheckBox cbSH_User;
        private BKP.Online.GUI.ImageCheckBox cbSH_Pass;
        private BKP.Online.GUI.ImageCheckBox cbGenerateThumbs;
        private System.Windows.Forms.Label lbFilesScanned_label;
        private System.Windows.Forms.Label lbFilesScanned;
        private System.Windows.Forms.LinkLabel linkReport;
    }
}

