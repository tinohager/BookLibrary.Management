using System;

namespace BookLibrary.Management.Contract.Model
{
    public class BorrowHistory
    {
        public string BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
