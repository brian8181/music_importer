﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace MusicImporter_Lib
{
    class TagLibExt
    {
        /// <summary>
        /// get the sha1 of media file data (not including tag info)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] MediaSHA1(TagLib.File file)
        {
            try
            {
                int len = (int)file.InvariantEndPosition - (int)file.InvariantStartPosition;
                file.Seek(file.InvariantStartPosition);
                //
                TagLib.ByteVector vector = file.ReadBlock(len);
                // 
                byte[] data = vector.Data;
                return Utility.CryptoFunctions.SHA1(data);
            }
            catch (OutOfMemoryException e)
            {
                Trace.WriteLine("OutOfMemoryException - " + file, Utility.Logger.Level.Error.ToString());
                return null;
            }
    
        }
    }
}
