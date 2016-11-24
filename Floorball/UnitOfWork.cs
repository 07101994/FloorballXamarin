using Floorball.LocalDB.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball
{
    public class UnitOfWork
    {

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
            TeamRepo = new TeamRepository();
            EventRepo = new EventRepository();
            EventMessageRepo = new EventMessageRepository();
            MatchRepo = new MatchRepository();
            PlayerRepo = new PlayerRepository();
            StadiumRepo = new StadiumRepository();
            StatiscticRepo = new StatisticRepository();
            LeagueRepo = new LeagueRepository();
            RefereeRepo = new RefereeRepository();

        }

    }
}
