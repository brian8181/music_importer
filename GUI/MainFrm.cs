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
using System.Threading;
using System.IO;
using System.Collections.Specialized;
using MusicImporter_Lib.Properties;
using MusicImporter_Lib;
using BKP.Online;
//
namespace music_importer
{
    public delegate void SafeSetLabelDelegate( Label l, string s );
    public delegate void SafeToggleDelegate( bool state );
    /// <summary>
    /// music importer GUI
    /// </summary>
    public partial class MainFrm : Form
    {
        #region Construction
        private string[] args = null;
        private Importer importer = null;
        private DateTime start_time = DateTime.Now;
              
    
        /// <summary>
        /// default contructor
        /// </summary>
        public MainFrm()
            : this( null )
        {
            
        }
        /// <summary>
        ///  cmd line constuctor
        /// </summary>
        /// <param name="args">args (may be  null)</param>
        public MainFrm( string[] args )
        {
            InitializeComponent();
            
            progressBar.Enabled = false;
            progressBar.Style = ProgressBarStyle.Continuous;
            this.args = args;
            if(args != null || args.Length < 1) LoadSettings();
            // GUI Settings
            cbSH_User.Checked = !Properties.Settings.Default.show_user;
            cbSH_Pass.Checked = !Properties.Settings.Default.show_pass;
            cmbVersion.SelectedIndex = 0;

            // init priorty combobox
            Array values = Enum.GetValues(typeof(ThreadPriority));
            foreach(ThreadPriority v in values)
            {
                cmbPriority.Items.Add( v ); 
            }
            cmbPriority.SelectedIndex = 1; //BelowNormal

            tt_btnStart.SetToolTip( btnOK, "click to start" );
            tt_create.SetToolTip( cbCreateDB, Properties.Resources.warn_create_db );
            tt_mm_message.SetToolTip( txtSQLite, Properties.Resources.mm_message );

            cbSH_Pass.SetImages( Properties.Resources.lock_trans_16, Properties.Resources.unlock_trans_16 );
            cbSH_User.SetImages( Properties.Resources.lock_trans_16, Properties.Resources.unlock_trans_16 );
            cbMysql.SetImages( Properties.Resources.enabled_trans_16, Properties.Resources.disabled_trans_16 );
            cbPlaylist.SetImages( Properties.Resources.enabled_trans_16, Properties.Resources.disabled_trans_16 );
            cbGenerateThumbs.SetImages( Properties.Resources.enabled_trans_16, Properties.Resources.disabled_trans_16 );
        }
        #endregion

        #region Settings / Configuration
        /// <summary>
        /// load application settings
        /// </summary>
        private void LoadSettings()
        {
            this.txtAddress.Text = Settings.Default.Address;
            this.txtUser.Text = Settings.Default.User;
            this.txtPassword.Text = Settings.Default.Pass;
            this.txtSchema.Text = Settings.Default.schema;
            this.txtMySql.Text = Settings.Default.mysql_conn_str;
            this.txtSQLite.Text = Settings.Default.mm_conn_str;
            this.cbClean.Checked = Settings.Default.Clean;
            this.cbLog.Checked = Settings.Default.Log;
            this.cbArt.Checked = Settings.Default.RecanArt;
            this.cbTags.Checked = Settings.Default.ScanTags;
            this.cbOptimize.Checked = Settings.Default.Optimize;
            this.cbPlaylist.Checked = Settings.Default.ScanPlaylist;
            this.txtMySql.Enabled = cbMysql.Checked;
            this.cbMysql.Checked = Settings.Default.use_conn_str;
            this.txtSQLite.Enabled = cbPlaylist.Checked;
            // always false
            //this.cbCreateDB.Checked = Settings.Default.create_db;
            this.cbCreateDB.Checked = false;
            this.txtRoot.Text = Settings.Default.music_root;
            StringCollection dirs = Settings.Default.Dirs;
            foreach(string s in dirs)
            {
                lbScanLocations.Items.Add( new FileInfo( s ) );
            }
            this.txtArtLoc.Text = Settings.Default.art_location;
            this.txtMask.Text = Settings.Default.file_mask;
            this.txtArtMask.Text = Settings.Default.art_mask;
            // art & thumb settings
            this.cbGenerateThumbs.Checked = Settings.Default.insert_art;
            this.btnBrowseArt.Enabled = Settings.Default.insert_art;
            this.txtArtLoc.Enabled = Settings.Default.insert_art;
            this.txtArtMask.Enabled = Settings.Default.insert_art;
            this.art_large.Enabled = Settings.Default.insert_art;
            this.art_small.Enabled = Settings.Default.insert_art;
            this.art_xsmall.Enabled = Settings.Default.insert_art;
            this.art_large.Value = Settings.Default.art_large;
            this.art_small.Value = Settings.Default.art_small;
            this.art_xsmall.Value = Settings.Default.art_xsmall;
            
            // Copy Settings to AutoCompleteCustomSource
            string[] strs = new string[Properties.Settings.Default.address_history.Count]; 
            Properties.Settings.Default.address_history.CopyTo( strs, 0 );
            this.txtAddress.AutoCompleteCustomSource.AddRange( strs );
            strs = new string[Properties.Settings.Default.port_history.Count];
            Properties.Settings.Default.port_history.CopyTo( strs, 0 );
            this.txtPort.AutoCompleteCustomSource.AddRange( strs );
            strs = new string[Properties.Settings.Default.schema_history.Count];
            Properties.Settings.Default.schema_history.CopyTo( strs, 0 );
            this.txtSchema.AutoCompleteCustomSource.AddRange( strs );
            // the mask are loaded but not saved
            strs = new string[Properties.Settings.Default.art_mask_history.Count];
            Properties.Settings.Default.art_mask_history.CopyTo( strs, 0 );
            this.txtArtMask.AutoCompleteCustomSource.AddRange( strs );
            strs = new string[Properties.Settings.Default.file_mask_history.Count];
            Properties.Settings.Default.file_mask_history.CopyTo( strs, 0 );
            this.txtMask.AutoCompleteCustomSource.AddRange( strs );
            
            if(Properties.Settings.Default.mysql_history != null)
            {
                Properties.Settings.Default.mysql_history = new StringCollection();
                strs = new string[Properties.Settings.Default.mysql_history.Count];
                Properties.Settings.Default.mysql_history.CopyTo( strs, 0 );
                this.txtMySql.AutoCompleteCustomSource.AddRange( strs );
            }
            else
            {
                Properties.Settings.Default.mysql_history = new StringCollection();
            }

            if(Properties.Settings.Default.sqlite_history != null)
            {
                strs = new string[Properties.Settings.Default.sqlite_history.Count];
                Properties.Settings.Default.sqlite_history.CopyTo( strs, 0 );
                this.txtSQLite.AutoCompleteCustomSource.AddRange( strs );
            }
            else
            {
                Properties.Settings.Default.sqlite_history = new StringCollection();
            }
            //load version box
            string proc_path = Path.GetDirectoryName( Globals.ProcessPath() );
            string[] files = Directory.GetFiles( proc_path, "update.?.?.?.sql" );
            foreach( string f in files )
            {
                string version = Path.GetFileNameWithoutExtension( f );
                cmbVersion.Items.Add(version);
            }
        }
        /// <summary>
        /// save application settings
        /// </summary>
        private void SaveSettings()
        {
            // importer Settings
            Settings.Default.Address = this.txtAddress.Text;
            Settings.Default.User = this.txtUser.Text;
            Settings.Default.Pass = this.txtPassword.Text;
            Settings.Default.schema = this.txtSchema.Text;
            Settings.Default.use_conn_str = cbMysql.Checked;
            if(cbMysql.Checked)
            {
                Settings.Default.mysql_conn_str = this.txtMySql.Text;
            }
            if(cbPlaylist.Checked)
            {
                Settings.Default.mm_conn_str = this.txtSQLite.Text;
            }
            Settings.Default.ScanPlaylist = this.cbPlaylist.Checked;
            Settings.Default.Clean = this.cbClean.Checked;
            Settings.Default.Log = this.cbLog.Checked;
            Settings.Default.RecanArt = this.cbArt.Checked;
            Settings.Default.ScanTags = this.cbTags.Checked;
            Settings.Default.Optimize = this.cbOptimize.Checked;
            Settings.Default.create_db = this.cbCreateDB.Checked;
            Settings.Default.Dirs.Clear();
            Settings.Default.music_root = this.txtRoot.Text;
            foreach(FileInfo fi in lbScanLocations.Items)
            {
                // do not save root
                if(fi.FullName.Length > txtRoot.Text.Length)  
                {
                    Settings.Default.Dirs.Add( fi.FullName );
                }
            }
            Settings.Default.art_location = this.txtArtLoc.Text;
            Settings.Default.file_mask = this.txtMask.Text;
            Settings.Default.art_mask = this.txtArtMask.Text;
            Settings.Default.insert_art = this.cbGenerateThumbs.Checked;
            Settings.Default.art_large = (int)this.art_large.Value;
            Settings.Default.art_small = (int)this.art_small.Value;
            Settings.Default.art_xsmall = (int)this.art_xsmall.Value;
            Settings.Default.Save();
            // GUI Settings
            Properties.Settings.Default.show_user = !cbSH_User.Checked;
            Properties.Settings.Default.show_pass = !cbSH_Pass.Checked;

            // Copy AutoCompleteCustomSource to Settings
            string[] strs = new string[this.txtAddress.AutoCompleteCustomSource.Count];
            this.txtAddress.AutoCompleteCustomSource.CopyTo( strs, 0 );
            Properties.Settings.Default.address_history.Clear();
            Properties.Settings.Default.address_history.AddRange( strs );

            strs = new string[this.txtMySql.AutoCompleteCustomSource.Count];
            this.txtMySql.AutoCompleteCustomSource.CopyTo( strs, 0 );
            Properties.Settings.Default.mysql_history.Clear();
            Properties.Settings.Default.mysql_history.AddRange( strs );

            strs = new string[this.txtPort.AutoCompleteCustomSource.Count];
            this.txtPort.AutoCompleteCustomSource.CopyTo( strs, 0 );
            Properties.Settings.Default.port_history.Clear();
            Properties.Settings.Default.port_history.AddRange( strs );

            strs = new string[this.txtSchema.AutoCompleteCustomSource.Count];
            this.txtSchema.AutoCompleteCustomSource.CopyTo( strs, 0 );
            Properties.Settings.Default.schema_history.Clear();
            Properties.Settings.Default.schema_history.AddRange( strs );

            strs = new string[this.txtSQLite.AutoCompleteCustomSource.Count];
            this.txtSQLite.AutoCompleteCustomSource.CopyTo( strs, 0 );
            Properties.Settings.Default.sqlite_history.Clear();
            Properties.Settings.Default.sqlite_history.AddRange( strs );
  
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Control Events
        /// <summary>
        ///  start clicked
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">args</param>
        private void btnOK_Click( object sender, EventArgs e )
        {
            ToggleOff();
            btnOK.Enabled = false;
            btnCancel.Text = "Quit";
            btnPause.Enabled = true;
            start_time = DateTime.Now;
            lbStartTime.Text = start_time.ToShortTimeString();
            // start progess marquee
            progressBar.Style = ProgressBarStyle.Marquee;
            // validate
            if(ValidateInput() == false)
            {
                lbMessage.Text = "Invalid input";
                MessageBox.Show(
                    "User input is not valid. Please check that each filed is entered and correct and try again.",
                    "Invalid Field Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1 );
                // revert changes and return
                importer_TagScanStopped();
                return;
            }
            UpdateHistory();            
            // SAVE GLOBAL SETTINGS BEFORE CONNECT //
            SaveSettings();
            // connect
            try
            {
                importer = new Importer( cmbVersion.SelectedItem.ToString() );
                importer.Connect();
            }
            catch(MySql.Data.MySqlClient.MySqlException exp)
            {
                MessageBox.Show(
                    exp.Message + ".\r\n\r\nPlease make sure connection fields are correct and try agian.",
                    "MySql Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1 );
                // revert changes and return
                importer_TagScanStopped();
                return;
            }
            importer.Status += new StringDelegate( importer_Status );
            importer.Message += new BKP.Online.StringDelegate( importer_Message );
            importer.ProcessDirectory += new StringDelegate( importer_ProcessDirectory );
            importer.Error += new StringDelegate( importer_Error );
            importer.ScanStarted += new VoidDelegate( importer_TagScanStarted );
            importer.ScanStopped += new VoidDelegate( importer_TagScanStopped );
            importer.SyncError += new VoidDelegate( importer_SyncError );
            importer.Priority = (ThreadPriority)cmbPriority.SelectedItem;
            importer.Scan( true );
        }
        /// <summary>
        /// btnCancel
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">arguments</param>
        private void btnCancel_Click( object sender, EventArgs e )
        {
            if(btnCancel.Text == "Quit")
            {
                ToggleOn();
                btnCancel.Enabled = false;
                btnPause.Text = "&Pause";
                btnPause.Enabled = false;
                importer.StopScan();
            }
            else
            {
                Close();
            }
        }
        /// <summary>
        /// btnPause 
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">arguments</param>
        private void btnPause_Click( object sender, EventArgs e )
        {
            if(importer != null)
            {
                if(btnPause.Text == "&Pause")
                {
                    importer.PauseScan();
                    btnPause.Text = "&Continue";
                    progressBar.Style = ProgressBarStyle.Continuous;
                }
                else
                {
                    importer.ContiueScan();
                    btnPause.Text = "&Pause";
                    progressBar.Style = ProgressBarStyle.Marquee;
                }
            }
        }
        /// <summary>
        /// btnAdd 
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">arguments</param>
        private void btnAdd_Click( object sender, EventArgs e )
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.Desktop;
                       
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                if( dlg.SelectedPath.StartsWith( txtRoot.Text, 
                                                 true, 
                                                 System.Globalization.CultureInfo.InvariantCulture ) &&
                                                 dlg.SelectedPath.Length > txtRoot.Text.Length )
                {
                    lbScanLocations.Items.Add( new FileInfo( dlg.SelectedPath ) );
                }
                else
                {
                    StdMsgBox.OK( "Path not under root" );
                }
            }
        }
        /// <summary>
        ///  btnRemove clicked
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">arguments</param>
        private void btnRemove_Click( object sender, EventArgs e )
        {
            object[] objs = new object[lbScanLocations.SelectedItems.Count];
            lbScanLocations.SelectedItems.CopyTo( objs, 0 );
            foreach(object o in objs)
            {
                lbScanLocations.Items.Remove( o );
            }
        }
        /// <summary>
        ///  btnClear clicked
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">arguments</param>
        private void btnClear_Click( object sender, EventArgs e )
        {
            lbScanLocations.Items.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRoot_TextChanged( object sender, EventArgs e )
        {
            lbScanLocations.Items.Clear();
            if(Directory.Exists( txtRoot.Text ))
            {
                btnAdd.Enabled = true;
                lbScanLocations.Items.Add( new FileInfo( txtRoot.Text ) );
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseRoot_Click( object sender, EventArgs e )
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.Desktop;
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                txtRoot.Text = dlg.SelectedPath;
            }
        }
        /// <summary>
        /// set use connnection string or not
        /// </summary>
        /// <param name="sender">the CheckBox</param>
        /// <param name="e">args</param>
        private void cbMysql_CheckedChanged( object sender, EventArgs e )
        {
            bool check = this.cbMysql.Checked;
            // set toggle button
            //cbMysql.Text = cbMysql.Checked ? "Disable" : "Enable";
            
            txtMySql.Enabled = check;
            // set server info
            txtAddress.Enabled = !check;
            txtPort.Enabled = !check;
            txtUser.Enabled = !check;
            txtPassword.Enabled = !check;
            txtSchema.Enabled = !check;
            // disallow creation with connect string
            //todo cbCreateDB.Checked = !check;
            cbCreateDB.Enabled = !check;
        }
        /// <summary>
        /// check box changed
        /// </summary>
        /// <param name="sender">checkbox</param>
        /// <param name="e">ags</param>
        private void cbPlaylist_CheckedChanged( object sender, EventArgs e )
        {
            // set toggle button
            //cbPlaylist.Text = cbPlaylist.Checked ? "Disable" : "Enable";
            txtSQLite.Enabled = cbPlaylist.Checked;
        }
        /// <summary>
        /// check box changed
        /// </summary>
        /// <param name="sender">checkbox</param>
        /// <param name="e">ags</param>
        private void btnBrowseArt_Click( object sender, EventArgs e )
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.Desktop;
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                txtArtLoc.Text = dlg.SelectedPath;
            }
        }
        /// <summary>
        /// show or hide user name
        /// </summary>
        /// <param name="sender">the toggle button</param>
        /// <param name="e">args</param>
        private void cbSH_User_CheckedChanged( object sender, EventArgs e )
        {
           txtUser.PasswordChar = cbSH_User.Checked ? '*' : (char)0;
        }
        /// <summary>
        /// show or hide pasword
        /// </summary>
        /// <param name="sender">the toggle button</param>
        /// <param name="e">args</param>
        private void cbSH_Pass_CheckedChanged( object sender, EventArgs e )
        {
            txtPassword.PasswordChar = cbSH_Pass.Checked ? '*' : (char)0;
        }
        /// <summary>
        /// change thread priority
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void cmbPriority_SelectedIndexChanged( object sender, EventArgs e )
        {
            if(importer != null)
                importer.Priority = (ThreadPriority)cmbPriority.SelectedItem;
        }
        /// <summary>
        /// create db is check changed
        /// </summary>
        /// <param name="sender">checkbox</param>
        /// <param name="e">e</param>
        private void cbCreateDB_CheckedChanged( object sender, EventArgs e )
        {
            if(cbCreateDB.Checked)
            {
                DialogResult dr = MessageBox.Show(
                    "Current setting will cause database to be (re)created, causing loss of all data are you sure you want to continue?",
                    "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1 );
                if(dr != DialogResult.Yes)
                {
                    cbCreateDB.Checked = false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbGenerateThumbs_CheckedChanged( object sender, EventArgs e )
        {
            // do art sizes make logical sense
            // Obsolete
            if(cbGenerateThumbs.Checked)
            {
                if(!( art_small.Value < art_large.Value && art_small.Value > art_xsmall.Value ))
                {
                    StdMsgBox.OK( "Art sizes do not make logical sense. (small < large and small > x-small)" );
                    cbGenerateThumbs.Checked = false;
                }
            }
            this.btnBrowseArt.Enabled = cbGenerateThumbs.Checked; 
            this.txtArtLoc.Enabled = cbGenerateThumbs.Checked;
            this.txtArtMask.Enabled = cbGenerateThumbs.Checked;
            this.art_large.Enabled = cbGenerateThumbs.Checked;
            this.art_small.Enabled = cbGenerateThumbs.Checked;
            this.art_xsmall.Enabled = cbGenerateThumbs.Checked;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void art_small_ValueChanged( object sender, EventArgs e )
        {
            art_xsmall.Maximum = art_small.Value - 1;
            art_large.Minimum = art_small.Value + 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void art_large_ValueChanged( object sender, EventArgs e )
        {
            art_small.Maximum = art_large.Value - 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void art_xsmall_ValueChanged( object sender, EventArgs e )
        {
            art_small.Minimum = art_xsmall.Value + 1;
        }
        #endregion

        #region Importer Events
        /// <summary>
        /// status changed
        /// </summary>
        /// <param name="str">new status message</param>
        void importer_Status( string str )
        {
            SafeSet_Label( lbStatus, str );
        }
        /// <summary>
        /// importer sync error
        /// </summary>
        void importer_SyncError()
        {
            MessageBox.Show( "A previous scan has not finished." );
        }
        /// <summary>
        ///  scan started
        /// </summary>
        private void importer_TagScanStarted()
        {
          SafeSet_Label( lbMessage, "Tag scan started" );
        }
        /// <summary>
        ///  scan finished
        /// </summary>
        private void importer_TagScanStopped()
        {
            SafeSet_Label( lbStatus, "&Finished" );
            this.Invoke( new VoidDelegate( delegate() {
                                                    btnCancel.Enabled = true;
                                                    btnCancel.Text = "&Finished";
                                                    btnPause.Enabled = false;
                                                    // stop progess marquee
                                                    progressBar.Style = ProgressBarStyle.Continuous;
                                                    ToggleOn();
                                                }
                                          ) );
            if( importer != null )
                importer.Close();
        }
        /// <summary>
        ///  scan error
        /// </summary>
        /// <param name="str">errror message</param>
        private void importer_Error( string error )
        {
            MessageBox.Show(
                    error + ".\r\n\r\nPlease make sure connection fields are correct and try agian.",
                    "MySql Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1 );

        }
        /// <summary>
        /// process diectory changed
        /// </summary>
        /// <param name="str">directory</param>
        private void importer_ProcessDirectory( string str )
        {
            if(this.InvokeRequired) // invoke on gui thread
            {
                this.Invoke( new StringDelegate( importer_ProcessDirectory ), new object[] { str } );
                return;
            }
            lbDirectory.Text = str;
        }
        /// <summary>
        /// importer status message
        /// </summary>
        /// <param name="str">message</param>
        private void importer_Message( string str )
        {
            SafeSet_Label( lbMessage, str );
        }
        #endregion

        #region Helpers
        public void UpdateHistory()
        {
            // address
            int idx = txtAddress.AutoCompleteCustomSource.IndexOf( txtAddress.Text );
            if(idx >= 0)
            {
                // remove and put on top
                txtAddress.AutoCompleteCustomSource.RemoveAt( idx );
                txtAddress.AutoCompleteCustomSource.Add( txtAddress.Text );
            }
            else
            {
                if( !String.IsNullOrEmpty( txtAddress.Text ) )
                    txtAddress.AutoCompleteCustomSource.Add( txtAddress.Text );
            }

            // mysql
            idx = txtMySql.AutoCompleteCustomSource.IndexOf( txtAddress.Text );
            if(idx >= 0)
            {
                // remove and put on top
                txtMySql.AutoCompleteCustomSource.RemoveAt( idx );
                txtMySql.AutoCompleteCustomSource.Add( txtMySql.Text );
            }
            else
            {
                if(!String.IsNullOrEmpty( txtAddress.Text ))
                    txtMySql.AutoCompleteCustomSource.Add( txtMySql.Text );
            }

            // port
            idx = txtPort.AutoCompleteCustomSource.IndexOf( txtPort.Text );
            if(idx >= 0)
            {
                // remove and put on top
                txtPort.AutoCompleteCustomSource.RemoveAt( idx );
                txtPort.AutoCompleteCustomSource.Add( txtPort.Text );
            }
            else
            {
                if(!String.IsNullOrEmpty( txtAddress.Text ))
                    txtPort.AutoCompleteCustomSource.Add( txtPort.Text );
            }

            // schema
            idx = txtSchema.AutoCompleteCustomSource.IndexOf( txtSchema.Text );
            if(idx >= 0)
            {
                // remove and put on top
                txtSchema.AutoCompleteCustomSource.RemoveAt( idx );
                txtSchema.AutoCompleteCustomSource.Add( txtSchema.Text );
            }
            else
            {
                if(!String.IsNullOrEmpty( txtAddress.Text ))
                    txtSchema.AutoCompleteCustomSource.Add( txtSchema.Text );
            }

            // sqlite
            idx = txtSQLite.AutoCompleteCustomSource.IndexOf( txtSQLite.Text );
            if(idx > 0)
            {
                // remove and put on top
                txtSQLite.AutoCompleteCustomSource.RemoveAt( idx );
                txtSQLite.AutoCompleteCustomSource.Add( txtSQLite.Text );
            }
            else
            {
                if(!String.IsNullOrEmpty( txtAddress.Text ))
                    txtSQLite.AutoCompleteCustomSource.Add( txtSQLite.Text );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        private void SafeSet_Label( Label l, string s )
        {
            if(this.InvokeRequired) // invoke on gui thread
            {
                this.Invoke( new SafeSetLabelDelegate( SafeSet_Label ), new object[] { l, s } );
                return;
            }
            l.Text = s;
            // this is a good place for now
            TimeSpan ts = DateTime.Now - start_time;
            lbElapsedTime.Text = ts.ToString();
        }
        /// <summary>
        ///  toggle control from enabled to disabled
        /// </summary>
        public void ToggleOn()
        {
            Toggle( true );
        }
        public void ToggleOff()
        {
            Toggle( false );
        }
        public void Toggle( bool state )
        {
            if(this.InvokeRequired) // invoke on gui thread
            {
                this.Invoke( new SafeToggleDelegate( Toggle ), state );
                return;
            }
            // progress bar
            progressBar.Enabled = !state;
            // button
            btnOK.Enabled = state;
            btnAdd.Enabled = state;
            btnRemove.Enabled = state;
            btnClear.Enabled = state;
            btnBrowseArt.Enabled = state;
            if(state) // turn on or leave off 
            {
                bool check = cbMysql.Checked;
                txtMySql.Enabled = check;
                txtAddress.Enabled = !check;
                txtPassword.Enabled = !check;
                txtUser.Enabled = !check;
                txtPort.Enabled = !check;
                txtSchema.Enabled = !check;
                cbCreateDB.Enabled = !check;
                txtSQLite.Enabled = cbPlaylist.Checked;
                check = cbGenerateThumbs.Checked;
                this.btnBrowseArt.Enabled = cbGenerateThumbs.Checked;
                this.txtArtLoc.Enabled = cbGenerateThumbs.Checked;
                this.txtArtMask.Enabled = cbGenerateThumbs.Checked;
                this.art_large.Enabled = cbGenerateThumbs.Checked;
                this.art_small.Enabled = cbGenerateThumbs.Checked;
                this.art_xsmall.Enabled = cbGenerateThumbs.Checked;
            }
            else // turn off
            {
                txtAddress.Enabled = state;
                txtPassword.Enabled = state;
                txtUser.Enabled = state;
                txtPort.Enabled = state;
                txtSchema.Enabled = state;
                txtMySql.Enabled = state;
                txtSQLite.Enabled = state;
                cbCreateDB.Enabled = state;
                this.btnBrowseArt.Enabled = state;
                this.txtArtLoc.Enabled = state;
                this.txtArtMask.Enabled = state;
                this.art_large.Enabled = state;
                this.art_small.Enabled = state;
                this.art_xsmall.Enabled = state;
            }
            txtRoot.Enabled = state;
            btnBrowseRoot.Enabled = state;
            txtMask.Enabled = state;
            cbGenerateThumbs.Enabled = state;
            // check boxes
            cbMysql.Enabled = state;
            cbArt.Enabled = state;
            cbClean.Enabled = state;
            cbLog.Enabled = state;
            cbOptimize.Enabled = state;
            cbPlaylist.Enabled = state;
            cbTags.Enabled = state;
        }
        #endregion

        #region Validation
        ///<summary>
        ///validate input
        ///</summary>
        ///<returns>true if valid otherwise false</returns>
        private bool ValidateInput()
        {
            bool result = true;
            if(!cbMysql.Checked)
            {
                result = result ? !string.IsNullOrEmpty( txtAddress.Text ) : false;
                result = result ? !string.IsNullOrEmpty( txtPassword.Text ) : false;
                result = result ? !string.IsNullOrEmpty( txtPort.Text ) : false;
                result = result ? Functions.isNumeric( txtPort.Text ) : false;
                uint port = uint.Parse( txtPort.Text );
                result = result ? port < 65535 : false;
                result = result ? !string.IsNullOrEmpty( txtUser.Text ) : false;
            }
            else
            {
                result = result ? !string.IsNullOrEmpty( txtMySql.Text ) : false;
            }
            if(cbPlaylist.Checked)
            {
                result = result ? !string.IsNullOrEmpty( txtSQLite.Text ) : false;
            }
            result = result ? ( lbScanLocations.Items.Count > 0 ) : false;
            if(cbGenerateThumbs.Checked && !Directory.Exists( txtArtLoc.Text ))
            {
                // create directory, if not exists   
                result = result ? !string.IsNullOrEmpty( txtArtLoc.Text ) : false;

                DialogResult dr = MessageBox.Show(
                    "Art location \"" + txtArtLoc.Text + "\" does not exist you do want to create it?",
                    "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1 );
                if(dr != DialogResult.Yes)
                    return false;
                try
                {
                    Directory.CreateDirectory( txtArtLoc.Text );
                }
                catch(System.IO.DirectoryNotFoundException e)
                {
                    StdMsgBox.OK( "Error creating directory.\r\n" + e.Message );
                }

                if(!Directory.Exists( txtArtLoc.Text ))
                {
                    return false;
                }
            }
            return result;
        }
        #endregion

        private void settingsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            SettingsFrm dlg = new SettingsFrm();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if(importer != null)
            {
                importer.StopScan(); 
                // todo wait for stop
                importer.Close();
            }
            Close();
        }
            
    }
}
