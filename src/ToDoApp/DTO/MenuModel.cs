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
using ToDoApp.Control;

namespace ToDoApp.DTO
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

        private ObservableCollection<Task> _taskInfos = new ObservableCollection<Task>();

        public ObservableCollection<Task> TaskInfos
        {
            get
            {
                BLL bus = new BLL();
                return bus.getTaskForMenuModel(Title, CurrentUserID, SearchString);
            }
            set { _taskInfos = value; RaisePropertyChanged(); }
        }

        public object ModifyTag { get { return new { ID, Title }; } }
    }
}


