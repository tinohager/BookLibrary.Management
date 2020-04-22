using BookLibrary.Management.Contract.Model;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.Contract
{
    public interface IPublisherRepository
    {
        Task<Publisher[]> GetAllAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default);
        Task<Publisher> GetAsync(string name, CancellationToken cancellationToken = default);
        Task<Publisher> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> AddAsync(Publisher item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Publisher item, CancellationToken cancellationToken = default);
    }
}
