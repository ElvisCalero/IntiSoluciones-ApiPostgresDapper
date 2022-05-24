using System;

namespace ApiPostgresDapper.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Create_Date { get; set; }
    }
}
