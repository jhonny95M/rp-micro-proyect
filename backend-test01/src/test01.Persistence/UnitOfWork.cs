using RealPlaza.Core.Core.Persistence;
using System;
using System.Diagnostics.CodeAnalysis;
using test01.Persistence.UserRequests.Repository;

namespace test01.Persistence
{
    [ExcludeFromCodeCoverage]
    public sealed class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(
            IServiceProvider serviceProvider, IUserRepository userRepository) : base(serviceProvider)
        {
            UserRepository = userRepository;
        }

    }
}
