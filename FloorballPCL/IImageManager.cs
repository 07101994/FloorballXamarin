using System;
namespace FloorballPCL
{
    public interface IImageManager
    {
        void SaveImage(byte[] image, string name);
        string GetImagesPath();
        string GetImagePath(string imageName);
    }
}
