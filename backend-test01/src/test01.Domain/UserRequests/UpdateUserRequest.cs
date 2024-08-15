using System;
using test01.Domain.Core;

namespace test01.Domain.UserRequests
{
    public class UpdateUserRequest : Entity, IAggregateRoot
    {
        public UpdateUserRequest(int id, string username, string email, DateTime dateOfBirth, bool isActive, int roleId)
        {
            Id = id;
            Username = username;
            Email = email;
            DateOfBirth = dateOfBirth;
            RoleId = roleId;
            UpdatedAt = DateTime.UtcNow;
            IsActive = isActive;
        }

        public UpdateUserRequest(int id, string username, string email,string password, DateTime dateOfBirth, bool isActive, int roleId)
        {
            Id = id;
            Username = username;
            Email = email;
            DateOfBirth = dateOfBirth;
            RoleId = roleId;
            UpdatedAt = DateTime.UtcNow;
            IsActive = isActive;
            Password = password;
        }
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}
