using BookStore.Application.Commands.Auth;
using BookStore.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers;

public class AuthController : BaseApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO model)
    {
        var result = await Mediator.Send(new RegisterUser { Model = model });

        if (result.Succeeded)
        {
            return Ok(new { Message = "User account created successfully" });
        }
        else
        {
            return BadRequest(new { Errors = result.Errors });
        }
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        return Ok(await Mediator.Send(new Login { LoginDTO = model }));
    }



}