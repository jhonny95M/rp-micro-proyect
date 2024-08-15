using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using RealPlaza.Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using test01.Application.Users.Validators;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using Wolverine;
using Xunit;

namespace test01.UnitTest.Validators;

public class CreateUserValidatorTest
{

    private CreateUserValidator _validator;
    private readonly Mock<IMessageBus> _busMock;
    public CreateUserValidatorTest()
    {
        _busMock = new Mock<IMessageBus>();
        _validator = new CreateUserValidator(_busMock.Object);
    }
    [Fact]
    public async Task Validate_WithValidEmailUnique_ShouldHaveValidationErrors()
    {
        // Arrange
        var command = new Internal.Contract.Users.Core.Commands.CreateUser
        {
            Username = "admin25",
            Password = "test",
            Email = "junior.malpartida.rp@viaexperis2.pe",
            DateOfBirth = DateTime.UtcNow,
            RoleId = 1
        };
        _busMock.Setup(x => x.InvokeAsync<QueryListResult<GetUsersResult>>(It.IsAny<GetUsers>(), System.Threading.CancellationToken.None, null))
            .ReturnsAsync(new QueryListResult<GetUsersResult>(ResultStatus.Success, 
            Data: new List<GetUsersResult>{
                new GetUsersResult(1,"","junior.malpartida.rp@viaexperis2.pe",DateTime.UtcNow,1,true)
            }));
        // Act
        var result = await _validator.TestValidateAsync(command);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.IsValid.Should().BeFalse();
    }
}
