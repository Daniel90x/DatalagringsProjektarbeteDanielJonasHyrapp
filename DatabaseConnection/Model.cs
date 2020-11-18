using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseConnection
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Rental> Sales { get; set; } // Ger tillåtelse för rental
    }
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }

        public string IMDB_Score { get; set; } // WiP

        public string Genre { get; set; }
        public virtual List<Rental> Sales { get; set; } // Ger tillåtelse för rental
    }
    public class Rental
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; } // Kallar på Customer
        public virtual Movie Movie { get; set; } // Kallar på Movie
    }
}
