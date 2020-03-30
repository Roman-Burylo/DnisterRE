using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DnisterRE.DTO.UsersDto
{
    public class LoginViewModel
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
