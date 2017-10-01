using System;
using System.Collections.Generic;
using Floorball;

namespace FloorballAdminiOS.UI.EntityChoose
{
    public class EntityChooserPresenter : Presenter<EntityChooserScreen>
    {
		public List<Tuple<UpdateEnum, string>> Entitites { get; set; } = new List<Tuple<UpdateEnum, string>>()
		{
			new Tuple<UpdateEnum, string>(UpdateEnum.League,"League"),
			new Tuple<UpdateEnum, string>(UpdateEnum.Player,"Player"),
			new Tuple<UpdateEnum, string>(UpdateEnum.Team,"Team"),
			new Tuple<UpdateEnum, string>(UpdateEnum.Match,"Match"),
			new Tuple<UpdateEnum, string>(UpdateEnum.Referee,"Referre"),
			new Tuple<UpdateEnum, string>(UpdateEnum.Stadium,"Stadium")
		};

        public EntityChooserPresenter()
        {
        }


    }
}
