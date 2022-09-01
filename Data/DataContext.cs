using Microsoft.EntityFrameworkCore;

namespace cjs_aa_cliente2_api.Data {
    public class DataContext : DbContext{

    
     public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<ProductsItem>? Products { get; set; }

    public DbSet<OrdersItem>? Orders { get; set; }

     public DbSet<CartItem>? Cart { get; set; }


    }
}
