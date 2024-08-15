using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace test01.Persistence.Roles.Core.Queries
{
    public interface IRolQueries
    {
        Task<List<RolQueryDto>> GetRoles();
    }
}
