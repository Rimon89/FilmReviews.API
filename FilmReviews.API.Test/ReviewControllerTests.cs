using FilmReviews.API.Controllers;
using FilmReviews.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static FilmReviews.API.Services.Reviews.Create;

namespace FilmReviews.API.Test
{
    public class ReviewControllerTests
    {
        private readonly ReviewController _sut;
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public ReviewControllerTests()
        {
            _sut = new ReviewController(_mediator.Object);
        }

        [Fact]
        public async Task CreateAsync_ReturnsStatus200Ok_WhenReviewWasCreated()
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                Name = "a",
                MovieReview = "b",
                MovieTitle = "c",
                Author = "d",
                ImdbId = "e",
                Rating = 1,
                ReviewDate = DateTime.Today
            };
            _mediator.Setup(m => m.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var result = await _sut.CreateAsync(review);

            Assert.IsType<OkResult>(result);
            Assert.NotNull(result);
            _mediator.Verify();
        }

        [Fact]
        public async Task CreateAsync_ReturnsBadRequest_WhenReviewWasNotCreated()
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                Name = "a",
                MovieReview = "b",
                MovieTitle = "c",
                Author = "d",
                ImdbId = "e",
                Rating = 1,
                ReviewDate = DateTime.Today
            };
            _mediator.Setup(m => m.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var result = await _sut.CreateAsync(review);

            Assert.IsType<BadRequestResult>(result);
            Assert.NotNull(result);
            _mediator.Verify();
        }

        [Fact]
        public async Task CreateAsync_ReturnsBadRequest_WhenAnExceptionIsThrown()
        {
            Review review = null;
            _mediator.Setup(m => m.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var result = await _sut.CreateAsync(review);

            Assert.IsType<BadRequestResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetReviewAsync_ReturnsOkWithTheReview_WhenReviewIsFoundInDb()
        {
            var id = Guid.NewGuid();
            var review = new Review
            {
                Id = id,
                Name = "a",
                MovieReview = "b",
                MovieTitle = "c",
                Author = "d",
                ImdbId = "e",
                Rating = 1,
                ReviewDate = DateTime.Today
            };
            _mediator.Setup(m => m.Send(It.IsAny<Services.Reviews.Details.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(review);

            var result = await _sut.GetReviewAsync(id);

            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Review>(actionResult.Value);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async Task GetReviewAsync_ReturnsNotFound_WhenReviewDoesNotExistsInDb()
        {
            var id = Guid.NewGuid();
            Review review = null;
            _mediator.Setup(m => m.Send(It.IsAny<Services.Reviews.Details.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(review);

            var result = await _sut.GetReviewAsync(id);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkObjectResult_WhenRetrievingAllReviews()
        {
            var reviews = new List<Review>();
            reviews.Add(new Review
            {
                Id = Guid.NewGuid(),
                Name = "a",
                MovieReview = "b",
                MovieTitle = "c",
                Author = "d",
                ImdbId = "e",
                Rating = 1,
                ReviewDate = DateTime.Today
            });
            reviews.Add(new Review
            {
                Id = Guid.NewGuid(),
                Name = "a",
                MovieReview = "b",
                MovieTitle = "c",
                Author = "d",
                ImdbId = "e",
                Rating = 1,
                ReviewDate = DateTime.Today
            });
            _mediator.Setup(m => m.Send(It.IsAny<Services.Reviews.List.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(reviews);

            var result = await _sut.GetAllAsync();

            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var models = Assert.IsAssignableFrom<List<Review>>(actionResult.Value);
            Assert.Equal(2, models.Count);
        }
    }
}
