using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicImporter_Lib
{
    public interface ISql
    {

        string INSERT_artist
        {
            get;
        }

        string INSERT_album
        {
            get;
        }

        string INSERT_song
        {
            get;
        }

        string INSERT_art
        {
            get;
        }

        string INSERT_art_link
        {
            get;
        }

        string UPDATE_song
        {
            get;
        }

        string UPDATE_album
        {
            get;
        }

        string SELECT_last_update
        {
            get;
        }

        string GRANT_default_user
        {
            get;
        }
    }
}
