﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BBTV6B_HFT_2022231.Models.Entities
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Company { get; set; }

        public string Ticker { get; set; }

        public bool Dividend { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Exchange Exchange { get; set; }

        [ForeignKey(nameof(Exchange))]
        public int ExchangeId { get; set; }

        [NotMapped]
        public virtual ICollection<Transaction> Transactions { get; set; }

        public Stock()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Stock(int id, string company, string ticker, int exchId, bool dividend)
        {
            Id = id;
            Company = company;
            Ticker = ticker;
            Dividend = dividend;
            ExchangeId = exchId;
            Transactions = new HashSet<Transaction>();
        }

        public override string ToString()
        {
            return $"#{Id}-STOCK: Ticker = {Ticker}, Company = {Company}, Dividend = {Dividend}, ExchangeId = {ExchangeId}";
        }
    }
}
