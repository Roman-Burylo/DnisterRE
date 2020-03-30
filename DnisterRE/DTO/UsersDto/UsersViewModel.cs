using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnisterRE.DTO.UsersDto
{
    public class UsersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortInformation { get; set; }
        public string ImageURl { get; set; }
        public string City { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
