using BookLibrary.Management.Contract.Model;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Management.DataAccessLayer
{
    public class BookLibraryContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book2Author> Book2Authors { get; set; }
        public DbSet<BorrowHistory> BorrowHistories { get; set; }

        public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .HasIndex(b => b.Email);

            modelBuilder.Entity<Book>()
                   .Property(o => o.Id)
                   .ValueGeneratedNever();

            modelBuilder.Entity<Author>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Publisher>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Book2Author>()
                .HasKey(o => new { o.BookId, o.AuthorId });

            modelBuilder.Entity<BorrowHistory>()
                .HasKey(o => new { o.BookId, o.CustomerId, o.StartDate });
        }
    }
}
