﻿using System;

namespace BookLibrary.Management.BusinessLogicLayer.BorrowService
{
    public class ReturnMediumDto
    {
        public string BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
