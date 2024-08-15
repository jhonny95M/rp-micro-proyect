using System.Threading.Tasks;
using test01.Domain.UserRequests;

namespace test01.Persistence.UserRequests.Repository
{
    public interface IUserRepository
    {
        Task<int>CreateAsync(UserRequest userRequest);
        Task DeleteAsync(DeleteUserRequest deleteUserRequest);
        Task UpdateAsync(UpdateUserRequest userRequest);
    }
}
