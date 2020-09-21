using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Social.API.Services
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> Save();
    }
}