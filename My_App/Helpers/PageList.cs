using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Helpers
{
    public class PageList<T>:List<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotlaCount { get; set; }

        public PageList(List<T> items,int count,int pagenumber,int pagesize)
        {
            this.TotlaCount = count;
            this.PageSize = pagesize;
            this.CurrentPage = pagenumber;
            this.TotalPages = (int)Math.Ceiling(count / (Double)PageSize);
            this.AddRange(items);
               
        }
public static async Task<PageList<T>> CreateAsync(IQueryable<T>source,int pageNamber,int PageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNamber - 1) * PageSize).Take(PageSize).ToListAsync();
            return new PageList<T>(items, count, pageNamber, PageSize);
        }
    }
}
