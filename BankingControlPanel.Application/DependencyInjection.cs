using BankingControlPanel.Application.Authentication;
using BankingControlPanel.Application.Shared.Cache.Abstraction;
using BankingControlPanel.Application.Shared.Cache.Concrete;
using BankingControlPanel.Domain.ClientManagement.Respository;
using BankingControlPanel.Domain.UserManagement.Repository;
using BankingControlPanel.Infrastructure.Repositories;
using BankingControlPanel.Shared.Infrastructure.Behaviors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BankingControlPanel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISearchSuggestionCacheService, SearchSuggestionCacheService>();
            services.AddScoped<ApplicationContext, ApplicationContext>(ctx => ApplicationContextFactory.Create(ctx));

            services.Configure<AuthenticationConfig>(configuration.GetSection(nameof(AuthenticationConfig)));

            return services;
        } 
    }
}
