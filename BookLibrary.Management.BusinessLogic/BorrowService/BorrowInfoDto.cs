using System;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public class BorrowInfoDto
    {
        public string BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal FeePrice { get; set; }
    }
}
