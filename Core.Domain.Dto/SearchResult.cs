using System.Collections.Generic;

namespace Core.Domain.Dto
{
    public class SearchResult<TResultType, TSearchParameter>
    {
        public TSearchParameter SearchParameter { get; set; }
        public List<TResultType> Result { get; set; }
        public int TotalCount { get; set; }
        public int FilterCount { get; set; }
    }

    public class SearchResult<TResultType>
    {
        public List<TResultType> Result { get; set; }
        public int TotalCount { get; set; }
    }
}
