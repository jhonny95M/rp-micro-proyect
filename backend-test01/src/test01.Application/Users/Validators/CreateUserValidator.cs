using FluentValidation;
using RealPlaza.Core.Common.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using Wolverine;

namespace test01.Application.Users.Validators
{
    public class CreateUserValidator: AbstractValidator<CreateUser>
    {
        private readonly IMessageBus _messageBus;
        public CreateUserValidator() { }
        public CreateUserValidator(IMessageBus messageBus)
        {
            _messageBus = messageBus;
            RuleFor(c => c.Username).NotEmpty().NotNull()
                                    .MustAsync(ExistUserName).WithMessage("Username is unique.");
            RuleFor(c => c.Email).NotEmpty().WithMessage($"El campo {nameof(CreateUser.Email)} es obligatorio")
                                .EmailAddress()
                                    .MustAsync(ExistEmail).WithMessage("Email is unique.");
        }
        public async Task<bool>ExistUserName(string username,CancellationToken ct)
        {
            var result = await _messageBus.InvokeAsync<QueryListResult<GetUsersResult>>(new GetUsers(userName:username), ct);
            return !(result.Data!=null && result.Data.Any());
        }
        public async Task<bool> ExistEmail(string email, CancellationToken ct)
        {
            var result = await _messageBus.InvokeAsync<QueryListResult<GetUsersResult>>(new GetUsers(email:email), ct);
            return !(result.Data != null && result.Data.Any());
        }
    }
}
