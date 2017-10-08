using System;
using Android.Content;
using Android.Content.Res;
using FloorballPCL;

namespace Floorball.Droid.Utils
{
    public class TextManager : ITextManager
    {

        public Context Ctx { get; set; }

        public TextManager(Context context)
        {
            
        }

        public string GetText(string key)
        {
            return Ctx.GetString(Ctx.Resources.GetIdentifier(key,"string",Ctx.PackageName));
        }

        public string GetText(Enum key)
        {
            return GetText(key.ToString());
        }
    }
}
