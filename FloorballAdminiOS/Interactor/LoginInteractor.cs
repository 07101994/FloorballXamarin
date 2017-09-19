using System;
using System.Threading.Tasks;
using Floorball.REST.RESTHelpers;

namespace FloorballAdminiOS.Interactor
{
    public class LoginInteractor : Interactor
    {

        public async Task LoginAsync(string userName, string password) 
        {
            await Task.Delay(2000);
            //await RESTHelper.LoginAsync(userName,password);
        }

    }
}
