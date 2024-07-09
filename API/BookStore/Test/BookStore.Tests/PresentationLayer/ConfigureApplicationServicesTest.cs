using BookStore.Application.Interfaces;
using BookStore.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Xunit;
using Moq;
using Assert = Xunit.Assert;

public class ConfigureApplicationServicesTests
{
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;
    private readonly WebApplication _app;
    private readonly Mock<ILoggerManager> _loggerMock;


    public ConfigureApplicationServicesTests()
    {
        _services = new ServiceCollection();
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "BackendUrl", "https://localhost:5052" },
                { "FrontendUrl", "https://localhost:7224" },
                { "JwtSettings:key", "ThisIsA32CharactersLongSecretKey!" },
                { "JwtSettings:validIssuer", "BooksStore" },
                { "JwtSettings:validAudience", "https://localhost:5052" }
            })
            .Build();
        var builder = WebApplication.CreateBuilder();
        _app = builder.Build();
        _loggerMock = new Mock<ILoggerManager>();
    }
  
   


    [Fact]
    public void ConfigureIdentity_SetsUpIdentityCorrectly()
    {
        // Act
        _services.ConfigureIdentity();

        // Assert

        var serviceProvider = _services.BuildServiceProvider();

        var identityOptions = serviceProvider.GetService<IdentityOptions>();

    }
}