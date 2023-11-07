using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VortexNote.Domain.Base.Abstacts
{
    [ApiController]
    [Route("api/[controller]s/[action]")]
    public class BaseApiController : ControllerBase
    {
        protected IMediator _mediator;
        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}