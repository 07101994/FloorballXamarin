using System;
using Floorball;
using Floorball.REST.RESTManagers;
using FloorballPCL.LocalDB;

namespace FloorballAdminiOS.Interactor
{
    public class Interactor
    {
        protected IRESTManager Network { get; set; }
        protected UnitOfWork UoW { get; set; }

        public Interactor()
        {
            Network = AppDelegate.SharedAppDelegate.Network;
            UoW = AppDelegate.SharedAppDelegate.UoW;
        }

}
}
