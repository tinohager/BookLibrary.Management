﻿using BookLibrary.Management.Contract;
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

        public DefaultBorrowService(IBorrowHistoryRepository borrowHistoryRepository,
            IBorrowingFeeCalculation borrowingFeeCalculation)
        {
            this._borrowHistoryRepository = borrowHistoryRepository;
            this._borrowingFeeCalculation = borrowingFeeCalculation;
        }

        public async Task<BorrowInfoDto[]> GetBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default)
        {
            var items = await this._borrowHistoryRepository.GetBorrowsAsync(customerId, take, skip, cancellationToken);
            return items.Select(o => this.Process(o)).ToArray();
        }

        public async Task<BorrowInfoDto[]> GetOutstandingBorrowsAsync(int take, int skip, CancellationToken cancellationToken = default)
        {
            var items = await this._borrowHistoryRepository.GetOutstandingBorrowsAsync(take, skip, cancellationToken);
            return items.Select(o => this.Process(o)).ToArray();
        }

        public async Task<BorrowInfoDto[]> GetOutstandingBorrowsAsync(int customerId, int take, int skip, CancellationToken cancellationToken = default)
        {
            var items = await this._borrowHistoryRepository.GetOutstandingBorrowsAsync(customerId, take, skip, cancellationToken);
            return items.Select(o => this.Process(o)).ToArray();
        }

        private BorrowInfoDto Process(BorrowHistory borrowHistory)
        {
            var item = borrowHistory.Adapt<BorrowInfoDto>();
            item.FeePrice = this._borrowingFeeCalculation.GetFee(item.StartDate, item.EndDate ?? DateTime.Now);
            return item;
        }

        public async Task<bool> BorrowBookAsync(string bookId, int customerId, DateTime startDate, CancellationToken cancellationToken = default)
        {
            return await this._borrowHistoryRepository.BorrowBookAsync(bookId, customerId, startDate, cancellationToken);
        }

        public async Task<bool> ReturnBookAsync(string bookId, int customerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await this._borrowHistoryRepository.ReturnBookAsync(bookId, customerId, startDate, endDate, cancellationToken);
        }
    }
}
