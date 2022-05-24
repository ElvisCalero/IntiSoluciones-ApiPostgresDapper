using System;

namespace ApiPostgresDapper.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Register_Date { get; set; }
    }
}
