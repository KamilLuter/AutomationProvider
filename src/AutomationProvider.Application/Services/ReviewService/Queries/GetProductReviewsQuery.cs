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
    public record GetProductReviewsQuery(
        Guid ProductsId,
        int page): IRequest<ErrorOr<List<Review>?>>;
}
