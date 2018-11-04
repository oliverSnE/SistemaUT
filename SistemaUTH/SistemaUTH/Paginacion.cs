using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SistemaUTH
{
    public class Paginacion<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPage { get; private set; }
        public int TotlR { get; private set; }

        public Paginacion(List<T> items,int count,int pageIndex,int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPage = (int)Math.Ceiling(count / (double) pageSize);
            this.TotlR = count;

            this.AddRange(items);
        }
        public bool hasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool hasNextPage
        {
            get
            {
                return (PageIndex < TotalPage);
            }
        }

        public static async Task<Paginacion<T>> CreatesAsync(IQueryable<T> source, int pageIndex,int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1)* pageSize).Take(pageSize).ToListAsync();

            return new Paginacion<T>(items, count, pageIndex, pageSize);
        }

    }
}
