using Dapper;
using RealPlaza.Core.Core.Persistence;
using System.Data;
using System.Threading.Tasks;
using test01.Domain.UserRequests;

namespace test01.Persistence.UserRequests.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericTransaction _transaction;

        public UserRepository(IGenericTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<int> CreateAsync(UserRequest userRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_username", userRequest.Username,DbType.String);
            parameters.Add("p_password", userRequest.Password,DbType.String);
            parameters.Add("p_email", userRequest.Email,DbType.String);
            parameters.Add("p_date_of_birth", userRequest.DateOfBirth,DbType.Date);
            parameters.Add("p_role_id", userRequest.RoleId,DbType.Int32);
            parameters.Add("p_is_active", userRequest.IsActive,DbType.Boolean);
            parameters.Add("p_created_at", userRequest.CreatedAt,DbType.DateTime);
            parameters.Add("p_id_out", null,DbType.Int32,ParameterDirection.Output);

            await _transaction.ExecuteAsync("create_user", parameters);
            var result = parameters.Get<int?>("p_id_out");
            return result??0;
        }

        public Task DeleteAsync(DeleteUserRequest deleteUserRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_id", deleteUserRequest.id,DbType.Int32);
            return _transaction.ExecuteAsync("desactivate_user", parameters);
        }

        public async Task UpdateAsync(UpdateUserRequest userRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_id", userRequest.Id,DbType.Int32);
            parameters.Add("p_username", userRequest.Username,DbType.String);
            parameters.Add("p_email", userRequest.Email,DbType.String);
            parameters.Add("p_password", userRequest.Password,DbType.String);
            parameters.Add("p_date_of_birth", userRequest.DateOfBirth,DbType.Date);
            parameters.Add("p_role_id", userRequest.RoleId,DbType.Int32);
            parameters.Add("p_is_active", userRequest.IsActive,DbType.Boolean);
            parameters.Add("p_updated_at", userRequest.UpdatedAt,DbType.DateTime);
            await _transaction.ExecuteAsync("update_user", parameters);
        }
    }
}
