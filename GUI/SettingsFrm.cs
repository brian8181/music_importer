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
using MusicImporter_Lib;
using Utility;

namespace music_importer
{
    public partial class SettingsFrm : Form
    {
        public SettingsFrm()
        {
            InitializeComponent();

            // set sha1 policy
            Importer.SHA1_Policy sha1_policy = Importer.SHA1_Policy.Always;
            try
            {
                sha1_policy = (Importer.SHA1_Policy)Enum.Parse(
                    typeof(Importer.SHA1_Policy), Properties.Settings.Default.sha1_policy);
            }
            catch(System.ArgumentException)
            {
                sha1_policy = Importer.SHA1_Policy.Always;
            }

            switch (sha1_policy)
            {
                case Importer.SHA1_Policy.Always:
                    rbAlways.Checked = true;
                    break;
                case Importer.SHA1_Policy.Insert_Only:
                    rbInsert_Only.Checked = true;
                    break;
                case Importer.SHA1_Policy.Insert_Or_Nulls:
                    rbInsert_Or_Nulls.Checked = true;
                    break;
                default:
                    rbAlways.Checked = true;
                    break;
            }
            
            // set log type radio
            Logger.LogType log_type = Logger.LogType.Single;
            try
            {
                log_type = (Logger.LogType)Enum.Parse(
                    typeof(Logger.LogType), Properties.Settings.Default.log_type);
            }
            catch (System.ArgumentException)
            {
                log_type = Logger.LogType.Single;
            }

            switch (log_type)
            {
                case Logger.LogType.Single:
                    rbNever.Checked = true;
                    break;
                case Logger.LogType.SplitRun:
                    rbSplitRun.Checked = true;
                    break;
                case Logger.LogType.SplitTime:
                    rbSplitTime.Checked = true;
                    break;
                case Logger.LogType.SplitSize:
                    rbSplitSize.Checked = true;
                    break;
                case Logger.LogType.Circular:
                    rbCircular.Checked = true;
                    break;
                default:
                    rbNever.Checked = true;
                    break;
            }

            cmbSplitTimeUnit.SelectedIndex = cmbSplitTimeUnit.FindStringExact("Hour");
            cmbSplitSizeUnit.SelectedIndex = cmbSplitSizeUnit.FindStringExact("MB");
            cmbCircularSizeUnit.SelectedIndex = cmbCircularSizeUnit.FindStringExact("MB");
            logOptionsCtrl.TextBox.Text = Properties.Settings.Default.log_path;
            reportOptionsCtrl.TextBox.Text = Properties.Settings.Default.report_path;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            // sha1 policy settings
            if (rbAlways.Checked)
            {
                Properties.Settings.Default.sha1_policy = Importer.SHA1_Policy.Always.ToString();
            }
            else if (rbInsert_Only.Checked)
            {
                Properties.Settings.Default.sha1_policy = Importer.SHA1_Policy.Insert_Only.ToString();
            }
            else if (rbInsert_Or_Nulls.Checked)
            {
                Properties.Settings.Default.sha1_policy = Importer.SHA1_Policy.Insert_Or_Nulls.ToString();
            }
                 
            // logger settings
            if (rbNever.Checked)
            {
                Properties.Settings.Default.log_type = Logger.LogType.Single.ToString();
            }
            else if (rbSplitRun.Checked)
            {
                Properties.Settings.Default.log_type = Logger.LogType.SplitRun.ToString();
            }
            else if (rbSplitSize.Checked)
            {
                Properties.Settings.Default.log_type = Logger.LogType.SplitSize.ToString();
            }
            else if (rbSplitTime.Checked)
            {
                Properties.Settings.Default.log_type = Logger.LogType.SplitTime.ToString();
            }
            else if (rbCircular.Checked)
            {
                Properties.Settings.Default.log_type = Logger.LogType.Circular.ToString();
            }

            Properties.Settings.Default.Save();
            Close();
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
        private void rbNever_CheckedChanged(object sender, EventArgs e)
        {
            upDownCircularSize.Enabled = !rbNever.Checked;
            upDownSplitSize.Enabled = !rbNever.Checked;
            upDownSplitTime.Enabled = !rbNever.Checked;
            cmbCircularSizeUnit.Enabled = !rbNever.Checked;
            cmbSplitSizeUnit.Enabled = !rbNever.Checked;
            cmbSplitTimeUnit.Enabled = !rbNever.Checked;
        }
    }
}