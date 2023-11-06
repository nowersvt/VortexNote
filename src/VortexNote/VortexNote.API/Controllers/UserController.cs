using MediatR;
using Microsoft.AspNetCore.Mvc;
using VortexNote.Application.Users.Command;
using VortexNote.Domain.Base.Abstacts;

namespace VortexNote.API.Controllers
{
    public class UserController : BaseApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var result = await _mediator.Send(new CreateUserCommand());
            return Ok(result);
        }
    }
}