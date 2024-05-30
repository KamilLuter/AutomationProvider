using AutomationProvider.Application.Services.ReviewService.Commands;
using AutomationProvider.Contracts.Reviews;
using AutomationProvider.Domain.Ratings;
using Mapster;

namespace AutomationProvider.Api.Common.Mapping
{
    public class ReviewConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(AddCommentRequest request, Guid? customerId), AddCommentCommand>()
                .Map(dest => dest.CustomerId, src => src.customerId)
                .Map(dest => dest.ProductId, src => src.request.ProductId)
                .Map(dest => dest.ParentReviewId, src => src.request.ParentReviewId)
                .Map(dest => dest.CustomerName, src => src.request.CustomerName)
                .Map(dest => dest.Comment, src => src.request.Comment)
                .Map(dest => dest.Rating, src => src.request.Rating);

            config.NewConfig<Review, ReviewResult>()
                .Map(dest => dest.ReviewId, src => src.Id)
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.ProductId, src => src.ProductId)
                .Map(dest => dest.ParentReviewId, src => src.ParentReview.Id)
                .Map(dest => dest.CustomerName, src => src.CustomerName)
                .Map(dest => dest.Comment, src => src.Comment)
                .Map(dest => dest.Rating, src => src.Rating);

            config.NewConfig<Review, GetReviewsResult>()
                .Map(dest => dest.ReviewId, src => src.Id)
                .Map(dest => dest.ProductId, src => src.ProductId)
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.CustomerName, src => src.CustomerName)
                .Map(dest => dest.Comment, src => src.Comment)
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.SubComments, src => src.SubComments);
        }
    }
}
