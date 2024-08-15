using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RealPlaza.Core.Common.Contracts;
using RealPlaza.Core.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using test01.Internal.Contract.Roles.Core.Commands;
using test01.Internal.Contract.Roles.Core.Commands.CommandResult;
using Wolverine;

namespace test01.Api.Endpoints.v1.Roles
{
    public class GetRolEndpoint
    {
        [Authorize]
        public static async Task<Results<Ok<List<GetRolResult>>,BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>>
            DoAsync([FromServices] IMessageBus bus, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(bus, nameof(bus));
            var result = await bus.InvokeAsync<QueryResult<List<GetRolResult>>>(new GetRol(), ct);
            return result.PrepareResponse();
        }
    }
}
