using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace music_importer
{
    public partial class LogOptionsCtrl : UserControl
    {
        private FolderBrowserDialog dlg = new FolderBrowserDialog();

        public TextBox TextBox
        {
            get { return txtPath; }
        }

        public LogOptionsCtrl()
        {
            InitializeComponent();

            cbEnabled.Checked = true;
            cbUsePath.Checked = false;
            cbDeleteAfter.Checked = false;
            
            cbEnabled_CheckedChanged(null, null);
            cbUsePath_CheckedChanged(null, null);
            cbDeleteAfter_CheckedChanged(null, null);
           
        }

        public LogOptionsCtrl(string path) : this()
        {
            txtPath.Text = path;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.Arguments = txtPath.Text;
            proc.Start();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteNow_Click(object sender, EventArgs e)
        {

        }

        private void cbDeleteAfter_CheckedChanged(object sender, EventArgs e)
        {
            upDownAfter.Enabled = cbDeleteAfter.Checked;
            cmbTimeUnit.Enabled = cbDeleteAfter.Checked;
            btnDeleteNow.Enabled = cbDeleteAfter.Checked;
        }

        private void cbUsePath_CheckedChanged(object sender, EventArgs e)
        {
            txtPath.Enabled = cbUsePath.Checked;
            btnBrowse.Enabled = cbUsePath.Checked;
        }

        private void cbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            cbUsePath.Enabled = cbEnabled.Checked;
            txtPath.Enabled = cbEnabled.Checked;
            cbDeleteAfter.Enabled = cbEnabled.Checked;
            upDownAfter.Enabled = cbEnabled.Checked;
            cmbTimeUnit.Enabled = cbEnabled.Checked;
            btnBrowse.Enabled = cbEnabled.Checked;
            btnDeleteNow.Enabled = cbEnabled.Checked;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlg.Reset();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = dlg.SelectedPath;
                txtPath.Text = path;
            }
        }
    }
}
