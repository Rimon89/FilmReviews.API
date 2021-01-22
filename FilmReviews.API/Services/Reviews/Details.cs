using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Reviews
{
    public class Details
    {
        public class Query : IRequest<Review>
        {
            public Guid ReviewId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Review>
        {
            private readonly IReviewRepository _reviewRepo;

            public Handler(IReviewRepository reviewRepo)
            {
                _reviewRepo = reviewRepo;
            }
            public async Task<Review> Handle(Query request, CancellationToken cancellationToken)
            {
                var review = await _reviewRepo.Find(request.ReviewId);
                return review;
            }
        }
    }
}
