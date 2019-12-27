using IronBookStoreAuthJWT.Core.Entities;
using IronBookStoreAuthJWT.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Data.Repositories
{
    public class BookStoreRepository : IBookStoreRepository
    {
        //TODO: Implementing only one Repository for testing in real apps: try to use: different repositories + UnitOfWWork
        private readonly ApplicationDbContext _context;

        public BookStoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            await _context.AddAsync(book);
        }

        public async Task<bool> BookExists(Guid bookId)
        {
            return await _context.Books.AnyAsync(b => b.BookId == bookId);
        }
#pragma warning disable 1998
        // disable async warning - no RemoveAsync available
        public async Task DeleteBook(Book book)
        {
            _context.Remove(book);
        }
#pragma warning restore 1998

        public async Task<Book> GetBook(Guid bookId)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.BookId == bookId);
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

#pragma warning disable 1998
        // disable async warning - no Update async available
        public async Task UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }
#pragma warning restore 1998
    }
}
