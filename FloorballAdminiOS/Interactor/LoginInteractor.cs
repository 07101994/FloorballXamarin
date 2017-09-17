﻿using System;
using System.Threading.Tasks;
using Floorball.REST.RESTHelpers;

namespace FloorballAdminiOS.Interactor
{
    public class LoginInteractor : Interactor
    {

        public async Task LoginAsync(string userName, string password) 
        {
            await RESTHelper.LoginAsync(userName,password);
        }

    }
}
