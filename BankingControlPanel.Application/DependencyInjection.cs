﻿using BankingControlPanel.Domain.ClientManagement.Respository;
using BankingControlPanel.Domain.UserManagement.Repository;
using BankingControlPanel.Infrastructure.Repositories;
using BankingControlPanel.Shared.Infrastructure.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BankingControlPanel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        } 
    }
}
