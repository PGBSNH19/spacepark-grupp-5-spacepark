using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Models
{
    public class Parkinglot
    {
       
        public int ParkinglotID { get; set; }
        public int Cost { get; set; }
        public int Length { get; set; }
        public bool IsOccupied { get; set; }
        public int? StarshipID { get; set; }
        [NotMapped]
        public Starship Starship { get; set; }
        
    }
}
