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
            Exchange nyse = new Exchange(1,"NYSE","New York Stock Exchange","USA");
            Exchange nasdaq = new Exchange(2,"NASDAQ", "NASDAQ Stock Exchange", "USA");
            Exchange sse = new Exchange(3,"SSE", "Shanghai Stock Exchange", "China");
            Exchange tse = new Exchange(4, "TSE", "Tokyo Stock Exchange", "Japan");

            Stock nio = new Stock(1, "Nio Inc.", "NIO", 1, 0, 11.98);
            nio.Exchange = nyse;
            Stock ali = new Stock(2, "Alibaba Group", "BABA", 1, 0, 84.87);
            ali.Exchange = nyse;
            Stock apple = new Stock(3, "Apple Inc.", "AAPL", 2, 0.65, 141.66);
            apple.Exchange = nasdaq;
            Stock amaz = new Stock(4, "Amazon.com, Inc.", "AMZN", 2, 0, 92.24);
            amaz.Exchange = nasdaq;
            Stock longi = new Stock(5, "LONGi Green Energy Tech. Co Ltd", "601012", 3, 0.41, 6.46);
            longi.Exchange = sse;
            Stock sany = new Stock(6, "Sany Heavy Industry Co., Ltd", "600031", 3, 2.83, 2.25);
            sany.Exchange = sse;
            Stock soft = new Stock(7, "SoftBank Group Corp", "9984", 4, 0.74, 42.72);
            soft.Exchange = tse;
            Stock mits = new Stock(8, "Mitsubishi Corp", "8058", 4, 3.39, 33.05);
            mits.Exchange = tse;

            Transaction t1 = new Transaction(1, 1, 3, new DateTime(2022, 11, 2));       // 3 Nio stock purchase
            t1.Stock = nio;
            Transaction t2 = new Transaction(2, 2, 6, new DateTime(2022, 11, 2));       // 6 Alibaba stock purchase
            t2.Stock = ali;
            Transaction t3 = new Transaction(3, 2, 3, new DateTime(2022, 11, 3));       // 3 Alibaba stock purchase
            t3.Stock = ali;
            Transaction t4 = new Transaction(4, 3, 2, new DateTime(2022, 11, 3));       // 2 Apple stock purchase
            t4.Stock = apple;
            Transaction t5 = new Transaction(5, 4, 4, new DateTime(2022, 11, 3));       // 4 Amazon stock purchase
            t5.Stock = amaz;
            Transaction t6 = new Transaction(6, 5, 100, new DateTime(2022, 11, 4));     // 100 LONGi stock purchase
            t6.Stock = longi;
            Transaction t7 = new Transaction(7, 6, 250, new DateTime(2022, 11, 4));     // 250 Sany stock purchase
            t7.Stock = sany;
            Transaction t8 = new Transaction(8, 7, 20, new DateTime(2022, 11, 5));      // 20 SoftBank stock purchase
            t8.Stock = soft;
            Transaction t9 = new Transaction(9, 8, 30, new DateTime(2022, 11, 7));      // 30 Mitsubishi stock purchase
            t9.Stock = mits;

            modelBuilder.Entity<Exchange>().HasData(nyse,nasdaq,sse,tse);
            modelBuilder.Entity<Stock>().HasData(nio, ali, apple, amaz, longi, sany, soft, mits);
            modelBuilder.Entity<Transaction>().HasData(t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }
    }
}
