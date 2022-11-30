using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Logic.Classes;
using BBTV6B_HFT_2022231.Models.Entities;
using BBTV6B_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;

namespace BBTV6B_HFT_2022231.Test
{
    [TestFixture]
    class ExchangeTester
    {
        ExchangeLogic exchangeLogic;
        Mock<IRepository<Exchange>> mockRepository;
        Exchange exchange1;
        Exchange exchange2;
        Exchange exchange3;

        [SetUp]
        public void Init()
        {
            mockRepository = new Mock<IRepository<Exchange>>();

            exchange1 = new Exchange(1, "NYSE", "New York Stock Exchange", "US");
            exchange2 = new Exchange(2, "SHG", "Shanghai Stock Exchange", "China");
            exchange3 = new Exchange(3, "JPX", "Japan Exchange Group", "Japan");

            var exchanges = new List<Exchange>() {
                exchange1, exchange2, exchange3
            }.AsQueryable();

            mockRepository.Setup(t => t.ReadAll()).Returns(exchanges);

            exchangeLogic = new ExchangeLogic(mockRepository.Object);
        }

        [Test]
        public void CreateTest()
        {
            //  ARRANGE
            var exchange4 = new Exchange(4,"BUX","Budapest Exchange","Hungary");
            //  ACT + ASSERT
            Assert.That(()=>exchangeLogic.Create(exchange4), Throws.Nothing);
        }
        
        [Test]
        public void ReadTest()
        {
            Assert.That(()=>exchangeLogic.Read(1), Throws.Nothing);
        }

        [Test]
        public void UpdateTest()
        {
            //  ARRANGE
            var updatedExchange = new Exchange(2,"LON","London Stock Exchange","UK");
            //  ACT + ASSERT
            Assert.That(() => exchangeLogic.Update(updatedExchange), Throws.Nothing);
        }

        [Test]
        public void DeleteTest()
        {
            //  ACT + ASSERT
            Assert.That(() => exchangeLogic.Delete(1), Throws.Nothing);
        }

    }
}
