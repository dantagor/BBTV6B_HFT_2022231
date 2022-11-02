using System;
using BBTV6B_HFT_2022231.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BBTV6B_HFT_2022231.Repository
{
    public class StockDbContext : DbContext
    {
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public StockDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("stocks");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>(entity => { 
                entity
                    .HasOne(stock => stock.Exchange)
                    .WithMany(exchange => exchange.Stocks)
                    .HasForeignKey(stock => stock.ExchangeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Transaction>(entity => {
                entity
                    .HasOne(transaction => transaction.Stock)
                    .WithMany(stock => stock.Transactions)
                    .HasForeignKey(transaction => transaction.StockId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            // LOAD DATA TO DATABASE

        }
    }
}
