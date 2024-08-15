using Dapper;
using RealPlaza.Core.Common.Utils;
using RealPlaza.Core.Core.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace test01.Persistence.UserRequests.Core.Queries;

public class UserQueries : IUserQueries
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserQueries(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<UserQueryDto> GetUserByIdQuery(int userId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("p_user_id", userId);
        return await connection.QuerySingleFunctionAsync<UserQueryDto>("get_user_by_id", parameters);
    }

    public async Task<IEnumerable<UserQueryDto>> GetUserQuery(UserQuery query)
    {
        //Querio que me devuelva los usuarios que cumplan con los parametros de busqueda segun los parametros que se le pasen
        
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("p_username", query.userName);
        parameters.Add("p_email",query.email);
        parameters.Add("p_role_id", query.roleId);
        parameters.Add("p_is_active", query.active);
        parameters.Add("p_page_size", query.pageSize);
        parameters.Add("p_page_current", query.pageCurrent);
        parameters.Add("p_search_filter", query.searchFilter);
        return await connection.QueryFunctionAsync<UserQueryDto>("search_users_dynamic", parameters);
    }
}
