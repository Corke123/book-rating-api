using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book_rating_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_rating_api.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 100, Name = "Book" },
                new Category { Id = 101, Name = "Comic" }
            );

            modelBuilder.Entity<Book>()
                .HasMany(b => b.MainActors)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserRating> UserRatings => Set<UserRating>();
    }
}