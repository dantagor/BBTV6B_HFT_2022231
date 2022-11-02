using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTV6B_HFT_2022231.Models.DTOs
{
    public class ExchangeStatistics
    {
        public string ExchangeName { get; set; }
        public int StockCount { get; set; }
        public int DividendCount { get; set; }
        public override bool Equals(object obj)
        {
            return obj is ExchangeStatistics statistic &&
                statistic.ExchangeName == ExchangeName &&
                statistic.StockCount == StockCount &&
                statistic.DividendCount == DividendCount;
        }

        public override string ToString()
        {
            return $"ExchangeName = {ExchangeName}, StockCount = {StockCount}, DividendCount = {DividendCount}";
        }
    }
}
