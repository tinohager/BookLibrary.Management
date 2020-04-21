using System;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public interface IBorrowingFeeCalculation
    {
        double GetFee(DateTime startDate, DateTime endDate);
    }
}
