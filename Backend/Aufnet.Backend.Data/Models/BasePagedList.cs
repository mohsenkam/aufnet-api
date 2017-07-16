﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Aufnet.Backend.Data.Models
{
    /// <summary>
    /// Abstract implementation for generic paged list with metadata and enumerator support
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class BasePagedList<TEntity> : IPagedList<TEntity>
    {
        protected readonly List<TEntity> Subset = new List<TEntity>();

        /// <summary>
        /// 	Initializes a new instance of a type deriving from <see cref = "BasePagedList{T}" /> and sets properties needed to calculate position and size data on the subset and superset.
        /// </summary>
        /// <param name = "pageNumber">The one-based index of the subset of objects contained by this instance.</param>
        /// <param name = "pageSize">The maximum size of any individual subset.</param>
        /// <param name = "totalItemCount">The size of the superset.</param>
        protected internal BasePagedList(int pageNumber, int pageSize, int totalItemCount)
        {            
            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0
                            ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                            : 0;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
        }


        #region Implementation of IEnumerable

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Subset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IPagedList<out TEntity>

        public TEntity this[int index] { get { return Subset[index]; } }

        public int Count { get { return Subset.Count; } }

        #endregion

        #region Implementation of IPagedList

        public int PageCount { get; protected set; }
        public int PageSize { get; protected set; }
        public int PageNumber { get; protected set; }
        public int TotalItemCount { get; protected set; }
        public bool IsFirstPage { get; protected set; }
        public bool IsLastPage { get; protected set; }

        #endregion
    }
}