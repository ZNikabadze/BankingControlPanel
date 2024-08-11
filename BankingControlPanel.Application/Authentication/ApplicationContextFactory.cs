using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace BankingControlPanel.Application.Authentication
{
    public class ApplicationContextFactory
    {
        public static ApplicationContext Create(IServiceProvider serviceProvider )
        {
            var httpContext = serviceProvider.GetService<IHttpContextAccessor>()
                ?.HttpContext;

            if (httpContext == null || httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return new ApplicationContext
                {
                    Username = string.Empty
                };
            };

            var username = httpContext.User.FindFirst(ClaimTypes.Name);
            var role = httpContext.User.FindFirst(ClaimTypes.Role);

            return new ApplicationContext
            {
                Username = username.Value,
                Role = role?.Value,
            };
        }
    }
}
