﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace music_importer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool show_user {
            get {
                return ((bool)(this["show_user"]));
            }
            set {
                this["show_user"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool show_pass {
            get {
                return ((bool)(this["show_pass"]));
            }
            set {
                this["show_pass"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>music</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection schema_history {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["schema_history"]));
            }
            set {
                this["schema_history"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>3306</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection port_history {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["port_history"]));
            }
            set {
                this["port_history"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>localhost</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection address_history {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["address_history"]));
            }
            set {
                this["address_history"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection mysql_history {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["mysql_history"]));
            }
            set {
                this["mysql_history"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection sqlite_history {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["sqlite_history"]));
            }
            set {
                this["sqlite_history"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string> *.jpg;*.jpeg;*.png;*.gif;*.bmp</string>
  <string>Cover.jp*;Cover.png;Cover.gif;Cover.bmp</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection art_mask_history {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["art_mask_history"]));
            }
            set {
                this["art_mask_history"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>*.mp3;*.wma;*.ogg;*.flac</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection file_mask_history {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["file_mask_history"]));
            }
            set {
                this["file_mask_history"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool clear_logs {
            get {
                return ((bool)(this["clear_logs"]));
            }
            set {
                this["clear_logs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string log_path {
            get {
                return ((string)(this["log_path"]));
            }
            set {
                this["log_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string report_path {
            get {
                return ((string)(this["report_path"]));
            }
            set {
                this["report_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Single")]
        public string log_type {
            get {
                return ((string)(this["log_type"]));
            }
            set {
                this["log_type"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MB")]
        public string log_size_unit {
            get {
                return ((string)(this["log_size_unit"]));
            }
            set {
                this["log_size_unit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Hour")]
        public string log_time_unit {
            get {
                return ((string)(this["log_time_unit"]));
            }
            set {
                this["log_time_unit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int log_size {
            get {
                return ((int)(this["log_size"]));
            }
            set {
                this["log_size"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Always")]
        public string sha1_policy {
            get {
                return ((string)(this["sha1_policy"]));
            }
            set {
                this["sha1_policy"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Always")]
        public string sha1_file_policy {
            get {
                return ((string)(this["sha1_file_policy"]));
            }
            set {
                this["sha1_file_policy"] = value;
            }
        }
    }
}
