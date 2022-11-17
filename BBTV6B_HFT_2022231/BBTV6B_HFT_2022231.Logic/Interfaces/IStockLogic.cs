using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Models.Entities;

namespace BBTV6B_HFT_2022231.Logic.Interfaces
{
    public interface IStockLogic
    {
        void Create(Stock item);
        void Update(Stock item);
        void Delete(int id);
        Stock Read(int id);
        IQueryable<Stock> ReadAll();
    }
}
