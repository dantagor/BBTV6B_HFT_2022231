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
    public class ExchangeController : ControllerBase
    {
        IExchangeLogic logic;
        IHubContext<SignalRHub> hub;

        //public ExchangeController(IExchangeLogic logic, IHubContext<SignalRHub> hub)
        //{
        //    this.logic = logic;
        //    this.hub = hub;
        //}

        public ExchangeController(IExchangeLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Exchange> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Exchange Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Exchange value)
        {
            this.logic.Create(value);
            //hub.Clients.All.SendAsync("ExchangeCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Exchange value)
        {
            this.logic.Update(value);
            //hub.Clients.All.SendAsync("ExchangeUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var ExchangeToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            //hub.Clients.All.SendAsync("ExchangeDeleted", ExchangeToDelete);
        }
    }
}
