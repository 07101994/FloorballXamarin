using System;
using System.IO;
using FloorballPCL.LocalDB;
using SQLite.Net.Interop;

namespace Floorball.Droid.Utils
{
    public class DBManager : IDBManager
    {
        public string GetDatabasePath()
        {

            var sqliteFilename = "Floorball.db3";

            string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder

            return Path.Combine(documentsPath, sqliteFilename);
		}

        public ISQLitePlatform GetPlatform()
        {
			return new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
		}
    }
}
