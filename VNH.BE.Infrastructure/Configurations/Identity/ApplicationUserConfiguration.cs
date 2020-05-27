using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VNH.BE.Domain.Aggregates.Identity;

namespace VNH.BE.Infrastructure.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(o => o.Address).HasMaxLength(500).IsUnicode();
            builder.Property(o => o.FirstName).HasMaxLength(50).IsUnicode();
            builder.Property(o => o.LastName).HasMaxLength(50).IsUnicode();
        }
    }
}
