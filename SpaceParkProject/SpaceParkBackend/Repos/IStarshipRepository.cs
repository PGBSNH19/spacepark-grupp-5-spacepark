using SpaceParkBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceParkBackend.Repos
{
    public interface IStarshipRepository : IRepository
    {
        Task<IList<Starship>> GetAllStarships();
        Task<Starship> GetStarshipById(int id);
    }
}