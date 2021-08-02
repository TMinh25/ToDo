using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using ToDoApp.DTO;
using ToDoApp.Control;

namespace ToDoApp.View
{
    public class MainViewModel : ViewModelBase
    {
        private BLL bus = new BLL();

        public MainViewModel(User _currentUser)
        {
            MenuModels = new ObservableCollection<MenuModel>()
            {
                new MenuModel() { IconFont = "\xe755", Title = "Hôm Nay", BackColor = "#E4572E", },
                new MenuModel() { IconFont = "\xe6b6", Title = "Quan Trọng", BackColor = "#48ACF0", },
                new MenuModel() { IconFont = "\xe6e1", Title = "Hoàn Thành", BackColor = "#2E933C", },
                new MenuModel() { IconFont = "\xe614", Title = "Tất Cả", BackColor = "#1E212B", }
            };

            CustomMenuModels = new ObservableCollection<MenuModel>();
            customMenuModels = bus.getCustomMenu(_currentUser.ID);

            CurrentUser = _currentUser;
            MenuModel = MenuModels[0];
            Refresh(_currentUser.ID);
            //Console.WriteLine(this.CurrentUser.ID);

            SelectedCommand = new RelayCommand<MenuModel>(t => Select(t));
            SelectedTaskCommand = new RelayCommand<Task>(t => SelectedTask(t));
        }

        private ObservableCollection<MenuModel> menuModels;

        public ObservableCollection<MenuModel> MenuModels
        {
            get { return menuModels; }
            set { menuModels = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<MenuModel> customMenuModels;

        public ObservableCollection<MenuModel> CustomMenuModels
        {
            get { return customMenuModels; }
            set { customMenuModels = value; RaisePropertyChanged(); }
        }

        public RelayCommand<MenuModel> SelectedCommand { get; set; }

        public RelayCommand<Task> SelectedTaskCommand { get; set; }

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

        private Task info;

        public Task Info
        {
            get { return info; }
            set { info = value; RaisePropertyChanged(); }
        }

        private void Select(MenuModel model)
        {
            MenuModel = model;
            SearchTaskInfos("");
            Refresh();
        }

        public void AddCustomMenu(string title, string icon, string backColor)
        {
            var menu = new MenuModel() { Title = title, IconFont = icon, BackColor = backColor };
            this.CustomMenuModels = bus.createCustomMenu(CurrentUser.ID, menu, this);
            Refresh(CurrentUser.ID);
            Select(MenuModels[0]);
        }

        public void UpdateCustomMenu(int menuID, string title)
        {
            this.CustomMenuModels = bus.updateCustomMenu(currentUser.ID, menuID, title);
            Refresh();
            Select(MenuModels[0]);
        }

        public void DeleteCustomMenu(int menuID)
        {
            this.CustomMenuModels = bus.deleteCustomMenu(currentUser.ID, menuID);
            Refresh();
            Select(MenuModels[0]);
        }

        public void Refresh(int id = 0)
        {
            if (this.MenuModel != null)
            {
                this.MenuModel.TaskInfos = this.MenuModel.TaskInfos;
            }
            foreach (MenuModel mainItem in this.MenuModels)
            {
                mainItem.TaskInfos = mainItem.TaskInfos;
                // Console.WriteLine(mainItem.TaskInfosCount);
            }
            if (id != 0)
            {
                foreach (MenuModel mainItem in this.MenuModels)
                {
                    mainItem.CurrentUserID = id;
                    mainItem.SearchString = "";
                }
                foreach (MenuModel customItem in this.CustomMenuModels)
                {
                    customItem.CurrentUserID = id;
                    customItem.SearchString = "";
                }
            }
        }

        public void SearchTaskInfos(string searchText)
        {
            this.MenuModel.SearchString = searchText;
            Refresh();
        }

        public void AddTaskInfo(string content, bool isImportant = false, string category = "")
        {
            if (content.Length > 0)
            {
                if (this.MenuModel?.Title != null)
                    category = this.MenuModel.Title;
                bool isComplete = false;
                switch (category.ToLower())
                {
                    case "hôm nay":
                    case "tất cả":
                        category = "";
                        break;
                    case "quan trọng":
                        category = "";
                        isImportant = true;
                        break;
                    case "hoàn thành":
                        category = "";
                        isComplete = true;
                        break;
                    default:
                        break;
                }
                var task = new Task(
                    content: content.Trim(),
                    timeCreated: DateTime.Now,
                    status: false,
                    isImportant: isImportant
                );
                task.Category = category;
                task.IsStatusDone = isComplete;
                this.MenuModel.TaskInfos = bus.addTaskForUser(CurrentUser.ID, task, viewModel: this);
            }
            else
                new PromptDialog("Hãy nhập thông tin vào nhé", promptInput: false, cancelButton: false).ShowDialog();
            Refresh();
        }

        public void UpdateTaskInfo(int taskID, bool status = false, bool isImportant = false, string content = "")
        {
            this.MenuModel.TaskInfos = bus.updateTaskForUser(taskID: taskID, viewModel: this, updateContent: content, taskStatus: status, isImportant: isImportant);
            Refresh();
        }

        public void DeleteTaskInfo(int taskID)
        {
            this.MenuModel.TaskInfos = bus.deleteTaskForUser(taskID, viewModel: this);
            Refresh();
        }

        public void SelectedTask(Task task)
        {
            Info = task;
            Messenger.Default.Send(task, "Expand");
        }
    }
}