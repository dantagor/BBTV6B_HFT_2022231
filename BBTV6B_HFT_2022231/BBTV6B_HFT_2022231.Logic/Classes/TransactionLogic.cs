﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Logic.Interfaces;
using BBTV6B_HFT_2022231.Models.DTOs;
using BBTV6B_HFT_2022231.Models.Entities;
using BBTV6B_HFT_2022231.Repository;

namespace BBTV6B_HFT_2022231.Logic.Classes
{
    public class TransactionLogic : ITransactionLogic
    {
        IRepository<Transaction> repo;

        public TransactionLogic(IRepository<Transaction> repo)
        {
            this.repo = repo;
        }


        public Stock BestSellerStockByExchange(string exchange)
        {
            //var res = repo.ReadAll().Where(o => o.Stock.Exchange.Name.Equals(exchange)).GroupBy(q => q.Stock).OrderByDescending(gp => gp.Count()).Select(g => g.Key);
            var res = from transaction in repo.ReadAll().ToList()
                      where transaction.Stock.Exchange.Name == exchange
                      group transaction by transaction.Stock into grp
                      orderby grp.Count() descending
                      select grp.Key;

            return res.FirstOrDefault();
        }

        public void Create(Transaction item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Transaction Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Transaction> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public IQueryable<TransactionStatistics> ReadTransactionStats()
        {
            var res = from transaction in repo.ReadAll().ToList()
                      group transaction by transaction.Date.Year into grp
                      select new TransactionStatistics()
                      {
                          TransactionYear = grp.Key.ToString(),
                          TransactionCount = grp.Count(),
                          Volume = grp.Sum(o => o.Stock.Price * o.Amount)
                      };
            return res.AsQueryable();
        }
        

        public int TotalTransactionsByExchange(string exchange)
        {
            return this.repo.ReadAll().Where(t => t.Stock.Exchange.Name == exchange).Count();
        }

        public void Update(Transaction item)
        {
            this.repo.Update(item);
        }

        public Transaction GetBiggestPurchase()
        {
            return repo.ReadAll().OrderBy(t => t.Amount * t.Stock.Price).FirstOrDefault();
        }
    }
}
