using Library.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book> // Добавление конфигурации для модели Book
{
    public void Configure(EntityTypeBuilder<Book> builder)  // Настройка конфигурации
    {
        builder.HasKey(b => b.Id);  // Первычный ключ 
        builder.Property(b => b.Title) // Название книги
            .IsRequired()  // Объязательное поле
            .HasMaxLength(200);  // Добавляем свойства HasMaxLength максимальный символ 200
        builder.HasOne(b => b.Author) // Один автор
               .WithMany(a => a.Books) // Может иметь Много книг
               .HasForeignKey(b => b.AuthorId); // Внешний ключ AuthorId
    }
}

