using System;

namespace test01.Internal.Contract.Users.Core.Commands
{
    public record CreateUser
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int RoleId { get; set; }

        public bool Status { get; set; }
    }
}
