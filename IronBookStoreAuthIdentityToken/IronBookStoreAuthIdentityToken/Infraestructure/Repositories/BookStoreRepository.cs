using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IronBookStoreAuthIdentityToken.Core.Entities;
using IronBookStoreAuthIdentityToken.Core.Services;

namespace IronBookStoreAuthIdentityToken.Infraestructure.Repositories
{
    public class BookStoreRepository : IBookStoreRepository
    {
        public Task AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BookExists(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBook(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooks()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email, bool includeRoles = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsBookOwner(Guid bookId, Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
