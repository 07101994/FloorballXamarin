using Floorball.LocalDB.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Interop;
using FloorballPCL;
using FloorballPCL.LocalDB;

namespace Floorball
{
    public class UnitOfWork
    {
        internal static ISQLitePlatform Platform { get; set; }
        internal static string DatabasePath { get; set; }

        public static IDBManager DBManager { get; set; }
        public static IImageManager ImageManager { get; set; }

        public EventRepository EventRepo { get; set; }

        public LeagueRepository LeagueRepo { get; set; }

        public MatchRepository MatchRepo { get; set; }

        public PlayerRepository PlayerRepo { get; set; }

        public RefereeRepository RefereeRepo { get; set; }

        public TeamRepository TeamRepo { get; set; }

        public EventMessageRepository EventMessageRepo { get; set; }

        public StatisticRepository StatiscticRepo { get; set; }

        public StadiumRepository StadiumRepo { get; set; }

        public UnitOfWork()
        {

            Platform = DBManager.GetPlatform();
            DatabasePath = DBManager.GetDatabasePath();

            TeamRepo = new TeamRepository(Platform, DatabasePath,ImageManager);
            EventRepo = new EventRepository(Platform, DatabasePath);
            EventMessageRepo = new EventMessageRepository(Platform, DatabasePath);
            MatchRepo = new MatchRepository(Platform, DatabasePath);
            PlayerRepo = new PlayerRepository(Platform, DatabasePath);
            StadiumRepo = new StadiumRepository(Platform, DatabasePath);
            StatiscticRepo = new StatisticRepository(Platform, DatabasePath);
            LeagueRepo = new LeagueRepository(Platform, DatabasePath);
            RefereeRepo = new RefereeRepository(Platform, DatabasePath);

        }

    }
}
