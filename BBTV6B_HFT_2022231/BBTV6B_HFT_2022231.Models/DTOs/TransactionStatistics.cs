using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Models.Entities;

namespace BBTV6B_HFT_2022231.Models.DTOs
{
    public class TransactionStatistics
    {
        public string TransactionYear { get; set; }
        public int TransactionCount { get; set; }
        public double Volume { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TransactionStatistics statistic &&
                TransactionYear == statistic.TransactionYear &&
                TransactionCount == statistic.TransactionCount &&
                Volume == statistic.Volume;
        }

        public override string ToString()
        {
            return $"Year = {TransactionYear}, Number of orders = {TransactionCount}, Total Volume = ${Volume}";
        }
    }
}
