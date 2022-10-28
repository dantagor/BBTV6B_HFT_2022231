using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTV6B_HFT_2022231.Models.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotMapped]
        public Stock Stock { get; set; }

        [ForeignKey(nameof(Stock))]
        public int StockId { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"#{Id}-TRANSACTION: {Date} - StockId = {StockId}, Amount = {Amount}";
        }
    }
}
