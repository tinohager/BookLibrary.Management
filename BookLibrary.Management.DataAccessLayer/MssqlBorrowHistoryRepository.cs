using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.DataAccessLayer
{
    public class MssqlBorrowHistoryRepository : IBorrowHistoryRepository
    {
        private readonly DbContextOptionsBuilder<BookLibraryContext> _optionsBuilder;

        public MssqlBorrowHistoryRepository(MssqlConfiguration configuration)
        {
            this._optionsBuilder = new DbContextOptionsBuilder<BookLibraryContext>();
            this._optionsBuilder.UseSqlServer(configuration.ConnectionString);
        }

        public async Task<BorrowHistory[]> GetOutstandingBorrowsAsync(int take, int skip, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.BorrowHistories.Where(o => o.EndDate == null).Take(take).Skip(skip).ToArrayAsync();
            }
        }

        public async Task<BorrowHistory[]> GetBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.BorrowHistories.Where(o => o.CustomerId == customerId).Take(take).Skip(skip).ToArrayAsync();
            }
        }

        public async Task<BorrowHistory[]> GetOutstandingBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                return await context.BorrowHistories.Where(o => o.CustomerId == customerId && o.EndDate == null).Take(take).Skip(skip).ToArrayAsync();
            }
        }

        public async Task<bool> BorrowBookAsync(string bookId, int customerId, DateTime startDate, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var item = new BorrowHistory
                {
                    BookId = bookId,
                    CustomerId = customerId,
                    StartDate = startDate
                };

                context.BorrowHistories.Add(item);
                if (await context.SaveChangesAsync(cancellationToken) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> ReturnBookAsync(string bookId, int customerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            using (var context = new BookLibraryContext(this._optionsBuilder.Options))
            {
                var rentalHistory = await context.BorrowHistories.SingleOrDefaultAsync(o => o.BookId == bookId &&
                    o.CustomerId == customerId &&
                    o.StartDate == startDate,
                    cancellationToken);

                if (rentalHistory == null)
                {
                    return false;
                }

                if (endDate >= rentalHistory.StartDate)
                {
                    return false;
                }

                rentalHistory.EndDate = endDate;

                await context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    }
}
