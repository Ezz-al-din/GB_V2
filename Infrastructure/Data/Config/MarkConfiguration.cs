using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MarkConfiguration : IEntityTypeConfiguration<Mark>
    {
        public void Configure(EntityTypeBuilder<Mark> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18.2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.MarkBrand).WithMany().HasForeignKey(p => p.MarkBrandId);
            builder.HasOne(t => t.MarkType).WithMany().HasForeignKey(p => p.MarkTypeId);
        }
    }
}