using System;
using System.IO;
using FloorballPCL;

namespace FloorballAdminiOS.Helper
{
    public class ImageManager : IImageManager
    {
		public string GetImagePath(string imageName)
		{
			return imageName == null ? "" : Path.Combine(GetImagesPath(), imageName);
		}

		public string GetImagesPath()
		{
			string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

			if (!Directory.Exists(Path.Combine(directoryPath, "TeamImages")))
			{
				Directory.CreateDirectory(directoryPath);
			}

			return directoryPath;
		}

		public void SaveImage(byte[] image, string name)
		{
			string documentsPath = GetImagesPath();

			using (var fs = new BinaryWriter(new FileStream(Path.Combine(documentsPath, name), FileMode.Append)))
			{
				fs.Write(image);
			}
		}
    }
}
