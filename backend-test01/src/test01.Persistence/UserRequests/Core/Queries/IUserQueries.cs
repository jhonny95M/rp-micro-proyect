using System.Collections.Generic;
using System.Threading.Tasks;

namespace test01.Persistence.UserRequests.Core.Queries;

public interface IUserQueries
{
    Task<IEnumerable<UserQueryDto>> GetUserQuery(UserQuery query);
    Task<UserQueryDto> GetUserByIdQuery(int userId);
}
