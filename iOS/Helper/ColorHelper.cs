using System;
using UIKit;

namespace Floorball.iOS
{
	public static class ColorHelper
	{

		public static UIColor FromHex(this UIColor color, int hexValue, double alpha = 1.0)
		{
			return new UIColor((((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
				(((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
				(((float)(hexValue & 0xFF)) / 255.0f),
			                   (float)alpha);
			/*return UIColor.FromRGB(
				(((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
				(((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
				(((float)(hexValue & 0xFF)) / 255.0f)
			);		*/	
		}


		public static UIColor FromRgb(this UIColor color, int red, int green, int blue)
		{
			return UIColor.FromRGB(
				red / 255.0f,
				green / 255.0f,
				blue / 255.0f);	

		}


	}
}
