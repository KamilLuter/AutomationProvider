using AutomationProvider.Contracts.Products;

namespace AutomationProvider.Contracts.Orders
{
    public record GetOrderDetailsResponse(
        DateTime OrderPlaced
        , DateTime OrderShipped
        , string Status
        , MoneyResponseContract Sum
        , List<OrderLineResponse> OrderLine
        , AddressContract Address
        , string UserName);

    public record OrderLineResponse(
        string ProductId
        , string ProductName
        , int Quantity
        , MoneyResponseContract Price);

    public record AddressContract(
        string Street
        , string StreetNumber
        , string City
        , string ZipCode
        , string Country);
}
