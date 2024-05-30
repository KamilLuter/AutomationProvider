using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Services.ProductService.Queries.GetProductDetailedData;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.Order;
using AutomationProvider.Domain.OrderAggregate.Entities;
using ErrorOr;
using MediatR;
using System.Reflection.Metadata.Ecma335;


namespace AutomationProvider.Application.Services.OrderService.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderCommandHandler(IProductRepository productRepository
            , IOrderRepository orderRepository
            , IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderLines = new List<OrderLine>();

            if (request.OrderLines is null || request.OrderLines.Count() == 0)
                return Errors.Order.ZeroOrderLines;

            foreach (var orderLine in request.OrderLines)
            {
                var productId = new Guid(orderLine.ProductId);
                var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);

                if (product is null)
                    return Errors.Order.ProductDoesntExsist;

                var price = Money.Create(orderLine.Price.Value, orderLine.Price.Currency);
                if (price.IsError)
                    return price.Errors;

                var newLine = OrderLine.Create(productId, orderLine.Quantity, price.Value);

                if (newLine.IsError)
                    return newLine.Errors;
                else
                    orderLines.Add(newLine.Value);
            }

            var addressRequest = request.Address;

            var address = Address.Create(addressRequest.Street
                , addressRequest.StreetNumber
                , addressRequest.City
                , addressRequest.ZipCode
                , addressRequest.Country);

            var order = Order.Create(orderLines, address.Value, request.UserId);

            if (order.Value.HasDifferentCurrencies())
                return Errors.Order.HasDifferentCurrencies;

            if (!order.IsError)
            {
                await _orderRepository.CreateOrderAsync(order.Value, cancellationToken);
                await _unitOfWork.SaveChangesAsync();
            }

            return order;
        }
    }
}
