using BookLibrary.Management.Contract;
using BookLibrary.Management.Contract.Model;
using Mapster;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public class DefaultBorrowService : IBorrowService
    {
        private readonly IBorrowHistoryRepository _borrowHistoryRepository;
        private readonly IBorrowingFeeCalculation _borrowingFeeCalculation;
        private readonly IBookRepository _bookRepository;

        public DefaultBorrowService(IBorrowHistoryRepository borrowHistoryRepository,
            IBorrowingFeeCalculation borrowingFeeCalculation,
            IBookRepository bookRepository)
        {
            this._borrowHistoryRepository = borrowHistoryRepository;
            this._borrowingFeeCalculation = borrowingFeeCalculation;
            this._bookRepository = bookRepository;
        }

        public async Task<BorrowInfoDto[]> GetBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default)
        {
            var items = await this._borrowHistoryRepository.GetBorrowsFromCustomerAsync(customerId, take, skip, cancellationToken);
            return items.Select(o => this.Process(o)).ToArray();
        }

        public async Task<BorrowInfoDto[]> GetOutstandingBorrowsAsync(int take, int skip, CancellationToken cancellationToken = default)
        {
            var items = await this._borrowHistoryRepository.GetOutstandingBorrowsAsync(take, skip, cancellationToken);
            return items.Select(o => this.Process(o)).ToArray();
        }

        public async Task<BorrowInfoDto[]> GetOutstandingBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default)
        {
            var items = await this._borrowHistoryRepository.GetOutstandingBorrowsFromCustomerAsync(customerId, take, skip, cancellationToken);
            return items.Select(o => this.Process(o)).ToArray();
        }

        private BorrowInfoDto Process(BorrowHistory borrowHistory)
        {
            var item = borrowHistory.Adapt<BorrowInfoDto>();
            item.FeePrice = this._borrowingFeeCalculation.GetFee(item.StartDate, item.EndDate ?? DateTime.Now);
            return item;
        }

        public async Task<bool> BorrowBookAsync(BorrowMediumDto borrowMedium, CancellationToken cancellationToken = default)
        {
            if (borrowMedium.StartDate >= DateTime.Now)
            {
                return false;
            }

            var book = await this._bookRepository.GetAsync(borrowMedium.BookId);
            if (book == null)
            {
                return false;
            }

            var outstandingBorrows = await this._borrowHistoryRepository.GetOutstandingBorrowsFromBookAsync(borrowMedium.BookId);
            if (book.BookCount <= outstandingBorrows.Length)
            {
                return false;
            }

            return await this._borrowHistoryRepository.BorrowBookAsync(borrowMedium.BookId, borrowMedium.CustomerId, borrowMedium.StartDate, cancellationToken);
        }

        public async Task<bool> ReturnBookAsync(ReturnMediumDto returnMedium, CancellationToken cancellationToken = default)
        {
            return await this._borrowHistoryRepository.ReturnBookAsync(returnMedium.BookId, returnMedium.CustomerId, returnMedium.StartDate, returnMedium.EndDate, cancellationToken);
        }
    }
}
