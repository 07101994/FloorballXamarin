using System;
using System.Threading.Tasks;
using FloorballAdminiOS.Interactor;

namespace FloorballAdminiOS.UI.Login
{
    public class LoginPresenter : Presenter<LoginScreen>
    {

        LoginInteractor loginInteractor;

        public override void AttachScreen(LoginScreen screen)
        {
            base.AttachScreen(screen);

            loginInteractor = new LoginInteractor();

        }

        public override void DetachScreen()
        {
            base.DetachScreen();

        }


        public async Task LoginAsync(string userName, string password) 
        {
            var loginTask = loginInteractor.LoginAsync(userName, password);

            await loginTask;
        }

    }
}
