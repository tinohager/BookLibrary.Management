using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Mapster;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.CustomerService
{
    public class DefaultCustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public DefaultCustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<CustomerDto[]> GetAllAsync(int take, int skip, CancellationToken cancellationToken = default)
        {
            var items = await this._customerRepository.GetAllAsync(take, skip, cancellationToken);
            var filteredItems = items.Where(o => !o.Deleted);
            return filteredItems.Adapt<CustomerDto[]>();
        }

        public async Task<CustomerDto> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await this._customerRepository.GetAsync(id, cancellationToken);
            if (item.Deleted)
            {
                return null;
            }

            return item.Adapt<CustomerDto>();
        }

        public async Task<bool> AddAsync(AddCustomerDto item, CancellationToken cancellationToken = default)
        {
            return await this._customerRepository.AddAsync(item.Adapt<Customer>(), cancellationToken);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCustomerDto item, CancellationToken cancellationToken = default)
        {
            var customer = item.Adapt<Customer>();
            customer.Id = id;

            return await this._customerRepository.UpdateAsync(customer, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (!await this.ExistsAsync(id, cancellationToken))
            {
                return false;
            }

            var item = await this._customerRepository.GetAsync(id, cancellationToken);
            if (item == null)
            {
                return false;
            }

            item.Deleted = true;
            item.DisguiseData();

            return await this._customerRepository.UpdateAsync(item, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await this.GetAsync(id);
            if (item == null)
            {
                return false;
            }

            return true;
        }
    }
}
