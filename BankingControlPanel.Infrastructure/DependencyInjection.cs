using BankingControlPanel.Domain.UserManagement;
using BankingControlPanel.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace BankingControlPanel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database

            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<BankingControlPanelDbContext>(options =>
                                    options.UseSqlServer(connectionString));

            #endregion

            #region Identity

           
            #endregion

            return services;
        }
    }
}
