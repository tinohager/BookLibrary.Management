using BookLibrary.Management.DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Management.MssqlDataAccessLayer
{
    public class MssqlInitialize
    {
        private readonly ILogger _logger;
        private readonly DbContextOptionsBuilder<BookLibraryContext> _optionsBuilder;

        public MssqlInitialize(MssqlConfiguration configuration, ILogger<MssqlInitialize> logger)
        {
            this._optionsBuilder = new DbContextOptionsBuilder<BookLibraryContext>();
            this._optionsBuilder.UseSqlServer(configuration.ConnectionString);

            this._logger = logger;

            this._logger.LogInformation(configuration.ConnectionString);
        }

        public async Task<bool> InitializeDatabaseAsync()
        {
            try
            {
                using (var context = new BookLibraryContext(this._optionsBuilder.Options))
                {
                    this._logger.LogInformation("Check database created");
                    await context.Database.EnsureCreatedAsync();
                    this._logger.LogInformation("Database is created");

                    #region Customer

                    if (!context.Customers.Any(o => o.Id == 1))
                    {
                        context.Customers.Add(new Customer
                        {
                            Gender = (int)Gender.Male,
                            Firstname = "Max",
                            Surname = "Mustermann",
                            Street = "Musterweg 1a",
                            PostalCode = "12345",
                            City = "Musterstadt",
                            CountryCode = "DE",
                            Email = "customer1@booklibrary.com",
                        });
                    }

                    if (!context.Customers.Any(o => o.Id == 2))
                    {
                        context.Customers.Add(new Customer
                        {
                            Gender = (int)Gender.Female,
                            Firstname = "Erika",
                            Surname = "Mustermann",
                            Street = "Musterweg 8c",
                            PostalCode = "1234",
                            City = "Musterhausen",
                            CountryCode = "AT",
                            Email = "customer2@booklibrary.com",
                        });
                    }

                    #endregion

                    #region Book

                    var author = new Author { Name = "Jojo Moyes" };
                    var publisher = new Publisher { Name = "Rowohlt Polaris" };
                    var book = new Book
                    {
                        Id = "9783499267369",
                        Title = "Weit weg und ganz nah",
                        Abstract = "Einmal angenommen... dein Mann hat sich aus dem Staub gemacht.",
                        BookCount = 1,
                    };

                    if (!context.Authors.Any(o => o.Id == 1))
                    {
                        context.Authors.Add(author);
                    }

                    if (!context.Publishers.Any(o => o.Id == 1))
                    {
                        context.Publishers.Add(publisher);
                    }

                    //Create ids for publisher and author
                    await context.SaveChangesAsync();

                    if (!context.Books.Any(o => o.Id == book.Id))
                    {
                        book.PublisherId = publisher.Id;

                        context.Books.Add(book);
                        context.Book2Authors.Add(new Book2Author { AuthorId = author.Id, BookId = book.Id });
                    }

                    #endregion

                    //Save book
                    await context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, nameof(InitializeDatabaseAsync));
            }

            return false;
        }
    }
}
