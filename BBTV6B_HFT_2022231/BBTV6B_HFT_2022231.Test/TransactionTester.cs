using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Logic.Classes;
using BBTV6B_HFT_2022231.Models.DTOs;
using BBTV6B_HFT_2022231.Models.Entities;
using BBTV6B_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;

namespace BBTV6B_HFT_2022231.Test
{
    [TestFixture]
    public class TransactionTester
    {
        TransactionLogic transLogic;
        Mock<IRepository<Transaction>> mockRepository;
        Exchange exchange1;
        Exchange exchange2;
        Stock stock1;
        Stock stock2;
        Transaction tran1;
        Transaction tran2;
        Transaction tran3;

        [SetUp]
        public void Init() {
            mockRepository = new Mock<IRepository<Transaction>>();

            exchange1 = new Exchange(1, "NYSE", "New York Stock Exchange", "USA");
            exchange2 = new Exchange(2, "NASDAQ", "NASDAQ Stock Exchange", "USA");
            stock1 = new Stock(1, "Nio Inc.", "NIO", 1, 0, 11.98);
            stock1.Exchange = exchange1;
            stock2 = new Stock(2, "Amazon.com, Inc.", "AMZN", 2, 0, 92.24);
            stock2.Exchange = exchange2;
            tran1 = new Transaction(1,1,20,new DateTime(2021,11,20));
            tran1.Stock = stock1;
            tran2 = new Transaction(2, 2, 4, new DateTime(2021, 12, 10));
            tran2.Stock = stock2;
            tran3 = new Transaction(3, 1, 30, new DateTime(2022, 4, 10));
            tran3.Stock = stock1;

            var transactions = new List<Transaction>()
            {
                tran1, tran2, tran3
            }.AsQueryable();

            mockRepository.Setup(t => t.ReadAll()).Returns(transactions);

            transLogic = new TransactionLogic(mockRepository.Object);
        }

        [Test]
        public void ReadTest()
        {
            //  ASSERT
            Assert.That(() => transLogic.Read(1), Throws.Nothing);
        }

        [Test]
        public void BestSellerStockByExchange() 
        {
            //  ARRANGE
            var bestSeller = stock1;
            //  ACT
            var actual = transLogic.BestSellerStockByExchange(exchange1.Name);
            //  ASSERT
            Assert.That(actual.Id, Is.EqualTo(bestSeller.Id));
            Assert.That(actual.Exchange.Name, Is.EqualTo(bestSeller.Exchange.Name));
            Assert.That(actual.Company, Is.EqualTo(bestSeller.Company));
        }

        [Test]
        public void ReadTransactionStatsTest()
        {
            //  ARRANGE
            TransactionStatistics stats = new TransactionStatistics()
            {
                TransactionYear = "2021",
                TransactionCount = 2,
                Volume = tran1.Amount * tran1.Stock.Price + tran2.Amount * tran2.Stock.Price
            };
            //  ACT
            var actual = transLogic.ReadTransactionStats();
            //  ASSERT
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(actual.ElementAt(0).TransactionYear, Is.EqualTo(stats.TransactionYear));
            Assert.That(actual.ElementAt(0).TransactionCount, Is.EqualTo(stats.TransactionCount));
            Assert.That(actual.ElementAt(0).Volume, Is.EqualTo(stats.Volume));
        }
    }
}
