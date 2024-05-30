using AutomationProvider.Domain.Ratings;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.ReviewService.Commands
{
    public record AddCommentCommand(
        Guid ProductId,
        Guid? CustomerId,
        Guid? ParentReviewId,
        string CustomerName,
        string Comment,
        int? Rating) : IRequest<ErrorOr<Review>>;
}
