using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Management.Contract.Model
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
