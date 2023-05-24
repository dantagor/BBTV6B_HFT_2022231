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
    public class TransactionController : ControllerBase
    {
        ITransactionLogic logic;
        IHubContext<SignalRHub> hub;
        public TransactionController(ITransactionLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Transaction> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Transaction Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Transaction value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("TransactionCreated", value);

        }

        [HttpPut]
        public void Update([FromBody] Transaction value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("TransactionUpdated", value);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var transactionToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("TransactionDeleted", transactionToDelete);

        }
    }
}
