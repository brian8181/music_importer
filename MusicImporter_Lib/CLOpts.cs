// Music Importer imports ID3 tags to a MySql database.
// Copyright (C) 2008  Brian Preston

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using CommandLine.OptParse;
using System.ComponentModel;

namespace MusicImporter.TagLibV
{
    public class ImporterOptions
    {
        [OptDef( OptValType.ValueReq )]
        [ShortOptionName( 'c' )]
        [LongOptionName( "conn_str" )]
        [UseNameAsLongOption( false )]
        [Description( "Connection String" )]
        public string ConnStr = string.Empty;

        [OptDef( OptValType.ValueReq )]
        [ShortOptionName( 'r' )]
        [LongOptionName( "root" )]
        [UseNameAsLongOption( false )]
        [Description( "Root Directory" )]
        public string Root = string.Empty;

        //Flags
        [OptDef( OptValType.Flag )]
        [ShortOptionName( 'p' )]
        [LongOptionName( "playlist" )]
        [UseNameAsLongOption( false )]
        [Description( "Import Playlist" )]
        public bool Playlist = false;

        [ShortOptionName( 'l' )]
        [LongOptionName( "log" )]
        [UseNameAsLongOption( false )]
        [Description( "Log" )]
        public bool Log = false;

        [OptDef( OptValType.Flag )]
        [ShortOptionName( 'd' )]
        [LongOptionName( "clean" )]
        [UseNameAsLongOption( false )]
        [Description( "Delete Orphaned" )]
        public bool Clean = false;

        [OptDef( OptValType.Flag )]
        [ShortOptionName( 'u' )]
        [LongOptionName( "update" )]
        [UseNameAsLongOption( false )]
        [Description( "Update" )]
        public bool Update = false;

        [OptDef( OptValType.Flag )]
        [ShortOptionName( 'a' )]
        [LongOptionName( "art" )]
        [UseNameAsLongOption( false )]
        [Description( "Scan Art" )]
        public bool Art = false;

        [OptDef( OptValType.Flag )]
        [ShortOptionName( 'o' )]
        [LongOptionName( "optimize" )]
        [UseNameAsLongOption( false )]
        [Description( "Optimize Tables" )]
        public bool Optimize = false;

        /// <summary>
        /// coversion to Settings type
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static explicit operator MusicImporter_Lib.Properties.Settings(ImporterOptions options)
        {
            MusicImporter_Lib.Properties.Settings settings = new MusicImporter_Lib.Properties.Settings();
            settings.mysql_conn_str = options.ConnStr;
            settings.ScanTags = options.Update;
            settings.ScanPlaylist = options.Playlist;
            settings.RecanArt = options.Art;
            settings.Log = options.Log;
            settings.Clean = options.Clean;
            settings.Optimize = options.Optimize;
            settings.Dirs.Clear();
            settings.Dirs.Add( options.Root );
            return settings;
        }
    }
}
