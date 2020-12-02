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


namespace Store {
    /// <summary>
    /// Interaction logic for HeadWindow.xaml
    /// </summary>
    public partial class HeadWindow : Window {
        public HeadWindow() {
            InitializeComponent();
            Load_Store("Action", 1);
            Load_Store("Drama", 3);
            Load_Store("Adventure", 5);
            Load_Store("Comedy", 7);
            Load_Store("Family", 9);
            Load_Store("Animation", 11);
            Load_Store("Romance", 13);
            Load_Home();
        }

        private void GoMyPage_Click(object sender, RoutedEventArgs e) {
            Title.Content = "My Page";
            Home_Scroll.Visibility = Visibility.Hidden;
            //Grid_My_Page.Visibility = Visibility.Visible;
            Store_Scroll.Visibility = Visibility.Hidden;
            My_Page_Scroll.Visibility = Visibility.Visible;
        }

        private void GoStore_Click(object sender, RoutedEventArgs e) {
            Title.Content = "Store";
            Home_Scroll.Visibility = Visibility.Hidden;
            //Grid_My_Page.Visibility = Visibility.Hidden;
            Store_Scroll.Visibility = Visibility.Visible;
            My_Page_Scroll.Visibility = Visibility.Hidden;

        }

        private void GoHome_Click(object sender, RoutedEventArgs e) {
            Title.Content = "Home";
            Home_Scroll.Visibility = Visibility.Visible;
            //Grid_My_Page.Visibility = Visibility.Hidden;
            Store_Scroll.Visibility = Visibility.Hidden;
            My_Page_Scroll.Visibility = Visibility.Hidden;

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
                            Grid.SetRow(image, y); //rad y
                            Grid.SetColumn(image, x); //column x
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

        public void Load_Store(string genre, int index) {
            State.Movies = API.GetMovies();

            int blabla = 0;

            List<Movie> movieList = new List<Movie>();

            //Loop som lägger till alla genres som stämmer överens med 'string genre'
            foreach (var e in State.Movies) {
                if (blabla < 30) {
                    List<string> genres = new List<string>(e.Genre.ToLower().Split("|"));
                    if (genres.Contains(genre.ToLower())) {
                        movieList.Add(e);
                    }
                    blabla += 1;
                }
            }
            int y = 0;
            int x = 0;
            foreach (var movie in movieList) {
                try {
                    
                    var image = new Image() { };
                    image.Cursor = Cursors.Hand;
                    image.MouseUp += Image_MouseUp;
                    image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                    image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                    image.Source = new BitmapImage(new Uri(movie.ImageURL)); // ImageURL Kom ihåg, kallar på bild

                    image.Height = 130;
                    image.Width = 80;
                    image.Stretch = Stretch.Fill;
                    image.Margin = new Thickness(2, 2, 2, 2);

                    Grid_Store.Children.Add(image); // placerar ut bilderna i griden

                    //Skapar ny ColumnDefinition per film
                    var cd = new ColumnDefinition();
                    cd.Width = new GridLength(200);
                    Grid_Store.ColumnDefinitions.Add(cd);
                    Grid.SetColumn(image, y);
                    Grid.SetRow(image, index);
                    x += 1;
                    y += 1;
                } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                      (e is ArgumentNullException ||
                       e is System.IO.FileNotFoundException ||
                       e is UriFormatException) {
                    continue; // Låter programmet köra vidare

                }
            }
        }

        public void Load_MyPage(Customer customer) {

            //Tillgängliga text
            int y = 0;
            State.User = customer;
            int userID = State.User.Id;
            State.Sales = API.GetSales(State.User);

            foreach (var sale in State.Sales) {
                try {
                    //sale.Date.ToString()
                    var string_date = new Label();
                    var current_time = DateTime.Now;
                    var time_difference = (sale.Date.ToLocalTime() - current_time);
                    //MessageBox.Show(time_difference.ToString(), "Purchase complete!", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (time_difference.Seconds > 0) { 
                        //MessageBox.Show(time_difference.ToString(), "Purchase complete!", MessageBoxButton.OK, MessageBoxImage.Information);
                        string_date.Content = "Tid kvar:\n" + time_difference.ToString();
                        string_date.HorizontalContentAlignment = HorizontalAlignment.Left;
                        string_date.VerticalContentAlignment = VerticalAlignment.Center;
                        Grid_My_Page.Children.Add(string_date);
                        Grid.SetRow(string_date, 1);
                        Grid.SetColumn(string_date, y);
                        y += 1;
                    }
                } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                  (e is ArgumentNullException ||
                   e is System.IO.FileNotFoundException ||
                   e is UriFormatException) {
                    continue; // Låter programmet köra vidare

                }
            }
            

            //Get all sales
            //Tillgängliga bilder
            y = 0;
            foreach(var sale in API.GetSaleMovies(State.User)) {
                State.Pick = API.GetMovie(sale.Id);
                try {
                    var image = new Image();
                    var cd = new ColumnDefinition();
                    cd.Width = new GridLength(200);
                    
                    image.Cursor = Cursors.Hand;
                    image.MouseUp += Image_MouseUp;
                    image.HorizontalAlignment = HorizontalAlignment.Right; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                    image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                    image.Source = new BitmapImage(new Uri(State.Pick.ImageURL)); // ImageURL Kom ihåg, kallar på bild
                    image.Height = 130;
                    image.Width = 80;
                    image.Stretch = Stretch.Fill;
                    image.Margin = new Thickness(2, 2, 2, 2);

                    Grid_My_Page.Children.Add(image); // placerar ut bilderna i griden
                    Grid_My_Page.ColumnDefinitions.Add(cd);

                    Grid.SetColumn(image, y);
                    Grid.SetRow(image, 1);
                    y += 1;
                } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                      (e is ArgumentNullException ||
                       e is System.IO.FileNotFoundException ||
                       e is UriFormatException) {
                    continue; // Låter programmet köra vidare

                }
            }

            //Historik
            y = 0;
            foreach (var sale in API.GetSaleMovies(State.User)) {
                State.Pick = API.GetMovie(sale.Id);
                try {
                    var image = new Image() { };
                    image.Cursor = Cursors.Hand;
                    image.MouseUp += Image_MouseUp;
                    image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                    image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                    image.Source = new BitmapImage(new Uri(State.Pick.ImageURL)); // ImageURL Kom ihåg, kallar på bild

                    image.Height = 130;
                    image.Width = 80;
                    image.Stretch = Stretch.Fill;
                    image.Margin = new Thickness(2, 2, 2, 2);

                    Grid_My_Page.Children.Add(image); // placerar ut bilderna i griden

                    //Skapar ny ColumnDefinition per film
                    var cd = new ColumnDefinition();
                    cd.Width = new GridLength(200);
                    Grid_My_Page.ColumnDefinitions.Add(cd);
                    Grid.SetColumn(image, y);
                    Grid.SetRow(image, 3);
                    y += 1;
                } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                      (e is ArgumentNullException ||
                       e is System.IO.FileNotFoundException ||
                       e is UriFormatException) {
                    continue; // Låter programmet köra vidare

                }
            }


            //DEbug
            //MessageBox.Show(State.User.Id.ToString(), "Purchase complete!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

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
