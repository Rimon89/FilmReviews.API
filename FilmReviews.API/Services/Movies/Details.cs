using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using MediatR;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Movies
{
    public class Details
    {
        public class Query : IRequest<Movie>
        {
            public string ImdbId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Movie>
        {
            private readonly IMovieRepository _movieRepo;
            private readonly IHttpClientFactory _clientFactory;
            private readonly IMediator _mediator;

            public Handler(IMovieRepository movieRepo, IHttpClientFactory clientFactory, IMediator mediator)
            {
                _movieRepo = movieRepo;
                _clientFactory = clientFactory;
                _mediator = mediator;
            }
            public async Task<Movie> Handle(Query request, CancellationToken cancellationToken)
            {
                var movie = await _movieRepo.Find(request.ImdbId);
                if (movie == null)
                {
                    var client = _clientFactory.CreateClient("omdb");
                    movie = await client.GetFromJsonAsync<Movie>($"?i={request.ImdbId}&apikey=6cf600c0");

                    if (movie.ImdbID != null)
                        await _mediator.Send(new Create.Command { Movie = movie });
                }
                return movie;
            }
        }
    }
}
