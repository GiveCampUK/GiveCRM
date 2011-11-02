using System.Collections.Generic;

namespace GiveCRM.Web.Infrastructure
{
    /// <summary>
    /// Defines a selection of properties for working with paged lists.  
    /// </summary>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// Gets or sets the number of items to display on a page.
        /// </summary>
        /// <value>
        /// The number of items to display on a page.
        /// </value>
        int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the index of the page to display.  The index is zero-based.
        /// </summary>
        /// <value>
        /// The index of the page to display.
        /// </value>
        int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the total number of items in the list.
        /// </summary>
        /// <value>
        /// The total number of items in the list.
        /// </value>
        int ItemCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not there is a page preceding the current page.  
        /// <c>HasPreviousPage == false</c> implies <c>PageIndex == 0</c>.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if there is a page preceding the current page; otherwise, <c>false</c>.
        /// </value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether or not there is a page after the current page.
        /// <c>HasNextPage == false</c> implies <c>PageIndex == Math.Ceil(ItemCount / ItemsPerPage)</c>.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if there is a page after the current page; otherwise, <c>false</c>.
        /// </value>
        bool HasNextPage { get; }
    }
}