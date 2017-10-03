using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.UI.Entity;

namespace FloorballAdminiOS.UI.Delegate
{
    public interface IDelegate
    {
        List<EntityTableViewModel> GetTableViewModel();

        string GetTableHeader(UpdateType crud);

        Task SetDataFromServer();

        Task Save(List<EntityTableViewModel> model);
    }
}
