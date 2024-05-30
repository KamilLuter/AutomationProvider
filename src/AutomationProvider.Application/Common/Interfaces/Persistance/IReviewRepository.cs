using AutomationProvider.Domain.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Common.Interfaces.Persistance
{
    public interface IReviewRepository
    {
        Task<Review?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken);
        Task DeleteReviewAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateReviewRatingAsync(Review review, CancellationToken cancellationToken);
        Task<IQueryable<Review>?> GetReviewsByProductId(
            Guid productId,
            int page,
            int limit,
            int subCommentLimit,
            CancellationToken cancellationToken);
        Task<IQueryable<Review>?> GetReviewsByCustomerId(Guid UserId, int page, int take, CancellationToken cancellationToken);
    }
}
