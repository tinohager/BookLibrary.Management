using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public interface IBorrowService
    {
        Task<BorrowInfoDto[]> GetOutstandingBorrowsAsync(int take, int skip, CancellationToken cancellationToken = default);
        Task<BorrowInfoDto[]> GetOutstandingBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default);
        Task<BorrowInfoDto[]> GetBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default);
        Task<bool> BorrowBookAsync(string bookId, int customerId, DateTime startDate, CancellationToken cancellationToken = default);
        Task<bool> ReturnBookAsync(string bookId, int customerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
