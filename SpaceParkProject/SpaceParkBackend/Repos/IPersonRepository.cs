using SpaceParkBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Repos
{
    public interface IPersonRepository : IRepository 
    {
        Task<IList<Person>> GetAllPersons();
        Task<Person> GetPersonById(int id);
    }
}
