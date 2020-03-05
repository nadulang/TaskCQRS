using System;
using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Infrastructure.Persistences
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }
        public DbSet<Customers> CustomersData { get; set; }
        public DbSet<CustomerPayments> PaymentsData { get; set; }
        public DbSet<Merchants> MerchantsData { get; set; }
        public DbSet<Products> ProductsData{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerPayments>()
                        .HasOne(j => j.customer)
                        .WithMany()
                        .HasForeignKey(j => j.customer_id);
            modelBuilder.Entity<Products>()
                        .HasOne(p => p.merchant)
                        .WithMany()
                        .HasForeignKey(p => p.merchant_id);
        }
    }
}
