using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.Ratings;
using ErrorOr;
using MediatR;
using AutomationProvider.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.ReviewService.Queries
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ErrorOr<Review>>
    {
        private readonly IReviewRepository _reviewRepository;
        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<ErrorOr<Review>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);

            if (review is null)
                return Errors.Review.NotFound;

            return review;
        }
    }
}
