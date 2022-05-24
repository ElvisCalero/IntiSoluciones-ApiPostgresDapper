using System;

namespace ApiPostgresDapper.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public DateTime Order_Date { get; set; }
    }
}
