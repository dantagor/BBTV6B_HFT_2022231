﻿using System;
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
        public Stock MostPopularStock { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TransactionStatistics statistic &&
                TransactionYear == statistic.TransactionYear &&
                TransactionCount == statistic.TransactionCount &&
                MostPopularStock == statistic.MostPopularStock;
        }

        public override string ToString()
        {
            return $"TransactionYear = {TransactionYear}, TransactionCount = {TransactionCount}, MostPopularStock = {MostPopularStock.Company}";
        }
    }
}
