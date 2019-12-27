using IronBookStoreAuthJWT.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Data
{
    public static class ApplicationDbContextExtensions
    {
        public static async Task EnsureSeedDataForContext(this ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                var books = new List<Book>()
                {
                    new Book()
                    {
                        BookId = new Guid("fec0a4d6-5830-4eb8-8024-272bd5d6d2bb"),
                        Name = "Clean Code",
                        Description = "best coding practice"
                    },
                    new Book()
                    {
                        BookId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                        Name = "Adaptive Code via c#",
                        Description = "Agile coding with design patterns and SOLID principles"
                    }
                };

                await context.Books.AddRangeAsync(books);
                await context.SaveChangesAsync();
            }

        }
    }
}
