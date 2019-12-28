using IronBookStoreAuthJWT.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronBookStoreAuthJWT.Data.EntityConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {

            builder
                .HasKey(s => new { s.UserId, s.RoleId });

            builder
                .HasOne(s => s.User)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(s => s.UserId);

            builder
                .HasOne(s => s.Role)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(s => s.RoleId);

        }
    }
}
