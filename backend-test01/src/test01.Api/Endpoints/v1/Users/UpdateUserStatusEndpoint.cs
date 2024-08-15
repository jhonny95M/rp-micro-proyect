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
    public class UpdateUserStatusEndpoint
    {
        public record UpdateUserStatusRequest(int status,string userId);
        [Authorize]
        public static async Task<Results<Ok<UpdateUserResult>, BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>>
            DoAsync(int id,[FromBody] UpdateUserStatusRequest command, [FromServices] IMessageBus bus, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(bus, nameof(bus));
            var result = await bus.InvokeAsync<CommandResult<UpdateUserResult>>(
                new UpdateUserStatus(id,Convert.ToBoolean(command.status)), ct);
            return result.PrepareResponse<UpdateUserResult>(result.Info);
        }
    }
}
