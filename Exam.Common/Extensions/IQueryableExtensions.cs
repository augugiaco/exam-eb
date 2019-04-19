using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Exam.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {          
            return new PagedResult<T>
            {
                Data = query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = query.Count()
            };
        }

        public static PagedResult<TK> ToPagedResult<T, TK>(this IQueryable<T> query, int page, int pageSize, Expression<Func<T, TK>> project) where T : class, new() where TK : class
        {
            return new PagedResult<TK>
            {
                Data = query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(project)
                    .ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = query.Count()
            };
        }
    }

    public class PagedResult<T> where T : class
    {
        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
