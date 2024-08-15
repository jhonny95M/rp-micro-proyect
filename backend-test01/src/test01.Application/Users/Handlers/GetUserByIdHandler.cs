using Microsoft.Extensions.Logging;
using RealPlaza.Core.Common.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using test01.Persistence.UserRequests.Core.Queries;

namespace test01.Application.Users.Handlers
{
    public class GetUserByIdHandler
    {
        private readonly ILogger<GetUserByIdHandler> _logger;
        private readonly IUserQueries _userQueries;

        public GetUserByIdHandler(ILogger<GetUserByIdHandler> logger, IUserQueries userQueries)
        {
            _logger = logger;
            _userQueries = userQueries;
        }
        public async Task<QueryResult<GetUserByIdResult>>HandleAsync(GetUserById query,CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(nameof(query));
            try
            {
                var user = await _userQueries.GetUserByIdQuery(query.UserId);
                if (user is null)
                    return new QueryResult<GetUserByIdResult>(ResultStatus.AggregateNotFound);
                return new QueryResult<GetUserByIdResult>(ResultStatus.Success, Data: new GetUserByIdResult(
                    user.Username, user.Email,user.Password, user.DateOfBirth, user.RoleId,user.IsActive
                ));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetUserByIdHandler Faild");
                return new QueryResult<GetUserByIdResult>(ResultStatus.FailedExecution);
            }
        }
    }
}
