using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Reviews
{
    public class Create
    {
        public class Command : IRequest<bool>
        {
            public Review Review { get; set; }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IReviewRepository _reviewRepo;

            public Handler(IReviewRepository reviewRepo)
            {
                _reviewRepo = reviewRepo;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _reviewRepo.Create(request.Review);
                return success;
            }
        }
    }
}
