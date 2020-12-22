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
        private readonly IHttpClientFactory _clientFactory;

        public MovieController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovieAsync(string imdbId)
        {
            var client = _clientFactory.CreateClient();

            var movie = await client.GetFromJsonAsync<Movie>($"http://www.omdbapi.com/?i={imdbId}&apikey=6cf600c0");

            if (movie.imdbID == null)
                return NotFound();

            return Ok(movie);
        }
    }
}
