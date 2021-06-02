using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToDoApp.Converters;
using ToDoApp.Control;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        BLL bus = new BLL();
        private Image avatarImage = null;
        private string fileName = null;
        public BitmapSource AvatarImageSource
        {
            get
            {
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var unknownUserPath = Path.Combine(outPutDirectory, "Assets\\Images\\unknownUser.jpg");
                BitmapSource unknownUserSource = new BitmapImage(new Uri(new Uri(unknownUserPath).LocalPath));
                return this.avatarImage == null ? unknownUserSource : ImgConverter.GetAvatarBitSource(avatarImage);
            }
        }
        public SignUp()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private Image _myImageSource;

        public Image MyImageSource
        {
            get { return _myImageSource; }
            set
            {
                _myImageSource = value;
            }
        }
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = false };
            if (openFileDialog.ShowDialog() == true)
            {
                avatarImage = Image.FromFile(openFileDialog.FileName);
                fileName = openFileDialog.FileName;
                imgAvatarSelection.ImageSource = ImgConverter.ConvertImageToBitSource(avatarImage);
            }
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

        private void signUp()
        {
            string userName = txtUserAccount.Text, password = txtPassword.Text, rePassword = txtRePassword.Text, displayName = txtDisplayName.Text;
            if (userName == string.Empty || password == string.Empty || rePassword == string.Empty || displayName == string.Empty)
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
                        if (bus.getUser(userName) != null)
                        {
                            txtErrorMessage.Text = "Tên tài khoản đã có người sử dụng!";
                        }
                        else
                        {
                            if (bus.createAccount(userName, password, displayName, avatarImage, fileName))
                            {
                                new PromptDialog("Đăng Kí Thành Công!", promptInput: false, cancelButton: false).ShowDialog();
                                this.Close();
                            }
                            else
                            {                                new PromptDialog("Thất Bại!", promptInput: false, cancelButton: false).ShowDialog();

                            }
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            signUp();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SignUp_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                signUp();
            }
        }
    }
}
