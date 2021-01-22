using FilmReviews.API.Contracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Reviews
{
    public class Delete
    {
        public class Command : IRequest<bool>
        {
            public Guid ReviewId { get; set; }
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
                var success = await _reviewRepo.Delete(request.ReviewId);
                return success;
            }
        }
    }
}
