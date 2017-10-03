using System;
using System.Collections.Generic;
using FloorballAdminiOS.UI.Entity;

namespace FloorballAdminiOS.UI.Delegate
{
    public class BaseDelegate
    {
        public List<EntityTableViewModel> Model { get; set; }

        public BaseDelegate()
        {
            Model = new List<EntityTableViewModel>();
        }
    }
}
