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

namespace FloorballAdminiOS.UI.EntityChoose
{
    public class EntityChooserPresenter : Presenter<EntityChooserScreen>
    {
		public List<Tuple<UpdateEnum, EntityPresenter<EntityScreen>>> Entitites { get; set; } = new List<Tuple<UpdateEnum, EntityPresenter<EntityScreen>>>()
		{
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>>(UpdateEnum.League,new LeaguePresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>>(UpdateEnum.Player,new PlayerPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>>(UpdateEnum.Team,new TeamPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>>(UpdateEnum.Match,new MatchPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>>(UpdateEnum.Referee,new RefereePresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>>(UpdateEnum.Stadium,new StadiumPresenter())
		};

    }
}
