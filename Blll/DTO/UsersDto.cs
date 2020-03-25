using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.DTO
{
    public class UsersDto
    {
        [JsonProperty("id")]
        public Guid UserDetailsId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShortInformation { get; set; }

        public string ImageURl { get; set; }

        public string City { get; set; }

        public string RoleName { get; set; }

        public DateTime? BirthDay { get; set; }
    }
}
