using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Floorball.LocalDB.Repository
{
    public abstract class Repository
    {

        protected ISQLitePlatform Platform { get; set; }
        protected string DatabasePath { get; set; }

        public Repository(ISQLitePlatform platform, string databasePath)
        {
            Platform = platform;            
            DatabasePath = databasePath;
        }

    }
}
