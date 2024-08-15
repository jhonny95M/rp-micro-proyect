using FluentAssertions;
using test01.Domain.UserRequests;
using Xunit;

namespace test01.UnitTest.Domain.Users
{
    public class UserTest
    {
        [Fact]
        public void Constructor_ShouldCreateUser()
        {
            // Arrange
            var id = 1;
            var username = "admin25";
            var email = "jmalpartida@experis.pe";
            var dateOfBirth = System.DateTime.UtcNow;
            var roleId = 1;
            var isActive = true;

            // Act
            var user = new UserRequest(id, username, email, dateOfBirth, roleId, isActive);
            // Assert
            Assert.NotNull(user);
            user.Id.Should().Be(id);
            user.Username.Should().Be(username);
            user.Email.Should().Be(email);
            user.DateOfBirth.Should().Be(dateOfBirth);
            user.RoleId.Should().Be(roleId);
            user.IsActive.Should().Be(isActive);
        }
    }
}
