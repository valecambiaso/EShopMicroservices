using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!; // Represents the Coupons table in the database

    public DiscountContext(DbContextOptions<DiscountContext> options) // Constructor accepting DbContextOptions
        : base(options) 
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData( // Seed initial data for the Coupons table
            new Coupon { Id = 1, ProductName = "Smartphone X", Description = "PhoneX Discount", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Smartphone 11", Description = "Phone11 Discount", Amount = 100 }
            );
    }
}
