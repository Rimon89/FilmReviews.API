using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Movies
{
    public class List
    {
        public class Query : IRequest<ICollection<Movie>> { }
        public class Handler : IRequestHandler<Query, ICollection<Movie>>
        {
            private readonly IMovieRepository _movieRepo;

            public Handler(IMovieRepository movieRepo)
            {
                _movieRepo = movieRepo;
            }

            public async Task<ICollection<Movie>> Handle(Query request, CancellationToken cancellationToken)
            {
                var movies = await _movieRepo.GetAll();

                return movies;
            }
        }
    }
}
