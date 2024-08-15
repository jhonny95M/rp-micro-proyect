using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RealPlaza.Core.Common.Contracts;
using RealPlaza.Core.Core.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using Wolverine;

namespace test01.Api.Endpoints.v1.Users
{
    #nullable enable
    public class GetUsersEndpoint
    {
        [Authorize]
        public static async Task<Results<Ok<SuccessQueryResult<GetUsersResult>>, BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>>
            DoAsync([FromServices] IMessageBus bus, CancellationToken ct,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageCurrent = 1,
            [FromQuery] string? searchFilter = null,
            [FromQuery] string? requestId = null,
            [FromQuery] bool? status = null)
        {
            ArgumentNullException.ThrowIfNull(bus, nameof(bus));

            var command = new GetUsers(pageSize, pageCurrent, searchFilter, status: status);
            var result = await bus.InvokeAsync<QueryListResult<GetUsersResult>>(command, ct);
            return result.PrepareResponse();
        }
    }
}
