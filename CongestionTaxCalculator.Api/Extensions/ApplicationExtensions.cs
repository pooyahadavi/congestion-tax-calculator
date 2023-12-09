using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CongestionTaxCalculator.Core.General;
using CongestionTaxCalculator.Core.Interfaces.Repository;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Repository;
using CongestionTaxCalculator.Core.Entities.Identity;
using CongestionTaxCalculator.Core.Interfaces.Service;
using CongestionTaxCalculator.Service;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using static CongestionTaxCalculator.Core.General.Constants;

namespace CongestionTaxCalculator.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApplicationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Congestion Tax Calculator", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                              Id = "Bearer",
                             Type = ReferenceType.SecurityScheme
                           }
                       },
                       Array.Empty<string>()
                    }
                });
            });
        }

        public static void AddApplicationAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "https://auth.fintranet.com",
                    ValidAudience = "https://api.fintranet.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(JwtSecretKey, EnvironmentVariableTarget.Machine)!))
                };
            });
        }

        public static void AddApplicationContext(this IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable(ConnectionStringKey, EnvironmentVariableTarget.Machine)));
        }

        public static void AddApplicationIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddRoleManager<RoleManager<ApplicationRole>>();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<ICongestionTaxService, CongestionTaxService>();
        }

        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            const string name = "superadmin";
            var scope = app.ApplicationServices.CreateScope();

            var userRepository = scope.ServiceProvider.GetRequiredService<IApplicationUserRepository>();
            var role = userRepository.FindRoleByNameAsync(name).Result;
            if (role is null)
            {
                userRepository.CreateRoleAsync(new(name)).Wait();
            }

            var superadmin = userRepository.FindByUserNameAsync(name).Result;
            if (superadmin is null)
            {
                userRepository.AddWithPasswordAsync(new(name), Environment.GetEnvironmentVariable(SuperadminPasswordKey, EnvironmentVariableTarget.Machine)!).Wait();
            }

            userRepository.AddToRoleAsync(new(name), name).Wait();
        }

        public static ActionResult HttpResult(this Result result, object? data = null)
        {
            return result.OperationResult.ToString() switch
            {
                nameof(OperationResult.Succeeded) => new OkObjectResult(data),
                nameof(OperationResult.NotFound) => new NotFoundResult(),
                nameof(OperationResult.NotValid) => new BadRequestObjectResult(new Error(result.Error)),
                nameof(OperationResult.Failed) => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                _ => throw new NotImplementedException()
            };
        }
    }
}
