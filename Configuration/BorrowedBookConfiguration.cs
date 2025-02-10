using Library.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Configuration;

public class BorrowedBookConfiguration : IEntityTypeConfiguration<BorrowedBook>  // Добавление конфигурации для модели BorrowedBook
{
    public void Configure(EntityTypeBuilder<BorrowedBook> builder)  // Настройка конфигурации
    {
        builder.HasKey(bb => bb.Id);  // Первычный ключ

        builder.HasOne(bb => bb.User)  // Один пользователь
               .WithMany(u => u.BorrowedBooks)  // Может получить несколько книг 
               .HasForeignKey(bb => bb.UserId) // Внешний ключ UserId
               .OnDelete(DeleteBehavior.Cascade); // Удаление каскадом

        builder.HasOne(bb => bb.Book)    // Одна книга
               .WithMany(b => b.BorrowedBooks)  // Может быть у многих книг
               .HasForeignKey(bb => bb.BookId)   // Внешний ключ BookId
               .OnDelete(DeleteBehavior.Cascade);  // Удаление каскадом 
    }
}

