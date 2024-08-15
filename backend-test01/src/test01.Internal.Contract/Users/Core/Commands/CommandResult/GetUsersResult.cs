using RealPlaza.Core.Common.Contracts;
using System;
using System.Collections.Generic;

namespace test01.Internal.Contract.Users.Core.Commands.CommandResult
{
    public record GetUsersResult(int id,string username, string email, DateTime dateOfbirth, int roleId,bool status,string password="*************");
    public record PaginationResponse<T>: ICommandResult
    {
        public Paging Paging { get; set; }
        public string[] Sorting { get; set; }
        public IEnumerable<T> Data { get; set; }

        public ResultStatus Status { get; set; }

        public ValidationResult ValidationResult { get; set; }
    }
    public record Paging
    {
        public int CurrentIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
    }
}
