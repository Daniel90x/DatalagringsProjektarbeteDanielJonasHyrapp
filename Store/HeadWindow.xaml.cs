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
        }

        private void GoMyPage_Click(object sender, RoutedEventArgs e) {
            var total_cd = Grid_My_Page.ColumnDefinitions.Count;
            if (total_cd > 0) {
                Grid_My_Page.ColumnDefinitions.RemoveRange(0, total_cd);
            }
            Grid_My_Page.Children.Clear(); 
            Load_MyPage(State.User);
            Title.Content = "My Page";
            Home_Scroll.Visibility = Visibility.Hidden;
            Store_Scroll.Visibility = Visibility.Hidden;
            My_Page_Scroll.Visibility = Visibility.Visible;
        }

        private void GoStore_Click(object sender, RoutedEventArgs e) {
            var total_cd = Grid_Store.ColumnDefinitions.Count;
            if (total_cd > 0) {
                Grid_Store.ColumnDefinitions.RemoveRange(0, total_cd);
            }
            Load_Store();
            Title.Content = "Store";
            Home_Scroll.Visibility = Visibility.Hidden;
            Store_Scroll.Visibility = Visibility.Visible;
            My_Page_Scroll.Visibility = Visibility.Hidden;

        }

        private void GoHome_Click(object sender, RoutedEventArgs e) {
            Load_Home();
            Title.Content = "Home";
            Home_Scroll.Visibility = Visibility.Visible;
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
                            var home_image = new Image() { };
                            home_image.MaxWidth = movie.Id;
                            home_image.Cursor = Cursors.Hand; // Gör så att pekarn blir till en hand när den håller över filmernas posters
                            home_image.MouseUp += Image_MouseUp;
                            home_image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                            home_image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                            home_image.Source = new BitmapImage(new Uri(movie.ImageURL)); // ImageURL Kom ihåg, kallar på bild
                            home_image.Height = 200;
                            home_image.Width = 150;
                            home_image.Stretch = Stretch.Fill;
                            home_image.Margin = new Thickness(2, 2, 2, 2);
                            home_image.Margin = new Thickness(2, 2, 2, 2);
                            home_image.ToolTip = "Title: " + movie.Title + "\nGenre: " + movie.Genre.Replace("|", ", ").ToString() + "\nImdb score: " + movie.IMDB_Score;
                            Grid_Home.Children.Add(home_image); // placerar ut bilderna i griden
                            Grid.SetRow(home_image, y); //rad y
                            Grid.SetColumn(home_image, x); //column x
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

        public void Load_Store() {
            State.Movies = API.GetMovies();
            int total_movies = 0;
            int action_y = 0;
            int drama_y = 0;
            int adventure_y = 0;
            int comedy_y = 0;
            int family_y = 0;
            int animation_y = 0;
            int romance_y = 0;
            //Loop som lägger till alla genres som stämmer överens med 'string genre'
            foreach (var movie in State.Movies) {

                //Hämta max 50 filmer
                if (total_movies < 50) {

                    List<string> genres = new List<string>(movie.Genre.ToLower().Split("|"));

                    foreach (var genre in genres) {
                        var store_image = new Image { };
                        store_image.MaxWidth = movie.Id;
                        store_image.Cursor = Cursors.Hand;
                        store_image.MouseUp += Image_MouseUp;
                        store_image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                        store_image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                        store_image.Source = new BitmapImage(new Uri(movie.ImageURL)); // ImageURL Kom ihåg, kallar på bild
                        store_image.Height = 130;
                        store_image.Width = 80;
                        store_image.Stretch = Stretch.Fill;
                        store_image.Margin = new Thickness(2, 2, 2, 2);
                        store_image.ToolTip = "Title: " + movie.Title + "\nGenre: " + movie.Genre.Replace("|", ", ").ToString() + "\nImdb score: " + movie.IMDB_Score;


                        var cd = new ColumnDefinition(); //Skapa ny ColumnDefinition att fylla ut
                        cd.Width = GridLength.Auto;
                        Grid_Store.ColumnDefinitions.Add(cd);
                        
                        if (action_y == 0) {
                            store_image.HorizontalAlignment = HorizontalAlignment.Right;
                        }
                        if (genre.ToLower() == "action") {
                            cd.Name = ("test" + movie.Id.ToString());
                            Grid_Store.Children.Add(store_image); //Skapa bilden
                            Grid.SetRow(store_image, 1);
                            Grid.SetColumn(store_image, action_y);
                            action_y += 1;
                        }
                        if (genre.ToLower() == "drama") {
                            Grid_Store.Children.Add(store_image); //Skapa bilden
                            Grid.SetColumn(store_image, drama_y);
                            Grid.SetRow(store_image, 3);
                            drama_y += 1;
                        }
                        if (genre.ToLower() == "adventure") {
                            Grid_Store.Children.Add(store_image); //Skapa bilden
                            Grid.SetColumn(store_image, adventure_y);
                            Grid.SetRow(store_image, 5);
                            adventure_y += 1;
                        }
                        if (genre.ToLower() == "comedy") {
                            Grid_Store.Children.Add(store_image); //Skapa bilden
                            Grid.SetColumn(store_image, comedy_y);
                            Grid.SetRow(store_image, 7);
                            comedy_y += 1;
                        }
                        if (genre.ToLower() == "family") {
                            Grid_Store.Children.Add(store_image); //Skapa bilden
                            Grid.SetColumn(store_image, family_y);
                            Grid.SetRow(store_image, 9);
                            family_y += 1;
                        }
                        if (genre.ToLower() == "animation") {
                            Grid_Store.Children.Add(store_image); //Skapa bilden
                            Grid.SetColumn(store_image, animation_y);
                            Grid.SetRow(store_image, 11);
                            animation_y += 1;
                        }
                        if (genre.ToLower() == "romance") {
                            Grid_Store.Children.Add(store_image); //Skapa bilden
                            Grid.SetColumn(store_image, romance_y);
                            Grid.SetRow(store_image, 13);
                            romance_y += 1;
                        }
                    }
                    total_movies += 1;
                }
            }

        }

        public void Load_MyPage(Customer customer) {
            //Tillgängliga text
            int y = 0;
            var current_time = DateTime.Now;
            State.User = customer;
            State.Sales = API.GetSales(State.User);

            //Rubrik 1
            var textBlock = new TextBlock();
            textBlock.Text = "Tillgängliga:";
            textBlock.FontSize = 15;
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            Grid_My_Page.Children.Add(textBlock);
            Grid.SetRow(textBlock, 0);
            Grid.SetColumn(textBlock, 0);

            //Rubrik 2
            textBlock = new TextBlock();
            textBlock.Text = "Historik:";
            textBlock.FontSize = 15;
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            Grid_My_Page.Children.Add(textBlock);
            Grid.SetRow(textBlock, 2);
            Grid.SetColumn(textBlock, 0);

            foreach (var sale in State.Sales) {
                State.Pick = API.GetMovie(sale.Movie.Id);
                var time_difference = (sale.Date.ToLocalTime() - current_time);
                
                
                
                if (time_difference.Seconds > 0) {
                    try {
                        if (time_difference.Seconds > 0) {

                            var cd = new ColumnDefinition();
                            cd.Width = GridLength.Auto;
                            Grid_My_Page.ColumnDefinitions.Add(cd);
                            
                            var image = new Image() { };
                            image.Cursor = Cursors.Hand;
                            image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                            image.VerticalAlignment = VerticalAlignment.Bottom; // WiP center avgör placering, finns .top; och .bottom; med
                            image.Source = new BitmapImage(new Uri(State.Pick.ImageURL)); // ImageURL Kom ihåg, kallar på bild
                            image.Height = 130;
                            image.Width = 80;
                            image.Stretch = Stretch.Fill;
                            image.Margin = new Thickness(2, 2, 2, 2);
                            image.Margin = new Thickness(2, 2, 2, 2);
                            image.ToolTip = "Title: " + State.Pick.Title + "\nGenre: " + State.Pick.Genre.Replace("|", ", ").ToString() + "\nImdb score: " + State.Pick.IMDB_Score;
                            Grid_My_Page.Children.Add(image); // placerar ut bilderna i griden

                            Grid.SetColumn(image, y);
                            Grid.SetRow(image, 1);

                            var string_date = new Label();
                            string_date.Content = "Tid kvar:\n" + time_difference.ToString(@"d\.h\:mm\:ss"); // La in tidformat i "ToSTRING"
                            string_date.BorderBrush = new SolidColorBrush(Colors.White);
                            string_date.HorizontalContentAlignment = HorizontalAlignment.Center;
                            string_date.VerticalContentAlignment = VerticalAlignment.Top;
                            string_date.FontSize = 12;
                            string_date.FontWeight = FontWeights.UltraBold;
                            Grid_My_Page.Children.Add(string_date);
                            Grid.SetRow(string_date, 1);
                            Grid.SetColumn(string_date, y);

                            //Förflytta oss ett steg åt höger
                            y += 1;
                        }
                    } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                      (e is ArgumentNullException ||
                       e is System.IO.FileNotFoundException ||
                       e is UriFormatException) {
                        continue; // Låter programmet köra vidare
                    }
                }
            }

            //Historik
            y = 0;
            foreach (var sale in API.GetSaleMovies(State.User)) {
                State.Pick = API.GetMovie(sale.Id);
                try {
                    var image = new Image() { };
                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    image.VerticalAlignment = VerticalAlignment.Top;
                    image.Source = new BitmapImage(new Uri(State.Pick.ImageURL));
                    image.Height = 130;
                    image.Width = 80;
                    image.Stretch = Stretch.Fill;
                    image.Margin = new Thickness(2, 2, 2, 2);
                    image.ToolTip = "Title: " + State.Pick.Title + "\nGenre: " + State.Pick.Genre.Replace(" | ", ", ").ToString() + "\nImdb score: " + State.Pick.IMDB_Score;
                    Grid_My_Page.Children.Add(image); // placerar ut bilderna i griden

                    //Skapar ny ColumnDefinition per film
                    var cd = new ColumnDefinition();
                    cd.Width = GridLength.Auto;
                    Grid_My_Page.ColumnDefinitions.Add(cd);
                    Grid.SetColumn(image, y);
                    Grid.SetRow(image, 3);

                    //Förflytta oss ett steg åt höger
                    y += 1;
                    image.ReleaseMouseCapture();
                } catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                      (e is ArgumentNullException ||
                       e is System.IO.FileNotFoundException ||
                       e is UriFormatException) {
                    continue; // Låter programmet köra vidare

                }
            }


            
        }

        private void Logout_Click(object sender, RoutedEventArgs e) {
            var next_window = new LoginWindow();
            next_window.Show();
            this.Close();
            State.User = null;
        }
        //Hyra film
        private void Image_MouseUp(object sender, MouseButtonEventArgs e) // Känner av vad användaren klickar på
        {

            //Använder MaxWidth från image i antingen store eller home
            //beroende från vart man klickar på bilden någonstans
            //som en variabel. MaxWidth blir movieid beroende på vilken
            //film man klickar på, Man kunde inte ha siffror i Name så
            //fick komma på en paniklösning

            var movieid = sender.GetType().GetProperty(MaxWidthProperty.ToString()).GetValue(sender, null).ToString();

            if (API.RegisterSale(State.User, API.GetMovie(int.Parse(movieid)))) {
                Load_MyPage(State.User);
                MessageBox.Show("Purchase complete!", "Purchase complete!", MessageBoxButton.OK, MessageBoxImage.Information);
            } else {
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


    }
}