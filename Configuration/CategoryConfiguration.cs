using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Entites;

namespace ShopApi.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(c => c.ParentCategory) // Связь с родительской категорией
            .WithMany(c => c.Subcategories)  // Родительская категория может иметь подкатегории
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Не удалять подкатегории при удалении родителя

        builder.HasMany(c => c.Products) // Категория может иметь несколько товаров
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
    }
}
