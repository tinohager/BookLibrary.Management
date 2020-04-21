using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.DataAccessLayer
{
    public class MssqlAuthorRepository : IAuthorRepository
    {
        private readonly DbContextOptionsBuilder<BookLibraryContext> _optionsBuilder;

        public MssqlAuthorRepository(MssqlConfiguration configuration)
        {
            this._optionsBuilder = new DbContextOptionsBuilder<BookLibraryContext>();
            this._optionsBuilder.UseSqlServer(configuration.ConnectionString);
        }

        public async Task<Author[]> GetAllAsync(int take, int skip, string search = null, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var query = context.Authors.AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.Name.Contains(search));
                }

                return await query.Take(take).Skip(skip).ToArrayAsync(cancellationToken);
            }
        }

        public async Task<Author> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.Authors.Where(o => o.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<Author> GetAsync(string name, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.Authors.Where(o => o.Name == name).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> AddAsync(Author item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                context.Authors.Add(item);
                if (await context.SaveChangesAsync(cancellationToken) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Author item, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var author = await context.Authors.SingleOrDefaultAsync(o => o.Id == item.Id, cancellationToken);
                if (author == null)
                {
                    return false;
                }

                item.Adapt(author);

                await context.SaveChangesAsync(cancellationToken);
            }

            return false;
        }
    }
}
