﻿namespace NashTechAssignmentDay5.Application.Helper
{
	public class PaginatedList<T> : List<T>
	{
		public int PageIndex { get; private set; }
		public int PageSize { get;set; }
		public int TotalPages { get; private set; }

		public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			PageSize = pageSize;

			this.AddRange(items);
		}

		public bool HasPreviousPage => PageIndex > 1;

		public bool HasNextPage => PageIndex < TotalPages;

		//Create a paginatedlist with a source as Queryable 
		//	and pageIndex to keep track of the current page showing 
		//	and pageSize to change amount of record in one page
		public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
		{
			var count = source.Count();
			var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
			return new PaginatedList<T>(items, count, pageIndex, pageSize);
		}
	}
}
