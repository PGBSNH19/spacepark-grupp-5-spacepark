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
        public virtual DbSet<Spaceship> Spaceships { get; set; }

    }
}
