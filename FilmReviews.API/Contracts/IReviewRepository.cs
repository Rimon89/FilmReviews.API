using FilmReviews.API.Models;
using System;
using System.Threading.Tasks;

namespace FilmReviews.API.Contracts
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<Review> Find(Guid id);
        Task<bool> Delete(Guid id);
        Task Update(Review entity);
    }
}
