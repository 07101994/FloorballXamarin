using System;
using CoreGraphics;
using UIKit;

namespace Floorball.iOS
{
	public class UIHelper
	{
		public static UIView MakeImageWithLabel(string imageName, string imageLabel)
		{
			var container = new UIView();

			var label = new UILabel();
			label.Text = imageLabel;
			label.SizeToFit();
			label.Center = container.Center;
			label.TextAlignment = UITextAlignment.Center;

			var image = UIImage.FromBundle(imageName);
			var imageView = new UIImageView(image);

			var imageAspect = image.Size.Width / image.Size.Height;

			imageView.Frame = new CGRect(label.Frame.X - label.Frame.Size.Height* imageAspect, label.Frame.Y, label.Frame.Size.Height* imageAspect, label.Frame.Size.Height);

			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

			label.Frame = new CGRect(label.Frame.X + 5, label.Frame.Y, label.Frame.Size.Width, label.Frame.Size.Height);

			container.AddSubview(imageView);
			container.AddSubview(label);

			container.SizeToFit();

			return container;
		}

		public static UIView MakeImageWithLabelInSectionHeader(int headerHeight, string imageName, string imageLabel)
		{
			var container = new UIView();

			var height = headerHeight - 10;

			var view = new UIView(new CGRect(10, container.Frame.Y + headerHeight / 2 - height / 2, 30, height));
			view.BackgroundColor = UIColor.Blue;

			container.BackgroundColor = new UIColor((float)(155.0 / 256.0), (float)(182.0 / 256.0), (float)(157.0 / 256.0), 1);

			var image = UIImage.FromBundle(imageName);
			var imageView = new UIImageView(image);
			imageView.BackgroundColor = UIColor.Green;
			imageView.Frame = new CGRect(10, container.Frame.Y + headerHeight / 2 - height / 2, height * image.Size.Width / image.Size.Height, height);
			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

			var label = new UILabel();
			label.Frame = new CGRect(5 + imageView.Frame.Width + imageView.Frame.X, container.Frame.Y + headerHeight / 2 - height / 2, 400, height);
			label.Text = imageLabel;
			label.TextAlignment = UITextAlignment.Left;

			container.AddSubview(imageView);
			container.AddSubview(label);

			container.SizeToFit();

			return container;
		}

	}
}
