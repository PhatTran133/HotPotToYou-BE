﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Service.Paging
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; } = 0;
        public int TotalPage { get; set; } = 0;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) 
        { 
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1)* pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
