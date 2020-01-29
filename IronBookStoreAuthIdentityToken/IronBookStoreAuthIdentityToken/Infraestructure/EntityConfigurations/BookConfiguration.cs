using IronBookStoreAuthIdentityToken.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IronBookStoreAuthIdentityToken.Infraestructure.EntityConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(b => b.BookId);

            builder
                .Property(b => b.BookId)
                .ValueGeneratedOnAdd(); //TODO: Generate BookId automatically on insert.

            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(b => b.Description)
                .HasMaxLength(512);

        }
    }
}
