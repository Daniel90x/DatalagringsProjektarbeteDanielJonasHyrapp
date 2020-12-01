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

            State.Movies = API.GetMovieSlice(0, 25); // 0 = är vilken film som visas först och 30 = hur många filmer som visas i tabellen.
            for (int y = 0; y < Grid_Store.RowDefinitions.Count; y++)
            {
                for (int x = 0; x < Grid_Store.ColumnDefinitions.Count; x++)
                {
                    int i = y * Grid_Store.ColumnDefinitions.Count + x;
                    if (i < State.Movies.Count)
                    {
                        var movie = State.Movies[i];

                        try
                        {
                            var image = new Image() { };
                            image.Cursor = Cursors.Hand; // Gör så att pekarn blir till en hand när den håller över filmernas posters
                     //       image.MouseUp += Image_MouseUp;
                            image.HorizontalAlignment = HorizontalAlignment.Center; // WiP Center avgör placering, kunde ha annat som .right; eller .left; eller .strech;
                            image.VerticalAlignment = VerticalAlignment.Center; // WiP center avgör placering, finns .top; och .bottom; med
                            image.Source = new BitmapImage(new Uri(movie.ImageURL)); // ImageURL Kom ihåg, kallar på bild
                            //image.Height = 120;
                            image.Margin = new Thickness(4, 4, 4, 4);

                            Grid_Store.Children.Add(image); // placerar ut bilderna i griden
                            Grid.SetRow(image, y);
                            Grid.SetColumn(image, x);
                        }

                        catch (Exception e) when // Undviker från att crasha om tidigare kod inte fungerar.
                            (e is ArgumentNullException ||
                             e is System.IO.FileNotFoundException ||
                             e is UriFormatException)
                        {
                            continue; // Låter programmet köra vidare

                        }










                    }









                }










            }



        }












        private void GoMyPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoStore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
