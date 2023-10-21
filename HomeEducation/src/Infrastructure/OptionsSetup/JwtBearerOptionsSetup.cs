using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HomeEducation.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options;

    public JwtBearerOptionsSetup(JwtOptions options)
    {
        _options = options;
    }

    public void Configure(JwtBearerOptions options)
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
            ValidIssuer = _options.Issuer,//configuration.GetValue<string>("jwt:Issuer"),
            ValidAudience = _options.Audience,//configuration.GetValue<string>("jwt:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey))
        };
    }
}
