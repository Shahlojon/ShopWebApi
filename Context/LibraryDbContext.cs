using Library.Configuration;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Context;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
        Database.EnsureCreated();   // Create database if doesn't exist
    }
    // DbSet<T> — это коллекция, которая представляет собой таблицу в базе данных и позволяет
    // выполнять с ней операции(чтение, добавление, обновление, удаление).
    public DbSet<User> Users { get; set; }  
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());  // Применяем конфигурацию для модели Author
        modelBuilder.ApplyConfiguration(new BookConfiguration());  // Применяем конфигурацию для модели Book
        modelBuilder.ApplyConfiguration(new UserConfiguration());  // Применяем конфигурацию для модели User
        modelBuilder.ApplyConfiguration(new BorrowedBookConfiguration());  // Применяем конфигурацию для модели BorrowedBook
        base.OnModelCreating(modelBuilder);
    }
}

