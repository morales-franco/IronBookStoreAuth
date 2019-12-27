using IronBookStoreAuthJWT.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Core.Services
{
    public interface IBookStoreRepository
    {
        Task AddBook(Book book);
        Task DeleteBook(Book book);
        Task<Book> GetBook(Guid bookId);
        Task<IEnumerable<Book>> GetBooks();
        Task<bool> SaveAsync();
        Task<bool> BookExists(Guid bookId);
        Task UpdateBook(Book book);
    }
}
