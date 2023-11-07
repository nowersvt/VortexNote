using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VortexNote.Application.Common.Handlers;
using VortexNote.Application.Common.Providers;
using VortexNote.Domain.Base.Exceptions;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Domain.Enums;
using VortexNote.Domain.Extentions;
using VortexNote.Domain.ViewModels;

namespace VortexNote.Application.Notes.Query
{
    public record GetUserNotesQuery(NoteSortType SortType) : IRequest<IEnumerable<NoteViewModel>>;
    public class GetUserNotesQueryHandler : BaseRequestHandler<GetUserNotesQuery, IEnumerable<NoteViewModel>>
    {
        private readonly IMapper _mapper;
        public GetUserNotesQueryHandler(IAppDbContext context,
            IIdentityProvider identityProvider,
            IMapper mapper) : base(context, identityProvider)
        {
            _mapper = mapper;
        }
        public override async Task<IEnumerable<NoteViewModel>> Handle(GetUserNotesQuery request, CancellationToken cancellationToken)
        {
            var checkResult = await CheckAuthorizationUser(cancellationToken);
            if (checkResult.IsFailed)
                throw new UnauthorizedAccessException(string.Join(", ", checkResult.Reasons.Select(x => x.Message)));

            var user = _identityProvider.UserId;
            var notes = _context.Notes.Where(x=>x.UserId== user.Value);
            notes.SortBy(request.SortType);

            var sortNotes = await notes.ToListAsync(cancellationToken);
            var vm = _mapper.Map<IEnumerable<NoteViewModel>>(sortNotes);
            return vm;
        }
    }
    public class GetUserNotesQueryValidator : AbstractValidator<GetUserNotesQuery>
    {

    }
}