using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Models
{
    public class Starship
    {
        public int StarshipID { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }
        public int? ParkinglotID { get; set; }

    }
}
