using System.Threading.Tasks;

namespace test01.Application.Common;

public class ValidateParameters
{
}
public interface IValidateParameters
{
    Task Validate(Internal.Contract.Users.Core.Commands.CreateUser request);
}
