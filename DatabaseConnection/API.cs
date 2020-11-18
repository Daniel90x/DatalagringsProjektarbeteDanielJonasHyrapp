﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public class API
    {
        public static List<Movie> GetMovieSlice(int a, int b) // Söker de 30 första filmerna
        {
            using var ctx = new Context();
            return ctx.Movies.OrderBy(m => m.Title).Skip(a).Take(b).ToList(); // Söker de 30 första filmerna, men hoppar över 0, för att det bara står text som titel och posters med mera
        }
        public static Customer GetCustomerByName(string name) // Kallar efter namnen, inlogningsnamn
        {
            using var ctx = new Context();
            return ctx.Customers.FirstOrDefault(c => c.Name.ToLower() == name.ToLower()); // Ser till så att gör så att lösenordet inte är känsligt för stora och små bokstäver. ToLower
        }
        public static bool RegisterSale(Customer customer, Movie movie)
        {
            using var ctx = new Context();
            try
            {
                // Här säger jag åt contextet att inte oroa sig över innehållet i dessa records.
                // Om jag inte gör detta så kommer den att försöka updatera databasens Id och cracha.
                ctx.Entry(customer).State = EntityState.Unchanged;
                ctx.Entry(movie).State = EntityState.Unchanged;

                ctx.Add(new Rental() { Date = DateTime.Now, Customer = customer, Movie = movie });
                return ctx.SaveChanges() == 1;
            }
            catch(DbUpdateException e) // Se till att crasha
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return false;
            }
        }
    }
}