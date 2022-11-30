using BBTV6B_HFT_2022231.Logic.Interfaces;
using BBTV6B_HFT_2022231.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BBTV6B_HFT_2022231.Endpoint.Controllers
{
    public class TransactionController : ControllerBase
    {
        ITransactionLogic logic;

        public TransactionController(ITransactionLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Transaction value)
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
