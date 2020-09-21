using System;
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
        public int StarshipID { get; set; }
        [NotMapped]
        public List<string> Starships { get; set; }
    }
}
