using IronBookStoreAuthIdentityToken.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IronBookStoreAuthIdentityToken.Core.Services
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
        Task<bool> IsBookOwner(Guid bookId, Guid ownerId);
        Task<User> GetUserByEmail(string email, bool includeRoles = false);
        Task<IEnumerable<User>> GetUsers();
        Task AddUser(User user);
    }
}
