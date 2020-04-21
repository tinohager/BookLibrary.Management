using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Management.Contract.Model
{
    public class Book
    {
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        [Required]
        public int PublisherId { get; set; }
        public DateTime? PublishDate { get; set; }
        [StringLength(2000)]
        public string Abstract { get; set; }
        public int BookCount { get; set; }
    }
}
