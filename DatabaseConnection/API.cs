using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public class API
    {
        public static List<Movie> GetMovieSlice(int n) // Tar n random filmer
        {
            using var ctx = new Context();
            return ctx.Movies.OrderBy(m => Guid.NewGuid()).Take(n).ToList(); // Lägger n random filmer i en lista och returnar denna listan
        }

        public static List<Movie> GetMovies() {
            using (var ctx = new Context()) {
                var query = ctx.Movies.OrderBy(m => m.Title);
                return query.ToList();
            }
        }

        public static Movie GetMovie(int id) {
            using var ctx = new Context();
            return ctx.Movies.FirstOrDefault(m => m.Id == id);
        }
        public static List<Movie> GetSaleMovies(Customer customer) {
            using var ctx = new Context();
            //return ctx.Sales.Include(m => m.Id).Where(c => c.Customer == customer).Select(m => m.Movie).ToList();
            return ctx.Sales.Where(c => c.Customer == customer).Select(m => m.Movie).ToList();
        }

        public static List<Rental> GetSales(Customer customer) {
            using var ctx = new Context();
            return ctx.Sales.Where(c => c.Customer == customer).Include(m => m.Movie).ToList();
        }

        public static Customer GetCustomerByName(string name) // Kallar efter namnen, inlogningsnamn
        {
            using var ctx = new Context();
            return ctx.Customers.FirstOrDefault(c => c.Name.ToLower() == name.ToLower()); // Ser till så att gör så att lösenordet inte är känsligt för stora och små bokstäver. ToLower

        }

        public static Customer GetCustomer(string email, string password) 
        {
            using var ctx = new Context();
            var user = ctx.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);
            return user;
        }
        public static bool RegisterSale(Customer customer, Movie movie)
        {
            DateTime current_time = DateTime.Now;
            using var ctx = new Context();
            try
            {
                // Här säger jag åt contextet att inte oroa sig över innehållet i dessa records.
                // Om jag inte gör detta så kommer den att försöka updatera databasens Id och cracha.
                ctx.Entry(customer).State = EntityState.Unchanged;
                ctx.Entry(movie).State = EntityState.Unchanged;
                

                ctx.Add(new Rental() { Date = current_time.AddDays(7), Customer = customer, Movie = movie });
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
