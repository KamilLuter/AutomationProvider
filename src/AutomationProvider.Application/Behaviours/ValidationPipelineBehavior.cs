using AutomationProvider.Application.Services.ProductService.Queries.GetProducts;
using ErrorOr;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace AutomationProvider.Application.Behaviours
{
    internal class ValidationPipelineBehavior<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
            where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;
        public ValidationPipelineBehavior(IValidator<TRequest>? validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if(_validator == null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(validationResult.IsValid) 
            {
                return await next();
            }

            var errors = validationResult.Errors
                .ConvertAll(validationFailure => Error.Validation(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage));

            var response = (TResponse?)typeof(TResponse)
                .GetMethod(
                    name: nameof(ErrorOr<object>.From),
                    bindingAttr: BindingFlags.Static | BindingFlags.Public,
                    types: new[] { typeof(List<Error>) })?
                .Invoke(null, new[] { errors })!;

            return response;
        }
    }
}
