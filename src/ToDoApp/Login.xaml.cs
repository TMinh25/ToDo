using System.Windows;
using System.Windows.Input;
using ToDoApp.Views;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        BUS bus = new BUS();
        public Login()
        {
            InitializeComponent();
        }

        private void logIn()
        {
            string userName = txtUserAccount.Text;
            string passWord = txtPassword.Text;
            if (userName == "" || passWord == "") return;
            var err = bus.signIn(userName, passWord);
            if (err == "")
                this.Hide();
            else
            {
                this.txtErrorMessage.Text = err;
                txtUserAccount.Focus();
            }
            txtUserAccount.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void LogIn_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logIn();
            }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            logIn();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
