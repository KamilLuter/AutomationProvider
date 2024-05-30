using AutomationProvider.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.ReviewService.Queries
{
    public record GetReviewByIdQuery(
        Guid ReviewId) : IRequest<ErrorOr<Review>>;
}
