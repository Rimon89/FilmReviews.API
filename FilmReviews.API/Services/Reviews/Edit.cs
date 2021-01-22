using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using FilmReviews.API.Services.Errors;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Reviews
{
    public class Edit
    {
        public class Command : IRequest<Review>
        {
            public Review Review { get; set; }
        }
        public class Handler : IRequestHandler<Command, Review>
        {
            private readonly IReviewRepository _reviewRepo;

            public Handler(IReviewRepository reviewRepo)
            {
                _reviewRepo = reviewRepo;
            }
            public async Task<Review> Handle(Command request, CancellationToken cancellationToken)
            {
                var review = await _reviewRepo.Find(request.Review.Id);

                if (review == null)
                    throw new RestException(HttpStatusCode.NotFound, new { review = "Not found" });

                review.Name = request.Review.Name;
                review.MovieReview = request.Review.MovieReview;
                review.Rating = request.Review.Rating;
                review.ReviewDate = request.Review.ReviewDate;

                await _reviewRepo.Update(review);

                return review;
            }
        }
    }
}
