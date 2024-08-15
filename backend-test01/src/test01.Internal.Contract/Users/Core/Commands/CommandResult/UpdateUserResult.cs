using System;

namespace test01.Internal.Contract.Users.Core.Commands.CommandResult;

public record UpdateUserResult(string username, string email, DateTime dateOfBirth, int roleId,bool status);