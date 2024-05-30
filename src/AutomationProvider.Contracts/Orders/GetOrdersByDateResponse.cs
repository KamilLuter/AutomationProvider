using AutomationProvider.Contracts.Products;
using System.Security.Cryptography.X509Certificates;

namespace AutomationProvider.Contracts.Orders
{
    public record GetOrdersByDateResponse(
        List<GetOrderResponse> Orders);

    public record GetOrderResponse(
        DateTime OrderPlaced
        , DateTime OrderShipped
        , string Status
        , MoneyResponseContract Sum
        , List<OrderLineResponse> OrderLine);
}
