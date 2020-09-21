using SpaceParkBackend.Database;
using SpaceParkBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Repos
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository(SpaceparkContext spaceContext) : base(spaceContext)
        {

        }
        public async Task<Person[]> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
