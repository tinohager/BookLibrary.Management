using System;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public class BorrowMediumDto
    {
        public string BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
