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

                //DEBUGGING
                //API.GetCustomer("test@test.com", "123333");
                //API.GetCustomer("test@test.com", "123");
                //API.GetCustomer("test2@test.com", "321");
                //API.GetCustomer("test3@test.com", null);
                //Console.ReadLine();

                ctx.AddRange(new List<Customer> {
                    new Customer { Name = "Björn" , Email = "test@test.com", Password = "123", PhoneNumber = 087020090}, // Lägger till personer med rättigheter för att logga in
                    new Customer { Name = "Robin" , Email = "test2@test.com", Password = "321", PhoneNumber = 061233152},
                    new Customer { Name = "Kalle",  Email = "test3@test.com", PhoneNumber = 061233526},
                });

                // Här laddas data in från SeedData foldern för att fylla ut Movies tabellen
                var movies = new List<Movie>();
                var lines = File.ReadAllLines(@"..\..\..\SeedData\MovieGenre.csv");
                for (int i = 1; i < 200; i++)
                {
                    // imdbId,Imdb Link,Title,IMDB Score,Genre,Poster
                    var cells = lines[i].Split(',');

                    var url = cells[5].Trim('"');  // Cell 5 är Poster som blir url..

                    string ImdbScore = cells[3]; // Cell 3 visar imdbscore...

                    // string Genre = cells[4]; 

                    // Hoppa över alla icke-fungerande url:er
                    try{ var test = new Uri(url); }
                    catch (Exception) { continue; }

                    movies.Add(new Movie { Title = cells[2], ImageURL = url, IMDB_Score = ImdbScore, Genre = cells[4], ImdbUrl = cells[1] }); // Tar titel, genre med mera och skickar till databasen.
                }
                ctx.AddRange(movies);

                ctx.SaveChanges();
            }
        }
    }
}
