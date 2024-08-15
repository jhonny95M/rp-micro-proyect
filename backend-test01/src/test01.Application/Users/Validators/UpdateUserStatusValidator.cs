using FluentValidation;
using test01.Internal.Contract.Users.Core.Commands;

namespace test01.Application.Users.Validators
{
    public class UpdateUserStatusValidator:AbstractValidator<UpdateUserStatus>
    {
        public UpdateUserStatusValidator()
        {
            RuleFor(c => c.id).NotEmpty();
            RuleFor(c => c.isActive).NotNull();
        }
    }
}
