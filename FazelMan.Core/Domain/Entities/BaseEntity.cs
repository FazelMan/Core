using System;
using FazelMan.Core.Domain.Entities.Auditing;

namespace FazelMan.Core.Domain.Entities
{
    public abstract class BaseEntity<Type> : IAuditable
    {
        public Type Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;
    }
}
