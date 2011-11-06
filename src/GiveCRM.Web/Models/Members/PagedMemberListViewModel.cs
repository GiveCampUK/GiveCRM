using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiveCRM.Models;
using PagedList;

namespace GiveCRM.Web.Models.Members
{
    public class PagedMemberListViewModel : IPagedList<Member>
    {
        private readonly IPagedList<Member> pagedMemberList;

        public PagedMemberListViewModel(IPagedList<Member> pagedMemberList, Func<int, string> pagingFunction)
        {
            if (pagedMemberList == null)
            {
                throw new ArgumentNullException("pagedMemberList");
            }

            if (pagingFunction == null)
            {
                throw new ArgumentNullException("pagingFunction");
            }

            this.pagedMemberList = pagedMemberList;
            PagingFunction = pagingFunction;
        }

        public Func<int, string> PagingFunction { get; private set; }

        /// <summary>
        /// Total number of subsets within the superset.
        /// </summary>
        /// <value>
        /// Total number of subsets within the superset.
        /// </value>
        public int PageCount
        {
            get { return pagedMemberList.PageCount; }
        }

        /// <summary>
        /// Total number of objects contained within the superset.
        /// </summary>
        /// <value>
        /// Total number of objects contained within the superset.
        /// </value>
        public int TotalItemCount
        {
            get { return pagedMemberList.TotalItemCount; }
        }

        /// <summary>
        /// One-based index of this subset within the superset.
        /// </summary>
        /// <value>
        /// One-based index of this subset within the superset.
        /// </value>
        public int PageNumber
        {
            get { return pagedMemberList.PageNumber; }
        }

        /// <summary>
        /// Maximum size any individual subset.
        /// </summary>
        /// <value>
        /// Maximum size any individual subset.
        /// </value>
        public int PageSize
        {
            get { return pagedMemberList.PageSize; }
        }

        /// <summary>
        /// Returns true if this is NOT the first subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is NOT the first subset within the superset.
        /// </value>
        public bool HasPreviousPage
        {
            get { return pagedMemberList.HasPreviousPage; }
        }

        /// <summary>
        /// Returns true if this is NOT the last subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is NOT the last subset within the superset.
        /// </value>
        public bool HasNextPage
        {
            get { return pagedMemberList.HasNextPage; }
        }

        /// <summary>
        /// Returns true if this is the first subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is the first subset within the superset.
        /// </value>
        public bool IsFirstPage
        {
            get { return pagedMemberList.IsFirstPage; }
        }

        /// <summary>
        /// Returns true if this is the last subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is the last subset within the superset.
        /// </value>
        public bool IsLastPage
        {
            get { return pagedMemberList.IsLastPage; }
        }

        /// <summary>
        /// One-based index of the first item in the paged subset.
        /// </summary>
        /// <value>
        /// One-based index of the first item in the paged subset.
        /// </value>
        public int FirstItemOnPage
        {
            get { return pagedMemberList.FirstItemOnPage; }
        }

        /// <summary>
        /// One-based index of the last item in the paged subset.
        /// </summary>
        /// <value>
        /// One-based index of the last item in the paged subset.
        /// </value>
        public int LastItemOnPage
        {
            get { return pagedMemberList.LastItemOnPage; }
        }

        
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Member> GetEnumerator()
        {
            return pagedMemberList.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        

        /// <summary>
        /// Gets a non-enumerable copy of this paged list.
        /// </summary>
        /// <returns>
        /// A non-enumerable copy of this paged list.
        /// </returns>
        public IPagedList GetMetaData()
        {
            return pagedMemberList.GetMetaData();
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        public Member this[int index]
        {
            get { return pagedMemberList[index]; }
        }

        /// <summary>
        /// Gets the number of elements contained on this page.
        /// </summary>
        public int Count
        {
            get { return pagedMemberList.Count; }
        }

        
    }
}