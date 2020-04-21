using BookLibrary.Management.Contract.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.Contract
{
    public interface IBorrowHistoryRepository
    {
        Task<BorrowHistory[]> GetOutstandingBorrowsAsync(int take, int skip, CancellationToken cancellationToken = default);
        Task<BorrowHistory[]> GetOutstandingBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default);
        Task<BorrowHistory[]> GetBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default);
        Task<bool> BorrowBookAsync(string bookId, int customerId, DateTime startDate, CancellationToken cancellationToken = default);
        Task<bool> ReturnBookAsync(string bookId, int customerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
