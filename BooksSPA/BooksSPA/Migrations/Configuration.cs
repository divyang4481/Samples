namespace BooksSPA.Migrations
{
    using BooksSPA.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BooksSPA.Models.BooksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BooksSPA.Models.BooksContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Books.AddOrUpdate(b => b.Title,
                new Book { Id = 1, Title = "Under the volcano", Rating = "5", Genre = "Novel", Year = 1947 },
                new Book { Id = 2, Title = "Œmieræ w Amazonii", Rating = "5", Genre = "Documentary", Year = 2013 },
                new Book { Id = 3, Title = "JKHKJHkjkj", Rating = "1", Genre = "tyrty", Year = 1980 },
                new Book { Id = 4, Title = "Forgotten Doors", Rating = "R", Genre = "Drama", Year = 1988 },
                new Book { Id = 5, Title = "Blue Moon June", Rating = "PG-13", Genre = "Drama", Year = 2000 },
                new Book { Id = 6, Title = "The Edge of the Sun", Rating = "PG-13", Genre = "Drama", Year = 1790 }
                );
        }
    }
}
