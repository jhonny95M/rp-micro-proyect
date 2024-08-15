
using System;
using System.Diagnostics.CodeAnalysis;

namespace test01.Domain.Core
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity
    {
        public string CreateUser { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
