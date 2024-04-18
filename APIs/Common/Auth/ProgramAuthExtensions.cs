using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace MyService.APIs;

public static class ProgramAuthExtensions
{
    public static void AddApiAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        string? domain = configuration.GetValue<string>("Auth0:Domain");
        string? audience = configuration.GetValue<string>("Auth0:Audience");

        if (string.IsNullOrWhiteSpace(audience))
        {
            throw new InvalidOperationException("Auth0:Audience is required");
        }
        if (string.IsNullOrWhiteSpace(domain))
        {
            throw new InvalidOperationException("Auth0:Domain is required");
        }

        string authority = $"https://{domain}/";
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "read:messages",
                policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain))
            );
        });

        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
    }

    public static void UseApiAuthentication(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void UseOpenApiAuthentication(
        this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions options
    )
    {
        // https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/interactive
        // https://knowyourtoolset.com/2022/07/swashbuckle-with-auth0/
    }
}
