using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Management.BusinessLogicLayer.BookService
{
    public class AddBookDto
    {
        [Required]
        public string Isbn { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Abstract { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int[] AuthorIds { get; set; }
        [Required]
        public int BookCount { get; set; }
    }
}
