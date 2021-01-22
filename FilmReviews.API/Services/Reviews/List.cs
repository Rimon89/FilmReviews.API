using FilmReviews.API.Contracts;
using FilmReviews.API.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FilmReviews.API.Services.Reviews
{
    public class List
    {
        public class Query : IRequest<ICollection<Review>> { }
        public class Handler : IRequestHandler<Query, ICollection<Review>>
        {
            private readonly IReviewRepository _reviewrepo;

            public Handler(IReviewRepository reviewrepo)
            {
                _reviewrepo = reviewrepo;
            }

            public async Task<ICollection<Review>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reviews = await _reviewrepo.GetAll();

                return reviews;
            }
        }
    }
}
