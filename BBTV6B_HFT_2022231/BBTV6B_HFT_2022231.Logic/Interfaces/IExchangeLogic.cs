using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTV6B_HFT_2022231.Models.DTOs;
using BBTV6B_HFT_2022231.Models.Entities;

namespace BBTV6B_HFT_2022231.Logic.Interfaces
{
    public interface IExchangeLogic
    {
        void Create(Exchange item);
        void Update(Exchange item);
        void Delete(int id);
        Exchange Read(int id);
        IEnumerable<Exchange> ReadAll();

    }
}
