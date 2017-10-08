using System;
using System.Collections.Generic;
using System.Linq;

namespace Floorball
{
    public enum CountriesEnum
    {
        HU, SE, FL, SW, CZ
    }

    public enum StateEnum
    {
        Confirmed,Playing,Ended
    }

    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    public enum UpdateEnum
    {
        League, Team, Match, Player, Stadium, Referee, Event, EventMessage,
        PlayerTeam, PlayerMatch, RefereeMatch
    }

    public enum UpdateType
    {
        Create, Update, Delete
    }

    public enum LeagueTypeEnum
    {
        League, Cup, PlayOff
    }

    public enum ClassEnum
    {
        FirstClass, SecondClass, ThirdClass, U21, U19, U17, U15, U13, U11, U9
    }

    public enum GenderEnum
    {
        Men, Women
    }

	public enum EventType
	{
		G, A, P2, P5, P10, PV, T, S
	}

	public enum StatType
	{
		G, A, P2, P5, P10, PV, M
	}
}

