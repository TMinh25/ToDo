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
    public User() { }

    public User(string displayName, Image avatar)
    {
      this.DisplayName = displayName;
      this.Avatar = avatar;
    }

    public int ID { get; set; }

    public string DisplayName { get; set; }

    internal Image Avatar { get; set; }

    internal string UserName { get; set; }

    internal string PassWord { get; set; }

    public BitmapSource DisplayImage
    {
      get
      {
        if (this.Avatar == null)
        {
          var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
          var unknownUserPath = Path.Combine(outPutDirectory, "Assets\\Images\\unknownUser.jpg");
          BitmapSource unknownUserSource = new BitmapImage(new Uri(new Uri(unknownUserPath).LocalPath));
          return unknownUserSource;
        }
        else
          return ImgConverter.GetAvatarBitSource(Avatar);
      }
    }
  }
}
