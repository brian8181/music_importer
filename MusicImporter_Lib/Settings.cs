using System;
using System.Text;
namespace MusicImporter_Lib.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        public string User_UTF8
        {
            get
            {
                byte[] bytes = Convert.FromBase64String(User);
                string utf8 = Encoding.UTF8.GetString(bytes);
                return utf8;
            }
            set
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                string base64 = Convert.ToBase64String(bytes);
                User = base64;
            }
        }
        public string Pass_UTF8
        {
            get
            {
                byte[] bytes = Convert.FromBase64String(Pass);
                string utf8 = Encoding.UTF8.GetString(bytes);
                return utf8;
            }
            set
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                string base64 = Convert.ToBase64String(bytes);
                Pass = base64;
            }
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }
    }
}
