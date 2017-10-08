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
using FloorballPCL;

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

        public List<EntityChooserPresenterModel> Entities { get; set; }

        public EntityChooserPresenter(ITextManager textManager) : base(textManager)
        {
			Entities = new List<EntityChooserPresenterModel>
    		{
    			new EntityChooserPresenterModel
    			{
    				UpdateEnum = UpdateEnum.League,
    				EntityPresenter = new LeaguePresenter(TextManager),
    				EntitySearchPresenter = new LeagueSearchPresenter(TextManager)
    			},
    			new EntityChooserPresenterModel
    			{
    				UpdateEnum = UpdateEnum.Player,
    				EntityPresenter = new PlayerPresenter(TextManager),
    				EntitySearchPresenter = new PlayerSearchPresenter(TextManager)
    			},
    			new EntityChooserPresenterModel
    			{
    				UpdateEnum = UpdateEnum.Team,
    				EntityPresenter = new TeamPresenter(TextManager),
    				EntitySearchPresenter = new TeamSearchPresenter(TextManager)
    			},
    			new EntityChooserPresenterModel
    			{
    				UpdateEnum = UpdateEnum.Match,
    				EntityPresenter = new MatchPresenter(TextManager),
    				EntitySearchPresenter = new MatchSearchPresenter(TextManager)
    			},
    			new EntityChooserPresenterModel
    			{
    				UpdateEnum = UpdateEnum.Referee,
    				EntityPresenter = new RefereePresenter(TextManager),
    				EntitySearchPresenter = new RefereeSearchPresenter(TextManager)
    			},
    			new EntityChooserPresenterModel
    			{
    				UpdateEnum = UpdateEnum.Stadium,
    				EntityPresenter = new StadiumPresenter(TextManager),
    				EntitySearchPresenter = new StadiumSearchPresenter(TextManager)
    			}

    		};

        }


    }
}
