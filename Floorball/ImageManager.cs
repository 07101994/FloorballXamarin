using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Floorball
{
    public class ImageManager
    {
        public static void SaveImage(byte[] image, string name)
        {
            string documentsPath = GetImagesPath();

            using (var fs = new BinaryWriter(new FileStream(System.IO.Path.Combine(documentsPath,name), FileMode.Append)))
            {
                fs.Write(image);
            }

        }

        private static string GetImagesPath()
        {
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (!Directory.Exists(System.IO.Path.Combine(directoryPath,"TeamImages")))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }
        
        public static string GetImagePath(string imageName)
        {
            return System.IO.Path.Combine(GetImagesPath(), imageName);
        }

    }
}
