using Microsoft.Extensions.Logging;
using RealPlaza.Core.Common.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using test01.Application.Users.Handlers;
using test01.Internal.Contract.Roles.Core.Commands;
using test01.Internal.Contract.Roles.Core.Commands.CommandResult;
using test01.Persistence.Roles.Core.Queries;
using test01.Persistence.UserRequests.Core.Queries;

namespace test01.Application.Roles.Handlers
{
    public class GetRolHandler
    {
        private readonly ILogger<GetRolHandler> _logger;
        private readonly IRolQueries _rolQueries;
        public GetRolHandler(ILogger<GetRolHandler> logger, IRolQueries rolQueries)
        {
            _rolQueries = rolQueries;
            _logger = logger;
        }
        public async Task<QueryResult<List<GetRolResult>>> HandleAsync(GetRol query, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(nameof(query));
            try
            {
                var roles = await _rolQueries.GetRoles();
                if (roles is null)
                    return new QueryResult<List<GetRolResult>>(ResultStatus.AggregateNotFound);
                return new QueryResult<List<GetRolResult>>(ResultStatus.Success,
                    Data: roles.ConvertAll(new Converter<RolQueryDto, GetRolResult>((ta) => 
                    new GetRolResult(ta.Id, ta.NameRol))));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetRolHandler Faild");
                return new QueryResult<List<GetRolResult>>(ResultStatus.FailedExecution);
            }
        }
    }
}
