using Microsoft.Extensions.Logging;
using RealPlaza.Core.Common.Contracts;
using RealPlaza.Core.Core.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using test01.Application.Users.Validators;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using test01.Persistence;
using test01.Persistence.UserRequests.Core.Queries;

namespace test01.Application.Users.Handlers
{
    public class UpdateUserHandler
    {
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly TryCommand<UpdateUser, UpdateUserValidator,UpdateUserResult> _tryCommand;
        private readonly UpdateUserValidator _validator;
        private readonly IUserQueries _userQueries;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(ILogger<UpdateUserHandler> logger, UpdateUserValidator validator, IUnitOfWork unitOfWork, IUserQueries userQueries)
        {
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _tryCommand = new TryCommand<UpdateUser, UpdateUserValidator, UpdateUserResult>(logger, ExecuteAsync, () => Task.FromResult(_validator));
            _userQueries = userQueries;
        }
        public async Task<CommandResult<UpdateUserResult>> HandleAsync(UpdateUser command, CancellationToken ct)=>await _tryCommand.ExecuteAsync(command,ct);
        public async Task<UpdateUserResult>ExecuteAsync(UpdateUser command, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(nameof(command));
            try
            {
                var user = await _userQueries.GetUserByIdQuery(command.id);
                if (user is null)
                    return null;
                await _unitOfWork.UserRepository.UpdateAsync(new Domain.UserRequests.UpdateUserRequest(command.id, command.UserName, command.Email,command.Password, command.DateOfBirth, command.isActive, command.RoleId));
                return new UpdateUserResult(user.Username, user.Email, user.DateOfBirth, user.RoleId,user.IsActive);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "UpdateUserHandler Faild");
                throw;
            }
        }
    }
}
