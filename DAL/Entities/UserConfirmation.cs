using System;

namespace DAL.Entities
{
    public class UserConfirmation
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public DateTime ConfDateTime { get; set; }

        public User User { get; set; }
    }
}