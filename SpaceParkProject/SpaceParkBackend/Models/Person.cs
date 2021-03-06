﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public bool HasPaid { get; set; }
        [ForeignKey("StarshipID")]
        public int StarshipID { get; set; }
        public Starship Starship { get; set; }
        [NotMapped]
        public List<string> Starships { get; set; }
    }
}
