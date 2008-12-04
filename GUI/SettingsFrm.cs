// Music Importer imports ID3 tags to a MySql database.
// Copyright (C) 2008  Brian Preston
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MusicImporter_Lib.Properties;
using Utility;

namespace music_importer
{
    public partial class SettingsFrm : Form
    {
        public SettingsFrm()
        {
            InitializeComponent();
            
            //logDirectory.TextBox.Text = Properties.Settings.Default.log_path;
            //reportDirectory.TextBox.Text = Properties.Settings.Default.report_path;
            //cbLoggingEnabled.Checked = Settings.Default.Log;
            //cbDeleteLogsAfter.Checked = true;
          
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            Utility.Logger.ClearLogs(Properties.Settings.Default.log_path);
        }
        private void btnClearReports_Click(object sender, EventArgs e)
        {
            MusicImporter_Lib.Reporter.ClearReports(Properties.Settings.Default.report_path);
        }
        private void btnShowLogs_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.Arguments = Properties.Settings.Default.log_path;
            proc.Start();
        }
        private void btnShowReports_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.Arguments = Properties.Settings.Default.report_path;
            proc.Start();
        }

        private void cbLoggingEnabled_CheckedChanged(object sender, EventArgs e)
        {
            //logDirectory.Enabled = cbLoggingEnabled.Checked;
            //cbDeleteLogsAfter.Enabled = cbLoggingEnabled.Checked;
            //upDownKeepLogDays.Enabled = cbLoggingEnabled.Checked;
            //lblLogDays.Enabled = cbLoggingEnabled.Checked;
            //btnLogsDeleteNow.Enabled = cbLoggingEnabled.Checked;
            //btnShowLogs.Enabled = cbLoggingEnabled.Checked;
            //btnClearLogs.Enabled = cbLoggingEnabled.Checked;
        }

        private void cbDeleteLogsAfter_CheckedChanged(object sender, EventArgs e)
        {
            //upDownKeepLogDays.Enabled = cbDeleteLogsAfter.Checked;
            //lblLogDays.Enabled = cbDeleteLogsAfter.Checked;
            //btnLogsDeleteNow.Enabled = cbDeleteLogsAfter.Checked;
        }
    }
}