using Newtonsoft.Json;

namespace DnisterRE.DTO.UsersDto
{
    public class LoginResponseDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
