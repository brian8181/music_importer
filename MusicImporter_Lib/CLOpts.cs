// Music Importer imports ID3 tags to a MySql database.
// Copyright (C) 2008  Brian Preston

using System;
using System.Collections.Generic;
using System.Text;
using CommandLine.OptParse;
using System.ComponentModel;

namespace MusicImporter_Lib
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
