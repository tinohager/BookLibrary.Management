using BookLibrary.Management.DataAccessLayer.Model;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.DataAccessLayer
{
    public interface ICustomerRepository
    {
        Task<Customer[]> GetAllAsync(int take, int skip, CancellationToken cancellationToken = default);
        Task<Customer> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> AddAsync(Customer item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Customer item, CancellationToken cancellationToken = default);
    }
}
