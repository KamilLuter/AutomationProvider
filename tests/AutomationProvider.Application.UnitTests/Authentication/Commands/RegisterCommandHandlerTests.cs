using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Services.OrderService.CreateOrder;
using AutomationProvider.Contracts.Orders;
using AutomationProvider.Domain.OrderAggregate;
using ErrorOr;
using Moq;
using FluentAssertions;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.Product;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.Common.Enums;

namespace AutomationProvider.Application.UnitTests.Authentication.Commands
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        public RegisterCommandHandlerTests()
        {
            _productRepositoryMock = new();
            _orderRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenOrderHasZeroLines()
        {
            // Arrange
            var command = new CreateOrderCommand(
                requestId: Guid.NewGuid(),
                UserId: Guid.NewGuid(),
                Address: new AddressContract(
                    Street: "123 Main St",
                    StreetNumber: "1",
                    City: "City",
                    ZipCode: "12345",
                    Country: "Country"),
                PaymentDetails: new PaymentCommand(
                    PaymentMethod: "CreditCard",
                    CardNumber: "1234 5678 9012 3456"),
                Sum: new MoneyCommand(
                    Value: 100.0m,
                    Currency: "USD"),
                OrderLines: new List<OrderLineCommand>());

            var product = Product.Create(
                        "name",
                        "desc",
                        "manu",
                        Guid.NewGuid(),
                        Money.Create(1, Currency.PLN).Value,
                        new Dictionary<string, object>(),
                        new List<Guid>());

            _productRepositoryMock.Setup(
                x => x.GetProductByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _orderRepositoryMock.Setup(
                x => x.CreateOrderAsync(
                    It.IsAny<Order>(),
                    It.IsAny<CancellationToken>()));

            var handler = new CreateOrderCommandHandler(
                _productRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            ErrorOr<Order> result = await handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(Errors.Order.ZeroOrderLines);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenCurrenciesAreDifferent()
        {
            // Arrange
            var command = new CreateOrderCommand(
                requestId: Guid.NewGuid(),
                UserId: Guid.NewGuid(),
                Address: new AddressContract(
                    Street: "123 Main St",
                    StreetNumber: "1",
                    City: "City",
                    ZipCode: "12345",
                    Country: "Country"),
                PaymentDetails: new PaymentCommand(
                    PaymentMethod: "CreditCard",
                    CardNumber: "1234 5678 9012 3456"),
                Sum: new MoneyCommand(
                    Value: 100.0m,
                    Currency: "USD"),
                OrderLines: new List<OrderLineCommand>()
                {
                    new OrderLineCommand(
                        ProductId: Guid.NewGuid().ToString(),
                        Quantity: 2,
                        Price: new MoneyCommand(50.0m, "USD")),
                    new OrderLineCommand(
                        ProductId: Guid.NewGuid().ToString(),
                        Quantity: 1,
                        Price: new MoneyCommand(30.0m, "PLN"))
                }
                );

            var product = Product.Create(
                        "name",
                        "desc",
                        "manu",
                        Guid.NewGuid(),
                        Money.Create(1, Currency.PLN).Value,
                        new Dictionary<string, object>(),
                        new List<Guid>());

            _productRepositoryMock.Setup(
                x => x.GetProductByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _orderRepositoryMock.Setup(
                x => x.CreateOrderAsync(
                    It.IsAny<Order>(),
                    It.IsAny<CancellationToken>()));

            var handler = new CreateOrderCommandHandler(
                _productRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            ErrorOr<Order> result = await handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(Errors.Order.HasDifferentCurrencies);
        }
        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            // Arrange
            var command = new CreateOrderCommand(
                requestId: Guid.NewGuid(),
                UserId: Guid.NewGuid(),
                Address: new AddressContract(
                    Street: "123 Main St",
                    StreetNumber: "1",
                    City: "City",
                    ZipCode: "12345",
                    Country: "Country"),
                PaymentDetails: new PaymentCommand(
                    PaymentMethod: "CreditCard",
                    CardNumber: "1234 5678 9012 3456"),
                Sum: new MoneyCommand(
                    Value: 100.0m,
                    Currency: "USD"),
                OrderLines: new List<OrderLineCommand>()
                {
                    new OrderLineCommand(
                        ProductId: Guid.NewGuid().ToString(),
                        Quantity: 2,
                        Price: new MoneyCommand(50.0m, "USD")),
                    new OrderLineCommand(
                        ProductId: Guid.NewGuid().ToString(),
                        Quantity: 1,
                        Price: new MoneyCommand(30.0m, "USD"))
                }
                );

            var product = Product.Create(
                        "name",
                        "desc",
                        "manu",
                        Guid.NewGuid(),
                        Money.Create(1, Currency.PLN).Value,
                        new Dictionary<string, object>(),
                        new List<Guid>());

            _productRepositoryMock.Setup(
                x => x.GetProductByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _orderRepositoryMock.Setup(
                x => x.CreateOrderAsync(
                    It.IsAny<Order>(),
                    It.IsAny<CancellationToken>()));

            var handler = new CreateOrderCommandHandler(
                _productRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            ErrorOr<Order> result = await handler.Handle(command, default);

            // Assert
            result.IsError.Should().BeFalse();
        }
    }
}
