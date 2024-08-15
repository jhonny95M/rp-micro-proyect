using Dapper;
using FluentAssertions;
using Moq;
using Moq.Dapper;
using RealPlaza.Core.Core.Persistence;
using System.Data;
using System.Threading.Tasks;
using test01.Persistence.UserRequests.Core.Queries;
using Xunit;

namespace test01.UnitTest.Queries.Users
{
    public class UserQueriesTest
    {
        [Fact]
        public async Task GetUserById_ShoultUserDto()
        {
            // Arrange
            var mockConnectionFactory=new Mock<IDbConnectionFactory>();
            var mockConnection=new Mock<IDbConnection>();

            mockConnectionFactory.Setup(x => x.CreateConnection()).Returns(mockConnection.Object);

            int id = 1;
            var expectDto = new UserQueryDto
            {
                Id = 1,
                Username = "admin25",
                Email = "",
                DateOfBirth = System.DateTime.UtcNow,
                RoleId = 1,
                IsActive = true
            };

            mockConnection.SetupDapperAsync(x => x.QuerySingleAsync<UserQueryDto>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int>(), It.IsAny<CommandType>()))
                .ReturnsAsync(expectDto);

            var userQueries = new UserQueries(mockConnectionFactory.Object);

            // Act
            var result = await userQueries.GetUserByIdQuery(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectDto.Id, result.Id);
            Assert.Equal(expectDto.Username, result.Username);
            Assert.Equal(expectDto.Email, result.Email);
            Assert.Equal(expectDto.DateOfBirth, result.DateOfBirth);
            Assert.Equal(expectDto.RoleId, result.RoleId);
            Assert.Equal(expectDto.IsActive, result.IsActive);
            result.Should().BeEquivalentTo(expectDto);
        }
    }
}
