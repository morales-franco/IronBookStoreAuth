using IronBookStoreAuthIdentityToken.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IronBookStoreAuthIdentityToken.Infraestructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .Property(b => b.Alias)
               .IsRequired()
               .HasMaxLength(100);
        }
    }
}
