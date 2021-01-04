using FilmReviews.API.Models;
using System.Threading.Tasks;

namespace FilmReviews.API.Contracts
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<Movie> Find(string id);
    }
}
