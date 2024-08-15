using System;

namespace test01.Internal.Contract.Users.Core.Commands
{
    public record UpdateUser(int id,string UserName, string Password, string Email, DateTime DateOfBirth, bool isActive, int RoleId);
}
