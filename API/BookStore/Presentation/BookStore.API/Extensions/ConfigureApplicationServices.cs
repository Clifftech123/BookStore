using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Domain.models;
using BookStore.Infrastructure.Context;

namespace BookStore.Presentation.Extensions;

public static partial class ConfigureApplicationServices
{
    /// <summary>
    /// Configures Cross-Origin Resource Sharing (CORS) with specified policies.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The application configuration where URLs are defined.</param>
 
   
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));
    }

    /// <summary>
    /// Configures the Identity framework for use in the application, setting up password policies and storage context.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        // Configuring identity options for better security and user management
        var builder = services.AddIdentityCore<IdentityUser>(o =>
        {
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = false;
            o.Password.RequiredLength = 8; // Ensure strong passwords
        }).AddEntityFrameworkStores<BookStoreContext>()
          .AddDefaultTokenProviders();
    }

    /// <summary>
    /// Configures JSON Web Token (JWT) authentication for the application, including token validation parameters.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The application configuration where JWT settings are defined.</param>
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["key"];

        // Ensure the secret key is securely stored and of sufficient length
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
            o.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        message = "You are not authorized to access this resource. Please authenticate."
                    });
                    return context.Response.WriteAsync(result);
                },
            };
        });
    }
    

    /// <summary>
    /// Configures global exception handling for the application, providing a uniform response structure for errors.
    /// </summary>
    /// <param name="app">The WebApplication to configure.</param>
    /// <param name="logger">The logger to log errors.</param>
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    // Providing more detailed error information
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    logger.LogError($"Something went wrong: {contextFeature.Error}");

                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                    }.ToString());
                }
            });
        });
    }
}