using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyVeterinaria.Prism.Models
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }

        public DateTime ExpirationLocal => Expiration.ToLocalTime();

    }
}
