using System;

namespace FazelMan.Domain.Entities
{
    public abstract class BaseEntity<Type> 
    {
        public Type Id { get; set; }
    }
}
