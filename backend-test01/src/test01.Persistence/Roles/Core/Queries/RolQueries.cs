using Dapper;
using Microsoft.AspNetCore.Connections;
using RealPlaza.Core.Core.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace test01.Persistence.Roles.Core.Queries
{
    public class RolQueries : IRolQueries
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RolQueries(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<List<RolQueryDto>> GetRoles()
        {
            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<RolQueryDto>("SELECT id, role_name as NameRol FROM public.roles;");
            return result.AsList();
        }
    }
}
