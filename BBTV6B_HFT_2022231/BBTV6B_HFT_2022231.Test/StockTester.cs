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
    class StockTester
    {
        StockLogic stockLogic;
        Mock<IRepository<Stock>> mockRepository;
        Exchange exchange;
        Stock stock1;
        Stock stock2;
        Stock stock3;

        [SetUp]
        public void Init()
        {
            mockRepository = new Mock<IRepository<Stock>>();

            exchange = new Exchange(1, "NYSE", "New York Stock Exchange", "USA");
            stock1 = new Stock(1, "Nio Inc.", "NIO", 1, 0, 11.98);
            stock1.Exchange = exchange;
            stock2 = new Stock(2, "Alibaba Group", "BABA", 1, 0, 84.87);
            stock2.Exchange = exchange;
            stock3 = new Stock(3, "Bank of America Corp", "BAC", 1, 2.42, 36.38);
            stock3.Exchange = exchange;

            var stocks = new List<Stock>()
            {
                stock1, stock2, stock3
            }.AsQueryable();

            mockRepository.Setup(t => t.ReadAll()).Returns(stocks);

            stockLogic = new StockLogic(mockRepository.Object);
        }

        [Test]
        public void CreateTest()
        {
            //  ARRANGE
            Stock stock4 = new Stock(4,"Citigroup Inc", "C", 1, 4.4, 46.40);
            stock4.Exchange = exchange;
            //  ACT + ASSERT
            Assert.That(() => stockLogic.Create(stock4), Throws.Nothing);
        }

        [Test]
        public void ReadTest()
        {
            //  ACT + ASSERT
            Assert.That(() => stockLogic.Read(1), Throws.Nothing);
            Assert.That(() => stockLogic.Read(23), Is.EqualTo(null));
        }

        [Test]
        public void DeleteTest()
        {
            //  ACT + ASSERT
            Assert.That(() => stockLogic.Delete(3), Throws.Nothing);
            Assert.That(()=>stockLogic.Read(3), Is.EqualTo(null));
        }

        [Test]
        public void HighestDividendStockFromRegionTest()
        {
            //  ARRANGE
            var highest = stock3;
            //  ACT
            var actual = stockLogic.HighestDividendStockFromRegion(exchange.Region);
            //  ASSERT
            Assert.That(() => stockLogic.HighestDividendStockFromRegion(exchange.Region), Throws.Nothing);
            Assert.That(highest.Id, Is.EqualTo(actual.Id));
            Assert.That(highest.Company, Is.EqualTo(actual.Company));
            Assert.That(highest.ExchangeId, Is.EqualTo(actual.ExchangeId));
        }

        [Test]
        public void ReadExchangeStatsTest()
        {
            //  ARRANGE
            ExchangeStatistics stat = new ExchangeStatistics() 
            {
                ExchangeName = exchange.Name,
                StockCount = 3,
                AvgDividend = (stock1.Dividend + stock2.Dividend + stock3.Dividend)/3
            };
            //  ACT
            var actual = stockLogic.ReadExchangeStats();
            //  ASSERT
            Assert.That(actual.Count(), Is.EqualTo(1));
            Assert.That(actual.ElementAt(0).ExchangeName, Is.EqualTo(stat.ExchangeName));
            Assert.That(actual.ElementAt(0).StockCount, Is.EqualTo(stat.StockCount));
            Assert.That(actual.ElementAt(0).AvgDividend, Is.EqualTo(stat.AvgDividend));
        }
    }
}
