using ErrorOr;

namespace AutomationProvider.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class Authorization
        {
            public static Error Unauthorized => Error.Unauthorized(code: "Authorization.Unathorized", description: "User not authorized");
        }
    }
}
