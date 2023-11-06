using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Domain.Entities;

namespace VortexNote.Application.Common.Providers
{
    public interface IIdentityProvider
    {
        Result<Guid> UserId { get; }
    }
    public class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IAppDbContext _context;

        public IdentityProvider(IHttpContextAccessor accessor, 
            IAppDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        public Result<Guid> UserId => GetUserId();
        public Result<User> User => GetUser().GetAwaiter().GetResult();

        private async Task<Result<User>> GetUser()
        {
            var userId = UserId;
            if (userId.IsSuccess)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId.Value);
                if (user is null)
                    return Result.Fail("User not found");

                return user;
            }
            else return Result.Fail("User not found");
        }

        private Result<Guid> GetUserId()
        {
            var userIdStr = _accessor.HttpContext.Request.Cookies["UID"];
            if(string.IsNullOrEmpty(userIdStr))
                return Result.Fail("You dont authorization");

            var userId = Guid.Parse(userIdStr);
            return Result.Ok(userId);
        }
    }
}
