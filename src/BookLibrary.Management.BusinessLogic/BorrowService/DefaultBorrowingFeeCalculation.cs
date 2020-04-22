using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public class DefaultBorrowingFeeCalculation : IBorrowingFeeCalculation
    {
        public decimal GetFee(DateTime startDate, DateTime endDate)
        {
            var days = this.GetChargeableDays(startDate, endDate);
            var feePrice = 0.0m;
            var smallFee = 0.1m;
            var bigFee = 0.5m;

            if (days.Length <= 3)
            {
                return feePrice;
            }

            if (days.Length > 3)
            {
                var countSmallFeeDays = days.Length - 3;
                if (countSmallFeeDays >= 7)
                {
                    countSmallFeeDays = 7;
                }

                feePrice += countSmallFeeDays * smallFee;
            }

            if (days.Length > 10)
            {
                var countBigFeeDays = days.Length - 10;
                feePrice += countBigFeeDays * bigFee;
            }

            return feePrice;
        }

        private DateTime[] GetChargeableDays(DateTime startDate, DateTime endDate)
        {
            var days = Enumerable.Range(0, 1 + endDate.Date.Subtract(startDate.Date).Days)
                .Select(offset => startDate.AddDays(offset))
                .ToArray();

            var chargeableDays = new List<DateTime>();
            foreach (var day in days)
            {
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                chargeableDays.Add(day);
            }

            return chargeableDays.ToArray();
        }
    }
}
