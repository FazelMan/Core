using System;

namespace Core.Domain.Dto
{
    public class SearchParameters<T>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int? LastLoadedId { get; set; }
        public bool NeedTotalCount { get; set; } = false;
        public T SearchParameter { get; set; }
        public string Search { get; set; } = "";
    }

    public class BaseSearchParameter : SearchParameters<DateTime>
    {

    }
}
