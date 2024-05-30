using AutomationProvider.Application.Services.OrderService.CreateOrder;
using AutomationProvider.Application.Services.OrderService.GetOrdersByDate;
using AutomationProvider.Contracts.Orders;
using Mapster;
using System.Globalization;

namespace AutomationProvider.Api.Common.Mapping 
{
    public class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(GetOrdersByDateRequest request, Guid id, DateTime defaultToDateTime), GetOrdersByDateQuery>()
                .Map(dest => dest.CustomerId, src => src.id)
                .Map(dest => dest.FromDate, 
                     src => !string.IsNullOrEmpty(src.request.from) 
                        ? DateTime.ParseExact(src.request.from.Replace("-", "/") + " 00:00:00"
                                                                        , "dd/MM/yyyy HH:mm:ss"
                                                                        , CultureInfo.InvariantCulture) 
                        : DateTime.MinValue)
                .Map(dest => dest.ToDate, src => !string.IsNullOrEmpty(src.request.to) 
                        ? DateTime.ParseExact(src.request.to.Replace("-", "/") + " 23:59:59" 
                                                                     , "dd/MM/yyyy HH:mm:ss"
                                                                     , CultureInfo.InvariantCulture) 
                        : src.defaultToDateTime)
                .Map(dest => dest.Page, src => src.request.page);

            config.NewConfig<(CreateOrderRequest request, Guid idempotencyKey), CreateOrderCommand>()
                .Map(dest => dest.requestId, src => src.idempotencyKey)
                .Map(dest => dest.UserId, src => src.request.UserId)
                .Map(dest => dest.Address, src => src.request.Address)
                .Map(dest => dest.PaymentDetails, src => src.request.PaymentDetails.Adapt<PaymentCommand>())
                .Map(dest => dest.Sum, src => src.request.Sum)
                .Map(dest => dest.OrderLines, src => src.request.OrderLines.Adapt<List<OrderLineCommand>>());

            config.NewConfig<OrderLineRequest, OrderLineCommand>()
                  .Map(dest => dest.ProductId, src => src.ProductId)
                  .Map(dest => dest.Quantity, src => src.Quantity)
                  .Map(dest => dest.Price, src => src.Price);

            config.NewConfig<PaymentContract, PaymentCommand>()
                  .Map(dest => dest.PaymentMethod, src => src.PaymentMethod)
                  .Map(dest => dest.CardNumber, src => src.CardNumber);
        }
    }
}
