using System;
namespace Floorball.REST.RESTHelpers
{
    public abstract class RESTHelperBase
    {
		protected static IRESTManager Network;

		static RESTHelperBase()
		{
			Network = new RESTManager();
		}
    }
}
