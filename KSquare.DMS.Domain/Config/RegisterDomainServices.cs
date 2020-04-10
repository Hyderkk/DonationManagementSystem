
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KSquare.DMS.Domain.Config
{
    public static class RegisterDomainServices
    {
        public static IServiceCollection _services;
        public static IConfiguration _configuration;

        public static void RegisterComponents(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

            _services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
