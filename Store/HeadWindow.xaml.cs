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
    /// Interaction logic for HeadWindow.xaml
    /// </summary>
    public partial class HeadWindow : Window
    {
        public HeadWindow()
        {
            InitializeComponent();
        }

        private void GoMyPage_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "My Page";
            Home_Scroll.Visibility = Visibility.Hidden;
            Grid_My_Page.Visibility = Visibility.Visible;
            Store_Scroll.Visibility = Visibility.Hidden;
        }

        private void GoStore_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Store";
            Home_Scroll.Visibility = Visibility.Hidden;
            Grid_My_Page.Visibility = Visibility.Hidden;
            Store_Scroll.Visibility = Visibility.Visible;
            Load_Store("Comedy");
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Home";
            Home_Scroll.Visibility = Visibility.Visible;
            Grid_My_Page.Visibility = Visibility.Hidden;
            Store_Scroll.Visibility = Visibility.Hidden;
            //oad_Home();
        }

        public void Load_Home() {
            State.Movies = API.GetMovieSlice(30); // 0 = är vilken film som visas först och 30 = hur många filmer som visas i tabellen.
            for (int y = 0; y < Grid_Home.RowDefinitions.Count; y++) {
                for (int x = 0; x < Grid_Home.ColumnDefinitions.Count; x++) {
                    int i = y * Grid_Home.ColumnDefinitions.Count + x;
                    if (i < State.Movies.Count) {
                        var movie = State.Movies[i];

                        try {
                            var image = new Image() { };
                            image.Cursor = Cursors.Hand; // Gör så att pekarn blir till en hand när den håller över filmernas posters
                            image.MouseUp += Image_MouseUp;
                            image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                            image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                            image.Source = new BitmapImage(new Uri(movie.ImageURL)); // ImageURL Kom ihåg, kallar på bild

                            image.Height = 200;
                            image.Width = 150;
                            image.Stretch = Stretch.Fill;
                            image.Margin = new Thickness(2, 2, 2, 2);

                            Grid_Home.Children.Add(image); // placerar ut bilderna i griden
                            Grid.SetRow(image, y);
                            Grid.SetColumn(image, x);
                        } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                              (e is ArgumentNullException ||
                               e is System.IO.FileNotFoundException ||
                               e is UriFormatException) {
                            continue; // Låter programmet köra vidare

                        }
                    }
                }
            }
        }

        public void Load_Store(string genre) {
            State.Movies = API.GetMovies();

            int blabla = 0;
            foreach(var e in State.Movies) {
                if (blabla < 10) {
                    List<string> genres = new List<string>(e.Genre.ToLower().Split("|"));
                    if(genres.Contains(genre.ToLower())){
                        MessageBox.Show(e.Title, "Purchase complete!", MessageBoxButton.OK, MessageBoxImage.Information);
                    } 
                    blabla += 1;
                }
            }
            for (int y = 0; y < Grid_Home.RowDefinitions.Count; y++) {
                for (int x = 0; x < Grid_Home.ColumnDefinitions.Count; x++) {
                    int i = y * Grid_Home.ColumnDefinitions.Count + x;
                    if (i < State.Movies.Count) {
                        var movie = State.Movies[i];

                        try {
                            var image = new Image() { };
                            image.Cursor = Cursors.Hand; // Gör så att pekarn blir till en hand när den håller över filmernas posters
                            image.MouseUp += Image_MouseUp;
                            image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                            image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                            image.Source = new BitmapImage(new Uri(movie.ImageURL)); // ImageURL Kom ihåg, kallar på bild

                            image.Height = 200;
                            image.Width = 150;
                            image.Stretch = Stretch.Fill;
                            image.Margin = new Thickness(2, 2, 2, 2);

                            Grid_Home.Children.Add(image); // placerar ut bilderna i griden
                            Grid.SetRow(image, y);
                            Grid.SetColumn(image, x);
                        } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                              (e is ArgumentNullException ||
                               e is System.IO.FileNotFoundException ||
                               e is UriFormatException) {
                            continue; // Låter programmet köra vidare

                        }
                    }
                }
            }
        }

        /*public void Load_MyPage() {
            State.Movies = API.GetMovieSlice(30); // 0 = är vilken film som visas först och 30 = hur många filmer som visas i tabellen.
            for (int y = 0; y < Grid_My_Page.RowDefinitions.Count; y++) {
                for (int x = 0; x < Grid_My_Page.ColumnDefinitions.Count; x++) {
                    int i = y * Grid_My_Page.ColumnDefinitions.Count + x;
                    if (i < State.Movies.Count) {
                        var movie = State.Movies[i];

                        try {
                            var image = new Image() { };
                            image.Cursor = Cursors.Hand; // Gör så att pekarn blir till en hand när den håller över filmernas posters
                            image.MouseUp += Image_MouseUp;
                            image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                            image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                            image.Source = new BitmapImage(new Uri(movie.ImageURL)); // ImageURL Kom ihåg, kallar på bild

                            image.Height = 200;
                            image.Width = 150;
                            image.Stretch = Stretch.Fill;
                            image.Margin = new Thickness(2, 2, 2, 2);

                            Grid_My_Page.Children.Add(image); // placerar ut bilderna i griden
                            Grid.SetRow(image, y);
                            Grid.SetColumn(image, x);
                        } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                              (e is ArgumentNullException ||
                               e is System.IO.FileNotFoundException ||
                               e is UriFormatException) {
                            continue; // Låter programmet köra vidare

                        }
                    }
                }
            }
        }*/


        //Hyra film
        private void Image_MouseUp(object sender, MouseButtonEventArgs e) // Känner av vad användaren klickar på
        {
            var x = Grid.GetColumn(sender as UIElement);
            var y = Grid.GetRow(sender as UIElement);

            int i = y * Grid_Home.ColumnDefinitions.Count + x;
            State.Pick = State.Movies[i];

            if (API.RegisterSale(State.User, State.Pick))
                MessageBox.Show("Purchase complete!","Purchase complete!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
