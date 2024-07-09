using BookStore.Application.Commands.Auth;
using BookStore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers;

public class AuthController : BaseApiController
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="model">The registration details.</param>
    /// <returns>A response indicating the result of the registration operation.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO model)
    {
        var result = await Mediator.Send(new RegisterUser { Model = model });

        return HandleResponse(result, "User account created successfully");
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="model">The login details.</param>
    /// <returns>A response indicating the result of the login operation.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var result = await Mediator.Send(new Login { LoginDTO = model });

        return HandleResponse(result, "User logged in successfully");
    }

    /// <summary>
    /// Handles the uniform response for all action methods in this controller.
    /// </summary>
    /// <param name="result">The result of the operation.</param>
    /// <param name="successMessage">The success message to return if the operation succeeded.</param>
    /// <returns>An IActionResult based on the operation's result.</returns>
    private IActionResult HandleResponse(dynamic result, string successMessage)
    {
        if (result.Succeeded)
        {
            return Ok(new { Message = successMessage });
        }
        else
        {
            return BadRequest(new { Errors = result.Errors });
        }
    }
}