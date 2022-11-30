using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Models.DTOs;
using BBTV6B_HFT_2022231.Models.Entities;

namespace BBTV6B_HFT_2022231.Client
{
    class NonCrudService
    {
        private RestService rest;

        public NonCrudService(RestService rest)
        {
            this.rest = rest;
        }

        public void HighestDividendStockFromRegion() 
        {
            Console.WriteLine("Enter the Region You are interested in:");
            string region = Console.ReadLine();
            var stock = rest.GetSingle<Stock>($"Stat/HighestDividendStockFromRegion?region={region}");
            Console.WriteLine($"Stock with highest payout from the region is: {stock}");
            Console.ReadLine();
        }

        public void TotalTransactionsByExchange()
        {
            Console.WriteLine("Enter the Exchange You are interested in:");
            string exchange = Console.ReadLine();
            var count = rest.GetSingle<int>($"Stat/TotalTransactionsByExchange?exchange={exchange}");
            Console.WriteLine($"Total number of transactions in that exchange is {count}.");
            Console.ReadLine();
        }

        public void BestSellerStockByExchange()
        {
            var stock = rest.GetSingle<Stock>("Stat/BestSellerStockByExchange");
            Console.WriteLine($"Most popular stock by transaction is: {stock}");
            Console.ReadLine();
        }

        public void ReadExchangeStats() 
        {
            var stats = rest.Get<ExchangeStatistics>("Stat/ReadExchangeStats");
            foreach (var stat in stats)
            {
                Console.WriteLine(stat);
            }
            Console.ReadLine();
        }

        public void ReadTransactionStats() 
        {
            var stats = rest.Get<TransactionStatistics>("Stat/ReadTransactionStats");
            foreach (var stat in stats)
            {
                Console.WriteLine(stat);
            }
            Console.ReadLine();
        }
    }
}
