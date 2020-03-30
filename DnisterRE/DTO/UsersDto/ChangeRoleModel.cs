using Newtonsoft.Json;
using System;

namespace DnisterRE.DTO.UsersDto
{
    public class ChangeRoleViewModel
    {
        [JsonProperty("new_role")]
        public Guid NewRoleId { get; set; }
    }
}
