using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers;

public class BaseApiController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}