using AutomationProvider.Application.Services.CustomerService;
using AutomationProvider.Contracts.Authentication;
using AutomationProvider.Contracts.Orders;
using AutomationProvider.Contracts.User;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.CustomerAggregate;
using Mapster;
using System.Xml.Linq;

namespace AutomationProvider.Api.Commmon.Mapping
{
    public class CustomerConfig: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, GetCustomerDetailsResponse>()
                .Map(dest => dest.Email, src => src.Email.Value)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Address, src => src.Address.Adapt<AddressContract>());

            config.NewConfig<(UpdateCustomerProfileRequest data, string Id), UpdateCustomerDetailsCommand>()
                .Map(dest => dest.CustomerId, src => src.Id)
                .Map(dest => dest.FirstName, src => src.data.FirstName)
                .Map(dest => dest.LastName, src => src.data.LastName)
                .Map(dest => dest.Address, src => src.data.Address.Adapt<AddressContract>())
                .Map(dest => dest.Payment, src => src.data.PrefferedPayment.Adapt<PaymentContract>());
        }
    }
}
