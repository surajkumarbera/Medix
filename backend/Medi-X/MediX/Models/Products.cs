namespace MediX.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Vendor { get; set; }
        public string Descriptions { get; set; }
        public string ImageUrl { get; set; }

        public int CategoriesId { get; set; }
        public virtual Categories Categories { get; set; }

        public int SubCategoriesId { get; set; }
        public virtual SubCategories SubCategories { get; set; }
    }
}