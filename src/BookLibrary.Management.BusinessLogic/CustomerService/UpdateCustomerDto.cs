using BookLibrary.Management.DataAccessLayer.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Management.BusinessLogicLayer.CustomerService
{
    public class UpdateCustomerDto
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
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
    }
}
