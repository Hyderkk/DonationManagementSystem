using KSquare.DMS.Services.Interfaces;
using KSquare.DMS.Services.ServicesImpl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KSquare.DMS.Services.Config
{
    public static class RegisterServices
    {
        public static IServiceCollection _services;
        public static IConfiguration _configuration;

        public static void RegisterComponents(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

            //Register all the services here
            _services.AddTransient<IUserService, UserService>();
            _services.AddTransient<ITokenService, TokenService>();
            _services.AddTransient<IAdminService, AdminService>();

        }
    }
}
