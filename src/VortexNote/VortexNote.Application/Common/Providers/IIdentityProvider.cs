using FluentResults;
using Microsoft.AspNetCore.Http;
using VortexNote.Domain.Base.Interfaces;

namespace VortexNote.Application.Common.Providers
{
    public interface IIdentityProvider
    {
        Result<Guid> UserId { get; }
        public class IdentityProvider : IIdentityProvider
        {
            private readonly IHttpContextAccessor _accessor;
            public IdentityProvider(IHttpContextAccessor accessor)
            {
                _accessor = accessor;
            }

            public Result<Guid> UserId => GetUserId();


            private Result<Guid> GetUserId()
            {
                var userIdStr = _accessor.HttpContext.Request.Cookies["UID"];
                if (string.IsNullOrEmpty(userIdStr))
                    return Result.Fail("You dont authorization");

                var userId = Guid.Parse(userIdStr);
                return Result.Ok(userId);
            }
        }
    }
}
