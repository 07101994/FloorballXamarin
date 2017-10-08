using System;
using FloorballPCL;
using Foundation;

namespace FloorballAdminiOS.Helper
{
    public class TextManager : ITextManager
    {
        
        public string GetText(string key)
        {
            return NSBundle.MainBundle.LocalizedString(key, "");
        }

        public string GetText(Enum key)
        {
            return GetText(key.ToString());
        }
    
    }
}
