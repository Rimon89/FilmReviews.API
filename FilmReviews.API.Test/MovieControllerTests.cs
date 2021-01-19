using FilmReviews.API.Contracts;
using FilmReviews.API.Controllers;
using FilmReviews.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmReviews.API.Test
{
    public class MovieControllerTests
    {
        private readonly MovieController _sut;
        private readonly Mock<IMovieRepository> _movieRepoMock = new Mock<IMovieRepository>();
        private readonly Mock<IHttpClientFactory> _clientFactoryMock = new Mock<IHttpClientFactory>();

        public MovieControllerTests()
        {
            _sut = new MovieController(_clientFactoryMock.Object, _movieRepoMock.Object);
        }

        [Fact]
        public async Task GetMovieAsync_ShouldReturnMovieFromDb_WhenMovieExists()
        {
            //Arrange
            var movieId = "12345";
            var movieTitle = "abc";
            var movieObj = new Movie
            {
                ImdbID = movieId,
                Title = movieTitle
            };
            _movieRepoMock.Setup(x => x.Find(movieId)).ReturnsAsync(movieObj);

            //Act
            var result = await _sut.GetMovieAsync(movieId);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Movie>(actionResult.Value);
            Assert.Equal(movieId, model.ImdbID);
            Assert.Equal(movieTitle, model.Title);
            Assert.NotNull(model);
        }

    }
}
