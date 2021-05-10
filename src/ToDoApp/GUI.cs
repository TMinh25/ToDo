using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using ToDoApp.Views;

namespace ToDoApp.Models
{
    public class MainViewModel : ViewModelBase
    {
        private BUS bus = new BUS();
       
        public MainViewModel(User _currentUser)
        {
            MenuModels = new ObservableCollection<MenuModel>();
            menuModels.Add(new MenuModel() { IconFont = "\xe755", Title = "Hôm Nay", BackColor = "#E4572E", });
            menuModels.Add(new MenuModel() { IconFont = "\xe6b6", Title = "Quan Trọng", BackColor = "#48ACF0", });
            menuModels.Add(new MenuModel() { IconFont = "\xe6e1", Title = "Kế Hoạch", BackColor = "#2E933C", });
            menuModels.Add(new MenuModel() { IconFont = "\xe614", Title = "Tất Cả", BackColor = "#1E212B", });

            this.CurrentUser = _currentUser;
            this.MenuModel = MenuModels[0];
            foreach (MenuModel mainItem in this.MenuModels)
            {
                mainItem.CurrentUserID = _currentUser.ID;
            }
            Console.WriteLine(this.CurrentUser.ID);

            SelectedCommand = new RelayCommand<MenuModel>(t => Select(t));
            SelectedTaskCommand = new RelayCommand<TaskInfo>(t => SelectedTask(t));
        }

        private ObservableCollection<MenuModel> menuModels;
     
        public ObservableCollection<MenuModel> MenuModels
        {
            get { return menuModels; }
            set { menuModels = value; RaisePropertyChanged(); }
        }

        public RelayCommand<MenuModel> SelectedCommand { get; set; }
   
        public RelayCommand<TaskInfo> SelectedTaskCommand { get; set; }

        private MenuModel menuModel;
     
        public MenuModel MenuModel
        {
            get { return menuModel; }
            set { menuModel = value; RaisePropertyChanged(); }
        }

        private User currentUser = new User();
     
        public User CurrentUser
        {
            get { return (User)currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }

        private TaskInfo info;
    
        public TaskInfo Info
        {
            get { return info; }
            set { info = value; RaisePropertyChanged(); }
        }

        private void Select(MenuModel model)
        {
            MenuModel = model;
        }
       
        private void RefreshTaskInfo()
        {
            this.MenuModel.TaskInfos = this.MenuModel.TaskInfos;
            foreach (MenuModel mainItem in this.MenuModels)
            {
                mainItem.TaskInfos = mainItem.TaskInfos;
                Console.WriteLine(mainItem.TaskInfosCount);
            }
        }

        public void AddTaskInfo(string content, bool isImportant = false, string category = "")
        {
            if (content.Length > 0)
            {
                category = this.MenuModel.Title;
                switch (category.ToLower())
                {
                    case "hôm nay":
                    case "tất cả":
                        category = "";
                        break;
                    case "quan trọng":
                        isImportant = true;
                        break;
                    default:
                        break;
                }
                var task = new TaskInfo(
                    content: content.Trim(),
                    timeCreated: DateTime.Now,
                    status: false,
                    isImportant: isImportant
                );
                task.Category = category;
                this.MenuModel.TaskInfos = bus.addTaskForUser(CurrentUser.ID, task, viewModel: this);
            }
            else
            {
                MessageBox.Show("Hãy nhập thông tin vào nhé");
            }
            RefreshTaskInfo();
        }

        public void UpdateTaskInfo(int taskID, bool status = false, bool isImportant = false, string content = "")
        {
            this.MenuModel.TaskInfos = bus.updateTaskForUser(taskID: taskID, viewModel: this, updateContent: content, taskStatus: status, isImportant: isImportant);
            RefreshTaskInfo();
        }

        public void DeleteTaskInfo(int taskID)
        {
            this.MenuModel.TaskInfos = bus.deleteTaskForUser(taskID, viewModel: this);
            RefreshTaskInfo();
        }

        public void SelectedTask(TaskInfo task)
        {
            Info = task;
            Messenger.Default.Send(task, "Expand");
        }
    }
}