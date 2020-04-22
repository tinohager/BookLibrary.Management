using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.BookService
{
    public class DefaultBookService : IBookService
    {
        private readonly ILogger _logger;
        private readonly IBookRepository _bookRepository;

        public DefaultBookService(ILogger<DefaultBookService> logger,
            IBookRepository bookRepository)
        {
            this._logger = logger;
            this._bookRepository = bookRepository;
        }

        public async Task<bool> CheckExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            id = this.NormalizeIsbn(id);

            var book = await this._bookRepository.GetAsync(id, cancellationToken);
            if (book == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddAsync(AddBookDto item, CancellationToken cancellationToken = default)
        {
            var id = this.NormalizeIsbn(item.Isbn);

            if (await this.CheckExistsAsync(id))
            {
                return false;
            }

            var book = new Book
            {
                Id = id,
                BookCount = item.BookCount,
                Title = item.Title,
                Abstract = item.Abstract,
                PublisherId = item.PublisherId,
            };

            if (!await this._bookRepository.AddAsync(book, cancellationToken))
            {
                return false;
            }

            foreach (var authorId in item.AuthorIds)
            {
                if (!await this._bookRepository.AddAuthorToBookAsync(book.Id, authorId, cancellationToken))
                {
                    this._logger.LogWarning($"Cannot create relation between book and author {book.Id} {authorId}");
                }
            }

            return true;
        }

        public string NormalizeIsbn(string isbn)
        {
            isbn = isbn.Replace("-", string.Empty);
            isbn = isbn.Trim();
            return isbn;
        }

        public async Task<Book[]> GetBooksAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default)
        {
            var books = await this._bookRepository.GetAllAsync(take, skip, search, cancellationToken);
            return books;
        }
    }
}
