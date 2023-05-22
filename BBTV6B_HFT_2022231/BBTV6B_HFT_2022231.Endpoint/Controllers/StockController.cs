using BBTV6B_HFT_2022231.Endpoint.Services;
using BBTV6B_HFT_2022231.Logic.Interfaces;
using BBTV6B_HFT_2022231.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace BBTV6B_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        IStockLogic logic;
        IHubContext<SignalRHub> hub;

        public StockController(IStockLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Stock> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Stock Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Stock value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Stock value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
