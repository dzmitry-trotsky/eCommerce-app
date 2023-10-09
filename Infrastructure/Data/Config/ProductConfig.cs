using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(_ => _.Id).IsRequired();
            builder.Property(_ => _.Name).IsRequired().HasMaxLength(100);
            builder.Property(_ => _.Description).IsRequired().HasMaxLength(180);
            builder.Property(_ => _.Price).HasColumnType("decimal(10,2)");
            builder.Property(_ => _.ImageUrl).IsRequired();
            builder.HasOne(_ => _.ProductBrand).WithMany()
                   .HasForeignKey(_ => _.ProductBrandId);
            builder.HasOne(_ => _.ProductType).WithMany()
                   .HasForeignKey(_ => _.ProductTypeId);
        }
    }
}
