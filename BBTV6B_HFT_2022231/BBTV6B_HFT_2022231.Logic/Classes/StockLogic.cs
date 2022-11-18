using System;
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
    internal class StockLogic : IStockLogic
    {
        IRepository<Stock> repo;

        public StockLogic(IRepository<Stock> repo)
        {
            this.repo = repo;
        }

        public void Create(Stock item)
        {
            repo.Create(item);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Stock HighestDividendStockFromRegion(string region)
        {
            throw new NotImplementedException();
        }

        public Stock Read(int id)
        {
            return repo.Read(id);
        }

        public IQueryable<Stock> ReadAll()
        {
            return repo.ReadAll();
        }

        public IQueryable<ExchangeStatistics> ReadExchangeStats()
        {
            throw new NotImplementedException();
        }

        public void Update(Stock item)
        {
            repo.Update(item);
        }
    }
}
