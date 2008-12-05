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
            rbSplitRun.Checked = true;
            cmbSplitTimeUnit.SelectedIndex = cmbSplitTimeUnit.FindStringExact("Hour");
            cmbSplitSizeUnit.SelectedIndex = cmbSplitSizeUnit.FindStringExact("MB");
            cmbCircularSizeUnit.SelectedIndex = cmbCircularSizeUnit.FindStringExact("MB");
            logOptionsCtrl.TextBox.Text = Properties.Settings.Default.log_path;
            reportOptionsCtrl.TextBox.Text = Properties.Settings.Default.report_path;
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
        private void rbSplitRun_CheckedChanged(object sender, EventArgs e)
        {
            upDownCircularSize.Enabled = !rbSplitRun.Checked;
            upDownSplitSize.Enabled = !rbSplitRun.Checked;
            upDownSplitTime.Enabled = !rbSplitRun.Checked;
            cmbCircularSizeUnit.Enabled = !rbSplitRun.Checked;
            cmbSplitSizeUnit.Enabled = !rbSplitRun.Checked;
            cmbSplitTimeUnit.Enabled = !rbSplitRun.Checked;
        }
        private void rbSplitTime_CheckedChanged(object sender, EventArgs e)
        {
            upDownSplitSize.Enabled = !rbSplitTime.Checked;
            cmbSplitSizeUnit.Enabled = !rbSplitTime.Checked;
            upDownSplitTime.Enabled = rbSplitTime.Checked;
            cmbSplitTimeUnit.Enabled = rbSplitTime.Checked;
            upDownCircularSize.Enabled = !rbSplitTime.Checked;
            cmbCircularSizeUnit.Enabled = !rbSplitTime.Checked;
        }
        private void rbCircular_CheckedChanged(object sender, EventArgs e)
        {
            upDownSplitSize.Enabled = !rbCircular.Checked;
            cmbSplitSizeUnit.Enabled = !rbCircular.Checked;
            upDownSplitTime.Enabled = !rbCircular.Checked;
            cmbSplitTimeUnit.Enabled = !rbCircular.Checked;
            upDownCircularSize.Enabled = rbCircular.Checked;
            cmbCircularSizeUnit.Enabled = rbCircular.Checked;
        }
        private void rbSplitSize_CheckedChanged(object sender, EventArgs e)
        {
            upDownSplitSize.Enabled = rbSplitSize.Checked;
            cmbSplitSizeUnit.Enabled = rbSplitSize.Checked;
            upDownSplitTime.Enabled = !rbSplitSize.Checked;
            cmbSplitTimeUnit.Enabled = !rbSplitSize.Checked;
            upDownCircularSize.Enabled = !rbSplitSize.Checked;
            cmbCircularSizeUnit.Enabled = !rbSplitSize.Checked;
        }
    }
}