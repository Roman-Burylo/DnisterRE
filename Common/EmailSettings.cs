using Newtonsoft.Json;

namespace Common
{
    public class EmailSettings
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
