using IronBookStoreAuthJWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                 .HasKey(r => r.UserId);

            builder
                .Property(r => r.UserId)
                .ValueGeneratedOnAdd();

            builder
               .Property(r => r.Email)
               .IsRequired()
               .HasMaxLength(128);

            builder
                .HasIndex(r => r.Email)
                .IsUnique();


            builder
                .Property(r => r.Password)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
