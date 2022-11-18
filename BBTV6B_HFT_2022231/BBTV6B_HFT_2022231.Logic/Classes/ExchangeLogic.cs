using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Logic.Interfaces;
using BBTV6B_HFT_2022231.Models.Entities;
using BBTV6B_HFT_2022231.Repository;

namespace BBTV6B_HFT_2022231.Logic.Classes
{
    public class ExchangeLogic : IExchangeLogic
    {
        IRepository<Exchange> repo;

        public ExchangeLogic(IRepository<Exchange> repo)
        {
            this.repo = repo;
        }

        public void Create(Exchange item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Exchange Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Exchange> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Exchange item)
        {
            this.repo.Update(item);
        }
    }
}
