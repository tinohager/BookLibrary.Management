using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.DataAccessLayer
{
    public class MssqlPublisherRepository : IPublisherRepository
    {
        private readonly DbContextOptionsBuilder<BookLibraryContext> _optionsBuilder;

        public MssqlPublisherRepository(MssqlConfiguration configuration)
        {
            this._optionsBuilder = new DbContextOptionsBuilder<BookLibraryContext>();
            this._optionsBuilder.UseSqlServer(configuration.ConnectionString);
        }

        public async Task<Publisher[]> GetAllAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var query = context.Publishers.AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.Name.Contains(search));
                }

                return await query.Take(take).Skip(skip).ToArrayAsync(cancellationToken);
            }
        }

        public async Task<Publisher> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.Publishers.Where(o => o.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<Publisher> GetAsync(string name, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.Publishers.Where(o => o.Name == name).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> AddAsync(Publisher item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                context.Publishers.Add(item);
                if (await context.SaveChangesAsync(cancellationToken) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Publisher item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var publisher = await context.Publishers.SingleOrDefaultAsync(o => o.Id == item.Id, cancellationToken);
                if (publisher == null)
                {
                    return false;
                }

                item.Adapt(publisher);

                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
