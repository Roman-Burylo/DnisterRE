﻿namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public UserRole Role { get; set; }
    }
}
