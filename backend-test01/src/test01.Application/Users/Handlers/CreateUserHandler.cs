using Microsoft.Extensions.Logging;
using RealPlaza.Core.Common.Contracts;
using RealPlaza.Core.Core.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using test01.Application.Users.Validators;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using test01.Persistence;

namespace test01.Application.Users.Handlers
{
    public class CreateUserHandler
    {
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly TryCommand<Internal.Contract.Users.Core.Commands.CreateUser,CreateUserValidator,UsersResult> _tryCommand;
        private readonly CreateUserValidator _validator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(ILogger<CreateUserHandler> logger, CreateUserValidator validator, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _tryCommand = new TryCommand<Internal.Contract.Users.Core.Commands.CreateUser, CreateUserValidator, UsersResult>(_logger, ExecuteAsync, () => Task.FromResult(_validator));
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<UsersResult>> HandleAsync(Internal.Contract.Users.Core.Commands.CreateUser command,CancellationToken ct)
        {
            return await _tryCommand.ExecuteAsync(command,ct);
        }
        public async Task<UsersResult> ExecuteAsync(Internal.Contract.Users.Core.Commands.CreateUser command, CancellationToken ct)
        {
            try
            {
                var id = await _unitOfWork.UserRepository.CreateAsync(new Domain.UserRequests.UserRequest
                {
                    CreatedAt=DateTime.UtcNow,
                    DateOfBirth=DateTime.UtcNow,
                    Email=command.Email,
                    IsActive=command.Status,
                    Password=command.Password,
                    RoleId=command.RoleId,
                    Username=command.Username
                });

                var userResult= new UsersResult(id,command.Username,command.Password,command.Email,command.DateOfBirth,command.RoleId);
                return userResult;
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, string.Format(LogConstants.LogErrorMessage, nameof(CreateUserHandler)));
                throw;

            }
        }
    }
}
