using Entities.Models;
using Repositories.EFCore.Extensions;
using System.Linq.Dynamic.Core;

namespace Repositories.Extensions
{
    public static class BookRepositoryExtension
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books,uint minPrice, uint maxPrice)
        {
            return books.Where(b => b.Price >= minPrice && b.Price <= maxPrice);
        }
        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return books
                .Where(b => b.Title.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return books.OrderBy(b => b.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderBy);
            return books.OrderBy(orderQuery);
        }

    }
}
