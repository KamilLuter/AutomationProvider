using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.Ratings;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationProvider.Domain.Common.Errors;

namespace AutomationProvider.Application.Services.ReviewService.Commands
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, List<Error>?>
    {
        private readonly IReviewRepository _reviewsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteReviewCommandHandler(IReviewRepository reviewsRepository, IUnitOfWork unitOfWork)
        {
            _reviewsRepository = reviewsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Error>?> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewsRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);

            var errors = new List<Error>();

            if (review is null)
            {
                errors.Add(Errors.Review.NotFound);
                return errors;
            }


            if (!review.CustomerId.Equals(request.CustomerId))
            {
                errors.Add(Errors.Review.Unauthorized);
                return errors;
            }

            await review.DeleteReviewAsync();

            await _unitOfWork.SaveChangesAsync();

            return null;
        }
    }
}
