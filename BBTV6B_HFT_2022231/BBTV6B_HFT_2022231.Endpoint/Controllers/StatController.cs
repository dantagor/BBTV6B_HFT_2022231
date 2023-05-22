using BBTV6B_HFT_2022231.Endpoint.Services;
using BBTV6B_HFT_2022231.Logic.Interfaces;
using BBTV6B_HFT_2022231.Models.DTOs;
using BBTV6B_HFT_2022231.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBTV6B_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        ITransactionLogic transLogic;
        IStockLogic stockLogic;
        IHubContext<SignalRHub> hub;

        public StatController(ITransactionLogic transLogic, IStockLogic stockLogic, IHubContext<SignalRHub> hub)
        {
            this.transLogic = transLogic;
            this.stockLogic = stockLogic;
            this.hub = hub;
        }

        [HttpGet]
        public Stock HighestDividendStockFromRegion(string region)
        {
            return this.stockLogic.HighestDividendStockFromRegion(region); 
        }

        [HttpGet]
        public IEnumerable<ExchangeStatistics> ReadExchangeStats()
        {
            return this.stockLogic.ReadExchangeStats();
        }

        [HttpGet]
        public int TotalTransactionsByExchange(string exchange) 
        {
            return this.transLogic.TotalTransactionsByExchange(exchange);
        }

        [HttpGet]
        public IEnumerable<TransactionStatistics> ReadTransactionStats()
        {
            return this.transLogic.ReadTransactionStats();
        }

        [HttpGet]
        public Stock BestSellerStockByExchange(string exchange)
        {
            return this.transLogic.BestSellerStockByExchange(exchange);
        }

        [HttpGet]
        public Transaction GetBiggestPurchase()
        {
            return this.transLogic.GetBiggestPurchase();
        }
    }
}
