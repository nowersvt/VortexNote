using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using VortexNote.Application.Common.Helpers;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Domain.Statics;

namespace VortexNote.Application.Common.Providers
{
    public interface IIdentityProvider
    {
        Result<Guid> UserId { get; }
        public class IdentityProvider : IIdentityProvider
        {
            private readonly IHttpContextAccessor _accessor;
            private readonly IHostingEnvironment _hostingEnvironment;
            public IdentityProvider(IHttpContextAccessor accessor,
                IHostingEnvironment hostingEnvironment)
            {
                _accessor = accessor;
                _hostingEnvironment = hostingEnvironment;
            }

            public Result<Guid> UserId => GetUserId(
                Path.Combine(_hostingEnvironment.ContentRootPath,
                    ApplicationStatic.APP_FILE_DICTIONARY,
                    ApplicationStatic.APP_FILE_NAME)
                );

            private Result<Guid> GetUserId(string path)
            {
                var userIdRes = UserFileSaverHelper.GetUIDInFile(path);
                if (userIdRes.IsFailed)
                    return Result.Fail("You dont authorization");

                var userId = userIdRes.Value;
                return Result.Ok(userId);
            }
        }
    }
}
