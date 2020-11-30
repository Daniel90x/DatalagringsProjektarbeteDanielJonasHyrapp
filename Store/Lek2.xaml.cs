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
using Store.SubViews;

namespace Store
{
    /// <summary>
    /// Interaction logic for Lek2.xaml
    /// </summary>
    public partial class Lek2 : Window
    {
        public Lek2()
        {
            InitializeComponent();
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Home";
            Home.Visibility = Visibility.Visible;
            MyPage.Visibility = Visibility.Hidden;
            Store.Visibility = Visibility.Hidden;
        }

        private void GoMyPage_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "My Page";
            Home.Visibility = Visibility.Hidden;
            MyPage.Visibility = Visibility.Visible;
            Store.Visibility = Visibility.Hidden;
        }

        private void GoStore_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Store";
            Home.Visibility = Visibility.Hidden;
            MyPage.Visibility = Visibility.Hidden;
            Store.Visibility = Visibility.Visible;




        }
    }

}
