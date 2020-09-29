using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceParkBackend.Database;
using SpaceParkBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Repos
{
    public class StarshipRepository : Repository, IStarshipRepository
    {
        public StarshipRepository(SpaceparkContext spaceparkContext, ILogger<StarshipRepository> logger) : base(spaceparkContext, logger)
        {}

        public async Task<IList<Starship>> GetAllStarships()
        {
            _logger.LogInformation("Getting all starships in order of their id.");
            IQueryable<Starship> query = _context.Starships.OrderBy(starship => starship.StarshipID);

            return await query.ToListAsync();
        }

        public async Task<Starship> GetStarshipById(int id)
        {
            _logger.LogInformation("Getting starship by their id.");
            IQueryable<Starship> query = _context.Starships.Where(starship => starship.StarshipID == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
