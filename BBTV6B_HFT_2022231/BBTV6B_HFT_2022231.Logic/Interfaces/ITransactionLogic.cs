using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Models.DTOs;
using BBTV6B_HFT_2022231.Models.Entities;

namespace BBTV6B_HFT_2022231.Logic.Interfaces
{
    public interface ITransactionLogic
    {
        //  CRUD
        void Create(Transaction item);
        void Update(Transaction item);
        void Delete(int id);
        Transaction Read(int id);
        IQueryable<Transaction> ReadAll();

        //  Non-CRUD
        Stock BestSellerStockByExchange(string exchange);
        int TotalTransactionsByExchange(string exchange);
        IQueryable<TransactionStatistics> ReadTransactionStats();
    }
}
