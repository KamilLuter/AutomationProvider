using AutomationProvider.Domain.OrderAggregate;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.OrderService.GetOrdersByDate
{
    public record GetOrdersByDateQuery(
        Guid CustomerId
        , DateTime FromDate
        , DateTime ToDate
        , int Page): IRequest<ErrorOr<List<Order>?>>;

}
