using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RealPlaza.Core.Common.Contracts;
using RealPlaza.Core.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using test01.Application.Users.Handlers;
using test01.Application.Users.Validators;
using test01.Domain.UserRequests;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using test01.Persistence;
using Wolverine;
using Xunit;

namespace test01.UnitTest.Handlers.Users
{
    public class CreateUserHandlerTest
    {
        private readonly Mock<ILogger<CreateUserHandler>> _loggerMock;
        private readonly TryCommand<Internal.Contract.Users.Core.Commands.CreateUser, CreateUserValidator, UsersResult> _tryCommand;
        private readonly  CreateUserValidator _validator;
        private readonly Mock<IUnitOfWork>_unitOfWorkMock;
        private readonly Mock<IMessageBus> _busMock;

        private readonly CreateUserHandler _createUserHandler;
        public CreateUserHandlerTest()
        {
            _loggerMock=new Mock<ILogger<CreateUserHandler>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _busMock=new Mock<IMessageBus>();
            _validator=new CreateUserValidator(_busMock.Object);
            _createUserHandler=new CreateUserHandler(_loggerMock.Object,_validator, _unitOfWorkMock.Object);
        }
       
        [Fact]
        public async Task HandleAsync_EmptyEmailCommand_ShoulFaildValidationCreateUser() 
        {
            // Arrange
            var command = new Internal.Contract.Users.Core.Commands.CreateUser
            {
                Username = "admin25",
                Password = "test",
                Email = "",
            };
            var expectedUserRequestId = 1;
            _unitOfWorkMock.Setup(x => x.UserRepository.CreateAsync(It.IsAny<UserRequest>()))
                .ReturnsAsync(expectedUserRequestId);
            var expectedUsers= new QueryListResult<GetUsersResult>(ResultStatus.Success, Data: new GetUsersResult[0]);
            _busMock.Setup(x => x.InvokeAsync<QueryListResult<GetUsersResult>>(It.IsAny<GetUsers>(), System.Threading.CancellationToken.None,null))
                .ReturnsAsync(expectedUsers);
            var expected = new CommandResult<UsersResult>(ResultStatus.FailedValidation, null, 
                new ValidationResult(
                    new Dictionary<string, string[]> 
                    { 
                        { nameof(CreateUser.Email), ["El campo Email es obligatorio", "'Email' no es una dirección de correo electrónico válida.", $"'{nameof(CreateUser.Email)}' no debería estar vacío.",] }
                    }));
            // Act
            var result = await _createUserHandler.HandleAsync(command, System.Threading.CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task HandleAsync_Command_ShoulCreateUser()
        {
            // Arrange
            var command = new Internal.Contract.Users.Core.Commands.CreateUser
            {
                Username = "admin13",
                Password = "test",
                Email = "admin13@gmail.com",
                DateOfBirth=DateTime.UtcNow,
                RoleId=1
            };
            var expectedUserRequestId = 1;
            _unitOfWorkMock.Setup(x => x.UserRepository.CreateAsync(It.IsAny<UserRequest>()))
                .ReturnsAsync(expectedUserRequestId);
            var expectedUsers = new QueryListResult<GetUsersResult>(ResultStatus.Success, Data: new GetUsersResult[0]);
            _busMock.Setup(x => x.InvokeAsync<QueryListResult<GetUsersResult>>(It.IsAny<GetUsers>(), System.Threading.CancellationToken.None, null))
                .ReturnsAsync(expectedUsers);
            var expected = new UsersResult(expectedUserRequestId, command.Username, command.Password, command.Email, command.DateOfBirth, command.RoleId);
            // Act
            var result = await _createUserHandler.HandleAsync(command, System.Threading.CancellationToken.None);

            // Assert
            result.Info.Should().BeEquivalentTo(expected);
        }
    }
}
