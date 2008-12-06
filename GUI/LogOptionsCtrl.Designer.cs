namespace music_importer
{
    partial class LogOptionsCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbTimeUnit = new System.Windows.Forms.ComboBox();
            this.cbUsePath = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.btnDeleteNow = new System.Windows.Forms.Button();
            this.cbDeleteAfter = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.upDownAfter = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAfter)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTimeUnit
            // 
            this.cmbTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeUnit.FormattingEnabled = true;
            this.cmbTimeUnit.Location = new System.Drawing.Point(167, 46);
            this.cmbTimeUnit.Name = "cmbTimeUnit";
            this.cmbTimeUnit.Size = new System.Drawing.Size(121, 21);
            this.cmbTimeUnit.TabIndex = 28;
            // 
            // cbUsePath
            // 
            this.cbUsePath.AutoSize = true;
            this.cbUsePath.Location = new System.Drawing.Point(0, 23);
            this.cbUsePath.Name = "cbUsePath";
            this.cbUsePath.Size = new System.Drawing.Size(70, 17);
            this.cbUsePath.TabIndex = 27;
            this.cbUsePath.Text = "Use Path";
            this.cbUsePath.UseVisualStyleBackColor = true;
            this.cbUsePath.CheckedChanged += new System.EventHandler(this.cbUsePath_CheckedChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(399, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(27, 23);
            this.btnBrowse.TabIndex = 26;
            this.btnBrowse.Text = "...";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(71, 21);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(319, 20);
            this.txtPath.TabIndex = 25;
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Location = new System.Drawing.Point(0, 0);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbEnabled.TabIndex = 22;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            this.cbEnabled.CheckedChanged += new System.EventHandler(this.cbEnabled_CheckedChanged);
            // 
            // btnDeleteNow
            // 
            this.btnDeleteNow.Location = new System.Drawing.Point(304, 47);
            this.btnDeleteNow.Name = "btnDeleteNow";
            this.btnDeleteNow.Size = new System.Drawing.Size(86, 23);
            this.btnDeleteNow.TabIndex = 24;
            this.btnDeleteNow.Text = "Delete Now";
            this.btnDeleteNow.UseVisualStyleBackColor = true;
            this.btnDeleteNow.Click += new System.EventHandler(this.btnDeleteNow_Click);
            // 
            // cbDeleteAfter
            // 
            this.cbDeleteAfter.AutoSize = true;
            this.cbDeleteAfter.Location = new System.Drawing.Point(0, 47);
            this.cbDeleteAfter.Name = "cbDeleteAfter";
            this.cbDeleteAfter.Size = new System.Drawing.Size(81, 17);
            this.cbDeleteAfter.TabIndex = 23;
            this.cbDeleteAfter.Text = "Delete after";
            this.cbDeleteAfter.UseVisualStyleBackColor = true;
            this.cbDeleteAfter.CheckedChanged += new System.EventHandler(this.cbDeleteAfter_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(304, 76);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 23);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(71, 76);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(86, 23);
            this.btnShow.TabIndex = 19;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // upDownAfter
            // 
            this.upDownAfter.Location = new System.Drawing.Point(87, 47);
            this.upDownAfter.Name = "upDownAfter";
            this.upDownAfter.Size = new System.Drawing.Size(59, 20);
            this.upDownAfter.TabIndex = 21;
            // 
            // LogOptionsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbTimeUnit);
            this.Controls.Add(this.cbUsePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.cbEnabled);
            this.Controls.Add(this.btnDeleteNow);
            this.Controls.Add(this.cbDeleteAfter);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.upDownAfter);
            this.Name = "LogOptionsCtrl";
            this.Size = new System.Drawing.Size(425, 100);
            ((System.ComponentModel.ISupportInitialize)(this.upDownAfter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTimeUnit;
        private System.Windows.Forms.CheckBox cbUsePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.Button btnDeleteNow;
        private System.Windows.Forms.CheckBox cbDeleteAfter;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.NumericUpDown upDownAfter;
    }
}
