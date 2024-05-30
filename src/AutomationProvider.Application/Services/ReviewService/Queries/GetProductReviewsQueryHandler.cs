using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.Ratings;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.ReviewService.Queries
{
    public class GetProductReviewsQueryHandler : IRequestHandler<GetProductReviewsQuery, ErrorOr<List<Review>?>>
    {
        private readonly IReviewRepository _reviewRepository;
        private const int MaxNumberOfReviews = 10;
        private const int MaxNumberOfSubcomments = 10;
        public GetProductReviewsQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<ErrorOr<List<Review>?>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetReviewsByProductId(
                                    request.ProductsId,
                                    request.page,
                                    MaxNumberOfReviews,
                                    MaxNumberOfSubcomments,
                                    cancellationToken);

            return reviews is null ? new List<Review>() : reviews.ToList();
        }
    }
}
