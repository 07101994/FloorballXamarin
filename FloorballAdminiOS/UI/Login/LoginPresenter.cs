using System;
using System.Threading.Tasks;
using FloorballAdminiOS.Interactor;
using FloorballPCL;

namespace FloorballAdminiOS.UI.Login
{
    public class LoginPresenter : Presenter<LoginScreen>
    {

        LoginInteractor loginInteractor;

        public LoginPresenter(ITextManager textManager) : base(textManager)
        {
        }

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
