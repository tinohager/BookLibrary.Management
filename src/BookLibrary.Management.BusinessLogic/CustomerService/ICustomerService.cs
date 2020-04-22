using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerDto[]> GetAllAsync(int take, int skip, CancellationToken cancellationToken = default);
        Task<CustomerDto> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(AddCustomerDto item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, UpdateCustomerDto item, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
