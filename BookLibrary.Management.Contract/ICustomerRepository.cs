using BookLibrary.Management.Contract.Model;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.Contract
{
    public interface ICustomerRepository
    {
        Task<Customer[]> GetAllAsync(int take, int skip, CancellationToken cancellationToken = default);
        Task<Customer> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> AddAsync(Customer item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Customer item, CancellationToken cancellationToken = default);
    }
}
