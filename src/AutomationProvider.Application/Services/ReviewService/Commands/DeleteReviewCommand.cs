using MediatR;
using ErrorOr;

namespace AutomationProvider.Application.Services.ReviewService.Commands
{
    public record DeleteReviewCommand(
        Guid ReviewId,
        Guid CustomerId): IRequest<List<Error>?>;
}
