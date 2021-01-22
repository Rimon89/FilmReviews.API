using FilmReviews.API.Models;
using FilmReviews.API.Services.Reviews;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FilmReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(Review review)
        {
            try
            {
                var success = await _mediator.Send(new Create.Command { Review = review });
                return success ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewAsync(Guid id)
        {
            try
            {
                var review = await _mediator.Send(new Details.Query { ReviewId = id });
                return review != null ? Ok(review) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var success = await _mediator.Send(new Delete.Command { ReviewId = id });
                return success ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Review request)
        {
            try
            {
                return Ok(await _mediator.Send(new Edit.Command { Review = request }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
