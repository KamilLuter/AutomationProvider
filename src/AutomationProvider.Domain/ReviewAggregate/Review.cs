using AutomationProvider.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomationProvider.Domain.Common.Errors.Errors;

namespace AutomationProvider.Domain.Ratings
{
    public class Review : AggregateRoot<Guid>
    {
        private readonly List<Review>? _subComments = new List<Review>();
        public string CustomerName { get; private set; }
        public string? Comment { get; private set; }
        public int? Rating { get; private set; }
        public Review? ParentReview { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid? CustomerId { get; private set; }
        public IReadOnlyList<Review>? SubComments => _subComments?.AsReadOnly();

        private Review(
            Guid reviewId,
            string customerName,
            string? comment,
            int? rating,
            Guid productId,
            Review? parentReview,
            Guid? customerId
            ): base(reviewId)
        {
            Id = reviewId;
            CustomerName = customerName;
            Comment = comment;
            ProductId = productId;
            Rating = rating;
            ParentReview = parentReview;
            CustomerId = customerId;
        }

        public static Review Create(
            string customerName,
            string? comment,
            int? rating,
            Guid productId,
            Review? parentReview,
            Guid? customerId)
        {
            return new Review(
                Guid.NewGuid(),
                customerName,
                comment,
                rating,
                productId,
                parentReview,
                customerId);
        }

        public Task DeleteReviewAsync()
        {
            CustomerName = String.Empty;
            Comment = String.Empty;
            Rating = null;
            CustomerId = null;

            return Task.CompletedTask;
        }

#pragma warning disable CS8618
        protected Review()
        {
        }
#pragma warning restore CS8618
    }
}
