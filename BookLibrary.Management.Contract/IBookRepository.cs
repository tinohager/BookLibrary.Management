using BookLibrary.Management.Contract.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.Contract
{
    public interface IBookRepository
    {
        Task<Book[]> GetAllAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default);
        Task<Book> GetAsync(string id, CancellationToken cancellationToken = default);
        IQueryable<Book> GetQueryable();

        Task<bool> AddAsync(Book item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Book item, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);

        Task<bool> AddAuthorToBookAsync(string bookId, int authorId, CancellationToken cancellationToken = default);
        Task<bool> RemoveAuthorFromBookAsync(string bookId, int authorId, CancellationToken cancellationToken = default);
        Task<Author[]> GetAuthorsFromBookAsync(string bookId, CancellationToken cancellationToken = default);
    }
}
