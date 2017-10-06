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

    public static class CountriesEnumExtensions
    {
        public static string ToFriendlyString(this CountriesEnum country)
        {
            switch (country)
            {
                case CountriesEnum.HU:
                    return "Hungary";
                case CountriesEnum.SE:
                    return "Sweden";
                case CountriesEnum.FL:
                    return "Finnland";
                case CountriesEnum.SW:
                    return "Switzerland";
                case CountriesEnum.CZ:
                    return "Checz Republic";
                default:
                    return "";
            }
        }

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
}

