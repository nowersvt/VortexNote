﻿using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading;
using VortexNote.Application.Common.Handlers;
using VortexNote.Application.Common.Helpers;
using VortexNote.Application.Common.Providers;
using VortexNote.Domain.Base.Exceptions;
using VortexNote.Domain.Base.Files;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Domain.Entities;
using VortexNote.Domain.Statics;

namespace VortexNote.Application.Users.Command
{
    public record CheckUserCommand() : IRequest<Guid>;
    public class CheckUserCommandHandler : BaseRequestHandler<CheckUserCommand, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _environment;
        private readonly IMapper _mapper;

        public CheckUserCommandHandler(IAppDbContext context,
            IIdentityProvider identityProvider,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment environment,
            IMapper mapper) : base(context, identityProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
            _mapper = mapper;
        }
        public override async Task<Guid> Handle(CheckUserCommand request, CancellationToken cancellationToken)
        {
            SavedData? data = null;
            try
            {
                var path = _environment.ContentRootPath;
                var filePath = Path.Combine(path, ApplicationStatic.APP_FILE_DICTIONARY, ApplicationStatic.APP_FILE_NAME);

                if (File.Exists(filePath))
                {
                    var readingResult = await UserFileSaverHelper.ReadFromFile(filePath, cancellationToken);
                    if (readingResult.IsSuccess)
                    {
                        data = readingResult.Value;
                    }
                }
                else
                {
                    var user = await CreateUser(cancellationToken);
                    data = _mapper.Map<SavedData>(user);
                    if (UserFileSaverHelper.SaveInFile(Path.Combine(path, ApplicationStatic.APP_FILE_DICTIONARY), filePath, data).IsFailed)
                    {
                        throw new DomainException("Problem with saving app-data-text-file");
                    }
                }
                if (data == null)
                    throw new DomainException("Error in getting user");
            }
            catch
            {
                throw;
            }
            if (!Guid.TryParse(data.UserId, out Guid userId))
            {
                throw new DomainException("Problem with loading user in db");
            }
            else
                return userId;
        }


        /// TODO : Set in other class helper
        
        private async Task<User> CreateUser(CancellationToken cancellationToken = default)
        {
            var user = new User();
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("UID", user.Id.ToString());
            return user;
        }
    }
    /// TODO : Delete or use in future
    public class CheckUserCommandValidator : AbstractValidator<CheckUserCommand>
    {

    }
}