using System;
using System.ComponentModel.DataAnnotations.Schema;
using SpyStoreModels;
using Microsoft.EntityFrameworkCore;
using SpyStoreModels.ViewModels;

namespace SpyStoreDAL.EF
{

    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=app.db");
            }
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartRecord> ShoppingCartRecords { get; set; }

        public DbSet<Category> Categories { get; set; }
        [NotMapped]
        public DbSet<CartRecordWithProductInfo> ViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.CurrentPrice)
                .HasColumnType("decimal(19,4)");

            modelBuilder.Entity<Product>()
                .Property(e => e.TimeStamp);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitCost)
                .HasColumnType("decimal(19,4)");

            modelBuilder.Entity<ShoppingCartRecord>()
                .Property(e => e.TimeStamp);
        }
    }
}
