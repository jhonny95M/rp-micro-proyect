using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RealPlaza.Core.Common.Contracts;
using RealPlaza.Core.Core.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using Wolverine;

namespace test01.Api.Endpoints.v1.Users
{
    public class CreateUserEndpoint
    {
        public record UserRequest(string UserName, string Password, string Email, DateTime DateOfBirth, int RoleId,bool status);
        [Authorize]
        public static async Task<Results<Ok<UsersResult>, BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>> 
            DoAsync([FromBody] UserRequest request, [FromServices] IMessageBus bus, CancellationToken ct)
        {
            
            ArgumentNullException.ThrowIfNull(bus, nameof(bus));
            var command = new Internal.Contract.Users.Core.Commands.CreateUser
            {
                Username = request.UserName,
                Password = request.Password,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                RoleId = request.RoleId,
                Status=request.status
            };
            var result = await bus.InvokeAsync<CommandResult<UsersResult>>(command, ct);
            return result.PrepareResponse(result.Info);
        }
        

    }
}
