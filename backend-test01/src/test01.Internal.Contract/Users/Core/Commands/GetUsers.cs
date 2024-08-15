using RealPlaza.Core.Common.Contracts;

namespace test01.Internal.Contract.Users.Core.Commands
{

    public record GetUsers(int pageSize=10,int pageCurrent=1,string searchFilter=null, string userName = null, string email = null, int? roleId = null, bool? status = null);
}
