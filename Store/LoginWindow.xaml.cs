using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            State.User = API.GetCustomer(Email.Text.Trim(), Password.Password.ToString());

            if (State.User != null)
            {
                var next_window = new HeadWindow(); // Ändrar om MainWindow till HeadWindow
                //var next_window = new MainWindow(); // Ändrar om MainWindow till HeadWindow
                //next_window.Load_Home();
                next_window.Show();
                next_window.Load_MyPage(State.User);
                this.Close();
                
            }
            else
            {
                Authenticated.Content = "Wrong email or password!";
                Email.Text = "Email";
                //Password.Text = "Password";
            }
        }
    }
}
