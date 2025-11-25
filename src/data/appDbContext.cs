using Microsoft.EntityFrameworkCore;
using api_orders.src.data;
using api_orders.src.model;

namespace api_orders.src.data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Material> Materials { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<ProductMaterial> ProductMaterials { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
            entity.HasKey(p => p.ProductCode);
            entity.Property(p => p.ProductCode).HasColumnName("ProductCode");
            entity.Property(p => p.Description).HasColumnName("ProductDescription");
            entity.Property(p => p.Image).HasColumnName("Image");
            entity.Property(p => p.CycleTime).HasColumnName("CycleTime");

            entity.Ignore(p => p.Id);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("Material");
            entity.HasKey(m => m.MaterialCode);
            entity.Property(m => m.MaterialCode).HasColumnName("MaterialCode");
            entity.Property(m => m.Description).HasColumnName("MaterialDescription");

            entity.Ignore(m => m.Id);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");
            entity.HasKey(o => o.OrderCode);
            entity.Property(o => o.OrderCode).HasColumnName("Order");
            entity.Property(o => o.Quantity).HasColumnName("Quantity");

            entity.Property(o => o.ProductCode).HasColumnName("ProductCode");
            entity.HasOne(o => o.Product)
                  .WithMany()
                  .HasForeignKey(o => o.ProductCode);

            entity.Ignore(o => o.Id);
        });

        modelBuilder.Entity<ProductMaterial>(entity =>
        {
            entity.ToTable("ProductMaterial");

            entity.HasKey(pm => new { pm.ProductCode, pm.MaterialCode });

            entity.HasOne(pm => pm.Product)
                .WithMany(p => p.ProductMaterials) 
                .HasForeignKey(pm => pm.ProductCode);

            entity.HasOne(pm => pm.Material)
                .WithMany(m => m.ProductMaterials) 
                .HasForeignKey(pm => pm.MaterialCode);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(u => u.Email);
            entity.Property(u => u.Email).HasColumnName("Email");
            entity.Property(u => u.Name).HasColumnName("Name");
            entity.Property(u => u.InitialDate).HasColumnName("InitialDate");
            entity.Property(u => u.EndDate).HasColumnName("EndDate");

            entity.Ignore(u => u.Id);
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.ToTable("Production");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ID");
            entity.Property(p => p.Email).HasColumnName("Email");
            entity.Property(p => p.OrderCodeFK).HasColumnName("Order");
            entity.Property(p => p.Quantity).HasColumnName("Quantity");
            entity.Property(p => p.MaterialCode).HasColumnName("MaterialCode");
            entity.Property(p => p.CycleTime).HasColumnName("CycleTime");

            entity.HasOne(p => p.User)
                  .WithMany()
                  .HasForeignKey(p => p.Email);

            entity.HasOne(p => p.Order)
                  .WithMany()
                  .HasForeignKey(p => p.OrderCodeFK);
        });

        base.OnModelCreating(modelBuilder);
    }
}
