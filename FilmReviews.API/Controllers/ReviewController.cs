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
            var reviewFromDb = await _reviewRepo.Find(review.Id);

            if (reviewFromDb != null)
                return BadRequest();

            var success = await _reviewRepo.Create(review);

            if(success)
                return Ok();

            return BadRequest();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepo.GetAll();

            return Ok(reviews);
        }
    }
}
