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
    public class UpdateUserEndpoint
    {
        public record UpdateUserRequest(string username, string password, string email, DateTime dateOfBirth, bool status, int roleId);
        [Authorize]
        public static async Task<Results<Ok<UpdateUserResult>, BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>>
            DoAsync(int id,[FromBody] UpdateUserRequest command, [FromServices] IMessageBus bus, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(bus, nameof(bus));
            var result = await bus.InvokeAsync<CommandResult<UpdateUserResult>>(
                new UpdateUser(id, command.username,command.password,command.email,command.dateOfBirth, command.status , command.roleId), ct);
            return result.PrepareResponse<UpdateUserResult>(result.Info);
        }
    }
}
