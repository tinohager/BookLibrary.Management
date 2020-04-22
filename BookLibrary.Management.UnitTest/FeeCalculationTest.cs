using BookLibrary.Management.BusinessLogicLayer.BorrowService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookLibrary.Management.UnitTest
{
    [TestClass]
    public class FeeCalculationTest
    {
        [TestMethod]
        public void CheckFeeFreeBorrow()
        {
            var startDate = new DateTime(2020, 04, 20, 14, 00, 00);
            var endDate = new DateTime(2020, 04, 22, 10, 00, 00);

            IBorrowingFeeCalculation feeCalculation = new DefaultBorrowingFeeCalculation();
            var feePrice = feeCalculation.GetFee(startDate, endDate);

            Assert.AreEqual(0m, feePrice);
        }

        [TestMethod]
        public void CheckFeeOneDayOvertime()
        {
            var startDate = new DateTime(2020, 04, 20, 14, 00, 00);
            var endDate = new DateTime(2020, 04, 23, 10, 00, 00);

            IBorrowingFeeCalculation feeCalculation = new DefaultBorrowingFeeCalculation();
            var feePrice = feeCalculation.GetFee(startDate, endDate);

            Assert.AreEqual(0.1m, feePrice);
        }

        [TestMethod]
        public void CheckFeeTwoDayOvertime()
        {
            var startDate = new DateTime(2020, 04, 20, 14, 00, 00);
            var endDate = new DateTime(2020, 04, 24, 10, 00, 00);

            IBorrowingFeeCalculation feeCalculation = new DefaultBorrowingFeeCalculation();
            var feePrice = feeCalculation.GetFee(startDate, endDate);

            Assert.AreEqual(0.2m, feePrice);
        }

        [TestMethod]
        public void CheckFeeThreeDayOvertime()
        {
            var startDate = new DateTime(2020, 04, 20, 14, 00, 00);
            var endDate = new DateTime(2020, 04, 27, 10, 00, 00);

            IBorrowingFeeCalculation feeCalculation = new DefaultBorrowingFeeCalculation();
            var feePrice = feeCalculation.GetFee(startDate, endDate);

            Assert.AreEqual(0.3m, feePrice);
        }

        [TestMethod]
        public void CheckFeeElevenDayOvertime()
        {
            var startDate = new DateTime(2020, 04, 06, 14, 00, 00);
            var endDate = new DateTime(2020, 04, 20, 10, 00, 00);

            IBorrowingFeeCalculation feeCalculation = new DefaultBorrowingFeeCalculation();
            var feePrice = feeCalculation.GetFee(startDate, endDate);

            Assert.AreEqual(1.2m, feePrice);
        }
    }
}
