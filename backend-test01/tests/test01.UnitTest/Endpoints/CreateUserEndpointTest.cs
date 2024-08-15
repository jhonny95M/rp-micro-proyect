using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using RealPlaza.Core.Common.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using test01.Api.Endpoints.v1.Users;
using test01.Internal.Contract.Users.Core.Commands;
using test01.Internal.Contract.Users.Core.Commands.CommandResult;
using Wolverine;
using Xunit;

namespace test01.UnitTest.Endpoints
{
    public class CreateUserEndpointTest
    {
        private readonly Mock<IMessageBus> _mockBus;
        private readonly Api.Endpoints.v1.Users.CreateUserEndpoint.UserRequest _userRequest;
        private readonly Api.Endpoints.v1.Users.CreateUserEndpoint.UserRequest _userInvalidRequest;
        private readonly CancellationToken _cancellationToken;
        public CreateUserEndpointTest()
        {
            _mockBus = new Mock<IMessageBus>();
            _userRequest = CreateSampleUsuarioRequest();
            _cancellationToken = new CancellationToken();
        }

        private static Api.Endpoints.v1.Users.CreateUserEndpoint.UserRequest CreateSampleUsuarioRequest()
        {
            return new Api.Endpoints.v1.Users.CreateUserEndpoint.UserRequest("admin25", "test", "admin25@experis.com",Convert.ToDateTime("2021-09-01"), 1,true);
        }        

        [Fact]
        public async Task DoAsync_WithValidateRequest_ShouldReturnOk()
        {
            // Arrange
            var command = new Internal.Contract.Users.Core.Commands.CreateUser
            {
                Username = _userRequest.UserName,
                Password = _userRequest.Password,
                Email = _userRequest.Email,
                RoleId = _userRequest.RoleId
            };
            var result = new CommandResult<UsersResult>(ResultStatus.Success,
                new UsersResult(1, command.Username, command.Password, command.Email, command.DateOfBirth, command.RoleId));
            _mockBus.Setup(x => x.InvokeAsync<CommandResult<UsersResult>>(It.IsAny<CreateUser>(), _cancellationToken, null))
                .ReturnsAsync(result);
            // Act
            var response = await Api.Endpoints.v1.Users.CreateUserEndpoint.DoAsync(_userRequest, _mockBus.Object, _cancellationToken);
            // Assert
            Assert.NotNull(response);
            Assert.IsType<Results<Ok<UsersResult>, BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>>(response);
            Assert.IsType<Ok<UsersResult>>(response.Result);
            response.Should().BeOfType<Results<Ok<UsersResult>, BadRequest<ValidationResult>, NotFound, Conflict, ProblemHttpResult>>()
                .Which.Result.Should().BeOfType<Ok<UsersResult>>()
                .Which.Value.Should().BeEquivalentTo(result.Info);
        }

    }
}
