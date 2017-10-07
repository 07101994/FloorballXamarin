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

    public class EntityChooserPresenterModel
    {
        public UpdateEnum UpdateEnum { get; set; }
        public EntityPresenter<EntityScreen> EntityPresenter { get; set; }
        public EntitySearchPresenter<EntitySearchScreen> EntitySearchPresenter { get; set; }

    }

    public class EntityChooserPresenter : Presenter<EntityChooserScreen>
    {
		/*public List<Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>> Entitites { get; set; } = new List<Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>>()
		{
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.League, new LeaguePresenter(), new LeagueSearchPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Player,new PlayerPresenter(), new PlayerSearchPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Team,new TeamPresenter(), new TeamSearchPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Match,new MatchPresenter(), new MatchSearchPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Referee,new RefereePresenter(), new RefereeSearchPresenter()),
			new Tuple<UpdateEnum, EntityPresenter<EntityScreen>, EntitySearchPresenter<EntitySearchScreen>>(UpdateEnum.Stadium,new StadiumPresenter(), new StadiumSearchPresenter())
		};*/

		public List<EntityChooserPresenterModel> Entitites { get; set; } = new List<EntityChooserPresenterModel>
		{
			new EntityChooserPresenterModel
            {
                UpdateEnum = UpdateEnum.League,
                EntityPresenter = new LeaguePresenter(),
                EntitySearchPresenter = new LeagueSearchPresenter()
            },
			new EntityChooserPresenterModel
			{
				UpdateEnum = UpdateEnum.Player,
				EntityPresenter = new PlayerPresenter(),
				EntitySearchPresenter = new PlayerSearchPresenter()
			},
			new EntityChooserPresenterModel
			{
				UpdateEnum = UpdateEnum.Team,
				EntityPresenter = new TeamPresenter(),
				EntitySearchPresenter = new TeamSearchPresenter()
			},
			new EntityChooserPresenterModel
			{
				UpdateEnum = UpdateEnum.Match,
				EntityPresenter = new MatchPresenter(),
				EntitySearchPresenter = new MatchSearchPresenter()
			},
			new EntityChooserPresenterModel
            {
                UpdateEnum = UpdateEnum.Referee,
				EntityPresenter = new RefereePresenter(),
				EntitySearchPresenter = new RefereeSearchPresenter()
			},
			new EntityChooserPresenterModel
			{
				UpdateEnum = UpdateEnum.Stadium,
				EntityPresenter = new StadiumPresenter(),
				EntitySearchPresenter = new StadiumSearchPresenter()
			}
			
		};

    }
}
