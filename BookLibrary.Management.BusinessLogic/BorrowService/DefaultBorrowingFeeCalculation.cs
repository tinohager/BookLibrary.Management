using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public class DefaultBorrowingFeeCalculation : IBorrowingFeeCalculation
    {
        public double GetFee(DateTime startDate, DateTime endDate)
        {
            return endDate.Subtract(startDate).TotalDays;
        }
    }
}
