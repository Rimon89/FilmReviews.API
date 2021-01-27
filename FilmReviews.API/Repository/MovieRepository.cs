using FilmReviews.API.Contracts;
using FilmReviews.API.Data;
using FilmReviews.API.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> Delete(string id)
        {
            var movieToDelete = await Find(id);

            if (movieToDelete == null)
                return false;

            var x = await _dbContext.Movies.Where(x => x.ImdbID == id).Include(x => x.Reviews).ToListAsync();
            _dbContext.Movies.RemoveRange(x);

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<Movie> Find(string id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);

            return movie;
        }

        public async Task<ICollection<Movie>> GetAll()
        {
            var movies = await _dbContext.Movies.ToListAsync();

            return movies;
        }
    }
}
