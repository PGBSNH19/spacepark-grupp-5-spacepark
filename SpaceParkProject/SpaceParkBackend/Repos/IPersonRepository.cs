using SpaceParkBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Repos
{
    public interface IPersonRepository
    {
        Task<Person[]> GetAll();
    }
}
