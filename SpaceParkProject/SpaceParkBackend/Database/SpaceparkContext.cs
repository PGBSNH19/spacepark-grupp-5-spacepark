using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpaceParkBackend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Database
{
    public class SpaceparkContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Parkinglot> Parkinglots { get; set; }
        public virtual DbSet<Starship> Starships { get; set; }

        private readonly IConfiguration _Configuration;

        public SpaceparkContext()
        {

        }
        public SpaceparkContext(IConfiguration configuration, DbContextOptions options) : base (options)
        {
            _Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.dev.json").Build().GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Starship>()
                .HasData(new
                {
                    StarshipID = 1,
                    Length = 32,
                    Name = "Sand Crawler"
                });
            modelBuilder.Entity<Parkinglot>()
                .HasData(new
                {
                    ParkinglotID = 1,
                    Cost = 500,
                    Length = 36,
                    IsOccupied = true,
                    StarshipID = 1
                });
            modelBuilder.Entity<Person>()
                .HasData(new
                {
                    PersonID = 1,
                    Name = "Luke Skywalker",
                    HasPaid = false,
                    StarshipID = 1                    
                });
        }
    }
}
