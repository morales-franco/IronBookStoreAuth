using IronBookStoreAuthJWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Data.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                 .HasKey(r => r.RoleId);

            builder
                .Property(r => r.RoleId)
                .ValueGeneratedOnAdd();

            builder
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
