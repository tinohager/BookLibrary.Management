using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Management.DataAccessLayer.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public int Gender { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string Street { get; set; }
        [StringLength(50)]
        public string PostalCode { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(2)]
        public string CountryCode { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public bool Deleted { get; set; }

        public void DisguiseData()
        {
            this.Firstname = this.GetDisguiseText(this.Firstname.Length);
            this.Surname = this.GetDisguiseText(this.Surname.Length);
            this.Street = this.GetDisguiseText(this.Street.Length);
            this.PostalCode = this.GetDisguiseText(this.PostalCode.Length);
            this.City = this.GetDisguiseText(this.City.Length);
            this.Email = this.GetDisguiseText(this.Email.Length);
        }

        private string GetDisguiseText(int length)
        {
            return new string('*', length);
        }
    }
}
