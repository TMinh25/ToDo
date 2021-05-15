using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for PromptDialog.xaml
    /// </summary>
    public partial class PromptDialog : Window
    {
        public PromptDialog(string question, string defaultAnswer = "", bool promptInput = true, bool cancelButton = true)
        {
            InitializeComponent();
            lblQuestion.Content = question;
            txtAnswer.Text = defaultAnswer;
            if (promptInput == false)
            {
                this.answerInput.Visibility = Visibility.Hidden;
                this.grid.RowDefinitions.RemoveAt(0);
            }
            else
            {
                this.answerInput.Visibility = Visibility.Visible;
                RowDefinition newRow = new RowDefinition();
                newRow.Height = new GridLength(1, GridUnitType.Auto);
                this.grid.RowDefinitions.Add(newRow);
            }
            if (cancelButton == true)
                this.btnDialogCancel.Visibility = Visibility.Visible;
            else 
                this.btnDialogCancel.Visibility = Visibility.Hidden;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        public string Answer
        {
            get { return txtAnswer.Text; }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Answer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.DialogResult = true;
            }
        }
    }
}