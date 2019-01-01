using System;

namespace FazelMan.Core.Domain.Entities.Auditing
{
    public interface IAuditable
    {
        DateTime CreatedDate { get; set; }
        bool IsRemoved { get; set; }
    }
}
