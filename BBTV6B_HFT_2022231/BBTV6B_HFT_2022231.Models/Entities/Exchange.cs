using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTV6B_HFT_2022231.Models.Entities
{
    public class Exchange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        [NotMapped]
        public virtual ICollection<Stock> Stocks { get; set; }

        public Exchange()
        {
            Stocks = new HashSet<Stock>();
        }

        public override string ToString()
        {
            return $"#{Id}-EXCHANGE: {Name}, Region = {Region}";
        }
    }
}
