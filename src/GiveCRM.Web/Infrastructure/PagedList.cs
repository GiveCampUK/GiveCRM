using System.Collections.Generic;
using System.Linq;

namespace GiveCRM.Web.Infrastructure
{
    /// <summary>
    /// Defines the default implementation of <see cref="IPagedList"/>. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            ItemsPerPage = pageSize;

            var itemsList = items.ToList();
            ItemCount = itemsList.Count;
            AddRange(itemsList.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public int ItemsPerPage { get; set; }
        public int PageIndex { get; set; }
        public int ItemCount { get; set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex*ItemsPerPage) <= ItemCount; }
        }
    }
}