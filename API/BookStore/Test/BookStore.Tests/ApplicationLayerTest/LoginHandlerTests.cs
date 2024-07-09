using BookStore.Application.Commands.Auth;
using BookStore.Application.Interfaces;
using BookStore.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Assert = Xunit.Assert;


namespace BookStore.Tests.ApplicationLayerTest;

public class LoginHandlerTests
{
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<ILoggerManager> _loggerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Login.LoginHandler _loginHandler;

    public LoginHandlerTests()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
        _loggerMock = new Mock<ILoggerManager>();
        _configurationMock = new Mock<IConfiguration>();

        _loginHandler = new Login.LoginHandler(_userManagerMock.Object, _loggerMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Handle_LoginSuccessful_ReturnsSuccessMessage()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = "test@example.com", Password = "Password123" };
        var user = new IdentityUser { UserName = "testUser", Email = "test@example.com" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(loginDTO.Email)).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDTO.Password)).ReturnsAsync(true);

        // Act
        var result = await _loginHandler.Handle(new Login { LoginDTO = loginDTO }, CancellationToken.None);

        // Assert
        Assert.Contains("User logged in successfully", new[] { result.ToString() });
    }

    [Fact]
    public async Task Handle_IncorrectPassword_ReturnsFailureMessage()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = "wrong@example.com", Password = "WrongPassword" };
        var user = new IdentityUser { UserName = "testUser", Email = "wrong@example.com" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(loginDTO.Email)).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDTO.Password)).ReturnsAsync(false);

        // Act
        var result = await _loginHandler.Handle(new Login { LoginDTO = loginDTO }, CancellationToken.None);

        // Assert
        Assert.Contains("Login failed. Incorrect email or password.", new[] { result.ToString() });
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsFailureMessage()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = "notfound@example.com", Password = "Password123" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(loginDTO.Email)).ReturnsAsync((IdentityUser)null);

        // Act
        var result = await _loginHandler.Handle(new Login { LoginDTO = loginDTO }, CancellationToken.None);

        // Assert
        Assert.Contains("Login failed. Incorrect email or password.", new[] { result.ToString() });
    }

}