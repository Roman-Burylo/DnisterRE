using Newtonsoft.Json;

namespace DnisterRE.DTO.UsersDto
{
    public class ConfirmUserViewModel
    {
        [JsonProperty("confirmation_number")]
        public string ConfirmationNumber { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
