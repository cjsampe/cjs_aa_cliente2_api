using Microsoft.EntityFrameworkCore;

namespace cjs_aa_cliente2_api.Data {
    public class DataContext : DbContext{

    
     public DataContext(DbContextOptions<DataContext>options) : base(options) {}

    public DbSet<ProductItem>? Products { get; set; }

    public DbSet<CartItem>? Carts { get; set; }


    }
}
