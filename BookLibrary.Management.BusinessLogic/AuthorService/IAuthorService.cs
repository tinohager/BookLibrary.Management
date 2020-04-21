using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.AuthorService
{
    public interface IAuthorService
    {
        Task<AuthorDto[]> GetAllAsync(int take, int skip, string search, CancellationToken cancellationToken = default);
        Task<AuthorDto> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<AuthorDto> GetAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(AddAuthorDto item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, UpdateAuthorDto item, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    }
}
