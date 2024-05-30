using AutomationProvider.Application.Idempotency;
using AutomationProvider.Application.Services.ProductService.Commands.Common;
using AutomationProvider.Domain.Common.ValueObjects;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.ProductService.Commands.CreateProductCommand
{
    public record CreateProductCommand(
        Guid requestId,
        string Name,
        string Description,
        string Manufacturer,
        Guid CategoryId,
        Money Price,
        Dictionary<string, object> ProductAttributes): IdempotentCommand<ErrorOr<ProductResult>>(requestId);
}
