using System.Collections.Generic;

namespace WorkingHours.Shared.Dto
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int PageCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
