using MediX.Models;
using System.Data.Entity;

namespace MediX.Context
{
    public class MediX_DbContext : DbContext
    {
        public MediX_DbContext() : base("name=cloudDb") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Prescriptions> Prescriptions { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
