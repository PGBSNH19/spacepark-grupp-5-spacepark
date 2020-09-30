﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IList<Person>> GetAllPersons()
        {
            var persons = _context.Persons;

            return await persons.ToListAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.PersonID == id);

            return person;
        }
    }
}
