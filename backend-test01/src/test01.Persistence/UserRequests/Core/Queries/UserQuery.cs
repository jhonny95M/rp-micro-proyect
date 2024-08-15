namespace test01.Persistence.UserRequests.Core.Queries;

public record UserQuery(int pageSize = 10, int pageCurrent = 1, string searchFilter = null,string userName = null,string email=null,int? roleId=null, bool? active=null);
