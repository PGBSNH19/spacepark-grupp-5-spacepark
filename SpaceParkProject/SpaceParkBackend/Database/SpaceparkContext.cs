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
                      
                modelBuilder.Entity<Parkinglot>()
                .HasData(new
                {
                    ParkinglotID = 1,
                    Cost = 500,
                    Length = 20,
                    IsOccupied = false                    
                });
            modelBuilder.Entity<Parkinglot>()
               .HasData(new
               {
                   ParkinglotID = 2,
                   Cost = 500,
                   Length = 20,
                   IsOccupied = false
               });
            modelBuilder.Entity<Parkinglot>()
               .HasData(new
               {
                   ParkinglotID = 3,
                   Cost = 500,
                   Length = 30,
                   IsOccupied = false
               });
            modelBuilder.Entity<Parkinglot>()
               .HasData(new
               {
                   ParkinglotID = 4,
                   Cost = 500,
                   Length = 30,
                   IsOccupied = false
               });
            modelBuilder.Entity<Parkinglot>()
               .HasData(new
               {
                   ParkinglotID = 5,
                   Cost = 500,
                   Length = 50,
                   IsOccupied = false
               });
            modelBuilder.Entity<Parkinglot>()
               .HasData(new
               {
                   ParkinglotID = 6,
                   Cost = 500,
                   Length = 100,
                   IsOccupied = false
               });

        }
    }
}
