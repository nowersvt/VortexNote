using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VortexNote.Application.Common.Handlers;
using VortexNote.Application.Common.Providers;
using VortexNote.Domain.Base.Exceptions;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Domain.Entities;
using VortexNote.Domain.ViewModels;

namespace VortexNote.Application.Notes.Command
{
    public record CreateNoteCommand(string Title, string Description) : IRequest<NoteViewModel>;
    public class CreateNoteCommandHandler : BaseRequestHandler<CreateNoteCommand, NoteViewModel>
    {
        private readonly IMapper _mapper;
        public CreateNoteCommandHandler(IAppDbContext context,
            IIdentityProvider identityProvider,
            IMapper mapper) : base(context, identityProvider)
        {
            _mapper = mapper;
        }

        public override async Task<NoteViewModel> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var res = await CheckAuthorizationUser(cancellationToken);
            if (res.IsFailed)
                throw new DomainException(string.Join(", ", res.Reasons.Select(x => x.Message)));

            var note = new Note(request.Title, request.Description);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _identityProvider.UserId.Value);
            if (user is null)
                throw new DomainException("User dont found in DB");

            _context.Users.Attach(user);
            await _context.Notes.AddAsync(note, cancellationToken);
            note.User = user;
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NoteViewModel>(note);
        }
    }
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {

        }
    }
}