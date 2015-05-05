using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MusicImporter_Lib;

namespace music_importer
{
    public partial class HTMLReportFrm : Form
    {
        private Reporter r = new Reporter();
        private string file = string.Empty;

        public HTMLReportFrm()
        {
            InitializeComponent();
        }

        public string DocumentText
        {
            get
            {
                return webBrowser1.DocumentText;
            }
            set
            {
                webBrowser1.DocumentText = value;
            }
        }

        public string File
        {
            set
            {
                string uri = string.Format(@"file:///{0}", value);
                webBrowser1.Url = new Uri(uri);

            }
        }
    }
}
