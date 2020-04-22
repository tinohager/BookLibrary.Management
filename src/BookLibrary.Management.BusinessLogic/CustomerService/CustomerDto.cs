using BookLibrary.Management.Contract.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BookLibrary.Management.BusinessLogicLayer.CustomerService
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
    }
}
