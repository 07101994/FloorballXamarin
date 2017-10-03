using System;
using System.Collections.Generic;
using Floorball;
using FloorballAdminiOS.UI.Delegate;

namespace FloorballAdminiOS.UI.EntityChoose
{
    public class EntityChooserPresenter : Presenter<EntityChooserScreen>
    {
		public List<Tuple<UpdateEnum, IDelegate>> Entitites { get; set; } = new List<Tuple<UpdateEnum, IDelegate>>()
		{
			new Tuple<UpdateEnum, IDelegate>(UpdateEnum.League,new LeagueDelegate()),
			new Tuple<UpdateEnum, IDelegate>(UpdateEnum.Player,new LeagueDelegate()),
			new Tuple<UpdateEnum, IDelegate>(UpdateEnum.Team,new LeagueDelegate()),
			new Tuple<UpdateEnum, IDelegate>(UpdateEnum.Match,new MatchDelegate()),
			new Tuple<UpdateEnum, IDelegate>(UpdateEnum.Referee,new LeagueDelegate()),
			new Tuple<UpdateEnum, IDelegate>(UpdateEnum.Stadium,new LeagueDelegate())
		};

    }
}
