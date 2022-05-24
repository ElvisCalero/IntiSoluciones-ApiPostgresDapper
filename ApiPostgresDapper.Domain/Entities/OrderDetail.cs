namespace ApiPostgresDapper.Domain.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public double Unit_Price { get; set; }
        public int Quantity { get; set; }
        public double Total_Price { get; set; }
    }
}
