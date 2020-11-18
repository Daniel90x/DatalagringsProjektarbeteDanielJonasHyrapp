using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    class Seeding
    {
        static void Main() 
        {
            using (var ctx = new Context())
            {
                ctx.RemoveRange(ctx.Sales); // Tar bort data från Arreyn
                ctx.RemoveRange(ctx.Movies);
                ctx.RemoveRange(ctx.Customers);

                ctx.AddRange(new List<Customer> {
                    new Customer { Name = "Björn" }, // Lägger till personer med rättigheter för att logga in.
                    new Customer { Name = "Robin" },
                    new Customer { Name = "Kalle" },
                });

                // Här laddas data in från SeedData foldern för att fylla ut Movies tabellen
                var movies = new List<Movie>();
                var lines = File.ReadAllLines(@"..\..\..\SeedData\MovieGenre.csv");
                for (int i = 1; i < 200; i++)
                {
                    // imdbId,Imdb Link,Title,IMDB Score,Genre,Poster
                    var cells = lines[i].Split(',');

                    var url = cells[5].Trim('"');  // Cell 5 är Poster som blir url..

                    var ImdbScore = Convert.ToDouble(cells[3]);

                    // var IMDB_Score = cells[3]; // Skapar en IMDB_Score variabel... WiP

                    // Hoppa över alla icke-fungerande url:er
                    try{ var test = new Uri(url); }
                    catch (Exception) { continue; }

                    movies.Add(new Movie { Title = cells[2], ImageURL = url, IMDB_Score = ImdbScore }); // Tar titlen och postern från databasen.
                }
                ctx.AddRange(movies);

                ctx.SaveChanges();
            }
        }
    }
}
