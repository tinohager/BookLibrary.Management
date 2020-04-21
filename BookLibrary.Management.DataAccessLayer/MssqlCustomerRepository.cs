using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using BookLibrary.Management.DataAccessLayer;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.Library.Contract
{
    public class MssqlCustomerRepository : ICustomerRepository
    {
        private readonly DbContextOptionsBuilder<BookLibraryContext> _optionsBuilder;

        public MssqlCustomerRepository(MssqlConfiguration configuration)
        {
            this._optionsBuilder = new DbContextOptionsBuilder<BookLibraryContext>();
            this._optionsBuilder.UseSqlServer(configuration.ConnectionString);
        }

        public async Task<Customer[]> GetAllAsync(int take, int skip, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.Customers.Take(take).Skip(skip).ToArrayAsync(cancellationToken);
            }
        }

        public async Task<Customer> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.Customers.SingleOrDefaultAsync(o => o.Id == id);
            }
        }

        public async Task<bool> AddAsync(Customer item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                context.Customers.Add(item);
                if (await context.SaveChangesAsync(cancellationToken) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Customer item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.Id == item.Id, cancellationToken);
                if (customer == null)
                {
                    return false;
                }

                item.Adapt(customer);

                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
