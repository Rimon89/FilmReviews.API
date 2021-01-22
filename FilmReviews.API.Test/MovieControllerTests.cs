using FilmReviews.API.Controllers;
using FilmReviews.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FilmReviews.API.Test
{
    public class MovieControllerTests
    {
        private readonly MovieController _sut;
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();

        public MovieControllerTests()
        {
            _sut = new MovieController(_mediator.Object);
        }

        [Fact]
        public async Task GetMovieAsync_ShouldReturnMovie_WhenMovieWithTheImdbIdExists()
        {
            var movieId = "12345";
            var movieTitle = "abc";
            var movie = new Movie
            {
                ImdbID = movieId,
                Title = movieTitle
            };

            _mediator.Setup(m => m.Send(It.IsAny<Services.Movies.Details.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(movie);

            var result = await _sut.GetMovieAsync(movieId);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Movie>(actionResult.Value);
            Assert.Equal(movieId, model.ImdbID);
            Assert.Equal(movieTitle, model.Title);
            Assert.NotNull(model);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMovieAsync_ShouldReturnNotFound_WhenReturnedMovieIsNull()
        {
            var imdbId = "abc";
            var movie = new Movie();

            _mediator.Setup(m => m.Send(It.IsAny<Services.Movies.Details.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(movie);

            var result = await _sut.GetMovieAsync(imdbId);

            Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(result);
        }
    }
}
