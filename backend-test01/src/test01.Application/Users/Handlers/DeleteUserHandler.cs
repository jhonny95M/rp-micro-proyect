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
using test01.Persistence.UserRequests.Repository;

namespace test01.Application.Users.Handlers
{
    public class DeleteUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserQueries _userQueries;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteUserHandler> _logger;
        private readonly TryCommand<DeleteUser, DeleteUserValidator, DeleteUserResult> _tryCommand;
        private readonly DeleteUserValidator _validators;

        public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<DeleteUserHandler> logger, DeleteUserValidator validators, IUserQueries userQueries)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validators = validators;
            _tryCommand = new TryCommand<DeleteUser, DeleteUserValidator, DeleteUserResult>(_logger, ExecuteAsync, () => Task.FromResult(_validators));
            _userQueries = userQueries;
        }
        public async Task<CommandResult<DeleteUserResult>> HandleAsync(DeleteUser command, CancellationToken ct) => await _tryCommand.ExecuteAsync(command, ct);
        public async Task<DeleteUserResult>ExecuteAsync(DeleteUser command, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(nameof(command));
            try
            {
                var user = await _userQueries.GetUserByIdQuery(command.userId);
                if (user is null)
                    return null;
                await _unitOfWork.UserRepository.DeleteAsync(new Domain.UserRequests.DeleteUserRequest(command.userId));
                return new DeleteUserResult(true);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "DeleteUserHandler Faild");
                throw;
            }
        }
    }
}
