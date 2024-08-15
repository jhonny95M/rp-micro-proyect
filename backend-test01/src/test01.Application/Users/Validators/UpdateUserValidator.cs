using FluentValidation;
using RealPlaza.Core.Common.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using Wolverine;
using System.Linq;

namespace test01.Application.Users.Validators
{
    public class UpdateUserValidator:AbstractValidator<UpdateUser>
    {
        private readonly IMessageBus _messageBus;
        public UpdateUserValidator() { }
        public UpdateUserValidator(IMessageBus messageBus)
        {
            _messageBus = messageBus;
            RuleFor(c => c.UserName).NotEmpty()
                                    .MustAsync((user, username, ct) => ExistUserName(user.id, username, ct)).WithMessage("Username is unique.");
            RuleFor(c => c.Email).NotEmpty()
                                    .MustAsync((user, email, ct) => ExistEmail(user.id, email, ct))
                                    .WithMessage("Email is unique.");
        }
        public async Task<bool> ExistUserName(int userId, string username, CancellationToken ct)
        {
            var result = await _messageBus.InvokeAsync<QueryListResult<GetUsersResult>>(new GetUsers(userName:username), ct);
            return result.Data == null || !result.Data.Any(u => u.id != userId && u.username.Equals(username));
        }
        public async Task<bool> ExistEmail(int userId, string email, CancellationToken ct)
        {
            var result = await _messageBus.InvokeAsync<QueryListResult<GetUsersResult>>(new GetUsers(email: email), ct);
            return result.Data == null || !result.Data.Any(u => u.id != userId && u.email.Equals(email));
        }
    }
}
