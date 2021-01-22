using FilmReviews.API.Services.Movies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FilmReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovieAsync(string imdbId)
        {
            try
            {
                var movie = await _mediator.Send(new Details.Query { ImdbId = imdbId });

                if (string.IsNullOrEmpty(movie.ImdbID))
                    return NotFound();

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _mediator.Send(new List.Query()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{imdbId}")]
        public async Task<IActionResult> DeleteAsync(string imdbId)
        {
            try
            {
                var success = await _mediator.Send(new Delete.Command { ImdbId = imdbId });
                return success ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
