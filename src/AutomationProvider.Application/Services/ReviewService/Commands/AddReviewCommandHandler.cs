using AutomationProvider.Application.Common.Interfaces.Persistance;
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
    public class AddReviewCommandHandler : IRequestHandler<AddCommentCommand, ErrorOr<Review>>
    {
        private readonly IReviewRepository _reviewsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddReviewCommandHandler(IReviewRepository reviewsRepository, IUnitOfWork unitOfWork)
        {
            _reviewsRepository = reviewsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Review>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            Review? parentReview = null;

            if (request.ParentReviewId is not null)
            {
                parentReview = 
                    await _reviewsRepository
                        .GetReviewByIdAsync(request.ParentReviewId.Value, cancellationToken);
            }
            
            var review = Review.Create(
                request.CustomerName,
                request.Comment,
                request.Rating,
                request.ProductId,
                parentReview,
                request.CustomerId);

            await _reviewsRepository.AddReviewAsync(review, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            return review;
        }
    }
}
