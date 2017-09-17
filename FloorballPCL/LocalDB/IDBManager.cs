using System;
using SQLite.Net.Interop;

namespace FloorballPCL.LocalDB
{
    public interface IDBManager
    {
        string GetDatabasePath();
        ISQLitePlatform GetPlatform();
    }
}
