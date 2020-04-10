using KSquare.DMS.API.Extensions;
using KSquare.DMS.Domain.Config;
using KSquare.DMS.Repositories.Config;
using KSquare.DMS.Services.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace KSquare.DMS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILogger<Startup> logger)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();

            _logger = logger;

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public static SymmetricSecurityKey signingKey;
        public static TokenProviderOptions userTokenOptions;
        public static TokenValidationParameters tokenValidationParameters;
        private readonly ILogger _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Register our custom services here.
            RegisterServices.RegisterComponents(services, Configuration);
            RegisterDALRepositories.RegisterComponents(services, Configuration);
            RegisterDomainServices.RegisterComponents(services, Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenProviderOptions:UserAuthSecretKey"]));

            userTokenOptions = new TokenProviderOptions
            {
                Audience = Configuration["TokenProviderOptions:TokenAudience"],
                Issuer = Configuration["TokenProviderOptions:TokenIssuer"],
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            IdentityModelEventSource.ShowPII = true;

            //All validations are closed for development purposes
            tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                //IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                //ValidIssuer = "Silicon",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                //ValidAudience = "Eticket",

                // Validate the token expiry
                ValidateLifetime = true,

                //Require signed token
                RequireSignedTokens = false,

                //Require Expiraion Time
                RequireExpirationTime = false,

                SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                {
                    var jwt = new JwtSecurityToken(token);

                    return jwt;
                },

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero,
                ValidAudience = Configuration["TokenProviderOptions:TokenAudience"],
                ValidIssuer = Configuration["TokenProviderOptions:TokenIssuer"],
                IssuerSigningKey = signingKey,

            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                var validator = options.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().SingleOrDefault();
                validator.InboundClaimTypeMap.Clear();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "KSquare Customer Portal API", Version = "V1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer",new string[]{}}
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.ConfigureExceptionHandler(logger);
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API V1");
            });

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
