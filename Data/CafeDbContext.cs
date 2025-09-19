using Microsoft.EntityFrameworkCore;
using CafeWebsite.Models;
using System.Text.Json;

namespace CafeWebsite.Data
{
    public class CafeDbContext : DbContext
    {
        public CafeDbContext(DbContextOptions<CafeDbContext> options) : base(options) { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.PriceS).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PriceM).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PriceL).HasColumnType("decimal(18,2)");
            });
            
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CustomerPhone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CustomerEmail).HasMaxLength(100);
                entity.Property(e => e.CustomerAddress).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
                entity.HasMany(e => e.Items).WithOne().HasForeignKey(oi => oi.OrderId);
                entity.HasOne(e => e.User).WithMany(u => u.Orders).HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Promotion).WithMany().HasForeignKey(e => e.PromotionId);
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Email).IsUnique();
            });
            
            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MinOrderAmount).HasColumnType("decimal(18,2)");
                entity.HasIndex(e => e.Code).IsUnique();
            });
            
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Toppings)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                    );
            });
            
            SeedData(modelBuilder);
            SeedPromotions(modelBuilder);
        }
        
        private void SeedData(ModelBuilder modelBuilder)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = "1",
                    Name = "Espresso Đậm Đà",
                    Description = "Cà phê espresso nguyên chất với hương vị đậm đà, thơm ngon.",
                    Category = ProductCategory.Coffee,
                    Image = "https://images.pexels.com/photos/312418/pexels-photo-312418.jpeg?auto=compress&cs=tinysrgb&w=400",
                    PriceS = 25000,
                    PriceM = 35000,
                    PriceL = 45000,
                    Available = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Product
                {
                    Id = "2",
                    Name = "Cappuccino Creamy",
                    Description = "Sự kết hợp hoàn hảo giữa espresso và sữa tạo bọt mịn màng.",
                    Category = ProductCategory.Coffee,
                    Image = "https://images.pexels.com/photos/302899/pexels-photo-302899.jpeg?auto=compress&cs=tinysrgb&w=400",
                    PriceS = 35000,
                    PriceM = 45000,
                    PriceL = 55000,
                    Available = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Product
                {
                    Id = "3",
                    Name = "Trà Sữa Trân Châu",
                    Description = "Trà sữa truyền thống với trân châu đen mềm mịn.",
                    Category = ProductCategory.Tea,
                    Image = "https://images.pexels.com/photos/4021971/pexels-photo-4021971.jpeg?auto=compress&cs=tinysrgb&w=400",
                    PriceS = 30000,
                    PriceM = 40000,
                    PriceL = 50000,
                    Available = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Product
                {
                    Id = "4",
                    Name = "Sinh Tố Bơ",
                    Description = "Sinh tố bơ béo ngậy với sữa đặc ngọt ngào.",
                    Category = ProductCategory.Smoothie,
                    Image = "https://images.pexels.com/photos/1092730/pexels-photo-1092730.jpeg?auto=compress&cs=tinysrgb&w=400",
                    PriceS = 35000,
                    PriceM = 45000,
                    PriceL = 55000,
                    Available = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Product
                {
                    Id = "5",
                    Name = "Nước Ép Cam Tươi",
                    Description = "Nước ép cam tươi 100% từ cam ngọt Việt Nam.",
                    Category = ProductCategory.Juice,
                    Image = "https://images.pexels.com/photos/162671/orange-juice-vitamins-drink-fresh-162671.jpeg?auto=compress&cs=tinysrgb&w=400",
                    PriceS = 25000,
                    PriceM = 35000,
                    PriceL = 45000,
                    Available = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            };
            
            modelBuilder.Entity<Product>().HasData(products);
        }
        
        private void SeedPromotions(ModelBuilder modelBuilder)
        {
            var promotions = new List<Promotion>
            {
                new Promotion
                {
                    Id = "promo1",
                    Title = "Giảm 20% cho thành viên mới",
                    Description = "Chào mừng thành viên mới với ưu đãi giảm 20%",
                    Code = "WELCOME20",
                    Type = PromotionType.Percentage,
                    Value = 20,
                    MinOrderAmount = 50000,
                    MaxUses = 100,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 12, 31),
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Promotion
                {
                    Id = "promo2",
                    Title = "Giảm 30k cho đơn từ 200k",
                    Description = "Giảm ngay 30.000đ cho đơn hàng từ 200.000đ",
                    Code = "SAVE30K",
                    Type = PromotionType.FixedAmount,
                    Value = 30000,
                    MinOrderAmount = 200000,
                    MaxUses = 50,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 6, 30),
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            };
            
            modelBuilder.Entity<Promotion>().HasData(promotions);
        }
    }
}