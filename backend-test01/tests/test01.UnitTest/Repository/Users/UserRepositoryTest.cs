using Moq;
using RealPlaza.Core.Core.Persistence;
using System.Threading.Tasks;
using Xunit;
using test01.Domain.UserRequests;
using Dapper;
using System.Data;
using test01.Persistence.UserRequests.Repository;
using FluentAssertions;

namespace test01.UnitTest.Repository.Users
{
    public class UserRepositoryTest
    {
        private readonly Mock<IGenericTransaction> _mockTransaction;
        public UserRepositoryTest()
        {
            _mockTransaction = new Mock<IGenericTransaction>();
        }
        [Fact]
        public async Task Create_ShouldReturnCorrectId()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                Username = "admin25",
                Password = "test",
                Email = "admin@experis.pe",
                DateOfBirth = System.DateTime.UtcNow,
                RoleId = 1
            };
            var parameters = new DynamicParameters();
            parameters.Add("p_username", userRequest.Username, DbType.String);
            parameters.Add("p_password", userRequest.Password, DbType.String);
            parameters.Add("p_email", userRequest.Email, DbType.String);
            parameters.Add("p_date_of_birth", userRequest.DateOfBirth, DbType.Date);
            parameters.Add("p_role_id", userRequest.RoleId, DbType.Int32);
            parameters.Add("p_is_active", userRequest.IsActive, DbType.Boolean);
            parameters.Add("p_created_at", userRequest.CreatedAt, DbType.DateTime);
            parameters.Add("p_updated_at", userRequest.UpdatedAt, DbType.DateTime);
            parameters.Add("p_id_out", null, DbType.Int32, ParameterDirection.Output);

            int expectedId = 1;
            _mockTransaction.Setup(x => x.ExecuteAsync("create_user", parameters)).ReturnsAsync(expectedId);
            var repository = new UserRepository(_mockTransaction.Object);
            
            // Act
            var result = await repository.CreateAsync(userRequest);

            // Assert
            Assert.Equal(0, result);
            result.Should().Be(0);
        }
    }
}
