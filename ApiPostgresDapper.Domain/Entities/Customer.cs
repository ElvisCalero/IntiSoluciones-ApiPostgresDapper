using System;

namespace ApiPostgresDapper.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public DateTime Create_Date { get; set; }
        public int User_Id { get; set; }
    }
}
