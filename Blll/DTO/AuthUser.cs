using System;

namespace Bll.DTO
{
    public class AuthUser
    {
        public Guid Id { get; set; }

        public string UserEmail { get; set; }

        public string Role { get; set; }
    }
}
