using Microsoft.EntityFrameworkCore;
using SpaceParkBackend.Database;
using SpaceParkBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SpaceParkBackend
{
    public class ParkingGuardRepository : IParkingGuardRepository
    {
        public Parkinglot CheckOpenStatus(SpaceparkContext context)
        {
            var parkingSpaces = context.Parkinglots.Where(p => p.IsOccupied == false).ToList();
           
            if(parkingSpaces.Count < 1)
            {
                Console.WriteLine("hweifi");
                return null;
            }

            return parkingSpaces.FirstOrDefault();
        }
    }
}
