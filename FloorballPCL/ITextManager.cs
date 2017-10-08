using System;
namespace FloorballPCL
{
    public interface ITextManager
    {
        string GetText(string key);

        string GetText(Enum key);
    }
}
