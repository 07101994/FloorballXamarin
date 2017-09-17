using System;
namespace Floorball
{
	public class RefereeStatModel
	{

        public string LeagueName { get; set; }

        public DateTime Year { get; set; }

        public int NumberOfMatches { get; set; }

		public int TwoMinutesPenalties { get; set; }

		public int FiveMinutesPenalties { get; set; }

		public int TenMinutesPenalties { get; set; }

		public int FinalPenalties { get; set; }

        public string LeagueYear
        {
            get
            {
                return Year.Year.ToString() + " - " + (Year.Year + 1).ToString();
            }
        }

    }
}
