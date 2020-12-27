using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FilmReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IHttpClientFactory _clientFactory;

        public MovieController(IHttpClientFactory clientFactory, IMovieRepository movieRepo)
        {
            _clientFactory = clientFactory;
            _movieRepo = movieRepo;
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovieAsync(string imdbId)
        {
            var movie = await _movieRepo.FindById(imdbId);

            if(movie == null)
            {
                var client = _clientFactory.CreateClient();

                movie = await client.GetFromJsonAsync<Movie>($"http://www.omdbapi.com/?i={imdbId}&apikey=6cf600c0");

                if (movie.ImdbID == null)
                    return NotFound();

                var success = await _movieRepo.Create(movie);

                if (!success)
                    return BadRequest();
            }

            return Ok(movie);
        }
    }
}
