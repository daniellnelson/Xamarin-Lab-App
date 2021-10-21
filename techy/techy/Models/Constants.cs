using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace techy.Models
{
    public static class Constants
    {
        public const string DatabaseFilename = "techy.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        private static TimeSpan globalTimeSpan = new TimeSpan(0, 2, 0);
        public static TimeSpan GlobalTimeOut
        {
            get
            {
                return globalTimeSpan;
            }
            set { globalTimeSpan = value; }
        }


        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
