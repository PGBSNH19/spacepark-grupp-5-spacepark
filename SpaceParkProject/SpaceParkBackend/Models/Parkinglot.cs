﻿using System;
using System.Collections.Generic;
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
        public Spaceship Spaceship { get; set; }
        public int? SpaceshipID { get; set; }
    }
}