using BookLibrary.Management.DataAccessLayer.Model;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.DataAccessLayer
{
    public interface IAuthorRepository
    {
        Task<Author[]> GetAllAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default);
        Task<Author> GetAsync(string name, CancellationToken cancellationToken = default);
        Task<Author> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> AddAsync(Author item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Author item, CancellationToken cancellationToken = default);
    }
}
