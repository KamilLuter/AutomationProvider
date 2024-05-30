using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.Ratings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Persistance.Repositories
{
    internal class ReviewRepository : IReviewRepository
    {
        private readonly DbSet<Review> _reviews;
        public ReviewRepository(AutomationProviderDbContext dbContext)
        {
            _reviews = dbContext.Reviews;
        }
        public async Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken)
        {
            await _reviews.AddAsync(review, cancellationToken);
            return review;
        }

        public Task UpdateReviewRatingAsync(Review review, CancellationToken cancellationToken)
        {
            _reviews.Attach(review);
            _reviews.Entry(review).Property(r => r.Rating).IsModified = true;
            return Task.CompletedTask;
        }

        public async Task DeleteReviewAsync(Guid id, CancellationToken cancellationToken)
        {
            var review = await _reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (review is not null)
            {
                _reviews.Remove(review);
            }
        }

        public async Task<Review?> GetReviewByIdAsync(
            Guid id, CancellationToken cancellationToken)
        {
            return await _reviews
                            .Include(r => r.SubComments)
                            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<IQueryable<Review>?> GetReviewsByCustomerId(
            Guid userId, int page, int take, CancellationToken cancellationToken)
        {
            var query = _reviews.Where(r => r.CustomerId == userId)
                                .Skip((page - 1) * take)
                                .Take(take);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<Review>?> GetReviewsByProductId(
            Guid productId,
            int page,
            int limit,
            int subCommentLimit,
            CancellationToken cancellationToken)
        {
            var query = _reviews.Where(r => r.ProductId == productId && r.ParentReview == null)
                                .Skip((page - 1) * limit)
                                .Take(limit)
                                .Include(r => r.SubComments.Take(subCommentLimit));

            return await Task.FromResult(query);
        }
    }
}
