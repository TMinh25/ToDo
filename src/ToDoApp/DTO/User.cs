using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using ToDoApp.Converters;

namespace ToDoApp.DTO
{
    public class User
    {
        public User(string displayName, Image avatar, string fileName)
        {
            this.displayName = displayName;
            this.avatar = avatar;
            this.fileName = fileName;
        }

        public User() { }

        public BitmapSource DisplayImage
        {
            get
            {
                if (this.avatar == null)
                {
                    var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                    var unknownUserPath = Path.Combine(outPutDirectory, "Assets\\Images\\unknownUser.jpg");
                    BitmapSource unknownUserSource = new BitmapImage(new Uri(new Uri(unknownUserPath).LocalPath));
                    return unknownUserSource;
                } else
                {
                    return ImgConverter.GetAvatarBitSource(avatar);
                }
            }
        }

        public int ID { get; set; }

        public string displayName { get; set; }

        public Image avatar { get; }

        public string fileName { get; set; }
    }
}
