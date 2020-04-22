using System;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public interface IBorrowingFeeCalculation
    {
        decimal GetFee(DateTime startDate, DateTime endDate);
    }
}
