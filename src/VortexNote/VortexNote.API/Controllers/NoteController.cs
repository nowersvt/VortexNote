using MediatR;
using Microsoft.AspNetCore.Mvc;
using VortexNote.Application.Notes.Command;
using VortexNote.Application.Notes.Query;
using VortexNote.Domain.Base.Abstacts;

namespace VortexNote.API.Controllers
{
    public class NoteController : BaseApiController
    {
        public NoteController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetUserNotesQuery query,
            CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(query, cancellationToken);
            if (res.Any())
                return Ok(res);
            else
                return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteCommand command,
            CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(command, cancellationToken);
            return Ok(res);
        }
    }
}