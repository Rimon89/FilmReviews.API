using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(Review review)
        {
            try
            {
                var success = await _reviewRepo.Create(review);

                if (success)
                    return Ok();

                return BadRequest();
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
                var review = await _reviewRepo.Find(id);

                if (review == null)
                    return NotFound();

                return Ok(review);
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
                var reviews = await _reviewRepo.GetAll();

                return Ok(reviews);
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
                var success = await _reviewRepo.Delete(id);

                if (success)
                    return Ok();

                return BadRequest();
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
                var reviewFromDb = await _reviewRepo.Find(request.Id);

                if (reviewFromDb == null)
                    return NotFound();

                reviewFromDb.Name = request.Name;
                reviewFromDb.MovieReview = request.MovieReview;
                reviewFromDb.Rating = request.Rating;
                reviewFromDb.ReviewDate = request.ReviewDate;

                await _reviewRepo.Update(reviewFromDb);

                return Ok(reviewFromDb);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
