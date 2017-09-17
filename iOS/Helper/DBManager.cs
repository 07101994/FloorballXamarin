using System;
using System.IO;
using FloorballPCL.LocalDB;
using SQLite.Net.Interop;

namespace Floorball.iOS.Helper
{
    public class DBManager : IDBManager
    {

        public string GetDatabasePath()
        {
            var sqliteFilename = "Floorball.db3";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            return Path.Combine(libraryPath, sqliteFilename);
		}

        public ISQLitePlatform GetPlatform()
        {
            return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
        }
    }
}
