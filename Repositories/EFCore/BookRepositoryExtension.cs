using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public static class BookRepositoryExtension
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books,uint minPrice, uint maxPrice)
        {
            return books.Where(b => (b.Price >= minPrice) && (b.Price <= maxPrice));
        }
    }
}
