using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Application.Idempotency;
using MediatR;

namespace AutomationProvider.Application.Behaviours
{
    internal sealed class IdempotentCommandPipelineBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IdempotentCommand<TResponse>
    {
        private readonly IIdempotencyService _idempotencyService;
        public IdempotentCommandPipelineBehaviour(IIdempotencyService idempotencyService)
        {
            _idempotencyService = idempotencyService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(await _idempotencyService.RequestExsistsAsync(request.RequestId))
            {
                return default(TResponse);
            }

            await _idempotencyService.CreateRequestAsync(request.RequestId, typeof(TRequest).Name);

            var response = await next();

            return response;
        }
    }
}
