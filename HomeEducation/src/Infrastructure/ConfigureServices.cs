using System.Security.Claims;
using System.Text;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Domain.Enums;
using HomeEducation.Infrastructure.Identity;
using HomeEducation.Infrastructure.Persistence;
using HomeEducation.Infrastructure.Persistence.Interceptors;
using HomeEducation.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.OptionsSetup;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddDbContext<HomeEducationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(HomeEducationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IHomeEducationDbContext>(provider => provider.GetRequiredService<HomeEducationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();


        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
           .AddJwtBearer(options =>
           {
               options.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = async ctx =>
                   {
                       var exceptionMessage = ctx.Exception;
                   },
               };
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration.GetValue<string>("jwt:Issuer"),
                   ValidAudience = configuration.GetValue<string>("jwt:Audience"),
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("jwt:SecretKey")))
               };
           });

        services.AddAuthorization();

        services.ConfigureOptions<JwtOptionsSetup>();
        //services.ConfigureOptions<JwtBearerOptionsSetup>();
        //services.ConfigureOptions<AuthorizationOptionsSetup>();
        return services;
    }
}
