using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.PublisherService
{
    public interface IPublisherService
    {
        Task<PublisherDto[]> GetAllAsync(int take, int skip, string search, CancellationToken cancellationToken = default);
        Task<PublisherDto> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<PublisherDto> GetAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(AddPublisherDto item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, UpdatePublisherDto item, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    }
}
