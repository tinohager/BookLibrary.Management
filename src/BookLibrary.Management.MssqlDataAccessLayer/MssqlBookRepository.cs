using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.DataAccessLayer
{
    public class MssqlBookRepository : IBookRepository
    {
        private readonly DbContextOptionsBuilder<BookLibraryContext> _optionsBuilder;

        public MssqlBookRepository(MssqlConfiguration configuration)
        {
            this._optionsBuilder = new DbContextOptionsBuilder<BookLibraryContext>();
            this._optionsBuilder.UseSqlServer(configuration.ConnectionString);
        }

        public IQueryable<Book> GetQueryable()
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return context.Books.AsQueryable();
            }
        }

        public async Task<Book[]> GetAllAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var query = context.Books.AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.Title.Contains(search));
                }

                return await query.Take(take).Skip(skip).ToArrayAsync(cancellationToken);
            }
        }

        public async Task<Book> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.Books.SingleOrDefaultAsync(o => o.Id == id);
            }
        }

        public async Task<bool> AddAsync(Book item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                context.Books.Add(item);
                if (await context.SaveChangesAsync(cancellationToken) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Book item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var book = await context.Books.SingleOrDefaultAsync(o => o.Id == item.Id, cancellationToken);
                if (book == null)
                {
                    return false;
                }

                item.Adapt(book);

                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var item = await context.Books.SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
                if (item == null)
                {
                    return false;
                }

                context.Books.Remove(item);

                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }

        #region Author relation

        public async Task<bool> AddAuthorToBookAsync(string bookId, int authorId, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                context.Book2Authors.Add(new Book2Author { BookId = bookId, AuthorId = authorId });

                if (await context.SaveChangesAsync(cancellationToken) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> RemoveAuthorFromBookAsync(string bookId, int authorId, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var relation = await context.Book2Authors.SingleOrDefaultAsync(o => o.BookId == bookId && o.AuthorId == authorId, cancellationToken);
                if (relation == null)
                {
                    return false;
                }

                context.Remove(relation);

                if (await context.SaveChangesAsync(cancellationToken) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<Author[]> GetAuthorsFromBookAsync(string bookId, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var authorIds = await context.Book2Authors.Where(o => o.BookId == bookId).Select(o => o.AuthorId).ToArrayAsync(cancellationToken);
                var authors = await context.Authors.Where(o => authorIds.Contains(o.Id)).ToArrayAsync(cancellationToken);
                return authors;
            }
        }

        #endregion
    }
}
