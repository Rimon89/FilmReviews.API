using FilmReviews.API.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Movies
{
    public class Delete
    {
        public class Command : IRequest<bool>
        {
            public string ImdbId { get; set; }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IMovieRepository _movieRepo;

            public Handler(IMovieRepository movieRepo)
            {
                _movieRepo = movieRepo;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _movieRepo.Delete(request.ImdbId);
                return success;
            }
        }
    }
}
