using BookLibrary.Management.Contract.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Management.BusinessLogicLayer.CustomerService
{
    public class AddCustomerDto
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
