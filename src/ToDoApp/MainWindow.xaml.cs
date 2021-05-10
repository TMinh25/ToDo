using GalaSoft.MvvmLight.Messaging;
using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp
{
    /// <summary>
    /// MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BUS bus = new BUS();
        private User _currentUser = null;
        public MainWindow(User user)
        {
            InitializeComponent();
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            Messenger.Default.Register<TaskInfo>(this, "Expand", ExpandColumn);
            _currentUser = user;
            MainViewModel mainViewModel = new MainViewModel(_currentUser: user);
            //mainViewModel.CurrentUser = _currentUser;
            this.DataContext = mainViewModel;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string inputValue = inputText.Text;
                if (inputValue == "") return;

                var vm = this.DataContext as MainViewModel;
                vm.AddTaskInfo(inputValue);
                inputText.Text = string.Empty;
            }
        }

        private void ExpandColumn(TaskInfo task)
        {
            var cdf = grc.ColumnDefinitions;
            if (cdf[1].Width == new GridLength(0))
            {
                cdf[1].Width = new GridLength(280);
                btnMinimize.Foreground = new SolidColorBrush(Colors.Black);
                btnMaximize.Foreground = new SolidColorBrush(Colors.Black);
                btnClose.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                cdf[1].Width = new GridLength(100);
                btnMinimize.Foreground = new SolidColorBrush(Colors.Red);
                btnMaximize.Foreground = new SolidColorBrush(Colors.Red);
                btnClose.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void btnMinClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaxClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void btnCloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAddTask_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string inputValue = inputText.Text;
            if (inputValue == "") return;

            var viewModel = this.DataContext as MainViewModel;
            viewModel.AddTaskInfo(inputValue);
            inputText.Text = string.Empty;
        }

        private void MenuItemSignOut_Click(object sender, RoutedEventArgs e)
        {
            bus.signOut();
            this.Close();
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            object taskBind = (object)((CheckBox)sender).Tag;
            int ID = (int)taskBind.GetType().GetProperty("Id").GetValue(taskBind, null);
            bool isImportant = (bool)taskBind.GetType().GetProperty("IsImportant").GetValue(taskBind, null);

            var viewModel = this.DataContext as MainViewModel;
            var newStatus = (bool)((CheckBox)sender).IsChecked;
            viewModel.UpdateTaskInfo(ID, status: newStatus, isImportant: isImportant);
            if (newStatus == true)
            {
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var soundPath = Path.Combine(outPutDirectory, "Assets\\taskDone.wav");
                SoundPlayer player = new SoundPlayer(soundPath);
                player.Play();
            }
        }

        private void CheckBoxImportant_CheckedChanged(object sender, RoutedEventArgs e)
        {
            object taskBind = (object)((CheckBox)sender).Tag;
            int ID = (int)taskBind.GetType().GetProperty("Id").GetValue(taskBind, null);
            bool isDone = (bool)taskBind.GetType().GetProperty("IsStatusDone").GetValue(taskBind, null);
            var viewModel = this.DataContext as MainViewModel;
            var isImportant = (bool)((CheckBox)sender).IsChecked;
            viewModel.UpdateTaskInfo(ID, status: isDone, isImportant: isImportant);
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            var taskBind = (object)((MenuItem)sender).Tag;
            Console.WriteLine(taskBind.ToString());
            int ID = (int)taskBind.GetType().GetProperty("Id").GetValue(taskBind, null);
            string taskContent = (string)taskBind.GetType().GetProperty("Content").GetValue(taskBind, null);
            PromptDialog inputDialog = new PromptDialog("Chỉnh sửa Task:", taskContent);
            if (inputDialog.ShowDialog() == true)
            {
                var res = inputDialog.Answer;
                if (res == "")
                {
                    MessageBox.Show("Hãy nhập thông tin nào", "Trống");
                }
                else
                {
                    var viewModel = this.DataContext as MainViewModel;
                    viewModel.UpdateTaskInfo(ID, content: res);
                }
            }
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            int ID = (int)((MenuItem)sender).Tag;
            Console.WriteLine(ID.ToString());
            var viewModel = this.DataContext as MainViewModel;
            viewModel.DeleteTaskInfo(ID);
        }
    }
}
