using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReviews.API.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<ICollection<T>> GetAll();
    }
}
