using AutomationProvider.Contracts.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Orders
{
    public record CreateOrderRequest(
        Guid UserId
        , AddressContract Address
        , PaymentContract PaymentDetails
        , MoneyResponseContract Sum
        , List<OrderLineRequest> OrderLines);

    public record OrderLineRequest(
        Guid ProductId
        , int Quantity
        , MoneyResponseContract Price);

    public record PaymentContract(
        string PaymentMethod
        , string CardNumber);
}
