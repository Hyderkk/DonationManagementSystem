using KSquare.DMS.Repositories.Context;
using KSquare.DMS.Repositories.Interfaces;
using KSquare.DMS.Repositories.RepositoriesImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KSquare.DMS.Repositories.Config
{
    public static class RegisterDALRepositories
    {
        public static IServiceCollection _services;
        public static IConfiguration _configuration;

        public static void RegisterComponents(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

            var connection = _configuration.GetSection("ConnectionStrings").GetSection("SQLSERVERDB").Value;

            _services.AddEntityFrameworkSqlServer()
                .AddDbContext<KSquareContext>(
                    options => options.UseSqlServer(connection)
                );

            //Only register our unitofwork and its factory.
            _services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
            _services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
