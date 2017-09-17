using System;
namespace FloorballAdminiOS.UI.Login
{
    public interface LoginScreen
    {
        void LogInInProgress();
        void LoggedIn();
        void LogInFailed();
    }
}
