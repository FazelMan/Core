using System;
using System.Reflection;

namespace FazelMan.Domain.Entities
{
    public abstract class Entity : Entity<int>, IEntity
    {

    }

    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
