using System;
using FloorballPCL;

namespace FloorballAdminiOS.UI
{
    public abstract class Presenter<T>
    {
        public ITextManager TextManager { get; set; }

        protected T Screen;

        protected Presenter(ITextManager textManager)
        {
            TextManager = textManager;
        }

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
