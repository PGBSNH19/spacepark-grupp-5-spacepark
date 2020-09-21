using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceParkBackend.Models;
using SpaceParkBackend.Repos;

namespace SpaceParkBackend.Repos
{
    public interface IParkinglotRepo : IRepository
    {
        Task<IList<Parkinglot>> GetAvailableParkingLots();

    }
}
