using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceParkBackend.Database;
using SpaceParkBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Repos
{
    public class PersonRepository : Repository, IPersonRepository
    {
        
        public PersonRepository(SpaceparkContext spaceContext, ILogger<Repository> logger) : base (spaceContext, logger)
        {
            spaceContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public async Task<IList<Person>> GetAllPersons(string name)
        {
            _logger.LogInformation("Getting all persons");
            var persons = _context.Persons; 
            if(string.IsNullOrEmpty(name) == false)
            {
                return persons.Where(p => p.Name == name).ToList();
            }

            return await persons.ToListAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            _logger.LogInformation($"Getting a person with a specific id {id}");
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.PersonID == id);

            return person;
        }
    }
}
