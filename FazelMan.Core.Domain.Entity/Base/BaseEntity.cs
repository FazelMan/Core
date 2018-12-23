using System;

namespace FazelMan.Core.Domain.Entity.Base
{
    public abstract class BaseEntity<Type> : IAuditable
    {
        public Type Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public bool IsRemoved { get; set; } = false;
        public int? Priority { get; set; }
    }
}
