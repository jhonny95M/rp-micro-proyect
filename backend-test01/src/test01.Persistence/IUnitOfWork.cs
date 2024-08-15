using RealPlaza.Core.Core.Persistence;
using test01.Persistence.UserRequests.Repository;


namespace test01.Persistence
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}
