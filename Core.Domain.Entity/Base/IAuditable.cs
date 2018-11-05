using System;

namespace Core.Domain.Entity.Base
{
    public interface IAuditable
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdateDate { get; set; }
        bool IsRemoved { get; set; }
    }
}
