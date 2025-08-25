using Entities.Dtos;
using Entities.Models;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<(IEnumerable<ExpandoObject> books, MetaData MetaData)> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges);
        Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book);
        Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges);
        Task DeleteOneBookAsync(int id, bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForPatchAsync(BookDtoForUpdate bookDto, Book book);
    }
}
