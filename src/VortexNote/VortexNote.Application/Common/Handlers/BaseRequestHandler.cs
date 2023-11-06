using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VortexNote.Application.Common.Providers;
using VortexNote.Domain.Base.Interfaces;


namespace VortexNote.Application.Common.Handlers
{
    public class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IAppDbContext _context;
        protected readonly IIdentityProvider _identityProvider;
        public BaseRequestHandler(IAppDbContext context, 
            IIdentityProvider identityProvider)
        {
            _context = context;
            _identityProvider = identityProvider;
        }
        
        public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<Result> CheckAuthorizationUser(CancellationToken cancellationToken)
        {
            var userRes = _identityProvider.UserId;
            if (userRes.IsFailed)
                return Result.Fail(userRes.Errors);
          
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userRes.Value, cancellationToken);
            if (user is null)
                return Result.Fail("User not found in db");

            return Result.Ok();
        }
    }
}