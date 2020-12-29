using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReviews.API.Contracts
{
    public interface IRepositoryBase<T> where T: class
    {
        Task<T> Find(string id);
        Task<bool> Create(T entity);
    }
}
