namespace MediX.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Orders Orders { get; set; }

        public int ProductsId { get; set; }
        public Products Products { get; set; }

        public int Quantity { get; set; }

    }
}