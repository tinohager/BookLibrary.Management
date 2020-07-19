using BookLibrary.Management.DataAccessLayer.Model;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.BookService
{
    public interface IBookService
    {
        Task<bool> CheckExistsAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(AddBookDto item, CancellationToken cancellationToken = default);
        Task<Book[]> GetBooksAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default);
        string NormalizeIsbn(string isbn);
    }
}
