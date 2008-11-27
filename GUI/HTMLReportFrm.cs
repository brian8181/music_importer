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
        Reporter r = new Reporter();
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
    }
}
