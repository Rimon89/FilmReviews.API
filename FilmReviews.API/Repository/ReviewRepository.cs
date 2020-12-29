using FilmReviews.API.Contracts;
using FilmReviews.API.Data;
using FilmReviews.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReviews.API.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReviewRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Review entity)
        {
            await _dbContext.AddAsync(entity);

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<Review> Find(string name)
        {
            var review = await _dbContext.Reviews.Where(r => r.Name == name).FirstOrDefaultAsync();

            return review;
        }
    }
}
