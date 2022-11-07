using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Models.Entities;
using BBTV6B_HFT_2022231.Repository.GenericRepository;

namespace BBTV6B_HFT_2022231.Repository.ModelRepositories
{
    public class StockRepository : Repository<Stock>
    {
        public StockRepository(StockDbContext ctx) : base(ctx)
        {
        }

        public override Stock Read(int id)
        {
            return ctx.Stocks.FirstOrDefault(s => s.Id == id);
        }

        public override void Update(Stock item)
        {
            var old = Read(item.Id);
            foreach (var prop in item.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
