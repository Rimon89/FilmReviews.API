using FilmReviews.API.Contracts;
using FilmReviews.API.Controllers;
using FilmReviews.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmReviews.API.Test
{
    public class ReviewControllerTests
    {
        private readonly ReviewController _sut;
        private readonly Mock<IReviewRepository> _reviewRepoMock = new Mock<IReviewRepository>();

        public ReviewControllerTests()
        {
            _sut = new ReviewController(_reviewRepoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ReturnsStatus200Ok_WhenReviewWasCreated()
        {
            //Arrange
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
            _reviewRepoMock.Setup(x => x.Create(review)).ReturnsAsync(true);

            //Act
            var result = await _sut.CreateAsync(review);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsBadRequest_WhenReviewWasNotCreated()
        {
            //Arrange
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
            _reviewRepoMock.Setup(x => x.Create(review)).ReturnsAsync(false);

            //Act
            var result = await _sut.CreateAsync(review);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsBadRequest_WhenAnExceptionIsThrown()
        {
            //Arrange
            Review review = null;

            //Act
            var result = await _sut.CreateAsync(review);

            //Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetReview_ReturnsOkWithTheReview_WhenReviewIsFoundInDb()
        {
            //Arrange
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
            _reviewRepoMock.Setup(x => x.Find(id)).ReturnsAsync(review);

            //Act
            var result = await _sut.GetReviewAsync(id);

            //Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Review>(actionResult.Value);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async Task GetReview_ReturnsNotFound_WhenReviewDoesNotExistsInDb()
        {
            //Arrange
            var id = Guid.NewGuid();
            Review review = null;
            _reviewRepoMock.Setup(x => x.Find(id)).ReturnsAsync(review);

            //Act
            var result = await _sut.GetReviewAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetReviewAsync_ReturnsOkObjectResult_WhenGetAllGetsCalled()
        {
            //Arrange
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
            _reviewRepoMock.Setup(x => x.GetAll()).ReturnsAsync(reviews);

            //Act
            var result = await _sut.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var models = Assert.IsAssignableFrom<List<Review>>(actionResult.Value);
            Assert.Equal(2, models.Count);
        }
    }
}
