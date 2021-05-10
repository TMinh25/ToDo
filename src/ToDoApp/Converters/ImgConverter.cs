using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ToDoApp.Converters
{
    class ImgConverter
    {
        public static byte[] ConvertImageToBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static Image ConvertByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        public static BitmapSource ConvertImageToBitSource(Image image)
        {
            var bitmap = new Bitmap(image);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                                         IntPtr.Zero,
                                                                         Int32Rect.Empty,
                                                                         BitmapSizeOptions.FromEmptyOptions());
            return bitmapSource;
        }

        public static BitmapSource GetAvatarBitSource(Image image)
        {
            var bitmap = new Bitmap(image);
            var avatarImage = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                                         IntPtr.Zero,
                                                                         Int32Rect.Empty,
                                                                         BitmapSizeOptions.FromEmptyOptions());
            return avatarImage;
        }
    }
}
