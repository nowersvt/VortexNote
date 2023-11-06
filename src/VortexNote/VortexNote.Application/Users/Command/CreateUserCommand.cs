using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using VortexNote.Application.Common.Handlers;
using VortexNote.Application.Common.Providers;
using VortexNote.Domain.Base.Exceptions;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Domain.Entities;

namespace VortexNote.Application.Users.Command
{
    public record CreateUserCommand() : IRequest<Guid>;
    public class CreateUserCommandHandler : BaseRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateUserCommandHandler(IAppDbContext context,
            IIdentityProvider identityProvider,
            IHttpContextAccessor httpContextAccessor) : base(context, identityProvider)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User();
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                _httpContextAccessor.HttpContext.Response.Cookies.Append("UID", user.Id.ToString());
                return user.Id;
            }
            catch (Exception)
            {
                throw;
            }
          
        }
    }
    /// TODO : Delete or use in future
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

    }
}