using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceParkBackend.Models;
using SpaceParkBackend.Database;

namespace SpaceParkBackend.Repos
{
    public class Repository : IRepository
    {
        protected readonly SpaceparkContext _context;
        protected readonly ILogger<Repository> _logger;
       
        public Repository(SpaceparkContext _context, ILogger<Repository> logger)
        {
            this._context = _context;
            _logger = logger;
        }

        public async Task Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            await _context.AddAsync(entity);
            await Save();
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _context.Remove(entity);
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _context.SaveChangesAsync()) >= 0;
        }
    }
}