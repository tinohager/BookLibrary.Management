using System;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public class BorrowInfoDto
    {
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int DurationDays {get;set;}
        public decimal FeePrice { get; set; }
    }
}
