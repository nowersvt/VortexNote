using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VortexNote.Application.Common.Handlers;
using VortexNote.Application.Common.Providers;
using VortexNote.Domain.Base.Exceptions;
using VortexNote.Domain.Base.Interfaces;

namespace VortexNote.Application.Notes.Command
{
    //public record UpdateNoteRouteData(Guid NoteId);
    public record UpdateNoteBodyData(string NewTitle, string NewDescription);
    public record UpdateNoteCommand(Guid NoteId, string NewTitle, string NewDescription) : IRequest<Unit>;
    public class UpdateNoteCommandHandler : BaseRequestHandler<UpdateNoteCommand, Unit>
    {
        public UpdateNoteCommandHandler(IAppDbContext context, IIdentityProvider identityProvider) : base(context, identityProvider)
        {
        }
        public override async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var checkResult = await CheckAuthorizationUser(cancellationToken);
            if (checkResult.IsFailed)
                throw new UnauthorizedAccessException(string.Join(", ", checkResult.Reasons.Select(x => x.Message)));

            var transaction = await _context.CreateTransactionAsync(cancellationToken);

            try
            {
                var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == request.NoteId, cancellationToken);
                if (note is null)
                    throw new DomainException($"Note with id {request.NoteId} not found");

                note.Update(request.NewTitle, request.NewDescription);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            return Unit.Value;
        }
    }
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {

    }
}