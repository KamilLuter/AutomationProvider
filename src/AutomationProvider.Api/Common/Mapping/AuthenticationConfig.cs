using AutomationProvider.Application.Services.AuthenticationService.Commands.Register;
using AutomationProvider.Application.Services.AuthenticationService.Common;
using AutomationProvider.Contracts.Authentication;
using Mapster;

namespace AutomationProvider.Api.Commmon.Mapping
{
    public class AuthenticationConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Email, src => src.user.Email.Value)
                .Map(dest => dest.FirstName, src => src.user.FirstName)
                .Map(dest => dest.LastName, src => src.user.LastName)
                .Map(dest => dest.Id, src => src.user.Id);

            config.NewConfig<(RegisterRequest req, string reqId), RegisterCommand>()
                .Map(dest => dest.RequestId, src => src.reqId)
                .Map(dest => dest.FirstName, src => src.req.FirstName)
                .Map(dest => dest.LastName, src => src.req.LastName)
                .Map(dest => dest.Email, src => src.req.Email)
                .Map(dest => dest.Password, src => src.req.Password)
                .Map(dest => dest.PasswordConfirmation, src => src.req.PasswordConfirmation);     
        }
    }
}
