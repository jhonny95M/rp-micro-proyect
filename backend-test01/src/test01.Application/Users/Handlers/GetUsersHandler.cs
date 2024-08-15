using Microsoft.Extensions.Logging;
using RealPlaza.Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using test01.Persistence.UserRequests.Core.Queries;

namespace test01.Application.Users.Handlers
{
    public class GetUsersHandler
    {
        private readonly ILogger<GetUsersHandler> _logger;
        private readonly IUserQueries _userQueries;

        public GetUsersHandler(ILogger<GetUsersHandler> logger, IUserQueries userQueries)
        {
            _logger = logger;
            _userQueries = userQueries;
        }
        public async Task<QueryListResult<GetUsersResult>> HandleAsync(GetUsers query, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(nameof(query));
            try
            {

                var users = await _userQueries.GetUserQuery(new UserQuery(query.pageSize, query.pageCurrent, query.searchFilter, query.userName, query.email, query.roleId, query.status));
                if (users is null)
                    return new QueryListResult<GetUsersResult>(ResultStatus.AggregateNotFound);
                var userResults = new List<GetUsersResult>();
                foreach (var user in users)
                    userResults.Add(new GetUsersResult(user.Id,
                    user.Username, user.Email, user.DateOfBirth, user.RoleId, user.IsActive
                    ));
                return new QueryListResult<GetUsersResult>(ResultStatus.Success, Data: userResults, Paging: new PagingResult(query.pageCurrent, query.pageSize, users.FirstOrDefault()?.TotalResults??0));

            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetUsersHandler Faild");
                return new QueryListResult<GetUsersResult>(ResultStatus.FailedExecution);
            }
        }
    }
}
