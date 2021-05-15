using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using ToDoApp.Converters;
using ToDoApp.Views;

namespace ToDoApp.Models
{
    public class MenuModel : ViewModelBase
    {
        public int ID { get; set; }
        public string SearchString { get; set; }
        public string IconFont { get; set; }
        public string Title { get; set; }
        public string BackColor { get; set; }
        public int CurrentUserID { get; set; }
        public bool Display { get; set; } = true;

        private ObservableCollection<TaskInfo> _taskInfos = new ObservableCollection<TaskInfo>();

        public ObservableCollection<TaskInfo> TaskInfos
        {
            get
            {
                BUS bus = new BUS();
                // Console.WriteLine("Model: UserID: " + CurrentUserID);
                var result = new ObservableCollection<TaskInfo>();
                switch (Title.ToLower())
                {
                    case "hôm nay":
                        result = bus.getTaskListOfToday(userID: CurrentUserID);
                        break;
                    case "tất cả":
                        result = bus.getTaskListOfUser(userID: CurrentUserID);
                        break;
                    case "quan trọng":
                        result = bus.getImportantTaskList(userID: CurrentUserID);
                        break;
                    case "hoàn thành":
                        result = bus.getDoneTaskList(userID: CurrentUserID);
                        break;
                    default:
                        result = bus.getTaskListOfUser(userID: CurrentUserID, category: Title);
                        break;
                }
                if (SearchString != "" && SearchString != null)
                {
                    string lower = SearchString.ToLower();
                    string unSign = LanguageConverter.convertToUnSign(SearchString).ToLower();


                    var taskFiltered = from task in result
                                       let content = task.Content.ToLower()
                                       let contentUnsign = LanguageConverter.convertToUnSign(task.Content.ToLower())
                                       where content.Contains(lower)
                                             || contentUnsign.Contains(unSign)
                                       select task;
                    result = new ObservableCollection<TaskInfo>(taskFiltered.Cast<TaskInfo>());
                }
                return new ObservableCollection<TaskInfo>(result.OrderBy(o => o.IsStatusDone));
            }
            set { _taskInfos = value; RaisePropertyChanged(); }
        }

        public object ModifyTag { get { return new { ID, Title }; } }
    }

    public class TaskInfo
    {
        public TaskInfo(string content, DateTime timeCreated, bool status, bool isImportant)
        {
            Content = content;
            TimeCreated = timeCreated;
            IsStatusDone = status;
            IsImportant = isImportant;
        }

        public string getTimeString(DateTime time)
        {
            DateTime resDate;
            if (!DateTime.TryParseExact(time.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out resDate))
            {
                //Invalid date
                //log , show error
                return "";
            }
            if (DateTime.Today == resDate)
            {
                return "Hôm nay, " + time.ToString("hh:mm tt");
            }
            else
            {
                return time.ToString();
            }
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool IsStatusDone { get; set; }

        public bool IsImportant { get; set; }

        public string TimeString { get => getTimeString(TimeCreated); }

        public object BindingCommand
        {
            get
            {
                return new
                {
                    Id = this.Id,
                    Content = this.Content,
                };
            }
        }

        public object BooleanField
        {
            get
            {
                return new
                {
                    Id = this.Id,
                    IsStatusDone = this.IsStatusDone,
                    IsImportant = this.IsImportant
                };
            }
        }
    }

    public class User
    {
        public User(string displayName, Image avatar, string fileName)
        {
            this.displayName = displayName;
            this.avatar = avatar;
            this.fileName = fileName;
        }

        public User() { }

        public BitmapSource displayImage
        {
            get
            {
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var unknownUserPath = Path.Combine(outPutDirectory, "Assets\\Images\\unknownUser.jpg");
                BitmapSource unknownUserSource = new BitmapImage(new Uri(new Uri(unknownUserPath).LocalPath));
                return this.avatar == null ? unknownUserSource : ImgConverter.GetAvatarBitSource(avatar);
            }
        }

        public int ID { get; set; }

        public string displayName { get; set; }

        public Image avatar { get; }

        public string fileName { get; set; }
    }
}


