using System;
using System.Collections.Generic;
using Floorball;
using Foundation;

namespace FloorballAdminiOS.Helper
{
    public static class iOSHelper
    {

		public static List<string> GetCountries()
		{
            return GetFromLocalizedString<CountriesEnum>();
		}

		public static List<string> GetLeagueTypes()
		{
            return GetFromLocalizedString<LeagueTypeEnum>();
		}

		public static List<string> GetClasses()
		{
            return GetFromLocalizedString<ClassEnum>();
		}

		public static List<string> GetGenders()
		{
			return GetFromLocalizedString<GenderEnum>();
		}

        private static List<string> GetFromLocalizedString<T>()
        {
            var list = new List<string>();

            foreach (T e in Enum.GetValues(typeof(T)))
			{
				list.Add(NSBundle.MainBundle.LocalizedString(e.ToString(), null));
			}

			return list;
        }

		public static DateTime NSDateToDateTime(NSDate date)
		{
			return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0)).AddSeconds(date.SecondsSinceReferenceDate);
		}

		public static NSDate DateTimeToNSDate(DateTime date)
		{
			return NSDate.FromTimeIntervalSinceReferenceDate((date - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0))).TotalSeconds);
		}

    }
}
