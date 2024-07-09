using System.Security.Claims;
using System.Text;
using BookStore.Application.Interfaces;
using BookStore.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Application.Commands.Auth;

public class Login : IRequest<object>
{
    public required LoginDTO LoginDTO { get; set; }

    public class LoginHandler : IRequestHandler<Login, object>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;

        public LoginHandler(UserManager<IdentityUser> userManager, ILoggerManager logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Handles the login request by validating user credentials and generating a JWT token if successful.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A response object with login details or an error message.</returns>
        public async Task<object> Handle(Login request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginDTO.Email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, request.LoginDTO.Password)))
            {
                _logger.LogWarn($"{nameof(Login)}: Login failed for email {request.LoginDTO.Email}. Incorrect credentials.");
                return new { Message = "Login failed. Incorrect email or password." };
            }
            var token = await CreateToken(user);
            return new
            {
                Message = "User logged in successfully",
                Username = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        /// <summary>
        /// Creates a JWT token for the authenticated user.
        /// </summary>
        /// <param name="user">The user for whom the token is being created.</param>
        /// <returns>A JWT token as a string.</returns>
        private async Task<string> CreateToken(IdentityUser user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["key"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // Additional claims can be added here

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}