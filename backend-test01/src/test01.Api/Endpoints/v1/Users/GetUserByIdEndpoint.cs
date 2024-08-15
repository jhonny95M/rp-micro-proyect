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
    public class GetUserByIdEndpoint
    {
        [Authorize]
        public static async Task<Results<Ok<GetUserByIdResult>, BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>>
            DoAsync(int id,[FromServices] IMessageBus bus, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(bus, nameof(bus));
            var result = await bus.InvokeAsync<QueryResult<GetUserByIdResult>>(new GetUserById(id), ct);
            return result.PrepareResponse();
        }
    }
}
