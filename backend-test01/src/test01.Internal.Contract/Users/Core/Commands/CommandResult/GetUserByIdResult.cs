using System;

namespace test01.Internal.Contract.Users.Core.Commands.CommandResult
{
    //public record GetUserByIdResult(string username, string email, DateTime dateOfbirth, int roleId);
    public record GetUserByIdResult(string username, string email,string password, DateTime dateOfbirth, int roleId,bool status);
}
