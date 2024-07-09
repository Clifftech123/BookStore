using BookStore.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;

namespace BookStore.Application.Commands.Auth;

public class RegisterUser : IRequest<IdentityResult>
{
    public RegisterUserDTO Model { get; set; }
}

public class RegisterUserHandler : IRequestHandler<RegisterUser, IdentityResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterUserHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Handles the registration of a new user.
    /// </summary>
    /// <param name="request">The registration request containing the user's details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the identity operation, indicating success or failure.</returns>
    public async Task<IdentityResult> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        if (request.Model == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Registration details are missing." });
        }

        var user = new IdentityUser
        {
            UserName = request.Model.Name,
            Email = request.Model.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Model.Password);

        if (!result.Succeeded)
        {
            return result;
        }

        return result;
    }
}