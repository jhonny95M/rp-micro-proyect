using System;
using test01.Domain.Core;

namespace test01.Domain.UserRequests
{
    public class UserRequest: Entity, IAggregateRoot
    {
        public UserRequest()
        {
        }
        public UserRequest(int id, string username, string email, DateTime dateOfBirth, int roleId, bool isActive)
        {
            Id = id;
            Username = username;
            Email = email;
            DateOfBirth = dateOfBirth;
            RoleId = roleId;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}
