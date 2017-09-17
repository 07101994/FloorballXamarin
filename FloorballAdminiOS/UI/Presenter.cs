using System;
namespace FloorballAdminiOS.UI
{
    public abstract class Presenter<T>
    {
        protected T Screen;

        public virtual void AttachScreen(T screen)
        {
            Screen = screen;
        }

        public virtual void DetachScreen()
        {
            Screen = default(T);
        }
    }
}
