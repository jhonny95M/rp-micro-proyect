using System;

namespace test01.Internal.Contract.Users.Core.Commands.CommandResult;

public record UsersResult(int id,string userName,string password, string email, DateTime dateOfbirth, int roleId);
