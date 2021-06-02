using Microsoft.Win32;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ToDoApp.Converters;
using ToDoApp.DTO;
using ToDoApp.Control;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class Profile : Window
    {
        BLL bus = new BLL();
        public string DisplayName { get; set; }
        public BitmapSource DisplayImage { get; set; }
        private Image _image = null;
        private Image Avatar { get { return _image; } set { _image = value; } }
        public string username { get; set; }
        public string password { get; set; }
        public string rePassword { get; set; }
        private string fileName = null;
        private MainWindow mainWindow = null;

        public User CurrentUser { get; set; }

        public Profile(MainWindow window, User user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.CurrentUser = user;
            this.DisplayName = user.DisplayName;
            this.DisplayImage = user.DisplayImage;
            this.username = user.UserName;
            this.password = user.PassWord;
            this.rePassword = user.PassWord;
            this.mainWindow = window;
        }

        private int isValidPassword(string password)
        {
            if (password.Length < 8)
            {
                return -1;
            }
            if (new Regex("[A-Z]").Matches(password).Count == 0)
            {
                return -2;
            }
            if (new Regex("[a-z]").Matches(password).Count == 0)
            {
                return -3;
            }
            return 1;
        }
        
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = false };
            if (openFileDialog.ShowDialog() == true)
            {
                this.Avatar = Image.FromFile(openFileDialog.FileName);
                fileName = openFileDialog.FileName;
                imgAvatarSelection.ImageSource = ImgConverter.ConvertImageToBitSource(this.Avatar);
            }
        }

        private void UpdateAccount()
        {
            if (password == string.Empty || rePassword == string.Empty || DisplayName == string.Empty)
            {
                txtErrorMessage.Text = "Hãy nhập đầy đủ thông tin";
            }
            else if (password != rePassword)
            {
                txtErrorMessage.Text = "Xác minh mật khẩu không chính xác!";
            }
            else if (password == rePassword)
            {
                switch (isValidPassword(password))
                {
                    case -1:
                        txtErrorMessage.Text = "Hãy nhập mật khẩu dài hơn 8 kí tự!";
                        break;
                    case -2:
                        txtErrorMessage.Text = "Mật khẩu phải có ít nhất 1 kí tự viết hoa!";
                        break;
                    case -3:
                        txtErrorMessage.Text = "Mật khẩu phải có ít nhất 1 kí tự viết thường!";
                        break;
                    case 1:
                        if (bus.updateAccount(CurrentUser.ID, this.password, this.DisplayName, this.fileName, this.Avatar))
                        {
                            new PromptDialog("Chỉnh Sửa Thành Công!", promptInput: false, cancelButton: false).ShowDialog();
                            this.Close();
                        }
                        else
                            new PromptDialog("Thất Bại!", promptInput: false, cancelButton: false).ShowDialog();
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateAccount();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                UpdateAccount();
            }
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            PromptDialog prompt = new PromptDialog("Bạn có chắc muốn xóa tài khoản của bạn?", promptInput: false); 
            if (prompt.ShowDialog() == true)
            {
                if (bus.deleteAccount(CurrentUser.ID))
                {
                    new PromptDialog("Thành Công! Bạn sẽ không thể đăng nhập bằng tài khoản: " + CurrentUser.UserName, promptInput: false, cancelButton: false).ShowDialog();
                    this.mainWindow.signOut();
                    this.Close();
                }
            }
        }
    }
}
