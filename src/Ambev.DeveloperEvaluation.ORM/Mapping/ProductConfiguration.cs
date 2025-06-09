using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Description)
            .HasMaxLength(500);
        
        builder.Property(p => p.Category)
            .HasMaxLength(50);
        
        builder.Property(p => p.Image)
            .HasMaxLength(500);
        
        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.OwnsOne(p => p.Rating, rating =>
        {
            rating.Property(r => r.Rate)
                .HasColumnName("RatingRate")
                .HasColumnType("decimal(5,2)");

            rating.Property(r => r.Count)
                .HasColumnName("RatingCount");
        });
    }
}