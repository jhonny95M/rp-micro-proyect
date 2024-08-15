using Microsoft.Extensions.Logging;
using RealPlaza.Core.Common.Contracts;
using RealPlaza.Core.Core.Configuration;
using System.Threading.Tasks;
using System.Threading;
using System;
using test01.Application.Users.Validators;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Persistence.UserRequests.Core.Queries;
using test01.Persistence;

namespace test01.Application.Users.Handlers
{
    public class UpdateUserStatusHandler
    {
        private readonly ILogger<UpdateUserStatusHandler> _logger;
        private readonly TryCommand<UpdateUserStatus, UpdateUserStatusValidator, UpdateUserResult> _tryCommand;
        private readonly UpdateUserStatusValidator _validator;
        private readonly IUserQueries _userQueries;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserStatusHandler(ILogger<UpdateUserStatusHandler> logger, UpdateUserStatusValidator validator, IUnitOfWork unitOfWork, IUserQueries userQueries)
        {
            _logger = logger;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _tryCommand = new TryCommand<UpdateUserStatus, UpdateUserStatusValidator, UpdateUserResult>(logger, ExecuteAsync, () => Task.FromResult(_validator));
            _userQueries = userQueries;
        }
        public async Task<CommandResult<UpdateUserResult>> HandleAsync(UpdateUserStatus command, CancellationToken ct) => await _tryCommand.ExecuteAsync(command, ct);
        public async Task<UpdateUserResult> ExecuteAsync(UpdateUserStatus command, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(nameof(command));
            try
            {
                var user = await _userQueries.GetUserByIdQuery(command.id);
                if (user is null)
                    return null;
                await _unitOfWork.UserRepository.UpdateAsync(new Domain.UserRequests.UpdateUserRequest(command.id, user.Username, user.Email, user.Password, user.DateOfBirth, command.isActive, user.RoleId));
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
