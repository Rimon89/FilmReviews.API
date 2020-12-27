using FilmReviews.API.Contracts;
using FilmReviews.API.Data;
using FilmReviews.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReviews.API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Movie entity)
        {
            await _dbContext.AddAsync(entity);

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<Movie> FindById(string id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            return movie;
        }
    }
}
