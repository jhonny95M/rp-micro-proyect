using FluentValidation;
using test01.Internal.Contract.Users.Core.Commands;

namespace test01.Application.Users.Validators
{
    public class DeleteUserValidator:AbstractValidator<DeleteUser>
    {
        public DeleteUserValidator()
        {
            RuleFor(c => c.userId).NotEmpty();
        }
    }
}
