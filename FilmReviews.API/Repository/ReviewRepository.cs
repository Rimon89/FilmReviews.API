using FilmReviews.API.Contracts;
using FilmReviews.API.Data;
using FilmReviews.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<bool> Delete(Guid id)
        {
            var reviewToDelete = await Find(id);

            _dbContext.Reviews.Remove(reviewToDelete);

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<Review> Find(Guid id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);

            return review;
        }

        public async Task<ICollection<Review>> GetAll()
        {
            var reviews = await _dbContext.Reviews.ToListAsync();

            return reviews;
        }
    }
}
