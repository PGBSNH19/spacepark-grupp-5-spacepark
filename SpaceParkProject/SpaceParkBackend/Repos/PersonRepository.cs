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

        }

        public async Task<Person[]> GetAllVisitors()
        {
            throw new NotImplementedException();
        }
    }
}
