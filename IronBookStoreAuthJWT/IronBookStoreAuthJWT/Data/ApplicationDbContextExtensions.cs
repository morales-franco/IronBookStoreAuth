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

            if (!context.Users.Any())
            {
                var roles = new List<Role>
                {
                    new Role()
                    {
                        RoleId = new Guid("6c63a444-9555-48de-9cc5-249da962cf75"),
                        Name = "Administrator",
                    },
                    new Role()
                    {
                        RoleId = new Guid("67b896ea-f184-4118-a46e-52977cc70017"),
                        Name = "GeneralManager",
                    },
                    new Role()
                    {
                        RoleId = new Guid("0c5d2046-5b2f-428c-8fb3-ca9052378c36"),
                        Name = "SecurityManager",
                    }
                };

                var user = new User()
                {
                    UserId = new Guid("62cc6a6f-c01d-48db-b77b-3c3ad3c7b25b"),
                    Email = "admin@fmoralesdev.com",
                    Password = "Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", //123456
                };

                user.UserRoles.Add(new UserRole()
                {
                    UserId = new Guid("62cc6a6f-c01d-48db-b77b-3c3ad3c7b25b"),
                    RoleId = new Guid("6c63a444-9555-48de-9cc5-249da962cf75")
                });

                await context.Roles.AddRangeAsync(roles);
                await context.Users.AddAsync(user);
               
                await context.SaveChangesAsync();
            }

        }
    }
}
