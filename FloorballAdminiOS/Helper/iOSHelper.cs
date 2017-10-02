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
			var countries = new List<string>();

			foreach (CountriesEnum country in Enum.GetValues(typeof(CountriesEnum)))
			{
				countries.Add(NSBundle.MainBundle.LocalizedString(country.ToString().ToLower(), null));
			}

			return countries;
		}
    }
}
