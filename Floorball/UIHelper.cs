﻿using System;
namespace Floorball
{
	public class UIHelper
	{
		public static string GetMatchTime(TimeSpan time, StateEnum state)
		{
			if (state == StateEnum.Ended || state == StateEnum.Playing)
			{
				if (time.Hours == 1)
				{
					return "3.\n60:00";
				}

				return GetPeriod(time) + ".\n" + GetTimeInPeriod(time);
			}

			return "";
		}

		public static string GetTimeInPeriod(TimeSpan time)
		{
			string str = "";

			int minutes = time.Minutes % 20;
			if (minutes < 10)
			{
				str += "0" + minutes;
			}
			else
			{
				str += minutes;
			}

			str += ":";

			int seconds = time.Seconds;
			if (seconds < 10)
			{
				str += "0" + seconds;
			}
			else
			{
				str += seconds;
			}

			return str;
		}

		public static string GetPeriod(TimeSpan time)
		{
			return (time.Minutes / 20 + 1).ToString();
		}

        public static string GetMatchFullTime(TimeSpan time)
        {
            string str = "";

            if (time.Minutes < 10)
            {
                str += "0" + time.Minutes;
            }
            else
            {
                str += time.Minutes;
            }

            str += ":";

            if (time.Seconds < 10)
            {
                str += "0" + time.Seconds;
            }
            else
            {
                str += time.Seconds;
            }

            return str;
        }

	}
}
