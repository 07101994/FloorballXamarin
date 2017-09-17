using System;
using System.Threading.Tasks;
using UIKit;

namespace FloorballAdminiOS.UI.Login
{
    public partial class LoginViewController : UIViewController, LoginScreen
    {

        public LoginPresenter LoginPresenter { get; set; }

        public LoginViewController() : base("LoginViewController", null)
        {
        }

		public LoginViewController(IntPtr handle) : base(handle)
        {

		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            LoginPresenter = new LoginPresenter();

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            LoginPresenter.AttachScreen(this);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            LoginPresenter.DetachScreen();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async partial void LoginTap(UIButton sender)
        {
            try
            {
				var task = LoginPresenter.LoginAsync(TFUserName.Text, TFPassword.Text);
				LoginView.Hidden = false;
				await task;
				LoginView.Hidden = true;
			}
            catch (Exception ex)
            {
                LoginView.Hidden = true;
                AppDelegate.SharedAppDelegate.ShowErrorMessage(ex.Message);
            }

        }

        public void LogInInProgress()
        {
            throw new NotImplementedException();
        }

        public void LoggedIn()
        {
            LoginView.Hidden = true;
        }

        public void LogInFailed()
        {
            LoginView.Hidden = false;
        }
    }
}

