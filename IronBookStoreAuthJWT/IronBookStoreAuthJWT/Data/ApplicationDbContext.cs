using IronBookStoreAuthJWT.Core.Entities;
using IronBookStoreAuthJWT.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            :base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());


            //TODO: Implementing audit with shadow properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(entityType => typeof(IAuditable).IsAssignableFrom(entityType.ClrType)))
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedOn")
                    .IsRequired();
                modelBuilder.Entity(entityType.Name).Property<string>("CreatedBy")
                    .HasMaxLength(512)
                    .IsRequired();

                modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdatedOn");

                modelBuilder.Entity(entityType.Name).Property<string>("UpdatedBy")
                    .HasMaxLength(512);
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, 
            CancellationToken cancellationToken = default)
        {
            var timestamp = DateTime.UtcNow;
            var userId = "c3b7f625-c07f-4d7d-9be1-ddff8ff93b4d";

            //TODO: get added or updated entries
            var addedOrUpdatedEntries = ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            //TODO: fill out the audit fields - shadow properties approach
            foreach (var entry in addedOrUpdatedEntries.Where(e => e.Entity is IAuditable))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedOn").CurrentValue = timestamp;
                    entry.Property("CreatedBy").CurrentValue = userId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedOn").CurrentValue = timestamp;
                    entry.Property("UpdatedBy").CurrentValue = userId;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


    }
}

