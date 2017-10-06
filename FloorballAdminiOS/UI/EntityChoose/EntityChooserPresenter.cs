using System;
using System.Collections.Generic;
using Floorball;
using FloorballAdminiOS.UI.Entity;
using FloorballAdminiOS.UI.Entity.League;
using FloorballAdminiOS.UI.Entity.Match;
using FloorballAdminiOS.UI.Entity.Player;
using FloorballAdminiOS.UI.Entity.Referee;
using FloorballAdminiOS.UI.Entity.Stadium;
using FloorballAdminiOS.UI.Entity.Team;
using FloorballAdminiOS.UI.EntitySearch;

namespace FloorballAdminiOS.UI.EntityChoose
{
    public class EntityChooserPresenter : Presenter<EntityChooserScreen>
    {
		public List<Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>> Entitites { get; set; } = new List<Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>>()
		{
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.League, new LeaguePresenter(), new LeagueSearchPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Player,new PlayerPresenter(), null),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Team,new TeamPresenter(), null),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Match,new MatchPresenter(), null),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Referee,new RefereePresenter(), null),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Stadium,new StadiumPresenter(), null)
		};

    }
}
