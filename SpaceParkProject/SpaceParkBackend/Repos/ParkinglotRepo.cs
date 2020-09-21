using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Logging;
using SpaceParkBackend.Database;
using SpaceParkBackend.Models;
using SpaceParkBackend.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpaceParkBackend.Repos
{
    public class ParkinglotRepo : Repository, IParkinglotRepo
    {
        public ParkinglotRepo(SpaceparkContext spaceparkContext, ILogger<ParkinglotRepo> logger) : base(spaceparkContext, logger)
        {}

        public async Task<IList<Parkinglot>> GetAvailableParkingLots()
        {
            _logger.LogInformation("Getting Parkinglots");
            
            var parkingSpaces = await _context.Parkinglots.Where(p => p.IsOccupied == false).ToListAsync();

            if (parkingSpaces.Count < 1)
            {
                _logger.LogInformation("Its Full B*TCH (Shoot em Down)");
                return parkingSpaces;
            }

            return parkingSpaces;
        }
    }
}
