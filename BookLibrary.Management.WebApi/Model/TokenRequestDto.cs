using System;

namespace BookLibrary.Management.WebApi.Model
{
    public class TokenRequestDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
