using Library.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author> // Добавление конфигурации для модели Author
{
    public void Configure(EntityTypeBuilder<Author> builder) // Настройка конфигурации
    {
        builder.HasKey(a => a.Id); // Устанавливаем первичный ключ
        builder.Property(a => a.Name)
            .IsRequired()  // Объязательное поле 
            .HasMaxLength(150); // Добавляем свойства HasMaxLength максимальный символ 150
    }
}

