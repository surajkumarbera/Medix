namespace MediX.Models
{
    public class Carts
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductsId { get; set; }
        public virtual Products Products { get; set; }

        public int Quantity { get; set; }
    }
}